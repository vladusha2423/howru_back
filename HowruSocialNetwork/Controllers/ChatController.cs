using Howru.Core.Hubs;
using Howru.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Abc.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ChatController: Controller
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatController (IHubContext<ChatHub> _hc)
        {
            _hubContext = _hc;
        }

        [Authorize]
        public async Task<ActionResult<Message>> Send([FromBody]Message message)
        {
            try
            {
                message.DateTime = DateTime.Now;
                string sender = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                await _hubContext.Clients.All.SendAsync("Send", 
                    sender, 
                    message.Text);
                return message;
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

    }
}
