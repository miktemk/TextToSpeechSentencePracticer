using Mono.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextToSpeechSentencePracticer.Code
{
    public class CommandLineArgs
    {
        private readonly OptionSet options;

        public CommandLineArgs(string[] args)
        {
            // these are the available options, not that they set the variables
            options = new OptionSet {
                { "l|lang=", $"TTS language. Default is {Lang}", x => Lang = x },
                { "h|help", "show this message and exit", x => ShowHelp = x != null },
                { "v", "increase debug message verbosity", x => IsVerbose = (x != null) },
                //{ "f|file=", "Input file", x => Filename = x },
            };

            List<string> extra;
            try
            {
                // parse the command line
                extra = options.Parse(args);
                Filename = extra.Skip(1).FirstOrDefault(); // NOTE: the 1st is the exe
            }
            catch (OptionException e)
            {
                // output some error message
                Console.WriteLine("Error parsing command line arguments.");
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `--help' for more information.");
                ArgumentError = true;
            }
        }

        public bool ArgumentError { get; private set; } = false;
        public string Lang { get; private set; } = "en";
        public bool ShowHelp { get; private set; } = false;
        public bool IsVerbose { get; private set; } = false;
        public string Filename { get; private set; }

        public void PrintHelpStringToConsole()
        {
            options.WriteOptionDescriptions(Console.Out);
        }
    }
}
