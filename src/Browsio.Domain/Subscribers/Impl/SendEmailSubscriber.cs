namespace Browsio.Domain
{
    #region << Using >>

    using System.Configuration;
    using System.Net.Mail;
    using Incoding.EventBroker;
    using Incoding.Utilities;

    #endregion

    public class SendEmailSubscriber : IEventSubscriber<OnSendEmailEvent>
    {
        #region Fields

        readonly IEmailSender sender;

        #endregion

        #region Constructors

        public SendEmailSubscriber(IEmailSender sender)
        {
            this.sender = sender;
        }

        #endregion

        #region IEventSubscriber<OnSendEmailEvent> Members

        public void Subscribe(OnSendEmailEvent @event)
        {
            string emailFrom = ConfigurationManager.AppSettings["MailFrom"];

            this.sender.Send(new MailMessage(new MailAddress(emailFrom), new MailAddress(@event.To))
                                 {
                                         Subject = @event.Subject, 
                                         Body = @event.Body, 
                                         IsBodyHtml = true
                                 });
        }

        #endregion

        ////ncrunch: no coverage start
        #region Disposable

        public void Dispose() { }

        #endregion

        ////ncrunch: no coverage end   
    }
}