namespace Browsio.Domain
{
    using Incoding.CQRS;

    public class ConfirUserCommand : CommandBase
    {
        public string UserId { get; set; }

        public override void Execute()
        {
            User user = this.Repository.GetById<User>(this.UserId);
            user.Activated = true;
        }
    }
}
