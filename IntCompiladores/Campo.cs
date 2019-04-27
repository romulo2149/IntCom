using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntCompiladores
{
    class Campo
    {
        private string nombreCampo;
        private string valorCampo;
        private string tipoCampo;
        private string estructuraPadre;

        public Campo(string nombreCampo, string valorCampo, string tipoCampo, string estructuraPadre)
        {
            this.NombreCampo = nombreCampo;
            this.ValorCampo = valorCampo;
            this.TipoCampo = tipoCampo;
            this.EstructuraPadre = estructuraPadre;
        }

        public string NombreCampo { get => nombreCampo; set => nombreCampo = value; }
        public string ValorCampo { get => valorCampo; set => valorCampo = value; }
        public string TipoCampo { get => tipoCampo; set => tipoCampo = value; }
        public string EstructuraPadre { get => estructuraPadre; set => estructuraPadre = value; }
    }
}
