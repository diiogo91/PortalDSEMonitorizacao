using System;

namespace PortalDSEMonitorizacao.Models
{
    public class EstatisticaEnvioRecepcao
    {
        public EstatisticaEnvioRecepcao(String cellnbr, String saida, String entrada, String duracao, String hora, String intervalo, String operadora, String acima_abaixo)
        {
            this.Cellnbr = Cellnbr;
            this.Saida = saida;
            this.Entrada = entrada;
            this.Duracao = duracao;
            this.Hora = hora;
            this.Intervalo = intervalo;
            this.Operadora = operadora;
            this.Acima_Abaixo = acima_abaixo;
        }

        public EstatisticaEnvioRecepcao()
        {
            
            
        }

        public String Cellnbr { set; get;}
        public String Saida { set; get;}
        public String Entrada{ set; get;}
        public String Duracao{ set; get;}
        public String Hora{ set; get;}
        public String Intervalo { set; get; }
        public String Operadora { set; get; }
        public String Acima_Abaixo { set; get; }

    }
}