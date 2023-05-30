namespace ChristmasPastryShop.Models.Delicacies.Models
{
    public class Gingerbread : Delicacy
    {
        private const double gignerbreadPrice = 4.00;
        public Gingerbread(string delicacyName)
            : base(delicacyName, gignerbreadPrice)
        {
        }
    }
}
