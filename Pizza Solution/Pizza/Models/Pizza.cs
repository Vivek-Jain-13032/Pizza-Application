﻿using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Pizza.Models
{
    public class Pizza
    {
        [Key]
        [Required]
        [BsonId]
        public string Pizza_Id { get; set; }

        [Required]
        public string Pizza_Name { get; set; }
    }
}
