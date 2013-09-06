namespace Browsio.UnitTest.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    [Subject(typeof(UserByLoginWhereSpec))]
    public class When_user_by_login_where
    {
        #region Establish value

        static IQueryable<User> fakeCollection;

        static List<User> filterCollection;

        #endregion

        Establish establish = () =>
                                  {
                                      
                                      Func<string,User> createEntity = (login) => Pleasure.MockAsObject<User>(mock => mock.SetupGet(r=>r.Login).Returns(login));

                                      fakeCollection = Pleasure.ToQueryable(createEntity(Pleasure.Generator.String()), createEntity(Pleasure.Generator.TheSameString()));
                                  };

        Because of = () =>
                         {
                             filterCollection = fakeCollection
                                     .Where(new UserByLoginWhereSpec(Pleasure.Generator.TheSameString()).IsSatisfiedBy())
                                     .ToList();
                         };

        It should_be_filter = () =>
                                  {
                                      filterCollection.Count.ShouldEqual(1);
                                      filterCollection[0].Login.ShouldBeTheSameString();
                                  };
    }
}