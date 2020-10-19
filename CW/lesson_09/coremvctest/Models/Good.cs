namespace coremvctest.Models
{
    public class Good
    {
        public int GoodId {get;set;}
        public string GoodName {get;set;}
        public decimal Price {get;set;}
        public decimal GoodCount {get;set;}
        public int? CategoryId {get;set;}
        public int? ManufacturerId {get;set;}
    }
}