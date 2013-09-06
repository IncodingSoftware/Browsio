
namespace Browsio.Domain
{
    public interface IFormAuthentication
    {
        void SignIn(string id, bool persistence);

        void SignOut();

        string GetUserData();

        bool IsAuthentication();
    }
}
