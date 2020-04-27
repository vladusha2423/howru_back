using System;
using System.Collections.Generic;
using System.Text;

namespace Howru.Data.Entities
{
    public class Friend
    {
        public Guid UserId { get; set; }
        public Guid FriendId { get; set; }
    }
}
