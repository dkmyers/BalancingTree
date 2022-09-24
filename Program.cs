using System;

namespace BinaryTree
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Node test = new Node(3);
            test.addNode(5);
            test.addNode(6);
            test.printTree();

            Console.WriteLine("\n\n" + test.GetHeight() + " " + test.GetBalance());
        }
    }
}
