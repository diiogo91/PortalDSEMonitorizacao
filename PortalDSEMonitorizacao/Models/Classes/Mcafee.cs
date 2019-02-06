using System;

namespace PortalDSEMonitorizacao.Models
{
    public class Mcafee
    {
        public Mcafee(String nomeServidor, String diasDesactualizado, String nomeBalcao, String percentagem, String indice )
        {
            this.Name = nomeServidor;
            this.nomeBalcao = nomeBalcao;
            this.diasDesactualizado = diasDesactualizado;
            this.indice = indice.Replace(".",",");
            this.percentagem = percentagem.Replace(".", ",");

        }

        public Mcafee()
        {
            
            
        }

        public String Name { set; get;}
        public String nomeBalcao { set; get;}
        public String diasDesactualizado { set; get; }
        public String indice { set; get; }
        public String percentagem { set; get; }
    }
}