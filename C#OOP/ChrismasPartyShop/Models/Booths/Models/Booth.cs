namespace ChristmasPastryShop.Models.Booths.Models
{
    using ChristmasPastryShop.Models.Booths.Contracts;
    using ChristmasPastryShop.Models.Cocktails.Contracts;
    using ChristmasPastryShop.Models.Delicacies.Contracts;
    using ChristmasPastryShop.Repositories.Contracts;
    using ChristmasPastryShop.Repositories.Models;
    using System;
    using System.Text;

    public class Booth : IBooth
    {
        private int boothId;
        private int capacity;
        private double currentBill;
        private double turnover;

        private readonly IRepository<IDelicacy> delicacies;
        private readonly IRepository<ICocktail> coctails; 

        public Booth(int boothId, int capacity)
        {
            this.BoothId = boothId;
            this.Capacity = capacity;

            this.delicacies = new DelicacyRepository(); 
            this.coctails = new CocktailRepository(); 

            this.currentBill = 0;
            this.turnover = 0;
        }

        public int BoothId
        {
            get => this.boothId; 
            private set => this.boothId = value;
        }

        public int Capacity
        {
            get => this.capacity;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Capacity has to be greater than 0!");
                }

                this.capacity = value;
            }
        }

        public IRepository<IDelicacy> DelicacyMenu => this.delicacies;

        public IRepository<ICocktail> CocktailMenu => this.coctails;

        public double CurrentBill => this.currentBill;

        public double Turnover => this.turnover;

        public bool IsReserved { get; private set; }

        public void UpdateCurrentBill(double amount)
        {
            this.currentBill += amount;
        }

        public void Charge()
        {
            this.turnover += this.currentBill;
            this.currentBill = 0;
        }

        public void ChangeStatus()
        {
            if (IsReserved)
            {
                IsReserved = false;
                return;
            }

            IsReserved = true; 
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb
                .AppendLine($"Booth: {this.boothId}")
                .AppendLine($"Capacity: {this.Capacity}")
                .AppendLine($"Turnover: {this.Turnover:f2} lv")
                .AppendLine("-Cocktail menu:");
            foreach (var coctail in this.CocktailMenu.Models)
            {
                sb
                    .AppendLine($"--{coctail}");
            }

            sb
                .AppendLine("-Delicacy menu:");
            foreach (var delicacy in this.DelicacyMenu.Models)
            {
                sb
                    .AppendLine($"--{delicacy}");
            }

            return sb.ToString().TrimEnd(); 
        }
    }
}
