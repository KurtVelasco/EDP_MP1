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
using System.Windows.Shapes;
using System.IO;
using System.Threading;
using Microsoft.Win32;



namespace EDP_MP1
{
    /// <summary>
    /// Interaction logic for readMorse.xaml
    /// </summary>
    public partial class readMorse : Window
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
        private List<string> morseFile = new List<string>();
        private string filePath = "";

        private writeMorse wm = new writeMorse();
        public readMorse()
        {
            InitializeComponent();
        }
        private void textbox_filePath_TextChanged(object sender, TextChangedEventArgs e)
        {
            filePath = textbox_filePath.Text;

            if (File.Exists(filePath))
            {
                button_readFile.IsEnabled = true;
            }
            else
            {
                button_readFile.IsEnabled = false;
            }
        }
        public void button_readFile_Click(object sender, RoutedEventArgs e)
        {
            morseFile.Clear();
            readFile();
            translateLetters();
            translateMorse();
        }
        private void readFile()
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line = string.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    morseFile.Add(line);
                }
            }
        }
        private void translateLetters()
        {
            textbox_morseTranslated.Text = string.Empty;
            foreach (string morseCode in morseFile)
            {
                string[] words = morseCode.Split(' ');

                foreach (string word in words)
                {
                    if (morseCodeDictionary.ContainsKey(word))
                    {
                        char letter = morseCodeDictionary[word];
                        textbox_morseTranslated.Text += letter;
                    }
                }
            }
        }
        private void translateMorse()
        {                 
            foreach (string morseCode in morseFile)
            {

                foreach (char c in morseCode)
                {
                    if (c == '.')
                    {
                        Console.Beep(3000, 200);
                        
                    }
                    else if (c == '-')
                    {
                        Console.Beep(3000, 500);                
                    }                                 
                }
                Thread.Sleep(150);
            }        
        }
        private void option_writeMorse_Click(object sender, RoutedEventArgs e)
        {
            if (option_writeMorse.IsChecked == true)
            {
                textbox_filePath.IsEnabled = false;
                textbox_userFileName.IsEnabled = true;
                button_sendMessage.IsEnabled = true;
                textbox_userMessage.IsEnabled = true;
                button_saveFile.IsEnabled = true;
            }
            else
            {
                textbox_userFileName.IsEnabled = false;
                button_sendMessage.IsEnabled = false;
                textbox_userMessage.IsEnabled = false;
                button_saveFile.IsEnabled = false;
                textbox_filePath.IsEnabled = true;
            }
        }

        private void textbox_userFileName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(textbox_userFileName.Text.Length < 5)
            {
                button_saveFile.IsEnabled = false;
            }
            else
            {
                button_saveFile.IsEnabled =true;
            }
            wm.getFileName(textbox_userFileName.Text);
        }

        private void textbox_userMessage_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(textbox_userMessage.Text.Length > 64)
            {
                MessageBox.Show("64 Character Limit Reached");
            }
            else
            {
                wm.getMessage(textbox_userMessage.Text);
            }
        }

        private void button_sendMessage_Click(object sender, RoutedEventArgs e)
        {
            string msg = wm.translateMessage();
            textbox_morseCode.Text = msg;
        }

        private void button_saveFile_Click(object sender, RoutedEventArgs e)
        {
            if(textbox_userFileName.Text.Length <= 0)
            {
                MessageBox.Show("Invalid File Name");
            }
            else
            {
                wm.saveFile();
                MessageBox.Show("File Saved");
            }
        }
    }
}
