namespace WebApplication1.Entities
{
    public class Product
    {
        public int Id { get; set;  }
        public string Name { get; set;  }
        public int  StockSize { get; set;  }
        public double Price { get; set; }
        public byte[]? ProductImage { get; set; }
    }
}
