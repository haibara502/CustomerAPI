using Contract;

namespace AvlTree
{
    public class Node
    {
        public int Height { get; set; } = 0;
        public Node? Left { get; set; } = null;
        public Node? Right { get; set; } = null;
        public Node? Parent { get; set; } = null;
        public int Size { get; set; } = 1;
        public int Balance { get; set; } = 0;
        
        public required Customer Customer { get; set; }

        public Node(Customer _customer)
        {
            this.Customer = _customer;
            this.Height = 1;
            this.Left = null;
            this.Right = null;
            this.Parent = null;
            this.Size = 1;
            this.Balance = 0;
        }

        public void Update()
        {
            this.Height = 1 + Math.Max(Left?.Height ?? 0, Right?.Height ?? 0);
            this.Balance = Left?.Height ?? 0 - Right?.Height ?? 0;
            this.Size = 1 + this.Left?.Size ?? 0 + this.Right?.Size ?? 0;
        }
    }
}
