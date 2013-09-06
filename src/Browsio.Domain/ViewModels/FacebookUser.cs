namespace Browsio.Domain
{
    using System.Collections.Generic;

    public class FacebookUser : ViewModel.User ,ISocialUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public SexOfType Gender { get; set; }

        public string EMail { get; set; }

        public IList<ISocial> Friends { get; set; }
    }
}