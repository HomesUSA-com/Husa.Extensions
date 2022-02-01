namespace Husa.Extensions.Authorization
{
    public interface IUserProvider
    {
        void SetCurrentUser(IUserContext user);
    }
}
