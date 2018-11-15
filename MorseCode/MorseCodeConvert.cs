using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MorseCode
{
    public static class MorseCodeConvert
    {
        public static readonly Dictionary<char, bool[]> characterToMorseCode = new Dictionary<char, bool[]>()
        {
            {'A',new bool[] { false,true } }, {'B',new bool[]{true,false,false,false} },
            {'C', new bool[] {true,false,true,false } },{ 'D',new bool[]{true,false,false } },
            {'E',new bool[]{false} }, {'F', new bool[]{false,false,true,false } },
            {'G',new bool[]{true,true,false }},{'H',new bool[]{false,false,false,false } },
            {'I' , new bool[]{false,false }},{'J',new bool[]{false,true,true,true } },
            {'K',new bool[]{true,false,true } },{'L',new bool[]{false,true,false,false } },
            {'M',new bool[]{true,true}},{'N',new bool[]{true,false }},{'O',new bool[]{true,true,true}},
            {'P' ,new bool[]{false,true,true,false } },{'Q',new bool[]{true,true,false,true } },
            {'R',new bool[]{false,true,false}},{'S',new bool[]{false,false,false } },{'T',new bool[]{true}},
            {'U',new bool[]{false,false,true }},{'V',new bool[]{false,false,true }},{'W',new bool[]{false,true,true } },
            {'X',new bool[]{true,false,false,true}},{'Y',new bool[]{true,false,true,true }},{'Z' , new bool[]{true,true,false,false }},
            {'0', new bool[]{true,true,true,true,true } },{'1',new bool[]{false,true,true,true,true }},{'2', new bool[]{false,false,true,true,true}},
            {'3', new bool[]{false,false,false,true,true }},{'4',new bool[]{false,false,false,false,true}},{'5',new bool[]{false,false,false,false,false}},
            {'6',new bool[]{true,false,false,false,false }},{'7', new bool[]{true,true,false,false,false } },{'8',new bool[]{true,true,true,false,false}},
            {'9',new bool[]{true,true,true,true,false }}            
        };
                 
        /// <summary>
        /// Return morse code string from a character. 2 space between each element
        /// </summary>
        /// <param name="character"></param>
        /// <returns>If the character is space, return 7 space as seperation per world</returns>
        public static string LetterToMorse(char character)
        {
            if (!Regex.IsMatch(character.ToString(), @"^[A-Z0-9_ ]*?$"))
            {
                throw new ArgumentException("Catain Special Character!");
            }
            string code = "";

            code = convertLetterToMorse(character);

            return code;
        }
        static string convertLetterToMorse(char character)
        {
      
            string code = string.Empty;
          
            if (character != ' ')
            {
                bool[]elements = characterToMorseCode[character];
                foreach (var element in elements)
                {
                    code += (element) ? "_ " : ". ";
                }
                code += new string(' ',2);
            }
            else
                code += new string(' ', 5);
            return code;
        }
        public static string StringToMorse(string content)
        {
            content = content.ToUpper();
            string code = "";

            foreach (var letter in content)
            {
                code += LetterToMorse(letter);
            }

            code = code.TrimEnd();
            return code;
        }
        public static List<bool[]>WordToMorseElement(string word)
        {
            word = word.ToUpper();
            List<bool[]> worldElements = new List<bool[]>();
            foreach (var letter in word)
            {
                worldElements.Add(characterToMorseCode[letter]);
            }
            return worldElements;
        }
        public static List<List<bool[]>>SentenceToMorseElements(string sentence)
        {
            List<List<bool[]>> sentenceElements = new List<List<bool[]>>();

            sentence = sentence.ToUpper();
            string[] words = sentence.Split(' ');
            foreach (var word in words)
            {
                sentenceElements.Add(WordToMorseElement(word));
            }
            return sentenceElements;
        }
        

       
    }
}
