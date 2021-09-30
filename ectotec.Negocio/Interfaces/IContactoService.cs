using ectotec.Models;
using ectotec.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ectotec.Negocio.Interfaces
{
    public interface IContactoService
    {
        Task<ContactoViewModel> CreateContact(Contacto model);
        Task<List<string>> GetConsulta(string text);
    }
}
