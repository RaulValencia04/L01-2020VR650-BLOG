﻿using System;
using System.ComponentModel.DataAnnotations;
namespace L01_2020VR650.Models
{
	public class usuario
	{

            [Key]

            public int usuarioId { get; set; }

            public int? rolId { get; set; }

            public string nombreusuario { get; set; }

            public string clave { get; set; }

            public string nombre { get; set; }

            public string apellido { get; set; }

		

	}


    
}

