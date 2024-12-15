using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmprMvcApp.EntityLayer.Entities
{
    internal class Company
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string? StreetAddress { get; set; } //açık adres
        public string? City { get; set; } //şehir info
        public string? State { get; set; } //ilçe info
        public string? PostalCode { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
