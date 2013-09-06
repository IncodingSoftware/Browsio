namespace Browsio.UnitTest
{
    #region << Using >>

    using Browsio.Domain;
    using Incoding.Data;
    using Incoding.MSpecContrib;
    using Machine.Specifications;
    using Moq;
    using It = Machine.Specifications.It;

    #endregion

    [Subject(typeof(SearchItemSubscriber))]
    public class When_search_item_subscriber
    {
        #region Establish value

        static SearchItemSubscriber subscriber;

        static OnChangeSearchItemEvent currentEvent;

        static Mock<IRepository> repository;

        #endregion

        Establish establish = () =>
                                  {
                                      currentEvent = Pleasure.Generator.Invent<OnChangeSearchItemEvent>();

                                      repository = Pleasure.Mock<IRepository>();
                                      subscriber = new SearchItemSubscriber(repository.Object);
                                  };

        Because of = () => subscriber.Subscribe(currentEvent);

        It should_be_save = () => repository.Verify(r => r.Save(Pleasure.MockIt.IsWeak<SearchItem, OnChangeSearchItemEvent>(currentEvent)));
    }
}