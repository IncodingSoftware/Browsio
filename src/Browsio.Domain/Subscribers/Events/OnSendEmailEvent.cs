namespace Browsio.Domain
{
    using Incoding.EventBroker;

    public class OnSendEmailEvent : IEvent
    {
        #region Properties

        public string Subject { get; set; }

        public string To { get; set; }

        public string Body { get; set; }

        #endregion

    }
}