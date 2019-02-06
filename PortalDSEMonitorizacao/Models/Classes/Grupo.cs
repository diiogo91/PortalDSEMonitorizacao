using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace PortalDSEMonitorizacao.Models
{
    public class Grupo
    {
        public Grupo(ObjectId id, String nomeGrupo, List<Equipa> equipas)
        {
            this.Id = id;
            this.NomeGrupo = nomeGrupo;
            this.Equipas = equipas;
        }

        public Grupo()
        {
        }

        public ObjectId Id { set; get;}
        public String NomeGrupo { set; get;}
        public List<Equipa> Equipas { set; get; }
    }
}