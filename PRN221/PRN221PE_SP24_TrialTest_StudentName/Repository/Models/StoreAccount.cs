﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repository.Models
{
    [Table("StoreAccount")]
    [Index("EmailAddress", Name = "UQ__StoreAcc__49A147408E5A40CD", IsUnique = true)]
    public partial class StoreAccount
    {
        [Key]
        [Column("AccountID")]
        public int AccountId { get; set; }
        [Required]
        [StringLength(40)]
        public string AccountPassword { get; set; }
        [Required]
        [StringLength(60)]
        public string FullName { get; set; }
        [StringLength(60)]
        public string EmailAddress { get; set; }
        public int? Role { get; set; }
    }
}