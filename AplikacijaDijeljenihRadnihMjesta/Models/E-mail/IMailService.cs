using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Models.ViewModel
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);

    }
}
