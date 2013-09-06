namespace Browsio.Domain
{
    using System.Collections.Generic;

    public interface ISocial
    {
        string Id { get; }
    }

    public interface ISocialUser : ISocial
    {
        string FirstName { get; }

        string LastName { get; }

        SexOfType Gender { get; }

        string EMail { get; }

        IList<ISocial> Friends { get; }

    }
}