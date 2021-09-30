using ectotec.Models;
using ectotec.Models.ViewModels;
using ectotec.Negocio.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ectotec.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactoController : ControllerBase
    {

        private readonly IContactoService _contactoService;

        public ContactoController(IContactoService contactoService)
        {
            _contactoService = contactoService;
        }

        [HttpPost("Contacto")]
        public async Task<IActionResult> PostContacto([FromBody] Contacto contacto)
        {

            var resultado = await _contactoService.CreateContact(contacto);

            return Ok(resultado);
        }

        [HttpPost]
        [Route("GetGeoName")]
        public async Task<ActionResult> GetGeoName([FromBody] RequestGeo text)
        {

            var viewModel = await _contactoService.GetConsulta(text.text);

            return Ok(viewModel);
        }

    }
}
