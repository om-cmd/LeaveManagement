﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Interface.IRepo
{
    public interface  IEmailRepo
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
