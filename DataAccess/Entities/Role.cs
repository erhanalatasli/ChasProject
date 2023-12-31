﻿#nullable disable

using Core.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Role : Record
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}
