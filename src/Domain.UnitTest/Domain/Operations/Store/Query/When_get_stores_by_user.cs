namespace Browsio.UnitTest.Domain
{
    #region << Using >>

    using System.Collections.Generic;
    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(GetStoresByUserQuery))]
    public class When_get_stores_by_user
    {
        #region Establish value

        static MockMessage<GetStoresByUserQuery, List<Store>> mockQuery;

        static List<Store> expected;

        #endregion

        Establish establish = () =>
                                  {
                                      var query = Pleasure.Generator.Invent<GetStoresByUserQuery>();
                                      expected = Pleasure.ToList(Pleasure.Generator.Invent<Store>());

                                      mockQuery = MockQuery<GetStoresByUserQuery, List<Store>>
                                              .When(query)
                                              .StubQuery(whereSpecification: new StoreByUserWhereSpec(query.UserId), 
                                                         fetchSpecification: new StoreWithGenreFetchSpec(), 
                                                         entities: expected.ToArray());
                                  };

        Because of = () => mockQuery.Original.Execute();

        It should_be_result = () => mockQuery.ShouldBeIsResult(expected);
    }
}