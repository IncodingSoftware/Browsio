namespace Browsio.UnitTest
{
    #region << Using >>

    using System.Collections.Generic;
    using System.Web.Mvc;
    using Browsio.Controllers;
    using Browsio.Domain;
    using Incoding.Extensions;
    using Incoding.MSpecContrib;
    using Incoding.MvcContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(CategoryController))]
    public class When_category_controller_fetch
    {
        #region Establish value

        static MockController<CategoryController> mockController;

        static ActionResult result;

        #endregion

        Establish establish = () =>
                                  {
                                      mockController = MockController<CategoryController>
                                              .When();
                                  };

        Because of = () => { result = mockController.Original.Fetch(); };

        It should_be_result = () => result.ShouldBeIncodingData(new OptGroupVm(new List<KeyValueVm>
                                                                                   {
                                                                                           new KeyValueVm((int)CategoryOfType.Book, CategoryOfType.Book.ToLocalization()), 
                                                                                           new KeyValueVm((int)CategoryOfType.Movie, CategoryOfType.Movie.ToLocalization()), 
                                                                                           new KeyValueVm((int)CategoryOfType.TVShow, CategoryOfType.TVShow.ToLocalization()), 
                                                                                           new KeyValueVm((int)CategoryOfType.VideoGame, CategoryOfType.VideoGame.ToLocalization()), 
                                                                                   }));
    }
}