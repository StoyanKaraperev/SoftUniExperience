namespace ChristmasPastryShop.Repositories.Models

{
    using ChristmasPastryShop.Models.Delicacies.Contracts;
    using ChristmasPastryShop.Repositories.Contracts;
    using System.Collections.Generic;

    internal class DelicacyRepository : IRepository<IDelicacy>
    {
        private readonly List<IDelicacy> delicacies;

        public DelicacyRepository()
        {
            this.delicacies = new List<IDelicacy>();
        }

        public IReadOnlyCollection<IDelicacy> Models => this.delicacies;

        public void AddModel(IDelicacy model)
        {
            this.delicacies.Add(model);
        }
    }
}
