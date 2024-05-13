using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {//Logic to send email
            return Task.CompletedTask;
        }
    }
}
/*
 why are we seeing this !EmailSender error ?

The reason is in register we modified the create user 
6:10 video 118
 */