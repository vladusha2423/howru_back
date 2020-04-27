using Howru.Core.EF;
using Howru.Data;
using Howru.Data.Converters;
using Howru.Data.Dto;
using Howru.Data.Entities;
using Howru.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Howru.Core.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly MyContext _context;


        public UserRepository(UserManager<User> um, MyContext context)
        {
            _userManager = um;
            _context = context;
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            return UserConverter.UserConvert
                (await _userManager.Users.ToListAsync());
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            return UserConverter.UserConvert(
                await _userManager.FindByIdAsync(id.ToString()));
        }

        public async Task<List<UserDto>> GetByStringAsync(string search)
        {
            if (search == "_")
            {
                return UserConverter.UserConvert(_userManager.Users.ToList());
            }
            else
            {
                return UserConverter.UserConvert(
                    await _userManager.Users.Where(a => a.UserName.ToLower().IndexOf(search) > -1 ||
                                                        a.Name.IndexOf(search) > -1 ||
                                                        a.Surname.IndexOf(search) > -1).ToListAsync());
            }
        }

        public async Task<UserDto> CreateAsync(UserDto item)
        {
            User user = UserConverter.UserConvert(item);
            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
                return null;
            var r = await _userManager.AddToRoleAsync(user, "user");
            if (!r.Succeeded)
                return null;
            return UserConverter.UserConvert(
                await _userManager.FindByEmailAsync(item.Email));
        }

        public async Task<bool> DeleteByEmailAsync(string email)
        {
            User user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return false;
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            User user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return false;
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }



        public async Task<UserDto> GetByNameAsync(string name)
        {
            return UserConverter.UserConvert(
                await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == name));
        }



        public async Task<bool> UpdateAsync(UserDto item)
        {
            User user = UserConverter.UserConvert(item);
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }



        public async Task<UserDto> GetByLoginAsync(string login)
        {
            return UserConverter.UserConvert(await _userManager.Users.FirstOrDefaultAsync(a => a.UserName == login));
        }

        public async Task<bool> DeleteByLoginAsync(string login)
        {
            User user = await _userManager.Users.FirstOrDefaultAsync(a => a.UserName == login);
            if (user == null)
                return false;
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }
        public async Task<List<UserDto>> GetFriendsAsync(string login)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == login);
            var friends = await _context.Friends.Where(u => u.UserId == user.Id).ToListAsync();
            List<UserDto> friendList = new List<UserDto>();
            foreach (var f in friends)
                friendList.Add(UserConverter.UserConvert(await _userManager.FindByIdAsync(f.FriendId.ToString())));
            return friendList;
        }

        public async Task<bool> AddFriendAsync(string login, Guid friendId)
        {
            var userId = _userManager.Users.FirstOrDefault(u => u.UserName == login).Id;
            await _context.Friends.AddAsync(new Friend
            {
                FriendId = friendId,
                UserId = userId
            });
            await _context.SaveChangesAsync();
            return true;

        }
    }
}
