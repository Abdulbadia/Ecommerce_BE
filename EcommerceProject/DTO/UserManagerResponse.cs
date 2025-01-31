﻿using System;
using System.Collections.Generic;

namespace EcommerceProject.ViewModels
{
    public class UserManagerResponse
    {

        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}
