namespace ChristmasPastryShop.Models.Cocktails.Models
{
    using ChristmasPastryShop.Models.Cocktails.Contracts;
    using System;
    using System.Text;

    public abstract class Cocktail : ICocktail
    {
        private string cocktailName;
        private string size;
        private double price;

        public Cocktail(string cocktailName, string size, double price)
        {
            this.Name = cocktailName;
            this.Size = size;
            this.Price = price;
        }

        public string Name
        {
            get => this.cocktailName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be null or whitespace!");
                }

                this.cocktailName = value;
            }
        }

        public string Size
        {
            get => this.size; 
            private set => this.size = value;
        }

        public double Price
        {
            get => this.price; 
            private set
            {
                if (this.Size == "Small")
                {
                    value /= 3;
                }
                else if(this.Size == "Middle")
                {
                    value = (value / 3) * 2;
                }

                this.price = value;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb
                .AppendLine($"{this.Name} ({this.Size}) - {this.Price:f2} lv");

            return sb.ToString().TrimEnd();
        }
    }
}
