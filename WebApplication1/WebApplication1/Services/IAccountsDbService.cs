using System.Threading.Tasks;
using WebApplication1.Help;
using WebApplication1.Models.DTO;

namespace WebApplication1.Services
{
    public interface IAccountsDbService
    {
        public Task Register(SomeSortOfUser user);
        public Task<Login> Login(SomeSortOfUser user);
        public Task<Login> RefreshToken(SomeSortOfToken someSortOfToken);
    }
}
