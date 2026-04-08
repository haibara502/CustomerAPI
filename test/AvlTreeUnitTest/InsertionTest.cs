// Copyright (c) Qinyun Song. All rights reserved.

using AvlTree;
using Contract;
using Xunit;

namespace AvlTreeUnitTest
{
    /// <summary>
    /// Test focusing on insertions.
    /// </summary>
    public class InsertionTest
    {
        /// <summary>
        /// Test Left Left case.
        /// </summary>
        [Fact]
        public void LeftLeftTest()
        {
            Node? root = null;

            Customer customer1 = new Customer(3, 0);
            Customer customer2 = new Customer(2, 0);
            Customer customer3 = new Customer(1, 0);

            Node node1 = new Node(customer1);
            Node node2 = new Node(customer2);
            Node node3 = new Node(customer3);

            root = Tree.Insert(root, customer1, node1);
            root = Tree.Insert(root, customer2, node2);
            root = Tree.Insert(root, customer3, node3);

            long[] expectedInOrder = [1, 2, 3];
            long[] expectedPreOrder = [2, 1, 3];

            Utils.VerifyTreeStructure(root, expectedInOrder, expectedPreOrder);
        }

        /// <summary>
        /// Test Left Right case.
        /// </summary>
        [Fact]
        public void LeftRightTest()
        {
            Node? root = null;

            Customer customer1 = new Customer(3, 0);
            Customer customer2 = new Customer(1, 0);
            Customer customer3 = new Customer(2, 0);

            Node node1 = new Node(customer1);
            Node node2 = new Node(customer2);
            Node node3 = new Node(customer3);

            root = Tree.Insert(root, customer1, node1);
            root = Tree.Insert(root, customer2, node2);
            root = Tree.Insert(root, customer3, node3);

            long[] expectedInOrder = [1, 2, 3];
            long[] expectedPreOrder = [2, 1, 3];

            Utils.VerifyTreeStructure(root, expectedInOrder, expectedPreOrder);
        }

        /// <summary>
        /// Test Right Left case.
        /// </summary>
        [Fact]
        public void RightLeftTest()
        {
            Node? root = null;

            Customer customer1 = new Customer(1, 0);
            Customer customer2 = new Customer(3, 0);
            Customer customer3 = new Customer(2, 0);

            Node node1 = new Node(customer1);
            Node node2 = new Node(customer2);
            Node node3 = new Node(customer3);

            root = Tree.Insert(root, customer1, node1);
            root = Tree.Insert(root, customer2, node2);
            root = Tree.Insert(root, customer3, node3);

            long[] expectedInOrder = [1, 2, 3];
            long[] expectedPreOrder = [2, 1, 3];

            Utils.VerifyTreeStructure(root, expectedInOrder, expectedPreOrder);
        }

        /// <summary>
        /// Test Right Right case.
        /// </summary>
        [Fact]
        public void RightRightTest()
        {
            Node? root = null;

            Customer customer1 = new Customer(1, 0);
            Customer customer2 = new Customer(2, 0);
            Customer customer3 = new Customer(3, 0);

            Node node1 = new Node(customer1);
            Node node2 = new Node(customer2);
            Node node3 = new Node(customer3);

            root = Tree.Insert(root, customer1, node1);
            root = Tree.Insert(root, customer2, node2);
            root = Tree.Insert(root, customer3, node3);

            long[] expectedInOrder = [1, 2, 3];
            long[] expectedPreOrder = [2, 1, 3];

            Utils.VerifyTreeStructure(root, expectedInOrder, expectedPreOrder);
        }

        /// <summary>
        /// Testing the insertion with a general case.
        /// </summary>
        [Fact]
        public void GeneralInsertTest()
        {
            Node? root = null;

            Customer[] customers = new[]
            {
                new Customer(63, 0),
                new Customer(9, 0),
                new Customer(19, 0),
                new Customer(27, 0),
                new Customer(18, 0),
                new Customer(108, 0),
                new Customer(99, 0),
                new Customer(81, 0),
            };

            foreach (var customer in customers)
            {
                root = Tree.Insert(root, customer, new Node(customer));
            }

            long[] expectedInOrder = [9, 18, 19, 27, 63, 81, 99, 108];
            long[] expectedPreOrder = [19, 9, 18, 63, 27, 99, 81, 108];

            Assert.NotNull(root);
            Utils.VerifyTreeStructure(root, expectedInOrder, expectedPreOrder);
        }
    }
}