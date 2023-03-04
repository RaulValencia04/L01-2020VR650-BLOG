using System;
using Microsoft.EntityFrameworkCore;
using L01_2020VR650.Models;

namespace L01_2020VR650.Models
{
	public class UsuarioContext : DbContext
	{
		public UsuarioContext(DbContextOptions<UsuarioContext> options) : base(options)
		{

		}

		public DbSet<usuario> usuarios { get; set; }
        public DbSet<calificaciones> calificaciones { get; set; }
		public DbSet<comentarios> comentarios { get; set; }

    }
}

