namespace ChristmasPastryShop.Models.Delicacies.Models
{
    using ChristmasPastryShop.Models.Delicacies.Contracts;
    using System;
    using System.Text;

    public abstract class Delicacy : IDelicacy
    {
        private string delicacyName;
        private double price;

        protected Delicacy(string delicacyName, double price)
        {
            Name = delicacyName;
            Price = price;
        }

        public string Name
        {
            get => delicacyName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be null or whitespace!");
                }

                delicacyName = value;
            }
        }

        public double Price
        {
            get => price;
            private set => price = value;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb
                .AppendLine($"{Name} - {Price:f2} lv");

            return sb.ToString().TrimEnd();
        }
    }
}
