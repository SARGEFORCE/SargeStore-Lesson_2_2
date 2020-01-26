using Microsoft.AspNetCore.Identity;
using SargeStoreDomain.Entities.Identity;

namespace SargeStore.Interfaces.Services
{
    public interface IUsersClient : 
        IUserRoleStore<User>, 
        IUserPasswordStore<User>, 
        IUserEmailStore<User>, 
        IUserPhoneNumberStore<User>, 
        IUserClaimStore<User>, 
        IUserTwoFactorStore<User>, 
        IUserLockoutStore<User>,
        IUserLoginStore<User>
    {
    }
}
