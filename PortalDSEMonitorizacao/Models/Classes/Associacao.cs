using MongoDB.Bson;
using System;

namespace PortalDSEMonitorizacao.Models
{
    public class Associacao
    {
        public Associacao(ObjectId id, Provedor provedor, ServicoMobile servicoMobile)
        {
            this.Id = id;
            this.Provedor = provedor;
            this.ServicoMobile = servicoMobile;

        }

        public Associacao()
        {
            
            
        }

        public ObjectId Id { set; get;}
        public Provedor Provedor { set; get;}
        public ServicoMobile ServicoMobile { set; get; }
    }
}