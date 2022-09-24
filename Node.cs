using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryTree
{
    class Node
    {
        public int value;
        public Node left;
        public Node right;

        public Node()
        {
            value = 0;
        }
        
        public Node(int givenVal)
        {
            value = givenVal;
        }

        ~Node()
        {
            
        }


        //Recursively prints the binary tree from left-to-right
        public void printTree()
        {
            //If this node has a leftNode, call its print function first
            if(this.left != null)
            {
                this.left.printTree();
            }

            //Then print this node's value
            Console.Write(" " + this.value + " |");

            //If this node has a rightNode, call its print function last
            if(this.right != null)
            {
                this.right.printTree();
            }
        }


        //Returns the height of a node, which is how far the node is from the farthest leaf
        //If node is uninitialized, return -1
        //If node has no leaf nodes, return 0
        //Else, return 1+maximum(getHeight(left), getHeight(right))
        public int GetHeight()
        {

            //check that this node was initialized
            if (this == null)
            {
                return -1;
            }

            //check if this node has any leaves, return 0 if not
            if ((this.left == null) && (this.right == null))
            {
                return 0;
            }

            //Check if this node only has one leaf
            if(this.left == null)
            {
                return 1 + this.right.GetHeight();
            }

            if(this.right == null)
            {
                return 1 + this.left.GetHeight();
            }

            //If both leaves are initialized, go through all of this node's leaf nodes, then add 1
            return 1 + (Math.Max(this.left.GetHeight(), this.right.GetHeight()));
        }

        //Determines the balance of this node, which is the difference between its left node's height and right node's height
            //Balance(x) = Height(x->left) - Height(x->right)
            //If a node is null, then its represented by '-1'
        public int GetBalance()
        {
            
            //Check that htis node is initialized
            if(this == null)
            {
                //Uninitialized trees are balanced to avoid them rotating
                return 0;
            }
           

            //Check that this node has at least one leaf node
            if((this.left == null) && (this.right == null))
            {
                //A node with no leaves is balanced because (-1) - (-1) = 0
                return 0;
            }
            

            //Check if this node has only one leaf node
            if(this.left == null)
            {
                //A null node has a height of -1
                return ((-1) - this.right.GetHeight());
            }

            if(this.right == null)
            {
                return this.left.GetHeight() - (-1);
            }
            
            //If both leaves are initialized, then return the difference
            return (this.left.GetHeight() - this.right.GetHeight());

        }

        //Given an integer value, creates a node and then adds it to this node's tree
        public void addNode(int givenInt)
        {
            Node createdNode = new Node(givenInt);
            this.addNode(createdNode);
        }

        //Given a node adds node to tree via binary search
        public void addNode(Node givenNode)
        {
            if(givenNode.value > this.value)
            {
                //check if givenNode can be placed here
                if (this.right == null)
                {
                    //if that spot is empty, that's where givenNode goes
                    this.right = givenNode;
                }
                else
                {
                    //recursively call the right node's addNode function
                    this.right.addNode(givenNode);
                }
            }
            else
            {
                //check if givenNode can be placed to the left of this node
                if (this.left == null)
                {
                    //spot is empty, so givenNode can be placed here
                    this.left = givenNode;
                }
                else
                {
                    //If left node is already present, call that node's addNode
                    this.left.addNode(givenNode);
                }
            }
        }
    }
}
