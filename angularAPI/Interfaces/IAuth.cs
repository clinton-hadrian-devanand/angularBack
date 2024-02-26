using angularAPI.Models;
using System.Data;

namespace angularAPI.Interfaces
{
    public interface IAuth
    {
        DataTable Authenticate(AuthModel auth);

        DataTable RegisterUser(AuthModel auth);


    }
}
