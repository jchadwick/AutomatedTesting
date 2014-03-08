using System;

namespace AutomatedTesting
{
    [Serializable]
    public class EntityNotFoundException : ApplicationException
    {
        public string EntityId { get; private set; }
        public string EntityType { get; private set; }

        public EntityNotFoundException(string entityType)
            : base(string.Format("{0} not found", entityType))
        {
            EntityType = entityType;
        }

        public EntityNotFoundException(string entityType, object id)
            : base(string.Format("{0} not found for Id {1}", entityType, id))
        {
            EntityType = entityType;
            try
            {
                EntityId = id == null ? null : id.ToString();
            }
            catch
            {
            }
        }
    }

    public class EntityNotFoundException<TEntity> : EntityNotFoundException
    {
        public EntityNotFoundException()
            : base(typeof(TEntity).Name)
        {
        }
        public EntityNotFoundException(object id)
            : base(typeof(TEntity).Name, id)
        {
        }
    }
}
