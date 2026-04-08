using AvlTree;
using Contract;

namespace AvlTree
{
    public class Tree
    {
        private static Node RightRotate(Node node)
        {
            var x = node.Left;
            var y = x!.Right;

            x.Right = node;
            node.Parent = x;

            node.Left = y;
            y.Parent = node;

            node.Update();
            x.Update();

            return x;
        }

        private static Node LeftRotate(Node node)
        {
            var x = node.Right;
            var y = x!.Left;

            x.Left = node;
            node.Parent = x;

            node.Right = y;
            y.Parent = node;

            node.Update();
            x.Update();

            return x;
        }

        private static Node Balance(Node node)
        {
            if (node.Balance > 1 && node.Left!.Balance >= 0)
            {
                // Left Left
                return RightRotate(node);
            }
            else if (node.Balance > 1 && node.Left!.Balance < 0)
            {
                // Left Right
                node.Left = LeftRotate(node.Left);
                return RightRotate(node);
            }
            else if (node.Balance < -1 && node.Right!.Balance <= 0)
            {
                // Right Right
                return LeftRotate(node);
            }
            else if (node.Balance < -1 && node.Right!.Balance > 0)
            {
                node.Right = RightRotate(node.Right);
                return LeftRotate(node);
            }

            return node;
        }

        private static Node Insert(Node? node, Customer customer)
        {
            if (node == null)
            {
                return new Node(customer);
            }

            if (customer < node.Customer)
            {
                node.Left = Insert(node.Left, customer);
                node.Left.Parent = node;
            }
            else
            {
                node.Right = Insert(node.Right, customer);
                node.Right.Parent = node;
            }

            node.Update();

            return Balance(node);
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

        private static Node? Delete(Node? node, Customer customer)
        {
            if (node == null)
            {
                return node;
            }

            if (customer < node.Customer)
            {
                node.Left = Delete(node.Left, customer);
                if (node.Left != null)
                {
                    node.Left.Parent = node;
                }
            }
            else if (customer > node.Customer)
            {
                node.Right = Delete(node.Right, customer);
                if (node.Right != null)
                {
                    node.Right.Parent = node;
                }
            }
            else
            {
                if (node.Left == null || node.Right == null)
                {
                    var child = node.Left ?? node.Right;
                    node = child;
                }
                else
                {
                    var minChild = GetMinNode(node.Right);
                    node.Customer = minChild.Customer;
                    node.Right = Delete(node.Right, minChild.Customer);
                    if (node.Right != null)
                    {
                        node.Right.Parent = node;
                    }
                }
            }

            if (node == null)
            {
                return node;  
            }

            node.Update();
            return Balance(node);
        }

        public static void GetRank(Node node)
        {
            int count = 1 + node.Left?.Size ?? 0;
            Node temp = node;
            while (temp.Parent != null)
            {
                if (temp == temp.Parent.Right)
                {
                    count += 1 + temp.Parent.Left?.Size ?? 0;
                }

                temp = temp.Parent;
            }
        }

        public static void GetRange(Node? node, int start, int end, List<Customer> customers)
        {
            if (node == null)
            {
                return;
            }

            int leftSize = node.Left?.Size ?? 0;

            if (start <= leftSize)
            {
                GetRange(node.Left, start, Math.Min(end, leftSize), customers);
            }

            if (start <= leftSize + 1 && end >= leftSize + 1)
            {
                customers.Add(node.Customer);
            }

            if (end > leftSize + 1)
            {
                GetRange(node.Right, Math.Max(start - leftSize - 1, 1), end - leftSize - 1, customers);
            }
        }
    }
}
