namespace Browsio.UnitTest.Domain
{
    #region << Using >>

    using Browsio.Domain;
    using Incoding.CQRS;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(GetStoreByIdQuery))]
    public class When_get_store_by_id
    {
        #region Establish value

        static MockMessage<GetStoreByIdQuery, Store> mockQuery;

        static Store expected;

        #endregion

        Establish establish = () =>
                                  {
                                      var query = Pleasure.Generator.Invent<GetStoreByIdQuery>();
                                      expected = Pleasure.Generator.Invent<Store>();

                                      mockQuery = MockQuery<GetStoreByIdQuery, Store>
                                              .When(query)
                                              .StubQuery(whereSpecification: new EntityByIdSpec<Store>(query.Id), 
                                                         fetchSpecification: new StoreWithGenreFetchSpec(), 
                                                         entities: expected);
                                  };

        Because of = () => mockQuery.Original.Execute();

        It should_be_result = () => mockQuery.ShouldBeIsResult(expected);
    }
}