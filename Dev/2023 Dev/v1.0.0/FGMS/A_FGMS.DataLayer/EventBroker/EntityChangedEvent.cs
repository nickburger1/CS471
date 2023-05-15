namespace A_FGMS.DataLayer.EventBroker
{
    /// <summary>
    /// Class used by the EntityChangeEventBroker to pass information to event subscribers when entities are updated.
    /// </summary>
    /// <author>Richard Nader, Jr.</author>
    /// <dateCreated>03/16/2023</dateCreated>
    public class EntityChangedEvent
    {
        public Type EntityType { get; }
        public int? EntityId { get; }
        public object Entity { get; }
        public string EntityName => EntityType.Name;

        // General "entity changed" notification
        public EntityChangedEvent(int? entityId, Type type, object data)
        {
            EntityId = entityId;
            EntityType = type;
            Entity = data;
        }
    }
}
