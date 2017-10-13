﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZF.DTO
{
    public class UserDTO:BaseDTO
    {
        public string PhoneNum { get; set; }
       // public string PasswordHash { get; set; }
       // public string PasswordSalt { get; set; }
        public int LoginErrorTimes { get; set; }
        public DateTime? LastLoginErrorDateTime { get; set; }
        public long? CityId { get; set; }
    }
}
