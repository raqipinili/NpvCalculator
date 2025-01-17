﻿using System.Collections.Generic;

namespace Security.Core.Classes
{
    public class Register
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public IEnumerable<int> Permissions { get; set; }
    }
}
