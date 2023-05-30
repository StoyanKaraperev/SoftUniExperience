namespace ChristmasPastryShop.Models.Cocktails.Models
{
    public class MulledWine : Cocktail
    {
        private const double priceMulledWine = 13.50;
        public MulledWine(string coctailName, string size) 
            : base(coctailName, size, priceMulledWine)
        {
        }
    }
}
