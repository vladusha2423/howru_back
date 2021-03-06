﻿using Howru.Data.Dto;
using Howru.Data.Repositories;
using HowruSocialNetwork.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Howru.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _repo;

        public UserController(IUserRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> Get()
        {
            try
            {
                return Ok(await _repo.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        
        [HttpGet("myself")]
        public async Task<ActionResult<UserDto>> GetMyself()
        {
            try
            {
                string sender = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                return Ok(await _repo.GetByNameAsync(sender));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        
        [HttpGet("name/{sender}")]
        public async Task<ActionResult<UserDto>> GetByName(string sender)
        {
            try
            {
                return Ok(await _repo.GetByNameAsync(sender));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> Get(Guid id)
        {
            try
            {
                UserDto user = await _repo.GetByIdAsync(id);
                if (user == null)
                    return NotFound();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpGet("search/{search}")]
        public async Task<ActionResult<List<UserDto>>> Get(string search)
        {
            try
            {
                List<UserDto> users = await _repo.GetByStringAsync(search);
                if (users == null)
                    return NotFound();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Post([FromBody]UserDto item)
        {
            try
            {
                UserDto user = await _repo.CreateAsync(item);
                if (user == null)
                    return BadRequest();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut]
        public async Task<ActionResult<bool>> Put([FromBody]UserDto item)
        {
            try
            {
                return await _repo.UpdateAsync(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            try
            {
                return await _repo.DeleteByIdAsync(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("email/{email}")]
        public async Task<ActionResult<bool>> Delete(string login)
        {
            try
            {
                return await _repo.DeleteByLoginAsync(login);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpGet("friends")]
        public async Task<ActionResult<List<UserDto>>> GetFriends()
        {
            try
            {
                string username = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                return await _repo.GetFriendsAsync(username);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("friends/{id}")]
        public async Task<ActionResult> AddFriend(Guid id)
        {
            try
            {
                string username = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                return Ok(await _repo.AddFriendAsync(username, id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
