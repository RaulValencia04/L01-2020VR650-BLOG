using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2020VR650.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2020VR650

{
    [Route("api/[controller]")]
    [ApiController]
    public class usuarioController : ControllerBase
    {
        private readonly UsuarioContext _usuarioContext;


        public usuarioController(UsuarioContext equipoContext)
        {
            _usuarioContext = equipoContext;
        }


        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {

            List<usuario> listadoEquipos = _usuarioContext.usuarios.ToList();

            if (listadoEquipos.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoEquipos);

        }
        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarEquipo([FromBody] usuario usuario)
        {
            try
            {

                _usuarioContext.usuarios.Add(usuario);
                _usuarioContext.SaveChanges();
                return Ok(usuario);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult actualizar(int id, [FromBody] usuario usuariomod)
        {

            usuario? existente = (from e in _usuarioContext.usuarios where e.usuarioId == id  select e).FirstOrDefault();

            if (existente == null)
            {
                return NotFound();
            }

            existente.nombreusuario = usuariomod.nombreusuario;
            existente.nombre = usuariomod.nombre;
            existente.apellido = usuariomod.apellido;

            _usuarioContext.Entry(existente).State = EntityState.Modified;
            _usuarioContext.SaveChanges();

            return Ok(existente);


        }


        [HttpPut]
        [Route("delete/{id}")]
        public IActionResult EliminarUsuario(int id)
        {
            usuario? existente = (from e in _usuarioContext.usuarios
                                  where e.usuarioId == id
                                  select e).FirstOrDefault();

            if (existente == null)
            {
                return NotFound();

            }


            _usuarioContext.usuarios.Attach(existente);
            _usuarioContext.usuarios.Remove(existente);
            //_usuarioContext.Entry(existente).State = EntityState.Modified;
            _usuarioContext.SaveChanges();


            return Ok(existente);

        }

        [HttpGet]
        [Route("Buscar_por_Nombre")]
        public IActionResult FindBynombre(string filtro)
        {
            usuario? equipo = (from e in _usuarioContext.usuarios
                              where (e.nombre.Contains(filtro))
                              select e).FirstOrDefault();

            if (equipo == null)
            {
                return NotFound();

            }


            return Ok(equipo);

        }
        [HttpGet]
        [Route("Buscar_por_Apellido")]
        public IActionResult FindByApellido(string filtro)
        {
            usuario? equipo = (from e in _usuarioContext.usuarios
                               where (e.apellido.Contains(filtro))
                               select e).FirstOrDefault();

            if (equipo == null)
            {
                return NotFound();

            }


            return Ok(equipo);

        }


        [HttpGet]
        [Route("Buscar_por_Rolll")]
        public IActionResult FindByROll(int filtro)
        {
            usuario? equipo = (from e in _usuarioContext.usuarios
                               where e.rolId == filtro
                               select e).FirstOrDefault();

            if (equipo == null)
            {
                return NotFound();

            }


            return Ok(equipo);

        }

    }
}
