namespace Browsio.UnitTest.Domain
{
    #region << Using >>

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(UserByResetTokenWhereSpec))]
    public class When_user_by_reset_token_where
    {
        #region Establish value

        static IQueryable<User> fakeCollection;

        static List<User> filterCollection;

        #endregion

        Establish establish = () =>
                                  {
                                      Func<string, User> createEntity = (token) => Pleasure.MockAsObject<User>(mock => mock.SetupGet(r => r.ResetToken).Returns(token));

                                      fakeCollection = Pleasure.ToQueryable(createEntity(Pleasure.Generator.String()), createEntity(Pleasure.Generator.TheSameString()));
                                  };

        Because of = () =>
                         {
                             filterCollection = fakeCollection
                                     .Where(new UserByResetTokenWhereSpec(Pleasure.Generator.TheSameString()).IsSatisfiedBy())
                                     .ToList();
                         };

        It should_be_filter = () =>
                                  {
                                      filterCollection.Count.ShouldEqual(1);
                                      filterCollection[0].ResetToken.ShouldBeTheSameString();
                                  };
    }
}