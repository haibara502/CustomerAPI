// Copyright (c) Qinyun Song. All rights reserved.
using System.Runtime.CompilerServices;
using Contract;

[assembly: InternalsVisibleTo("AvlTreeUnitTest")]

namespace AvlTree
{
    /// <summary>
    /// The AVL Tree.
    /// </summary>
    public class Tree
    {
        /// <summary>
        /// Insert a new node to the tree.
        /// </summary>
        /// <param name="root">Root of current tree.</param>
        /// <param name="customer">The key of the new node.</param>
        /// <param name="newNode">The new node.</param>
        /// <returns>Returns the new root of the tree.</returns>
        public static Node Insert(Node? root, Customer customer, Node newNode)
        {
            if (root == null)
            {
                return newNode;
            }

            if (customer < root.Customer)
            {
                root.Left = Insert(root.Left, customer, newNode);
                root.Left.Parent = root;
            }
            else
            {
                root.Right = Insert(root.Right, customer, newNode);
                root.Right.Parent = root;
            }

            root.Update();

            return Balance(root);
        }

        /// <summary>
        /// Delete an key from the tree.
        /// </summary>
        /// <param name="root">Root of the tree.</param>
        /// <param name="customer">Key to be deleted.</param>
        /// <returns>Returns the new root of the tree.</returns>
        public static Node? Delete(Node? root, Customer customer)
        {
            if (root == null)
            {
                return root;
            }

            if (customer < root.Customer)
            {
                root.Left = Delete(root.Left, customer);
                if (root.Left != null)
                {
                    root.Left.Parent = root;
                }
            }
            else if (customer > root.Customer)
            {
                root.Right = Delete(root.Right, customer);
                if (root.Right != null)
                {
                    root.Right.Parent = root;
                }
            }
            else
            {
                if (root.Left == null || root.Right == null)
                {
                    var child = root.Left ?? root.Right;

                    var temp = root;
                    root = child;

                    if (temp.Parent != null)
                    {
                        if (root != null)
                        {
                            root.Parent = temp.Parent;
                        }

                        if (temp == temp.Parent.Left)
                        {
                            temp.Parent.Left = root;
                        }
                        else
                        {
                            temp.Parent.Right = root;
                        }
                    }
                }
                else
                {
                    var minChild = GetMinNode(root.Right);
                    root.Customer = minChild.Customer;
                    root.Right = Delete(root.Right, minChild.Customer);
                    if (root.Right != null)
                    {
                        root.Right.Parent = root;
                    }
                }
            }

            if (root == null)
            {
                return root;
            }

            root.Update();
            return Balance(root);
        }

        /// <summary>
        /// Get the rank of a node.
        /// </summary>
        /// <param name="node">The node to calculate.</param>
        /// <returns>The rank of the node.</returns>
        public static int GetRank(Node node)
        {
            int count = 1 + (node.Left?.Size ?? 0);
            Node temp = node;
            while (temp.Parent != null)
            {
                if (temp == temp.Parent.Right)
                {
                    count += 1 + (temp.Parent.Left?.Size ?? 0);
                }

                temp = temp.Parent;
            }

            return count;
        }

        /// <summary>
        /// Get a list of keys for nodes whose rank insides a given range.
        /// </summary>
        /// <param name="root">Root of the tree.</param>
        /// <param name="start">Range start.</param>
        /// <param name="end">Range end.</param>
        /// <param name="customers">List of customers whose rank insides the range.</param>
        public static void GetRange(Node? root, int start, int end, List<Customer> customers)
        {
            if (root == null)
            {
                return;
            }

            int leftSize = root.Left?.Size ?? 0;

            if (start <= leftSize)
            {
                GetRange(root.Left, start, Math.Min(end, leftSize), customers);
            }

            if (start <= leftSize + 1 && end >= leftSize + 1)
            {
                customers.Add(root.Customer);
            }

            if (end > leftSize + 1)
            {
                GetRange(root.Right, Math.Max(start - leftSize - 1, 1), end - leftSize - 1, customers);
            }
        }

        /// <summary>
        /// Right rotate the tree.
        /// </summary>
        /// <param name="root">Root of the tree.</param>
        /// <returns>The new root.</returns>
        internal static Node RightRotate(Node root)
        {
            var x = root.Left;
            var y = x!.Right;
            var parent = root.Parent;

            x.Right = root;
            root.Parent = x;
            if (parent == null)
            {
                x.Parent = null;
            }
            else if (root == parent.Left)
            {
                x.Parent = parent;
                parent.Left = x;
            }
            else
            {
                x.Parent = parent;
                parent.Right = x;
            }

            root.Left = y;
            if (y != null)
            {
                y.Parent = root;
            }

            root.Update();
            x.Update();

            return x;
        }

        /// <summary>
        /// Left rotate the tree.
        /// </summary>
        /// <param name="root">Root of the tree.</param>
        /// <returns>The new root.</returns>
        internal static Node LeftRotate(Node root)
        {
            var x = root.Right;
            var y = x!.Left;
            var parent = root.Parent;

            x.Left = root;
            root.Parent = x;
            if (parent == null)
            {
                x.Parent = null;
            }
            else if (root == parent.Left)
            {
                x.Parent = parent;
                parent.Left = x;
            }
            else
            {
                x.Parent = parent;
                parent.Right = x;
            }

            root.Right = y;
            if (y != null)
            {
                y.Parent = root;
            }

            root.Update();
            x.Update();

            return x;
        }

        private static Node Balance(Node root)
        {
            if (root.Balance > 1 && root.Left!.Balance >= 0)
            {
                // Left Left
                return RightRotate(root);
            }
            else if (root.Balance > 1 && root.Left!.Balance < 0)
            {
                // Left Right
                root.Left = LeftRotate(root.Left);
                return RightRotate(root);
            }
            else if (root.Balance < -1 && root.Right!.Balance <= 0)
            {
                // Right Right
                return LeftRotate(root);
            }
            else if (root.Balance < -1 && root.Right!.Balance > 0)
            {
                root.Right = RightRotate(root.Right);
                return LeftRotate(root);
            }

            return root;
        }

        private static Node GetMinNode(Node root)
        {
            Node node = root;
            while (node.Left != null)
            {
                node = node.Left;
            }

            return node;
        }
    }
}
