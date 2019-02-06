using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace PortalDSEMonitorizacao.Models
{
    public class Indisponibilidade
    {
        public Indisponibilidade(ObjectId id, DateTime dataInicio , DateTime dataFim, Provedor operadora, ServicoMobile servico, String descricaoProblema, Boolean resolvido, String User, TimeSpan intervalo)
        {
            this.Id = id;
            this.DataInicio = dataInicio;
            this.DataFim = dataFim;
            this.Operadora = operadora;
            this.Servico = servico;
            this.DescricaoProblema = descricaoProblema;
            this.Resolvido = resolvido;
            this.User = User;
            this.Intervalo = intervalo;
        }

        public Indisponibilidade(ObjectId id, DateTime dataInicio, DateTime dataFim, Provedor operadora, Aplicacao aplicacao, String descricaoProblema, Boolean resolvido, String User, TimeSpan intervalo)
        {
            this.Id = id;
            this.DataInicio = dataInicio;
            this.DataFim = dataFim;
            this.Operadora = operadora;
            this.Aplicacao = aplicacao;
            this.DescricaoProblema = descricaoProblema;
            this.Resolvido = resolvido;
            this.User = User;
            this.Intervalo = intervalo;
        }

        public Indisponibilidade()
        {
            
            
        }

        public ObjectId Id { set; get;}
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DataInicio { set; get;}
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DataFim { set; get;}
        public Provedor Operadora { set; get;}
        public ServicoMobile Servico { set; get;}
        public Aplicacao Aplicacao { set; get; }
        public String DescricaoProblema{ set; get;}
        public Boolean Resolvido { set; get; }
        public String User { set; get; }
        public TimeSpan Intervalo { set; get; }

    }
}