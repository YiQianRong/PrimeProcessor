using System;

namespace Stack
{
    //2) Create your own class or a set of functions that implement a stack.
    //   Include the following four methods:
    //    a.Push: Adds a data element to the top of the stack
    //    b.Pop: Removes a data element from the top of the stack
    //    c.Size: Returns the total number of elements in the stack
    //    d.isEmpty: Returns true if the stack is empty.

    public class Node<T>
    {
        public Node(T i)
        {
            Value = i;
            Next = null;
        }

        public Node<T> Next { set; get; }
        public T Value { set; get; }
    }

    public class Stack<T>
    {
        public Node<T> Top { set; get; }
        public int Size { set; get; }

        public Stack()
        {
            Size = 0;
            Top = null;
        }

        public Node<T> Pop()
        {
            if (Top == null)
                return null;

            Node<T> n = Top;
            Top = Top.Next;
            Size--;
            return n;
        }

        public void Push(T i)
        {
            Node<T> n = new Node<T>(i);
            n.Next = Top;
            Top = n;
            Size++;
        }

        public bool IsEmpty
        {
            get
            {
                return Top == default(Node<T>);
            }
        }
    }


}
