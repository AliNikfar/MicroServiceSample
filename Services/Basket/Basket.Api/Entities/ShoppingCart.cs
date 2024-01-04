namespace Basket.Api.Entities
{
    public class ShoppingCart
    {
        public ShoppingCart()
        { }
        public ShoppingCart(string userName)
        {
            UserName = userName;
        }
        public string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; }
        public Decimal TotalPrice 
        {
            get
            {
                // Rsult of Both must be Equal ; shold to test 
                if(Items is not null && Items.Any())
                return Items.Sum(p=>p.Price*p.Quantity);
                return 0;
                //decimal totalPrice = 0;
                //foreach(ShoppingCartItem item in Items)
                //{
                //    totalPrice +=item.Price*item.Quantity;
                //}
                //return totalPrice;
            }
                
        }
    }
}
