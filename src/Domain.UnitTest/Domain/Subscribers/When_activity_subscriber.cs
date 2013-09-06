namespace Browsio.UnitTest
{
    using System;
    using Browsio.Domain;
    using Incoding.Data;
    using Incoding.MSpecContrib;
    using Machine.Specifications;
    using Moq;
    using It = Machine.Specifications.It;

    [Subject(typeof(ActivitySubscriber))]
    public class When_activity_subscriber
    {
        #region Establish value

        static ActivitySubscriber subscriber;

        static OnActivity currentEvent;

        static Mock<IRepository> repository;

        #endregion

        Establish establish = () =>
                                  {
                                      currentEvent = Pleasure.Generator.Invent<OnActivity>();

                                      repository = Pleasure.Mock<IRepository>();

                                      subscriber = new ActivitySubscriber(repository.Object);
                                  };

        Because of = () => subscriber.Subscribe(currentEvent);

        It should_be_verify = () => repository.Verify(r => r.Save(Pleasure.MockIt.IsWeak<Activity, OnActivity>(currentEvent, 
            dsl => dsl
                .ForwardToAction(activity => activity.Date, activity => activity.Date.ShouldBeDate(DateTime.Now)))));
    }

}