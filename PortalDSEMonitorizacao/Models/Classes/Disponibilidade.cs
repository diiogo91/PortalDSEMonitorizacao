using System;
using System.Collections.Generic;

namespace PortalDSEMonitorizacao.Models
{
    public class Disponibilidade
    {
        public Disponibilidade(String idProvedor, String idServico, long disponibilidadeMinutos, TimeSpan intervalo, bool Resolvido, List<DateTime> days, String descricao)
        {
            this.IDProvedor = idProvedor;
            this.IDServico = idServico;
            this.DisponibilidadeMinutos = disponibilidadeMinutos;
            this.Intervalo = intervalo;
            this.Resolvido = Resolvido;
            this.days = days;
            this.Descricao = descricao;
        }

        public Disponibilidade()
        {
            
            
        }

        public String IDProvedor { set; get;}
        public String IDServico { set; get; }
        public long DisponibilidadeMinutos { set; get;}
        public TimeSpan Intervalo { set; get; }
        public Boolean Resolvido { set; get; }
        public List<DateTime> days { set; get; }
        public String Descricao { set; get; }
    }
}