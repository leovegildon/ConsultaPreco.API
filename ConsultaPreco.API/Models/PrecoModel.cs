using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultaPreco.API.Models
{
    public class PrecoModel
    {
        public PrecoModel()
        {
        }
        public PrecoModel(string centro, string material, string descricao, string precoRegular, string precoMinhaLe)
        {
            this.centro = centro;
            this.material = material;
            this.descricao = descricao;
            this.precoRegular = precoRegular;
            this.precoMinhaLe = precoMinhaLe;
        }
        public string centro { get; set; }
        public string material { get; set; }
        public string descricao { get; set; }
        public string precoRegular { get; set; }
        public string precoMinhaLe { get; set; }
    }
}
