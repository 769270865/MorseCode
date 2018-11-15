using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MorseCode.Test
{
    [TestClass]
    public class MorseCodeConvertTest
    {
        [TestMethod]
        public void LetterToMorseTest()
        {
            Random random = new Random();
            int ASCIINumber = random.Next(65, 91);
            char letter = (char)ASCIINumber;

            bool[] exceptedMorseElements = MorseCodeConvert.characterToMorseCode[letter];
            string exceptedMorseString = "";
            foreach (var element in exceptedMorseElements)
            {
                exceptedMorseString += element ? "_ " : ". ";
            }
            exceptedMorseString += "  ";

            string acturalMorseString = MorseCodeConvert.LetterToMorse(letter);

            Assert.IsTrue(exceptedMorseString == acturalMorseString);
        }

        [TestMethod]      
        public void IllegalCharacterArgumentExceptionTest()
        {
            Random random = new Random();
            int ASCIINumber = random.Next(33, 48);
            char IllegalCharacter = (char)ASCIINumber;


            Assert.ThrowsException<ArgumentException>(() => MorseCodeConvert.LetterToMorse(IllegalCharacter));
        }
        [TestMethod]
        public void StringToMorseTest()
        {
            string testString = "Foo Bar";

            string exceptedMorse = ". . _ .   _ _ _   _ _ _        _ . . .   . _   . _ .";

            string actural = MorseCodeConvert.StringToMorse(testString);

            Assert.IsTrue(exceptedMorse == actural);
        }
    }
}
