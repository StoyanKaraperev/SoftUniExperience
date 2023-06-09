﻿namespace ChristmasPastryShop.Repositories.Models
{
    using ChristmasPastryShop.Models.Cocktails.Contracts;
    using ChristmasPastryShop.Repositories.Contracts;
    using System.Collections.Generic;

    internal class CocktailRepository : IRepository<ICocktail>
    {
        private readonly List<ICocktail> cocktails;

        public CocktailRepository()
        {
            this.cocktails = new List<ICocktail>();
        }

        public IReadOnlyCollection<ICocktail> Models => this.cocktails;

        public void AddModel(ICocktail model)
        {
            this.cocktails.Add(model);
        }
    }
}
