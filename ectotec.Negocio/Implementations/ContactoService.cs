using ectotec.AccesoDatos.Data;
using ectotec.Models;
using ectotec.Models.ViewModels;
using ectotec.Negocio.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace ectotec.Negocio.Implementations
{
    public class ContactoService : IContactoService
    {
        private readonly ApplicationDbContext _context;
        public readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly string Ruta;
        private readonly IEmailSender _emailSender;
        private readonly IGeoNameService _geoNameService;

        public ContactoService(
            ApplicationDbContext context,
            IConfiguration configuration,
            IWebHostEnvironment env,
            IEmailSender emailSender,
            IGeoNameService geoNameService)
        {
            _configuration = configuration;
            _context = context;
            _env = env;
            Ruta = Path.Combine(_env.ContentRootPath);
            _emailSender = emailSender;
            _geoNameService = geoNameService;
        }


        public async Task<ContactoViewModel> CreateContact(Contacto model)
        {
            var resp = new ContactoViewModel();
            try
            {
                // _context.Contacto.Add(model);
                // await _context.SaveChangesAsync();

                var email = _emailSender.MailRegistro(Ruta, model.Nombre, model.Email);

                await _emailSender.SendEmailAsync(model.Email, "Registro", email);


                resp.done = true;
                resp.msj = "Alta Exitosa";
            }
            catch
            {
                resp.done = false;
                resp.msj = "Ocurrio un error";
            }

            return resp;

        }


        public async Task<List<string>> GetConsulta(string text)
        {
            var r = await _geoNameService.GetConsulta(text);

            return r.geonames.Select(s => s.name).ToList();

        }

    }
}
