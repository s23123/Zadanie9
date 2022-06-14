using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication1.Help;
using WebApplication1.Models;
using WebApplication1.Models.DTO;

namespace WebApplication1.Services
{
    public class AccountsDbService : IAccountsDbService
    {
        private readonly IConfiguration _config;
        private readonly MainDbContext _mainDbcontext;

        public AccountsDbService(IConfiguration config, MainDbContext context)
        {
            _config = config;
            _mainDbcontext = context;
        }

        public async Task<Login> Login(SomeSortOfUser user)
        {
            var xuser = await _mainDbcontext.User.FirstOrDefaultAsync(e => e.Login == user.Login);

            if(xuser == null)
            {
                throw new System.Exception($"Nie znaleziono uzytkownika o loginie {xuser.Login}");
            }

            if(xuser.Password != Security.HashedSaltedPasswd(user.Password, xuser.Salt))
            {
                throw new System.Exception($"Zle haslo");
            }

            var token = PobierzToken();

            xuser.RefreshToken = Guid.NewGuid().ToString();
            xuser.RefreshTokenExpiration = DateTime.Now.AddHours(12);


            await _mainDbcontext.SaveChangesAsync();


            return new Login(new JwtSecurityTokenHandler().WriteToken(token).ToString(), xuser.RefreshToken);
        }

        public async Task<Login> RefreshToken(SomeSortOfToken someSortOfToken)
        {
            var xuser = await _mainDbcontext.User.FirstOrDefaultAsync(e => e.RefreshToken == someSortOfToken.RefreshToken);

            if(xuser == null)
            {
                throw new SystemException($"Nie znaleziono uzytkownika");
            }

            if(xuser.RefreshTokenExpiration < DateTime.Now)
            {
                throw new SystemException($"Token przeterminowany");
            }

            
            
            
            var token = PobierzToken();

            xuser.RefreshToken = Guid.NewGuid().ToString();
            xuser.RefreshTokenExpiration = DateTime.Now.AddHours(12);

            
            await _mainDbcontext.SaveChangesAsync();

            return new Login(new JwtSecurityTokenHandler().WriteToken(token).ToString(), xuser.RefreshToken);
        }

        public async Task Register(SomeSortOfUser user)
        {
            if(user.Password.Length < 6)
            {
                 throw new SystemException($"Haslo za krotkie");
               
            }

            var xuser = await _mainDbcontext.User.FirstOrDefaultAsync(e => e.Login == user.Login);
            if(xuser != null)
            {
                throw new SystemException($"Uzytkownik juz istnieje");

               
            }

            var hashedSalt = Security.HashedPasswordSalt(user.Password);

            var x = new User
            {
                Login = user.Login,
                Password = hashedSalt.Item1,
                Salt = hashedSalt.Item2,
                RefreshToken = Guid.NewGuid().ToString(),
                RefreshTokenExpiration = DateTime.Now.AddHours(12)
            };

            await _mainDbcontext.User.AddAsync(x);
            await _mainDbcontext.SaveChangesAsync();

           
        }

        public JwtSecurityToken PobierzToken()
        {
            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.Role, "user"),
                new Claim(ClaimTypes.Role,"client")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["klucz"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer:"http://localhost",
                audience:"http://localhost",
                claims: claims,
                expires:DateTime.Now.AddMinutes(10),
                signingCredentials:credentials);


            return token;
        }
    }
}
