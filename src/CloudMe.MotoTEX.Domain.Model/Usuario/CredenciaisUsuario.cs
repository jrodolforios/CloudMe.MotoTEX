using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Domain.Model.Usuario
{
    public class CredenciaisUsuario
    {
        public string Login { get; set; }
        public string SenhaAnterior { get; set; }
        public string Senha { get; set; }
        public string ConfirmarSenha { get; set; }
    }
}
