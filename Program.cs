using System;

namespace BinaryTree
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            Node test = new Node(3);
            test.addNode(8);
            test.addNode(5);

            test.PrintNodeInfo();
            test.parent.PrintNodeInfo();
            test.parent.right.PrintNodeInfo();
            
            test.GetRootInfo();
            
        }
    }
}