using Howru.Data.Dto;
using Howru.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Howru.Data
{
    public class User : IdentityUser<Guid>
    {
        public string Photo { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        
    }
}
