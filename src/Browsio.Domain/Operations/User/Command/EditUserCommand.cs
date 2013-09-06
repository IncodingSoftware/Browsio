namespace Browsio.Domain
{
    #region << Using >>

    using System.Web;
    using Incoding.CQRS;
    using Incoding.Maybe;
    using Incoding.MvcContrib;

    #endregion

    public class EditUserCommand : CommandBase
    {
        #region Constructors

        public EditUserCommand() { }

        public EditUserCommand(User user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Sex = user.Sex;
            HasImage = user.Image.If(r => r.Length > 0).Has();
        }

        #endregion

        #region Properties

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public SexOfType Sex { get; set; }

        public HttpPostedFileBase DisplayPicture { get; set; }

        public bool HasImage { get; set; }

        #endregion

        public override void Execute()
        {
            var user = Repository.GetById<User>(BrowsioApp.UserId);
            user.FirstName = FirstName;
            user.LastName = LastName;
            user.Sex = Sex;
            if (DisplayPicture != null)
                user.Image = new HttpMemoryPostedFile(DisplayPicture).ContentAsBytes;

            EventBroker.Publish(new OnChangeSearchItemEvent(user));
        }
    }
}