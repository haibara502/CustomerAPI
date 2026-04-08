// Copyright (c) Qinyun Song. All rights reserved.

using AvlTree;
using Contract;
using Xunit;

namespace AvlTreeUnitTest
{
    /// <summary>
    /// Test focusing on rotations.
    /// </summary>
    public class RotationTest
    {
        /// <summary>
        /// Test the LeftRotate function.
        /// </summary>
        [Fact]
        public void TestLeftRotate()
        {
            /*
            Before:
                30
                /\
              25  40
                  /\
                 35 45
            After:
                40
                /\
              30 45
              /\
            25  35
            */
            Node node1 = new Node(new Customer(30, 0));
            Node node2 = new Node(new Customer(25, 0));
            Node node3 = new Node(new Customer(40, 0));
            Node node4 = new Node(new Customer(35, 0));
            Node node5 = new Node(new Customer(45, 0));

            node1.Left = node2;
            node2.Parent = node1;

            node1.Right = node3;
            node3.Parent = node1;

            node3.Left = node4;
            node4.Parent = node3;

            node3.Right = node5;
            node5.Parent = node3;

            node3.Update();
            node1.Update();

            var root = Tree.LeftRotate(node1);

            long[] expectedInOrder = [25, 30, 35, 40, 45];
            long[] expectedPreOrder = [40, 30, 25, 35, 45];

            Utils.VerifyTreeStructure(root, expectedInOrder, expectedPreOrder);
        }

        /// <summary>
        /// Test the RightRotate function.
        /// </summary>
        [Fact]
        public void TestRightRotate()
        {
            /*
            Before:
                40
                /\
              30  45
              /\
            25  35
            After:
                30
                /\
              25 40
                 /\
               35  45
            */
            Node node1 = new Node(new Customer(40, 0));
            Node node2 = new Node(new Customer(30, 0));
            Node node3 = new Node(new Customer(45, 0));
            Node node4 = new Node(new Customer(25, 0));
            Node node5 = new Node(new Customer(35, 0));

            node1.Left = node2;
            node2.Parent = node1;

            node1.Right = node3;
            node3.Parent = node1;

            node2.Left = node4;
            node4.Parent = node2;

            node2.Right = node5;
            node5.Parent = node2;

            node2.Update();
            node1.Update();

            var root = Tree.RightRotate(node1);

            long[] expectedInOrder = [25, 30, 35, 40, 45];
            long[] expectedPreOrder = [30, 25, 40, 35, 45];

            Utils.VerifyTreeStructure(root, expectedInOrder, expectedPreOrder);
        }
    }
}