using Howru.Data.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Howru.Data.Repositories
{
    public interface IUserRepository
    {
        Task<List<UserDto>> GetAllAsync();

        Task<UserDto> GetByIdAsync(Guid id);

        Task<List<UserDto>> GetByStringAsync(string search);

        Task<UserDto> CreateAsync(UserDto item);

        Task<UserDto> GetByNameAsync(string name);

        Task<bool> UpdateAsync(UserDto item);

        Task<bool> DeleteByIdAsync(Guid id);

        Task<bool> DeleteByLoginAsync(string login);

        Task<List<UserDto>> GetFriendsAsync(string login);

        Task<bool> AddFriendAsync(string login, Guid friendId);
    }
}
