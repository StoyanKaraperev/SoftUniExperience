namespace ChristmasPastryShop.Models.Cocktails.Models
{
    public class Hibernation : Cocktail
    {
        private const double priceHibernation = 10.50;
        public Hibernation(string coctailName, string size) 
            : base(coctailName, size, priceHibernation)
        {
        }
    }
}
