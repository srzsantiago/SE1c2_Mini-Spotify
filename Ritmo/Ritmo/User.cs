﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    class User : Person
    {
        public User(string email, string password, AccessLevel access) : base(email, password, access)
        {
        }
    }
}