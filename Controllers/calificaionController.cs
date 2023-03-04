using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2020VR650.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2020VR650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class calificaionController : ControllerBase
    {
        private readonly UsuarioContext _usuarioContext;


        public calificaionController(UsuarioContext equipoContext)
        {
            _usuarioContext = equipoContext;
        }


        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {

            List<calificaciones> listadoEquipos = _usuarioContext.calificaciones.ToList();

            if (listadoEquipos.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoEquipos);

        }
        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarCALI([FromBody] calificaciones calificaciones)
        {
            try
            {

                _usuarioContext.calificaciones.Add(calificaciones);
                _usuarioContext.SaveChanges();
                return Ok(calificaciones);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult actualizar(int id, [FromBody] calificaciones caliomod)
        {

            calificaciones? existente = (from e in _usuarioContext.calificaciones where e.calificacionId == id select e).FirstOrDefault();

            if (existente == null)
            {
                return NotFound();
            }

            existente.calificacionId = caliomod.calificacionId;
            existente.publicacionId = caliomod.publicacionId;
            existente.usuarioId = caliomod.usuarioId;
            existente.calificacion = caliomod.calificacion;

            _usuarioContext.Entry(existente).State = EntityState.Modified;
            _usuarioContext.SaveChanges();

            return Ok(existente);


        }
        [HttpPut]
        [Route("delete/{id}")]
        public IActionResult EliminarCali(int id)
        {
            calificaciones? existente = (from e in _usuarioContext.calificaciones
                                  where e.calificacionId == id
                                  select e).FirstOrDefault();

            if (existente == null)
            {
                return NotFound();

            }


            _usuarioContext.calificaciones.Attach(existente);
            _usuarioContext.calificaciones.Remove(existente);
            //_usuarioContext.Entry(existente).State = EntityState.Modified;
            _usuarioContext.SaveChanges();


            return Ok(existente);

        }

        [HttpGet]
        [Route("Buscar_por_publicacion")]
        public IActionResult FindByROll(int filtro)
        {
            calificaciones? equipo = (from e in _usuarioContext.calificaciones
                               where e.publicacionId == filtro
                               select e).FirstOrDefault();

            if (equipo == null)
            {
                return NotFound();

            }


            return Ok(equipo);

        }

    }
}
