using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDP_MP1
{
    internal class writeMorse
    {
        private Dictionary<string, char> morseCodeDictionary = new Dictionary<string, char>()
        {
            {".-", 'A'}, {"-...", 'B'}, {"-.-.", 'C'}, {"-..", 'D'}, {".", 'E'}, {"..-.", 'F'}, {"--.", 'G'},
            {"....", 'H'}, {"..", 'I'}, {".---", 'J'}, {"-.-", 'K'}, {".-..", 'L'}, {"--", 'M'}, {"-.", 'N'},
            {"---", 'O'}, {".--.", 'P'}, {"--.-", 'Q'}, {".-.", 'R'}, {"...", 'S'}, {"-", 'T'}, {"..-", 'U'},
            {"...-", 'V'}, {".--", 'W'}, {"-..-", 'X'}, {"-.--", 'Y'}, {"--..", 'Z'},
            {".----", '1'}, {"..---", '2'}, {"...--", '3'}, {"....-", '4'}, {".....", '5'},
            {"-....", '6'}, {"--...", '7'}, {"---..", '8'}, {"----.", '9'}, {"-----", '0'}
        };
        private string fileName = "";
        private string untranslatedMessage = "";
        public string translatedMessage = "";

        public void getFileName(string name)
        {
            fileName = name;  
        }
        public void getMessage(string message)
        {
            untranslatedMessage = message;  
        }
        public string translateMessage()
        {
            translatedMessage = "";
            foreach (char letter in untranslatedMessage.ToUpper())
            {
                if (letter == ' ')
                {
                    translatedMessage += " / " +'\n';
                }
                else if (morseCodeDictionary.ContainsValue(letter))
                {
                    foreach (KeyValuePair<string, char> pair in morseCodeDictionary)
                    {
                        if (pair.Value == letter)
                        {
                            translatedMessage += pair.Key + '\n';
                            break;
                        }
                    }
                }
            }
            return translatedMessage;
        }
        public void saveFile()
        {           
            using (StreamWriter sw = new StreamWriter(fileName + ".txt"))
            {
                sw.WriteLine(translatedMessage);
            }          
  
        }
    }
}
