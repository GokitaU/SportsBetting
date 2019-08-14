﻿namespace SportsBetting.Handlers.Queries.Common
{
    using System.Collections.Generic;
    using System.Linq;

    using SportsBetting.Data.Cache.Contracts;
    using SportsBetting.Data.Models.Base;
    using SportsBetting.Handlers.Queries.Contracts;

    public class EntitiesByKeyQueryHandler<TEntity> : IQueryHandler<EntitiesByKeyQuery<TEntity>, IEnumerable<TEntity>>
        where TEntity : BaseModel
    {
        private readonly ICache<TEntity> cache;

        public EntitiesByKeyQueryHandler(ICache<TEntity> cache)
        {
            this.cache = cache;
        }

        public IEnumerable<TEntity> Handle(EntitiesByKeyQuery<TEntity> query)
        {
            IEnumerable<TEntity> entities = cache.All(x => query.Keys.Contains(x.Key));

            return entities;
        }
    }
}