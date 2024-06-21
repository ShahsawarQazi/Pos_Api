namespace Pos.Application.Contracts.Request.Menu
{
    public class CreateMenuRequest
    {
        public string Name { get; set; }
        public string Item { get; set; }
        public string Variant { get; set; }
        public string Size { get; set; }
        public double price { get; set; }

    }
}
