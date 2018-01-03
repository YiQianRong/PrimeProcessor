using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringsProcessor
{
    public class StringsProcessor
    {
        //Reverses the order of words of a sentence
        public static string ReversesSentence(string sentence)
        {
            //To separate words by space ' '
            char[] separators = new char[] { ' ', '\t'};
            var words = sentence.Split(separators);

            //To rebuild the new sentence
            var newSentence = new StringBuilder();
            for(int i = words.Length - 1; i >= 0; i--)
            {
                if(words[i].Length == 0)
                {
                    continue;
                }

                if (newSentence.Length > 0)
                {
                    newSentence.Append(" ");
                }
                newSentence.Append(words[i]);
            }

            return newSentence.ToString();
        }

        //To construct union character list from str
        public static List<char> UnionCharacterList(string str)
        {
            var charList = new List<char>();
            foreach (var c in str)
            {
                if (!charList.Contains(c))
                {
                    charList.Add(c);
                }
            }

            return charList;
        }

        //To find common characters
        public static string CommonCharacters(string string1, string string2)
        {
            var charList1 = UnionCharacterList(string1);
            var charList2 = UnionCharacterList(string2);
            var commonList = new List<char>();

            foreach (var c in charList1)
            {
                if (charList2.Contains(c))
                {
                    commonList.Add(c);
                }
            }

            var newSentence = new StringBuilder();
            foreach (var c in commonList)
            {
                newSentence.Append(c);
            }
            return newSentence.ToString();
        }
    }
}
