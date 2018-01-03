using NUnit.Framework;
using System.Text;

namespace Stack.Test
{
    [TestFixture]
    public class StackTest
    {
        //2) Create your own class or a set of functions that implement a stack.
        //   Include the following four methods:
        //    a.Push: Adds a data element to the top of the stack
        //    b.Pop: Removes a data element from the top of the stack
        //    c.Size: Returns the total number of elements in the stack
        //    d.isEmpty: Returns true if the stack is empty.

        [Test]
        public void StackIsEmptyTest()
        {
            var intStack = new Stack<int>();
            bool result = true;

            Assert.AreEqual(result, intStack.IsEmpty);
        }

        [Test]
        public void StackPushPopTest()
        {
            var charStack = new Stack<char>();
            charStack.Push('A');
            charStack.Push('B');
            charStack.Push('C');
            charStack.Push('D');
            charStack.Push('E');

            var newString = new StringBuilder();
            while (!charStack.IsEmpty)
            {
                var node = charStack.Pop();
                newString.Append(node.Value);
            }

            string result = "EDCBA";
            Assert.AreEqual(result, newString.ToString());
        }

        [Test]
        public void StackSizeTest()
        {
            var charStack = new Stack<char>();
            charStack.Push('A');
            charStack.Push('B');
            charStack.Push('C');
            charStack.Push('D');
            charStack.Push('E');
            charStack.Push('1');
            var node = charStack.Pop();

            int result = 5;
            Assert.AreEqual(result, charStack.Size);
        }
    }
}
