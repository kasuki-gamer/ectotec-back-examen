using ectotec.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ectotec.Negocio.Interfaces
{
    public interface IGeoNameService
    {
        Task<GeoNameResponse> GetConsulta(string text);
    }
}
