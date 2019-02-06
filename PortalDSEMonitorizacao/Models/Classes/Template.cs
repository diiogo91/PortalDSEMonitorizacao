using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace PortalDSEMonitorizacao.Models
{
    public class Template
    {
        public Template(ObjectId id, String nomeTemplate, List<Contacto> contactos, List<Equipa> equipas)
        {
            this.Id = id;
            this.NomeTemplate = nomeTemplate;
            this.Contactos = contactos;
            this.Equipas = equipas;
        }

        public Template()
        {
            
        }

        public ObjectId Id { set; get;}
        public String NomeTemplate { set; get;}
        public List<Contacto> Contactos { set; get; }
        public List<Equipa> Equipas { set; get; }
    }
}