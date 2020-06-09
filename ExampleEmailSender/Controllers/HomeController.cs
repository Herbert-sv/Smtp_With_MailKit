using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using ExampleEmailSender.Models;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace ExampleEmailSender.Controllers
{
    public class HomeController : Controller
    {
              
        string _UserName;
        string _UserId;
        string _UserPassword;
       
        [HttpGet]
        public IActionResult MainPage()
        {
         
            return View();

        }
        [HttpPost]
        public IActionResult MainPage(string UserName,string UserId, string UserPassword)
        {
            this._UserName = UserName;
            this._UserId = UserId;
            this._UserPassword = UserPassword;
            
            if(UserId==null && UserPassword==null)
            {
                ViewBag.EmailSettings = "Boxes must be filled !";
                return View();
            }
            else
            {
                ViewData["UserName"] = _UserName;
                ViewData["UserId"] = _UserId;
                ViewData["Password"] = _UserPassword;
                return View("Index");
            }

                
            
        }
        [Route("home/_")]
        [HttpGet]
        public IActionResult Index()
        {
            
            return View();
        }
        [HttpPost] 
        public IActionResult Indexx(string To,string From, string Password, string Subject, string Body)
        {
            this._UserId = From;
            this._UserPassword = Password;
            try
            {
                SmtpClient SmtpHost = new SmtpClient();
                SmtpHost.Connect("smtp.live.com",25,false);
                SmtpHost.Authenticate(_UserId, _UserPassword);
                MimeMessage msg = new MimeMessage();
                msg.From.Add(new MailboxAddress(_UserId));
                msg.To.Add(new MailboxAddress(To));
                msg.Subject = Subject;
                var EmailBody = new BodyBuilder();
                EmailBody.TextBody = Body;
                msg.Body = EmailBody.ToMessageBody();
                SmtpHost.Send(msg);
                SmtpHost.Disconnect(true);
                ViewBag.successful = "Message has been delivered " + DateTime.Now;
                ModelState.Clear();


            }
            
            catch(ServiceNotConnectedException error )
            {
                ViewBag.Error= error.Message;
            }
            
            return View("Index");
        }

             
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
