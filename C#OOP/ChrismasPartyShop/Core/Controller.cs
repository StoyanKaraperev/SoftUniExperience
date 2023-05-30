namespace ChristmasPastryShop.Core
{
    using ChristmasPastryShop.Core.Contracts;
    using ChristmasPastryShop.Models.Booths.Contracts;
    using ChristmasPastryShop.Models.Booths.Models;
    using ChristmasPastryShop.Models.Cocktails.Contracts;
    using ChristmasPastryShop.Models.Cocktails.Models;
    using ChristmasPastryShop.Models.Delicacies.Contracts;
    using ChristmasPastryShop.Models.Delicacies.Models;
    using ChristmasPastryShop.Repositories.Contracts;
    using ChristmasPastryShop.Repositories.Models;
    using System;
    using System.Linq;
    using System.Text;

    public class Controller : IController
    {
        private readonly IRepository<IBooth> booths;

        public Controller()
        {
            this.booths = new BoothRepository();
        }

        public string AddBooth(int capacity)
        {
            int boothId = this.booths.Models.Count + 1;

            IBooth booth = new Booth(boothId, capacity); 
            
            this.booths.AddModel(booth);

            return $"Added booth number {boothId} with capacity {capacity} in the pastry shop!";
        }

        public string AddDelicacy(int boothId, string delicacyTypeName, string delicacyName)
        {
            if (this.booths.Models.Any(b => b.DelicacyMenu.Models.Any(d => d.Name == delicacyName)))
            {
                return $"{delicacyName} is already added in the pastry shop!";
            }

            IDelicacy delicacy = delicacyTypeName switch
            {
                nameof(Gingerbread) => new Gingerbread(delicacyName),
                nameof(Stolen) => new Stolen(delicacyName),
                _ => throw new InvalidOperationException($"Delicacy type {delicacyTypeName} is not supported in our application!")
            };

            IBooth booth = this.booths.Models.FirstOrDefault(b => b.BoothId == boothId);
            booth.DelicacyMenu.AddModel(delicacy);

            return $"{delicacyTypeName} {delicacyName} added to the pastry shop!";
        }

        public string AddCocktail(int boothId, string cocktailTypeName, string cocktailName, string size)
        {
            ICocktail coctail = cocktailTypeName switch
            {
                nameof(Hibernation) => new Hibernation(cocktailName, size),
                nameof(MulledWine) => new MulledWine(cocktailName, size),
                _ => throw new InvalidOperationException($"Cocktail type {cocktailTypeName} is not supported in our application!")
            };

            if (size != "Small" && size != "Middle" && size != "Large")
            {
                return $"{size} is not recognized as valid cocktail size!";
            }

            if (this.booths.Models.Any(b => b.CocktailMenu.Models.Any(cm => cm.Size == size)) &&
                this.booths.Models.Any(b => b.CocktailMenu.Models.Any(cm => cm.GetType().Name == cocktailTypeName)))
            {
                return $"{size} {cocktailName} is already added in the pastry shop!";
            }

            IBooth booth = this.booths.Models.FirstOrDefault(b => b.BoothId == boothId); 
            booth.CocktailMenu.AddModel(coctail);

            return $"{size} {cocktailName} {cocktailTypeName} added to the pastry shop!";
        }

        public string ReserveBooth(int countOfPeople)
        {
            var boots = this.booths.Models
                .Where(b => b.IsReserved == false && b.Capacity >= countOfPeople)
                .OrderBy(b => b.Capacity)
                .ThenByDescending(b => b.BoothId)
                .FirstOrDefault();

            if (boots == null)
            {
                return $"No available booth for {countOfPeople} people!"; 
            }

            boots.ChangeStatus();
            return $"Booth {boots.BoothId} has been reserved for {countOfPeople} people!";
        }

        public string TryOrder(int boothId, string order)
        {
            IBooth booth = this.booths.Models.FirstOrDefault(b => b.BoothId == boothId);

            string[] orderArray = order.Split('/'); 
            string itemTypeName = orderArray[0];
            string itemName = orderArray[1];
            int pieces = int.Parse(orderArray[2]);

            if (itemTypeName != nameof(Hibernation) && 
                itemTypeName != nameof(MulledWine) && 
                itemTypeName != nameof(Gingerbread) && 
                itemTypeName != nameof(Stolen))
            {
                return $"{itemTypeName} is not recognized type!";
            }

            if (!this.booths.Models.Any(b => b.CocktailMenu.Models.Any(cm => cm.Name == itemName)) && 
                !this.booths.Models.Any(b => b.DelicacyMenu.Models.Any(dm => dm.Name == itemName)))
            {
                return $"There is no {itemTypeName} {itemName} available!";
            }

            if (itemTypeName == nameof(Hibernation) || 
                itemTypeName == nameof(MulledWine))
            {
                string size = orderArray[3];

                ICocktail desiredCoctail =
                    booth.CocktailMenu.Models
                    .FirstOrDefault(cm => cm.GetType().Name == itemTypeName && cm.Name == itemName && cm.Size == size);

                if (desiredCoctail == null) 
                {
                    return $"There is no {size} {itemName} available!";
                }

                booth.UpdateCurrentBill(desiredCoctail.Price * pieces);
                return $"Booth {boothId} ordered {pieces} {itemName}!";
            }

            else
            {
                IDelicacy desiredDelicaties =
                    booth.DelicacyMenu.Models
                    .FirstOrDefault(dm => dm.GetType().Name == itemTypeName && dm.Name == itemName);

                if (desiredDelicaties == null)
                {
                    return $"There is no {itemTypeName} {itemName} available!";
                }

                booth.UpdateCurrentBill(desiredDelicaties.Price * pieces);
                return $"Booth {boothId} ordered {pieces} {itemName}!";
            }
        }

        public string LeaveBooth(int boothId)
        {
            var sb = new StringBuilder();

            IBooth booth = this.booths.Models.FirstOrDefault(b => b.BoothId == boothId);

            sb
                .AppendLine($"Bill {booth.CurrentBill:f2} lv");

            booth.Charge();
            booth.ChangeStatus();

            sb
                .AppendLine($"Booth {boothId} is now available!"); 

            return sb.ToString().TrimEnd();
        }

        public string BoothReport(int boothId)
        {
            return this.booths.Models.FirstOrDefault(b => b.BoothId == boothId).ToString().TrimEnd();

        }
    }
}