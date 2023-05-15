using System.ComponentModel;
using A_FGMS.DataLayer.Exceptions;

namespace A_FGMS.DataLayer.EventBroker
{
    /// <summary>
    /// Class used as a broker to notify subscribers when data needs to be refreshed
    /// </summary>
    /// <author>Richard Nader, Jr.</author>
    /// <dateCreated>04/05/2023</dateCreated>
    public class DataRefreshEventBroker
    {
        private List<Action<CancelEventArgs, string>> Subscribers {  get; set; } = new List<Action<CancelEventArgs, string>>();

        /// <summary>
        /// Adds an action to the list of subscribers.
        /// When an event is published to the broker, all subscribers will be notified.
        /// </summary>
        /// <param name="subscriber">Action to be called when a DataUpdatedEvent is published to the broker</param>
        /// <author>Richard Nader, Jr.</author>
        /// <dateCreated>04/05/2023</dateCreated>
        public void Subscribe(Action<CancelEventArgs, string> subscriber)
        {
            Subscribers.Add(subscriber);
        }

        /// <summary>
        /// Removes an action from the list of subscribers.
        /// </summary>
        /// <param name="subscriber">Action to be removed from the list of subscribers. This action will no longer be called when an event is published</param>
        /// <author>Richard Nader, Jr.</author>
        /// <dateCreated>04/05/2023</dateCreated>
        public void Unsubscribe(Action<CancelEventArgs, string> subscriber)
        {
            Subscribers.Remove(subscriber);
        }

        /// <summary>
        /// Publishes an event to all subscribers.
        /// </summary>
        /// <param name="event">Event to be sent to all the registered subscribers</param>
        /// <author>Richard Nader, Jr.</author>
        /// <dateCreated>04/05/2023</dateCreated>
        public void Publish(string @event)
        {
            var publishEvent = new CancelEventArgs();
            foreach (var subscriber in Subscribers)
            {
                try
                {
                    subscriber(publishEvent, @event);
                }
                catch(RefreshDataCustomException ex)
                {
                    return;
                }
            }
        }
    }
}
