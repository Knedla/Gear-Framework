using System;

namespace Entity.Data
{
    [Serializable]
    public abstract class EntityAmount<T> where T : IDataEntity
    {
        public T Entity { get; }
        public int Amount { get; set; }
        public EntityAmount(T entity, int amount = 1)
        {
            Entity = entity;
            Amount = amount;
        }
    }
}
