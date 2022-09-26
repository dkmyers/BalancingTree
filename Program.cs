using System;

namespace BinaryTree
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            Node test = new Node(3);
            test.addNode(4);
            test.addNode(5);
            Console.WriteLine("\n\nRoot node height: " + test.GetRootNode().GetHeight() + "\nRoot node balance: " + test.GetRootNode().GetBalance());
            test.LeftRotate();
            Console.WriteLine("\n\nRoot node height: " + test.GetRootNode().GetHeight() + "\nRoot node balance: " + test.GetRootNode().GetBalance());
        }
    }
}