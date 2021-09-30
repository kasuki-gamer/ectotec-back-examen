using ectotec.Models.ViewModels;
using ectotec.Negocio.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace ectotec.Negocio.Implementations
{
    public class EmailSender: IEmailSender
    {
        private string host;
        private int port;
        private bool enableSSL;
        private string userName;
        private string password;
        private SmtpClient client = null;


        private IConfiguration _configuration;

        private IConfigurationSection _Settings;


        public EmailSender(IConfiguration configuration)
        {
            this.host = "smtp.gmail.com";
            this.port = 587;
            this.enableSSL = true;
            this.userName = "israel.pacheco.garcia@gmail.com";
            this.password = "240489Israel*";
            client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(userName, password),
                EnableSsl = enableSSL,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true
            };
            _configuration = configuration;
            _Settings = _configuration.GetSection("Settings");
        }


        public Task SendEmailAsync(string email, string subject, AlternateView alternateView, ConfigMail configMail = null)
        {
            if (configMail != null)
            {
                this.host = configMail.host;
                this.port = configMail.port;
                this.userName = configMail.user;
                this.password = configMail.pass;
                client = new SmtpClient(this.host, this.port)
                {
                    Credentials = new NetworkCredential(this.userName, this.password),
                    EnableSsl = enableSSL,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = true
                };
            }


            string em = _Settings.GetValue<string>("Email");


            MailMessage mail = new MailMessage(userName, em);
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.AlternateViews.Add(alternateView);
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.SubjectEncoding = System.Text.Encoding.Default;
            return client.SendMailAsync(
               mail
           );
        }



        public AlternateView MailRegistro(string RutaImgs, string Nombre, string email)
        {

            LinkedResource head = new LinkedResource(RutaImgs + @"\wwwroot\img\banner.jpg", "image/jpg");

            StringBuilder sb = new StringBuilder();

            sb.Append(" <!doctype html>                                                                                                                                                         ");
            sb.Append(" <html>                                                                                                                                                                  ");
            sb.Append(" <head>                                                                                                                                                                  ");
            sb.Append(" <meta charset='UTF-8'>                                                                                                                                                  ");
            sb.Append(" <title></title>                                                                                                                                              ");
            sb.Append(" <style>.univers {font-family:'Univers Next Pro',sans-serif;}</style>                                                                                                    ");
            sb.Append(" </head>                                                                                                                                                                 ");
            sb.Append("                                                                                                                                                                         ");
            sb.Append(" <body style='margin:0;'>                                                                                                                                                ");
            sb.Append(" <table width='600' border='0' align='center' cellpadding='0' cellspacing='0' class='univers'>                                                                           ");
            sb.Append(" <tr><td width='600'><img src='cid:" + head.ContentId + "' width='600' alt='HSBC' style='display:block; border:0px;'/></td></tr>  ");

            sb.Append(" <tr>                                                                                                                                                                    ");
            sb.Append(" <td height='15' style='color:#000000;font-size:15px;padding:12px;'>                                                                                                     ");

            sb.Append(" <strong style='color:#FF0109;font-size:20px;'>Estimado, "+Nombre+"</strong>                                                                                    ");

            sb.Append(" <br>                                                                                                                                                                    ");
            sb.Append(" <span data-ogsc='black'>Recuerda que podrás obtener                                                                                                                     ");
            sb.Append(" <strong>Hemos recibido sus datos y nos pondremos en contacto con usted en la brevedad posible. Enviaremos un correo con información a su cuenta: "+email+".  </span><br><br><br>                                                                                       ");
            sb.Append("                                                                                                                                                                         ");
            
            sb.Append("                                                                                                                                                                         ");
            sb.Append(" </table>                                                                                                                                                                ");
            sb.Append(" </body>                                                                                                                                                                 ");
            sb.Append(" </html>																																									");


            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(sb.ToString(), null, MediaTypeNames.Text.Html);

            alternateView.LinkedResources.Add(head);

            return alternateView;

        }

    }
}
