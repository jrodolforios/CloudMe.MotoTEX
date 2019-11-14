using CloudMe.ToDeTaxi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.ToDeTaxi.Domain.Model.Usuario
{
    public class UsuarioSummary
    {
        public Guid? Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public CredenciaisUsuario Credenciais { get; set; }
        public TipoUsuario Tipo { get; set; }
    }
}
