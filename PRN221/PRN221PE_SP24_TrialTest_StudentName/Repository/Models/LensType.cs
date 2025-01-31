﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repository.Models
{
    [Table("LensType")]
    public partial class LensType
    {
        public LensType()
        {
            Eyeglasses = new HashSet<Eyeglass>();
        }

        [Key]
        [StringLength(30)]
        public string LensTypeId { get; set; }
        [Required]
        [StringLength(100)]
        public string LensTypeName { get; set; }
        [Required]
        [StringLength(250)]
        public string LensTypeDescription { get; set; }
        public bool? IsPrescription { get; set; }

        [InverseProperty("LensType")]
        public virtual ICollection<Eyeglass> Eyeglasses { get; set; }
    }
}