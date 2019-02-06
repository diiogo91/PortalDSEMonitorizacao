using MongoDB.Bson;
using System;

namespace PortalDSEMonitorizacao.Models
{
    public class Credencial
    {
        public Credencial(ObjectId id, String user, String password, String descricao, DateTime dataActualizacao, Boolean visibilidade)
        {
            this.Id = id;
            this.User = user;
            this.Password = password;
            this.Descricao = descricao;
            this.DataActualizacao = dataActualizacao;
            this.Visibilidade = visibilidade;
        }

        public Credencial()
        {
            
            
        }

        public ObjectId Id { set; get;}
        public String User{ set; get;}
        public String Password { set; get; }
        public String Descricao { set; get; }
        public DateTime DataActualizacao { set; get; }
        public Boolean Visibilidade { set; get; }
    }
}