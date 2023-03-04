using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using L01_2020VR650.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2020VR650.Models;
using Microsoft.EntityFrameworkCore;


namespace L01_2020VR650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class comenntarioController : ControllerBase
    {
        private readonly UsuarioContext _usuarioContext;


        public comenntarioController(UsuarioContext comentarioContext)
        {
            _usuarioContext = comentarioContext;
        }


        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {

            List<comentarios> listadoEquipos = _usuarioContext.comentarios.ToList();

            if (listadoEquipos.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoEquipos);

        }
        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarComentario([FromBody] comentarios comentarios)
        {
            try
            {

                _usuarioContext.comentarios.Add(comentarios);
                _usuarioContext.SaveChanges();
                return Ok(comentarios);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult actualizar(int id, [FromBody] comentarios comentmod)
        {

            comentarios? existente = (from e in _usuarioContext.comentarios where e.cometarioId == id select e).FirstOrDefault();

            if (existente == null)
            {
                return NotFound();
            }

            existente.comentario = comentmod.comentario;
            //existente.cometarioId = comentmod.cometarioId;
            existente.publicacionId = comentmod.publicacionId;
            existente.usuarioId = comentmod.usuarioId;

            _usuarioContext.Entry(existente).State = EntityState.Modified;
            _usuarioContext.SaveChanges();

            return Ok(existente);


        }
        [HttpPut]
        [Route("delete/{id}")]
        public IActionResult Eliminarcomen(int id)
        {
            comentarios? existente = (from e in _usuarioContext.comentarios
                                         where e.cometarioId == id
                                         select e).FirstOrDefault();

            if (existente == null)
            {
                return NotFound();

            }


            _usuarioContext.comentarios.Attach(existente);
            _usuarioContext.comentarios.Remove(existente);
            //_usuarioContext.Entry(existente).State = EntityState.Modified;
            _usuarioContext.SaveChanges();


            return Ok(existente);

        }
        [HttpGet]
        [Route("Buscar_por_usuario")]
        public IActionResult FindByROll(int filtro)
        {
            comentarios? comen = (from e in _usuarioContext.comentarios
                                      where e.usuarioId == filtro
                                      select e).FirstOrDefault();

            if (comen == null)
            {
                return NotFound();

            }


            return Ok(comen);

        }
    }
}
