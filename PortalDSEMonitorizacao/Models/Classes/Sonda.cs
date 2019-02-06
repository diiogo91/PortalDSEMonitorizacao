using MongoDB.Bson;
using System;
using System.Collections.Generic;
namespace PortalDSEMonitorizacao.Models
{
    public class Sonda
    {
        public Sonda(ObjectId id, String dns, String ip, String sistemaOperativo, List<Credencial> credencial, String descricao)
        {
            this.Id = id;
            this.Dns = dns;
            this.Ip = ip;
            this.Credencial = credencial;
            this.Descricao = descricao;
            this.SistemaOperativo = sistemaOperativo;
        }

        public Sonda()
        {
            
        }

        public ObjectId Id { set; get;}
        public String Dns{ set; get;}
        public String Ip { set; get;}
        public List<Credencial> Credencial { set; get;}
        public String Descricao { set; get; }
        public String SistemaOperativo { set; get; }
    }
}