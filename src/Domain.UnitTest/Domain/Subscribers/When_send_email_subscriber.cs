namespace Browsio.UnitTest
{
    #region << Using >>

    using System;
    using System.Net.Mail;
    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Incoding.Utilities;
    using Machine.Specifications;
    using Moq;
    using It = Machine.Specifications.It;

    #endregion

    [Subject(typeof(SendEmailSubscriber))]
    public class When_send_email_subscriber
    {
        #region Establish value

        static SendEmailSubscriber subscriber;

        static OnSendEmailEvent currentEvent;

        static Mock<IEmailSender> emailSender;

        #endregion

        Establish establish = () =>
                                  {
                                      currentEvent = Pleasure.Generator.Invent<OnSendEmailEvent>(dsl => dsl.Tuning(r => r.To, Pleasure.Generator.Email()));

                                      Action<MailMessage> verify = message =>
                                                                       {
                                                                           message.To[0].ShouldEqual(new MailAddress(currentEvent.To));
                                                                           message.From.ShouldEqual(new MailAddress("null-notification@incoding.biz"));
                                                                           message.Body.ShouldEqual(currentEvent.Body);
                                                                           message.Subject.ShouldEqual(currentEvent.Subject);
                                                                           message.IsBodyHtml.ShouldBeTrue();
                                                                       };
                                      emailSender = Pleasure.MockStrict<IEmailSender>(mock => mock.Setup(r => r.Send(Pleasure.MockIt.Is(verify))));

                                      subscriber = new SendEmailSubscriber(emailSender.Object);
                                  };

        Because of = () => subscriber.Subscribe(currentEvent);

        It should_be_verify = () => emailSender.VerifyAll();
    }
}