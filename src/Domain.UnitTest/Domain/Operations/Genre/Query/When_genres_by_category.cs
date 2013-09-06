namespace Browsio.UnitTest.Domain
{
    #region << Using >>

    using System.Collections.Generic;
    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(GetGenresByCategoryQuery))]
    public class When_genres_by_category
    {
        #region Establish value

        static MockMessage<GetGenresByCategoryQuery, List<Genre>> mockQuery;

        static List<Genre> expected;

        #endregion

        Establish establish = () =>
                                  {
                                      var query = Pleasure.Generator.Invent<GetGenresByCategoryQuery>();
                                      expected = Pleasure.ToList(Pleasure.Generator.Invent<Genre>());

                                      mockQuery = MockQuery<GetGenresByCategoryQuery, List<Genre>>
                                              .When(query)
                                              .StubQuery(whereSpecification: new GenreByCategoryOptWhereSpec(query.Category), 
                                                         entities: expected.ToArray());
                                  };

        Because of = () => mockQuery.Original.Execute();

        It should_be_result = () => mockQuery.ShouldBeIsResult(expected);
    }
}