using System;
using System.Collections.Generic;
using System.Text;

namespace Howru.Data.Entities
{
    public class Message
    {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public Guid RecipientId { get; set; }
        public string Type { get; set; }
        public DateTime DateTime { get; set; }
        public string Text { get; set; }
    }
}
