namespace Browsio.UnitTest
{
    #region << Using >>

    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(User))]
    public class When_user_full_name
    {
        #region Establish value

        static User original;

        #endregion

        Establish establish = () =>
                                  {
                                      original = Pleasure.Generator.Invent<User>(dsl => dsl.Tuning(r => r.FirstName, "Vlad")
                                                                                           .Tuning(r => r.LastName, "Kopachinsky"));
                                  };

        It should_be_equal = () => original.FullName.ShouldEqual("Vlad Kopachinsky");
    }
}