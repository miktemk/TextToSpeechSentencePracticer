using Miktemk.TextToSpeech.Core;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TextToSpeechSentencePracticer.Controls
{
    public partial class TextBlockWithHighlight : UserControl
    {
        public TextBlockWithHighlight()
        {
            InitializeComponent();
            //DataContext = scope; // .... Do NOT set DataContext b/c cannot apply Binding to any DPs from MainWindow.xaml. Instead set it to inner Layout XAML element
        }

        public TextBlockWithHighlight_VM Scope { get; } = new TextBlockWithHighlight_VM();

        #region ------------------ Text --------------------

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(TextBlockWithHighlight),
                        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTextPropertyChanged));

        private static void OnTextPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as TextBlockWithHighlight;
            if (control != null)
                control.OnTextChanged((string)e.OldValue, (string)e.NewValue);
        }

        private void OnTextChanged(string oldValue, string newValue)
        {
            Scope.Apply(Text, WordHighlight);
        }

        #endregion

        #region ------------------ WordHighlight --------------------

        public WordHighlight WordHighlight
        {
            get { return (WordHighlight)GetValue(WordHighlightProperty); }
            set { SetValue(WordHighlightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WordHighlight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WordHighlightProperty =
            DependencyProperty.Register("WordHighlight", typeof(WordHighlight), typeof(TextBlockWithHighlight),
                        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, OnWordHighlightPropertyChanged));

        private static void OnWordHighlightPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as TextBlockWithHighlight;
            if (control != null)
                control.OnWordHighlightChanged((WordHighlight)e.OldValue, (WordHighlight)e.NewValue);
        }

        private void OnWordHighlightChanged(WordHighlight oldValue, WordHighlight newValue)
        {
            Scope.Apply(Text, WordHighlight);
        }

        #endregion

    }

    [AddINotifyPropertyChangedInterface]
    public class TextBlockWithHighlight_VM
    {
        public string TextBefore { get; set; } = "before";
        public string TextHighlight { get; set; } = "highlight";
        public string TextAfter { get; set; } = "after";

        public void Apply(string text, WordHighlight wordHighlight)
        {
            if (string.IsNullOrEmpty(text))
            {
                TextBefore = TextHighlight = TextAfter = "";
                return;
            }
            if (wordHighlight == null)
            {
                TextBefore = text;
                TextHighlight = TextAfter = "";
                return;
            }
            var startIndex = wordHighlight.StartIndex;
            if (startIndex < 0)
                startIndex = 0;
            var endIndex = wordHighlight.StartIndex + wordHighlight.Length;
            if (endIndex > text.Length)
                endIndex = text.Length;
            TextBefore = text.Substring(0, startIndex);
            TextHighlight = text.Substring(startIndex, endIndex - startIndex);
            TextAfter = text.Substring(endIndex, text.Length - endIndex);
        }
    }
}
