using MongoDB.Bson;
using System;

namespace PortalDSEMonitorizacao.Models
{
    public class ContactExtranet
    {
        public ContactExtranet(ObjectId id, Contacto contacto, String notf_Email,
            String notf_SMS, String fim)
        {
            this.Id = id;
            this.Contacto = contacto;
            this.Notf_Email = notf_Email;
            this.Notf_SMS = notf_SMS;
            this.Fim = fim;
        }

        public ContactExtranet()
        {
            
            
        }

        public ObjectId Id { set; get;}
        public Contacto Contacto { set; get; }
        public String Notf_Email{ set; get;}
        public String Fim { set; get;}
        public String Notf_SMS { set; get; }
    }
}