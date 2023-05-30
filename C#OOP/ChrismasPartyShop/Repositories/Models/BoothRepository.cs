using ChristmasPastryShop.Models.Booths.Contracts;
namespace ChristmasPastryShop.Repositories.Models
{
    using ChristmasPastryShop.Repositories.Contracts;
    using System.Collections.Generic;
    public class BoothRepository : IRepository<IBooth>
    {
        private readonly List<IBooth> booths;

        public BoothRepository()
        {
            this.booths = new List<IBooth>();
        }

        public IReadOnlyCollection<IBooth> Models => this.booths; 

        public void AddModel(IBooth model)
        {
            this.booths.Add(model);
        }
    }
}
