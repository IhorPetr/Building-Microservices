﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatlerWaldorfCorp.TeamService.Models
{
    public class Member
    {
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Member()
        {
            
        }

        public Member(Guid id) : this()
        {
            this.ID = id;
        }

        public Member(string firstName, string lastName, Guid id) : this(id)
        {
            this.FirstName = firstName; this.LastName = lastName;
        }
        public override string ToString() => this.LastName; 
    }
}
