﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SmprMvcApp.EntityLayer.Entities
{
    public class AppUser : IdentityUser // kullanıcı tüm identity ayarlarına sahip olacak. IdentityUser'ı kullanabilmek için Microsoft.Extensions.Identity.Stores kütüphanesini projemize dahil etmemiz gerekir.
    {
        [Required]
        public string Name { get; set; }

        public string? StreetAddress { get; set; } //açık adres
        public string? City { get; set; } //şehir info
        public string? State { get; set; } //ilçe info
        public string? PostalCode { get; set; }
    }
}