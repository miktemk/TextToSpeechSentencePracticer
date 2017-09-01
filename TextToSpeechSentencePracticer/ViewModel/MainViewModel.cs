using GalaSoft.MvvmLight;
using Miktemk.TextToSpeech.Core;
using Miktemk.TextToSpeech.Services;
using System;
using System.IO;
using TextToSpeechSentencePracticer.Code;

namespace TextToSpeechSentencePracticer.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public string CurText { get; set; }
        public WordHighlight CurHighlightRegion { get; set; }

        public MainViewModel(ITtsService ttsService)
        {
            var args = new CommandLineArgs(Environment.GetCommandLineArgs());
            if (args.ArgumentError)
            {
                System.Windows.Application.Current.Shutdown();
                return;
            }
            if (args.ShowHelp)
            {
                args.PrintHelpStringToConsole();
                System.Windows.Application.Current.Shutdown();
                return;
            }
            if (args.Filename == null)
            {
                Console.WriteLine("Please specify filename as command line argument");
                System.Windows.Application.Current.Shutdown();
                return;
            }

            // TODO: this shit
            var allLines = File.ReadAllLines(args.Filename);

            CurText = allLines[4];

            ttsService.AddWordCallback(ttsService_wordCallback);

            ttsService.SayAsync(args.Lang, CurText, () =>
            {
                CurHighlightRegion = null;
            });
        }

        private void ttsService_wordCallback(string text, int offset, int start, int length)
        {
            CurHighlightRegion = new WordHighlight(start, length);
        }
    }
}