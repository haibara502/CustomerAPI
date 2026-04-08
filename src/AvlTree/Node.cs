// Copyright (c) Qinyun Song. All rights reserved.

using Contract;

namespace AvlTree
{
    /// <summary>
    /// Node in the AVL Tree.
    /// </summary>
    public class Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Node"/> class.
        /// </summary>
        /// <param name="customer">Key of the node.</param>
        public Node(Customer customer)
        {
            this.Customer = customer;
        }

        /// <summary>
        /// Gets or sets the height of the node.
        /// </summary>
        public int Height { get; set; } = 1;

        /// <summary>
        /// Gets or sets the left child of the node.
        /// </summary>
        public Node? Left { get; set; } = null;

        /// <summary>
        /// Gets or sets the right child of the node.
        /// </summary>
        public Node? Right { get; set; } = null;

        /// <summary>
        /// Gets or sets the parent of the node.
        /// </summary>
        public Node? Parent { get; set; } = null;

        /// <summary>
        /// Gets or sets the size of the tree with current node as root.
        /// </summary>
        public int Size { get; set; } = 1;

        /// <summary>
        /// Gets or sets the balance of the node.
        /// </summary>
        public int Balance { get; set; } = 0;

        /// <summary>
        /// Gets or sets the key of the node.
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// Update the height, balance and size of the node.
        /// You should always call this function after editing the child of the node.
        /// </summary>
        public void Update()
        {
            this.Height = 1 + Math.Max(this.Left?.Height ?? 0, this.Right?.Height ?? 0);
            this.Balance = (this.Left?.Height ?? 0) - (this.Right?.Height ?? 0);
            this.Size = 1 + (this.Left?.Size ?? 0) + (this.Right?.Size ?? 0);
        }
    }
}
