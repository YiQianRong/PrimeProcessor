using NUnit.Framework;

namespace StringsProcessor.Test
{
    [TestFixture]
    public class StringsProcessorTest
    {
        [Test]
        //1) Write a function that reverses the order of words of a sentence.
        public void ReversesSentenceTest()
        {
            var test = "write    a function  \t that reverses the order of words of a sentence";
            var result = StringsProcessor.ReversesSentence(test);

            var statement = "sentence a of words of order the reverses that function a write";
            Assert.AreEqual(statement.ToString(), result);
        }

        [Test]
        //3) Write a function f(a, b) which takes two character string arguments and returns a string containing only the characters found in both strings.
        public void CommonCharactersTest()
        {
            var string1 = "11234567890aabcdefghijklmnopqrstuvw!!!@#$%^&*()";
            var string2 = "11234567890aABCDEFGHIJKLMNOPQRSTUVW!@@@#$%^&*()";
            var result = StringsProcessor.CommonCharacters(string1, string2);
            var statement = "1234567890a!@#$%^&*()";

            Assert.AreEqual(statement, result);
        }

    }
}
