namespace ItemInsertor.Core.Domain
{
    public class InsertItemResult
    {
        public string Sku { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}