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
        public Node parent;
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
            if (this.left != null)
            {
                this.left.printTree();
            }

            //Then print this node's value
            Console.Write(" " + this.value + " |");

            //If this node has a rightNode, call its print function last
            if (this.right != null)
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
            if (this.left == null)
            {
                return 1 + this.right.GetHeight();
            }

            if (this.right == null)
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

            //Check that this node is initialized
            //Uninitialized nodes are balanced by default
            if (this == null)
            {
                return 0;
            }


            //Check that this node has at least one leaf node, to avoid null nodes being processed
            //A node with no leaves is balanced because (-1) - (-1) = 0
            if ((this.left == null) && (this.right == null))
            {
                return 0;
            }


            //Check if this node has only one leaf node
            //If so, the null node is treated as having a value of (-1)
            if (this.left == null)
            {
                return ((-1) - this.right.GetHeight());
            }

            if (this.right == null)
            {
                return this.left.GetHeight() - (-1);
            }

            //If both leaves are initialized, then return the difference
            return (this.left.GetHeight() - this.right.GetHeight());

        }

        //Rotates this node clockwise, aka 'right'
        //The node pointing to this node points to this node's left node
        //This node's left node is put in its place
        //The formerly-left-node's right node (if present) is put as this node's left node
        //This node is the formerly-left-node's new right node
        //
        //Things being tracked:
        //This node: Node calling the function
        //Elevated node: This node's left node, which is being elevated to take this node's place
        public void RightRotate()
        {
            //1. This node's parent node now points to the newly-elevated node
            if (this.parent != null)
            {
                //1a: Elevated node's parent is now the same as this node's parent
                this.left.parent = this.parent;

                //1b: This node's parent's left node is the elevated node
                this.parent.left = this.left;
                this.parent = this.left;

                //1c: This node no longer views the elevated node as its left node
                this.left = null;
            }
            else
            {
                //rotating around the root node
                //root node's parent node is null
                this.parent = this.left;
                this.parent.parent = null;
                this.left = null;
            }
            //2. The newly-promoted node's right node is this node
            //If that spot is already taken, then the right node is this node's new left node
            if (this.parent.right == null)
            {
                this.parent.right = this;
            }
            else
            {
                this.left = this.parent.right;
                this.parent.right = this;
            }
        }

        //Given a root node, searches from that point down
        //until the lowest imbalanced node is returned
        protected Node GetLowestImbalancedNode()
        {
            if (this.left != null)
            {
                if (this.left.GetBalance() > 1 || this.left.GetBalance() < -1)
                {
                    return this.left.GetLowestImbalancedNode();
                }
            }


            if (this.right != null)
            {
                if (this.right.GetBalance() > 1 || this.right.GetBalance() < -1)
                {
                    return this.right.GetLowestImbalancedNode();
                }
            }

            return this;
        }

        //Given any node, recursively moves up the AVL tree until a parentless node is discovered (presumably the root node)
        //The root node is returned
        public Node GetRootNode()
        {
            if (this.parent == null)
            {
                return this;
            }
            else
            {
                return this.parent.GetRootNode();
            }
        }

        public void PrintNodeInfo()
        {
            Console.WriteLine("\nValue: " + this.value);
            Console.WriteLine("Height: " + this.GetHeight());
            Console.WriteLine("Balance: " + this.GetBalance());
            if(this.left != null)
            {
                Console.WriteLine("Left: " + this.left.value);
            }

            if(this.right != null)
            {
                Console.WriteLine("Right: " + this.right.value);

            }

            if(this.parent != null)
            {
                Console.WriteLine("Parent: " + this.parent.value);
            }
            else
            {
                Console.WriteLine("Parent: Root");
            }


        }

        public void GetRootInfo()
        {
            Node root = this.GetRootNode();
            root.PrintNodeInfo();
        }

        protected void LeftRightRotate()
        {
            //1. Swap node with parent node to make this subtree indistinguishable from a left-heavy node after a left-insertion
            //1a. Find lowest imbalanced node and then get its left subtree
            Node rotor = this.GetRootNode().GetLowestImbalancedNode().left;
            //1b swap it with its right subtree
            rotor.right.parent = rotor.parent;
            rotor.parent.left = rotor.right;

            rotor.parent = rotor.right;
            rotor.right = null;
            rotor.parent.left = rotor;

            //2. Right-rotate at lowest imbalanced node
            //rotor = rotor.GetRootNode().GetLowestImbalancedNode();
            rotor.parent.parent.RightRotate();
        }

        protected void RightLeftRotate()
        {
            //1. Swap node with parent node to make this subtree indistinguishable from a right-heavy node after a right-insertion
            //1a. Find lowest imbalanced node and then get its left subtree
            Node rotor = this.GetRootNode().GetLowestImbalancedNode().right;
            //1b swap it with its right subtree
            rotor.left.parent = rotor.parent;
            rotor.parent.right = rotor.left;

            rotor.parent = rotor.left;
            rotor.left = null;
            rotor.parent.right = rotor;

            //2. Right-rotate at lowest imbalanced node
            //rotor = rotor.GetRootNode().GetLowestImbalancedNode();
            rotor.parent.parent.LeftRotate();
        }

        protected void LeftRotate()
        {
            //1. This node's parent node now points to the newly-elevated node
            if (this.parent != null)
            {
               
                //1b: This node's parent's right node is the elevated node
                if (this.right == null)
                {

                }
                else
                {
                    //1a: Elevated node's parent is now the same as this node's parent
                    this.right.parent = this.parent;
                    this.parent.right = this.right;
                }
                this.parent = this.right;

                //1c: This node no longer views the elevated node as its right node
                this.right = null;
            }
            else
            {
                //rotating around the root node
                //root node's parent node is null
                this.parent = this.right;
                this.parent.parent = null;
                this.right = null;
            }
            //2. The newly-promoted node's left node is this node
            //If that spot is already taken, then the left node is this node's new right node
            if (this.parent != null)
            {
                if (this.parent.left == null)
                {
                    this.parent.left = this;
                }
                else
                {
                    this.right = this.parent.left;
                    this.parent.left = this;
                }
            }
        }

        //Given an integer value, creates a node and then adds it to this node's tree
        //This insertion is done starting at the root node, so any node object can be used for node insertion
        public void addNode(int givenInt)
        {
            Node createdNode = new Node(givenInt);
            this.GetRootNode().addNodeObject(createdNode);
        }

        //Given a node, adds node to tree via binary search
        //This insertion must be done at the root node, and so access to this function requires using the addNode() function
        protected void addNodeObject(Node givenNode)
        {
            if (givenNode.value > this.value)
            {
                //if right node is empty, then place givenNode there,
                //otherwise call rightNode until an empty spot is found
                //When node is placed, check this node's balance and call relevant rotation function
                if (this.right == null)
                {
                    givenNode.parent = this;
                    this.right = givenNode;
                    //After insertion, check if root node is imbalanced
                    //If so, run either a LeftRotate() (if right-heavy) or a RightLeftRotate() (if left-heavy)
                    //Perform these rotations at this node's parent node


                    //if((this.parent != null) && (this.parent.GetBalance() < -1))
                    if (this.GetRootNode().GetBalance() < -1)
                    {

                        //Right-heavy, right insertion -> Left Rotation
                        this.GetRootNode().GetLowestImbalancedNode().LeftRotate();
                    }
                    else if (this.GetRootNode().GetBalance() > 1)
                    {
                        //left heavy, right insertion -> Left-Right rotation
                        this.GetRootNode().GetLowestImbalancedNode().LeftRightRotate();
                    }

                }
                else
                {
                    this.right.addNodeObject(givenNode);
                }
            }
            else
            {
                //if left  node is empty, then place givenNode there,
                //otherwise call leftNode until an empty spot is found
                if (this.left == null)
                {
                    //After insertion, check if root node is imbalanced
                    //If so, run either a LeftRotate() (if right-heavy) or a RightLeftRotate() (if left-heavy)
                    //Perform these rotations at this node's parent node
                    givenNode.parent = this;
                    this.left = givenNode;
                    //if ((this.parent != null) && (this.parent.GetBalance() > 1))
                    if (this.GetRootNode().GetBalance() > 1)
                    {
                        //Left insertion, left-heavy -> Right Rotation
                        this.GetRootNode().GetLowestImbalancedNode().RightRotate();
                    }
                    else if (this.GetRootNode().GetBalance() < -1)
                    {
                        //Left insertion, right-heavy -> Right-Left rotation
                        this.GetRootNode().GetLowestImbalancedNode().RightLeftRotate();
                    }
                }
                else
                {
                    this.left.addNodeObject(givenNode);
                }
            }
        }
    }//end Node definition
}