using CloudMe.MotoTEX.Infraestructure.Entries.Localidade;
using System;

namespace CloudMe.MotoTEX.Infraestructure.Entries.Piloto
{
    public class RegistroVeiculo: EntryBase<Guid>
    {
        public Guid IdVeiculo { get; set; }
        public Guid IdFotoCRLV { get; set; }
        public Guid IdFotoFrente { get; set; }
        public Guid IdFotoTras { get; set; }
        public Guid IdFotoEsquerda { get; set; }
        public Guid IdFotoDireita { get; set; }
        public Guid IdUF { get; set; }
        public int AnoExercicio { get; set; }
        public string Chassi { get; set; }
        public string Renavam { get; set; }

        public virtual Veiculo Veiculo { get; set; }
        public virtual Foto FotoCRLV { get; set; }
        public virtual Foto FotoFrente { get; set; }
        public virtual Foto FotoTras { get; set; }
        public virtual Foto FotoEsquerda { get; set; }
        public virtual Foto FotoDireita { get; set; }
        public virtual UF UF { get; set; }
    }
}
