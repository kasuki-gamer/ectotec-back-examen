using ectotec.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ectotec.Negocio.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, AlternateView alternateView, ConfigMail configMail = null);
        AlternateView MailRegistro(string RutaImgs, string Nombre, string email);
    }
}
