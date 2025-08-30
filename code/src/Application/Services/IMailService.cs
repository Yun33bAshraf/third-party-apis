using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using OpenProfileAPI.Application.Common.Interfaces;

namespace OpenProfileAPI.Application.Services;

public interface IMailService
{
    Task SendEmailAsync(string email, string subject, string message);
}
