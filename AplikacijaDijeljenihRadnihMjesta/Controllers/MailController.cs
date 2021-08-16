using AplikacijaDijeljenihRadnihMjesta.Models.ViewModel;
//using MailKit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Controllers
{
   
        [Route("api/[controller]")]
        [ApiController]
        public class MailController : ControllerBase 
        {
            private readonly IMailService mailServices;
            public MailController(IMailService mailService)
            {
                this.mailServices = mailService;
            }
            [HttpPost("send")]
            public async Task<IActionResult> SendMail([FromForm] MailRequest request)
            {
                try
                {
                    await mailServices.SendEmailAsync(request);
                    return Ok();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
   
}
