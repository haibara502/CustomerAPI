
namespace Contract
{
    public class Customer
    {
        public Int64 Id { get; set; }
        public decimal Score { get; set; } = 0;

        public static bool operator <(Customer c1, Customer c2)
        {
            return c1.Score == c2.Score ? c1.Id < c2.Id : c1.Score < c2.Score;
        }

        public static bool operator >(Customer c1, Customer c2)
        {
            return c1.Score == c2.Score? c1.Id > c2.Id : c1.Score > c2.Score;
        }
    }
}
