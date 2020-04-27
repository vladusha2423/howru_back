using Howru.Data.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Howru.Data.Converters
{
    public class UserConverter
    {
        public static UserDto  UserConvert(User item)
        {
            return new UserDto
            {
                Email = item.Email,
                UserName = item.UserName,
                Name = item.Name,
                Surname = item.Surname,
                Photo = item.Photo,
                Id = item.Id
            };
        }
        public static User UserConvert(UserDto item)
        {
            return new User
            {
                Name = item.Name,
                Surname = item.Surname,
                Photo = item.Photo,
                Id = item.Id,
                UserName = item.UserName,
                Email = item.Email
            };
        }
        public static List<UserDto> UserConvert(List<User> items)
        {
            return items.Select(a => UserConvert(a)).ToList();
        }
        public static List<User> UserConvert(List<UserDto> items)
        {
            return items.Select(a => UserConvert(a)).ToList();
        }
    }
}
