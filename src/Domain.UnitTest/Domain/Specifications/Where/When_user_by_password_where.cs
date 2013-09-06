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

    [Subject(typeof(UserByPasswordWhereSpec))]
    public class When_user_by_password_where
    {
        #region Establish value

        static IQueryable<User> fakeCollection;

        static List<User> filterCollection;

        #endregion

        Establish establish = () =>
                                  {
                                      Func<string, User> createEntity = (password) => Pleasure.MockAsObject<User>(mock => mock.SetupGet(r => r.Password).Returns(password));

                                      fakeCollection = Pleasure.ToQueryable(createEntity(Pleasure.Generator.String()), createEntity(Pleasure.Generator.TheSameString()));
                                  };

        Because of = () =>
                         {
                             filterCollection = fakeCollection
                                     .Where(new UserByPasswordWhereSpec(Pleasure.Generator.TheSameString()).IsSatisfiedBy())
                                     .ToList();
                         };

        It should_be_filter = () =>
                                  {
                                      filterCollection.Count.ShouldEqual(1);
                                      filterCollection[0].Password.ShouldBeTheSameString();
                                  };
    }
}