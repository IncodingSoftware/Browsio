namespace Browsio.UnitTest
{
    #region << Using >>

    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(Store))]
    public class When_store_category_as_amazon
    {
        It should_be_book = () => Pleasure.Generator
                                          .Invent<Store>(dsl => dsl.Tuning(r => r.Category, CategoryOfType.Book))
                                          .CategoryAsAmazon
                                          .ShouldEqual("Books");

        It should_be_movie = () => Pleasure.Generator
                                           .Invent<Store>(dsl => dsl.Tuning(r => r.Category, CategoryOfType.Movie))
                                           .CategoryAsAmazon
                                           .ShouldEqual("DVD");

        It should_be_tv_show = () => Pleasure.Generator
                                             .Invent<Store>(dsl => dsl.Tuning(r => r.Category, CategoryOfType.TVShow))
                                             .CategoryAsAmazon
                                             .ShouldEqual("DVD");

        It should_be_tv_video_game = () => Pleasure.Generator
                                                   .Invent<Store>(dsl => dsl.Tuning(r => r.Category, CategoryOfType.VideoGame))
                                                   .CategoryAsAmazon
                                                   .ShouldEqual("VideoGames");
    }
}