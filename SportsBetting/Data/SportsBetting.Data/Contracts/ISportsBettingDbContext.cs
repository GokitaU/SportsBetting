﻿namespace SportsBetting.Data.Contracts
{
    using MongoDB.Driver;

    using SportsBetting.Data.Models.Base;

    public interface ISportsBettingDbContext
    {
        IMongoCollection<TEntity> GetCollection<TEntity>()
             where TEntity : BaseModel;
    }
}