namespace Browsio.UnitTest
{
    using Browsio.Domain;
    using Incoding.Data;
    using Incoding.MSpecContrib;
    using Machine.Specifications;
    using Moq;
    using It = Machine.Specifications.It;

    [Subject(typeof(SearchItemSubscriber))]
    public class When_search_item_subscriber_with_item
    {
        #region Establish value

        static SearchItemSubscriber subscriber;

        static OnChangeSearchItemEvent currentEvent;

        static Mock<SearchItem> searchItem;

        #endregion

        Establish establish = () =>
                                  {
                                      currentEvent = Pleasure.Generator.Invent<OnChangeSearchItemEvent>();

                                      searchItem = Pleasure.MockStrict<SearchItem>(mock => mock.SetupSet(r => r.Query = currentEvent.Query));
                                      var repository = Pleasure.Mock<IRepository>(mock => mock.StubQuery(whereSpecification: new SearchItemByOwnerWhereSpec(currentEvent.OwnerId), 
                                                                                                         entities: searchItem.Object));

                                      subscriber = new SearchItemSubscriber(repository.Object);
                                  };

        Because of = () => subscriber.Subscribe(currentEvent);

        It should_be_verify = () => searchItem.VerifyAll();
    }
}