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

    [Subject(typeof(UserByFBIdWhereSpec))]
    public class When_user_by_fb_id
    {
        #region Establish value

        static IQueryable<User> fakeCollection;

        static List<User> filterCollection;

        #endregion

        Establish establish = () =>
                                  {
                                      Func<string, User> createEntity = (fbId) => Pleasure.MockAsObject<User>(mock => mock.SetupGet(r => r.FbId).Returns(fbId));

                                      fakeCollection = Pleasure.ToQueryable(createEntity(Pleasure.Generator.TheSameString()), createEntity(Pleasure.Generator.String()));
                                  };

        Because of = () =>
                         {
                             filterCollection = fakeCollection
                                     .Where(new UserByFBIdWhereSpec(Pleasure.Generator.TheSameString()).IsSatisfiedBy())
                                     .ToList();
                         };

        It should_be_filter = () =>
                                  {
                                      filterCollection.Count.ShouldEqual(1);
                                      filterCollection[0].FbId.ShouldBeTheSameString();
                                  };
    }
}