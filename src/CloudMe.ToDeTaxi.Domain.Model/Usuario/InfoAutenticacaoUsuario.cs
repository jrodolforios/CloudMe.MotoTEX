using System;
using System.Collections.Generic;
using CloudMe.ToDeTaxi.Domain.Enums;

namespace CloudMe.ToDeTaxi.Domain.Model.Usuario
{
    public class InfoAutenticacaoUsuario
    {
        public string email { get; set; }
        public string password {get; set;}
        public string fullName {get; set;}
        public string confirmPassword {get; set;}
    }
}
