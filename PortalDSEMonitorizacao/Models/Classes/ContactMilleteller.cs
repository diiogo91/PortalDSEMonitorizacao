using MongoDB.Bson;
using System;

namespace PortalDSEMonitorizacao.Models
{
    public class ContactMilleteller
    {
        public ContactMilleteller(ObjectId id, Contacto contacto, String notf_Email,
            String notf_SMS, String fim)
        {
            this.Id = id;
            this.Contacto = contacto;
            this.Notf_Email = notf_Email;
            this.Notf_SMS = notf_SMS;
            this.Fim = fim;
        }

        public ContactMilleteller()
        {
            
            
        }

        public ObjectId Id { set; get;}
        public Contacto Contacto { set; get; }
        public String Notf_Email{ set; get;}
        public String Notf_SMS{ set; get;}
        public String Fim { set; get; }
    }
}