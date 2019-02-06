using MongoDB.Bson;
using System;

namespace PortalDSEMonitorizacao.Models
{
    public class ContactAPC
    {
        public ContactAPC(ObjectId id, Contacto contacto, String notf_SMS)
        {
            this.Id = id;
            this.Contacto = contacto;
            this.Notf_SMS = notf_SMS;
        }

        public ContactAPC()
        {
        }

        public ObjectId Id { set; get;}
        public Contacto Contacto { set; get; }
        public String Notf_SMS{ set; get;}
    }
}