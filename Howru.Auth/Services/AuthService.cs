using Howru.Auth.Interfaces;
using Howru.Data;
using Howru.Data.Converters;
using Howru.Data.Dto;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Howru.Auth.Services
{
    public class AuthService: IAuthService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IJwtGenerator _jwt;

        public AuthService(SignInManager<User> sim, UserManager<User> um, IJwtGenerator jwt)
        {
            _signInManager = sim;
            _userManager = um;
            _jwt = jwt;
        }

        public async Task<object> Login(string username, string password)
        {
            if (username == null || password == null)
                return null;

            var result = await _signInManager.PasswordSignInAsync(username, password, false, false);

            if (result.Succeeded)
            {
                var appUser = await _userManager.Users.FirstOrDefaultAsync(a => a.UserName == username);
                return await _jwt.GenerateJwt(appUser);
            }
            return null;
        }

        public async Task<object> Register(UserDto item)
        {
            User user = UserConverter.UserConvert(item);
            var result = await _userManager.CreateAsync(user, item.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                await _userManager.AddToRoleAsync(user, "user");
                return await _jwt.GenerateJwt(user);
            }

            return null;
        }
    }
}
