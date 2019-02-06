using MongoDB.Bson;
using System;

namespace PortalDSEMonitorizacao.Models
{
    public class ContactExchange
    {
        public ContactExchange(ObjectId id, Contacto contacto, String notf_SMS)
        {
            this.Id = id;
            this.Contacto = contacto;
            this.Notf_SMS = notf_SMS;
        }

        public ContactExchange()
        {
        }

        public ObjectId Id { set; get;}
        public Contacto Contacto { set; get; }
        public String Notf_SMS{ set; get;}
    }
}