using AvlTree;
using Contract;

namespace Backend
{
    public class Leaderboard
    {
        private AvlTree.Tree avlTree = new AvlTree.Tree();
        private Dictionary<Int64, Node> idToNode = new Dictionary<Int64, Node>();
        private Dictionary<Int64, Customer> idToCustomer = new Dictionary<Int64, Customer>();

        public void UpdateCustomerScore(Int64 id, decimal delta)
        {
            if (idToCustomer.ContainsKey(id))
            {
                avlTree.Remove(idToCustomer[id]);
                idToCustomer[id].UpdateScore(delta);
            }
            else
            {
                idToCustomer[id] = new Customer(id, delta);
            }

            idToNode[id] = avlTree.Insert(idToCustomer[id]);
        }

        public void GetCustomerById(Int64 id, int lower, int upper)
        {
            if (!idToCustomer.ContainsKey(id))
            {
                return;
            }

            int rank = Tree.GetRank(idToNode[id]);

            if (rank < lower)
            {
                return;
            }

            if (rank + upper - 1 > idToNode.Count)
            {
                return;
            }

            var customers = avlTree.GetRankRange(rank - lower + 1, rank + upper - 1);
            var tableRows = customers.Select(x => x.ToString()).ToList();
            return tableRows;
        }
    }
}
