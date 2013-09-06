namespace Browsio.UnitTest
{
    #region << Using >>

    using System.Collections.Generic;
    using System.Web.Mvc;
    using Browsio.Controllers;
    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Incoding.MvcContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(GenreController))]
    public class When_genre_controller_fetch
    {
        #region Establish value

        static MockController<GenreController> mockController;

        static ActionResult result;

        static GetGenresByCategoryQuery query;

        static List<Genre> expected;

        #endregion

        Establish establish = () =>
                                  {
                                      query = Pleasure.Generator.Invent<GetGenresByCategoryQuery>();
                                      expected = Pleasure.ToList(Pleasure.Generator.Invent<Genre>());

                                      mockController = MockController<GenreController>
                                              .When()
                                              .StubQuery(query, expected);
                                  };

        Because of = () => { result = mockController.Original.Fetch(query); };

        It should_be_result
                = () => result.ShouldBeIncodingData<OptGroupVm>(vm => vm.Items.ShouldEqualWeakEach(expected, (dsl, i) => dsl.IgnoreBecauseNotUse(r => r.Selected)
                                                                                                                            .IgnoreBecauseNotUse(r => r.Title)
                                                                                                                            .ForwardToValue(r => r.Text, expected[i].Name)
                                                                                                                            .ForwardToValue(r => r.Value, expected[i].Id.ToString())));
    }
}