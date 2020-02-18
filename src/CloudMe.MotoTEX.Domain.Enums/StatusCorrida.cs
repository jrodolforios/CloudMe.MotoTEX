using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Domain.Enums
{
    public enum StatusCorrida
    {
        Indefinido = 0,
        Agendada,
        Solicitada,
        EmCurso,
        EmEspera,
        Cancelada,
        Concluida,
        EmNegociacao,
        CanceladaPassageiro
    }
}
