namespace Browsio.Domain
{
    using Incoding.CQRS;

    public class IsUserSocialQuery : QueryBase<IncStructureResponse<bool>>
    {
        protected override IncStructureResponse<bool> ExecuteResult()
        {
            var user = this.Repository.GetById<User>(BrowsioApp.UserId);
            return user == null ? new IncStructureResponse<bool>(true) : new IncStructureResponse<bool>(user.FbId == null);
        }
    }
}