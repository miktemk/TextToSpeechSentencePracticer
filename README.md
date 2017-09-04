### Batch files and sublime build

verbi-test.bat

    verbi -n=500 -o=zzztest.txt -v %1

verbi-drill.bat

    verbi -n=500 -o=zzztest.txt -v %1
    START TextToSpeechSentencePracticer.exe --rate=0 --lang=es zzztest.txt

phrasi.sublime-build

    {
        "variants": [
            { "name": "Test just output text",
              "cmd": ["verbi-test.bat", "$file"],
            },
            { "name": "Run UI",
              "cmd": ["verbi-drill.bat", "$file"],
            }
        ]
    }
