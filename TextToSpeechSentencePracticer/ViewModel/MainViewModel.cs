using GalaSoft.MvvmLight;
using Miktemk.TextToSpeech.Core;

namespace TextToSpeechSentencePracticer.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public string CurText { get; set; }
        public WordHighlight CurHighlightRegion { get; set; }

        public MainViewModel()
        {
            CurText = "xello mexicana YEHA its the eye!!!!!";
            CurHighlightRegion = new WordHighlight(6, 7);
        }


    }
}