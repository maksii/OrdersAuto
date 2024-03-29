﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EYM.Repositories.Interfaces;

namespace EYM.Entities
{
	public class Provider : IEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public string Name { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string ImagePath { get; set; }
		public string Description { get; set; }
		public string AdditionalInfo { get; set; }
		public string Type { get; set; }

		public virtual List<ProductTemplate> ProductTemplates { get; set; }

		#region Constructors

		public Provider()
		{
			
		}

		public Provider(string name, string phone, string email, 
			string imagePath, string description, string additionalinfo, string type)
		{
			Name = name;
			Phone = phone;
			Email = email;
			ImagePath = imagePath;
			Description = description;
			AdditionalInfo = additionalinfo;
			Type = type;
		}
		#endregion
	}
}
