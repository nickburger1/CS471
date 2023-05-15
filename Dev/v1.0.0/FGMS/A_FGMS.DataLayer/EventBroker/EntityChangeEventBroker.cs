namespace A_FGMS.DataLayer.EventBroker
{
    /// <summary>
    /// Class used as a broker to notify subscribers when data has been modified.
    /// </summary>
    /// <author>Richard Nader, Jr.</author>
    /// <dateCreated>03/16/2023</dateCreated>
    public class EntityChangeEventBroker
    {
        private List<Action<List<EntityChangedEvent>>> Subscribers {  get; set; } = new List<Action<List<EntityChangedEvent>>>();

        /// <summary>
        /// Adds an action to the list of subscribers.
        /// When a DataUpdatedEvent event is published to the broker, all subscribers will be notified.
        /// </summary>
        /// <param name="subscriber">Action to be called when a DataUpdatedEvent is published to the broker</param>
        /// <author>Richard Nader, Jr.</author>
        /// <dateCreated>03/16/2023</dateCreated>
        public void Subscribe(Action<List<EntityChangedEvent>> subscriber)
        {
            Subscribers.Add(subscriber);
        }

        /// <summary>
        /// Removes an action from the list of subscribers.
        /// </summary>
        /// <param name="subscriber">Action to be removed from the list of subscribers. This action will no longer be called when a DataUpdatedEvent is published</param>
        /// <author>Richard Nader, Jr.</author>
        /// <dateCreated>03/16/2023</dateCreated>
        public void Unsubscribe(Action<List<EntityChangedEvent>> subscriber)
        {
            Subscribers.Remove(subscriber);
        }

        /// <summary>
        /// Publishes an DataUpdatedEvent to all subscribers.
        /// </summary>
        /// <param name="event">DataUpdatedEvent to be sent to all the registered subscribers</param>
        /// <author>Richard Nader, Jr.</author>
        /// <dateCreated>03/16/2023</dateCreated>
        public void Publish(List<EntityChangedEvent> @event)
        {
            foreach (var subscriber in Subscribers)
            {
                subscriber(@event);
            }
        }
    }
}
