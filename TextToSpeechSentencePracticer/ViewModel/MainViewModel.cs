using GalaSoft.MvvmLight;
using Miktemk.TextToSpeech.Core;
using Miktemk.TextToSpeech.Services;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using TextToSpeechSentencePracticer.Code;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace TextToSpeechSentencePracticer.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ITtsService ttsService;
        private IEnumerator<string> sentenceEnumerator;
        private string curLang;

        public string CurText { get; set; }
        public WordHighlight CurHighlightRegion { get; set; }

        public ICommand GoNextSentence { get; }
        public ICommand RepeatCurSentence { get; }

        public MainViewModel(ITtsService ttsService)
        {
            // .... set up shit
            this.ttsService = ttsService;
            ttsService.AddWordCallback(ttsService_wordCallback);

            // .... process command line args
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
            if (args.Rate != null)
                ttsService.SetVoiceOverrideSpeed(args.Rate);

            // .... commands
            GoNextSentence = new RelayCommand(GoNextSentenceFn);
            RepeatCurSentence = new RelayCommand(RepeatCurSentenceFn);

            // .... load input file
            var allLines = File.ReadAllLines(args.Filename);
            sentenceEnumerator = allLines.AsEnumerable().GetEnumerator();

            CurText = "";
            curLang = args.Lang;
        }

        private void RepeatCurSentenceFn()
        {
            if (string.IsNullOrEmpty(CurText))
                return;
            ttsService.SayAsync(curLang, CurText, () =>
            {
                CurHighlightRegion = null;
            });
        }

        private void GoNextSentenceFn()
        {
            if (sentenceEnumerator == null)
                return;
            var noMore = !sentenceEnumerator.MoveNext();
            CurText = sentenceEnumerator.Current;
            if (noMore)
                sentenceEnumerator = null;
            RepeatCurSentenceFn();
        }

        private void ttsService_wordCallback(string text, int offset, int start, int length)
        {
            CurHighlightRegion = new WordHighlight(start, length);
        }


    }
}