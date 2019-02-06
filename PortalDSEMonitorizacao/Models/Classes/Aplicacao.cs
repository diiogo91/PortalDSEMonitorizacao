using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace PortalDSEMonitorizacao.Models
{
    public class Aplicacao
    {
        public Aplicacao(ObjectId id, String nome, String preRequisito, List<Credencial> credencial, String perfil, String endereco, String requisitoMonitorar, List<Sonda> sonda)
        {
            this.Id = id;
            this.Endereco = endereco;
            this.Nome = nome;
            this.Credencial = credencial;
            this.PreRequisito = preRequisito;
            this.Perfil = perfil;
            this.RequisitoMonitorar = requisitoMonitorar;
            this.Sonda = sonda;
        }

        public Aplicacao()
        {
            
        }

        public ObjectId Id { set; get; }
        public String Endereco{ set; get;}
        public String PreRequisito { set; get; }
        public List<Credencial> Credencial { set; get; }
        public String Perfil { set; get; }
        public String RequisitoMonitorar { set; get; }
        public String Nome { set; get; }
        public List<Sonda> Sonda { set; get; }
    }
}