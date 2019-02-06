using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PortalDSEMonitorizacao.Models
{
    public class PortalDSEMonDB
    {
        public IMongoDatabase Database;
        public String DataBaseName = "PortalDSEMonDB";
        string conexaoMongoDB = "";

        public PortalDSEMonDB()
        {

            conexaoMongoDB = ConfigurationManager.ConnectionStrings["conexaoMongoDB"].ConnectionString;
            var cliente = new MongoClient(conexaoMongoDB);
            Database = cliente.GetDatabase(DataBaseName);
        }

        public IMongoCollection<Contacto> Contactos
        {
            get
            {
                var Contactos = Database.GetCollection<Contacto>("Contactos");
                return Contactos;
            }
        }
        public IMongoCollection<CartaoSIM> cartoesSIM
        {
            get
            {
                var cartoesSIM = Database.GetCollection<CartaoSIM>("CartoesSIM");
                return cartoesSIM;
            }
        }

        public IMongoCollection<HistoricoMobile> HistoricoMobile
        {
            get
            {
                var HistoricoMobile = Database.GetCollection<HistoricoMobile>("HistoricoTransacoes");
                return HistoricoMobile;
            }
        }
        public IMongoCollection<HistoricoMobileTime> HistoricoMobileTime
        {
            get
            {
                var HistoricoMobileTime = Database.GetCollection<HistoricoMobileTime>("HistoricoTransacoesTime");
                return HistoricoMobileTime;
            }
        }
        public IMongoCollection<HistoricoMobileToProcess> HistoricoMobileToProcess
        {
            get
            {
                var HistoricoMobileToProcess = Database.GetCollection<HistoricoMobileToProcess>("HistoricoTransacoesPorProcessar");
                return HistoricoMobileToProcess;
            }
        }
        public IMongoCollection<HistoricoMobileError> HistoricoMobileError
        {
            get
            {
                var HistoricoMobileError = Database.GetCollection<HistoricoMobileError>("HistoricoTransacoesErro");
                return HistoricoMobileError;
            }
        }

        public IMongoCollection<Provedor> Provedores
        {
            get
            {
                var Provedores = Database.GetCollection<Provedor>("Provedores");
                return Provedores;
            }
        }
        public IMongoCollection<ContactBalcao> ContactosBalcoes
        {
            get
            {
                var ContactosBalcoes = Database.GetCollection<ContactBalcao>("ContactosBalcoes");
                return ContactosBalcoes;
            }
        }
        public IMongoCollection<ContactAPC> ContactosAPC
        {
            get
            {
                var ContactosAPC = Database.GetCollection<ContactAPC>("ContactosAPC");
                return ContactosAPC;
            }
        }

        public IMongoCollection<ContactExchange> ContactosExchange
        {
            get
            {
                var ContactosExchange = Database.GetCollection<ContactExchange>("ContactosExchange");
                return ContactosExchange;
            }
        }
        public IMongoCollection<ContactExtranet> ContactosExtranet
        {
            get
            {
                var ContactosExtranet = Database.GetCollection<ContactExtranet>("ContactosExtranet");
                return ContactosExtranet;
            }
        }

        public IMongoCollection<ContactMilleteller> ContactosMilleteller
        {
            get
            {
                var ContactosMilleteller = Database.GetCollection<ContactMilleteller>("ContactosMilleteller");
                return ContactosMilleteller;
            }
        }

        public IMongoCollection<ServicoMobile> ServicosMobile
        {
            get
            {
                var ServicosMobile = Database.GetCollection<ServicoMobile>("ServicosMobile");
                return ServicosMobile;
            }
        }

        public IMongoCollection<Equipa> Equipas
        {
            get
            {
                var Equipas = Database.GetCollection<Equipa>("Equipas");
                return Equipas;
            }
        }
        public IMongoCollection<Associacao> Associacoes
        {
            get
            {
                var Associacoes = Database.GetCollection<Associacao>("Associacoes");
                return Associacoes;
            }
        }

        public IMongoCollection<Credencial> Credenciais
        {
            get
            {
                var Credenciais = Database.GetCollection<Credencial>("Credenciais");
                return Credenciais;
            }
        }
        public IMongoCollection<Aplicacao> Aplicacoes
        {
            get
            {
                var Aplicacoes = Database.GetCollection<Aplicacao>("Aplicacoes");
                return Aplicacoes;
            }
        }
        public IMongoCollection<Sonda> Sondas
        {
            get
            {
                var Sondas = Database.GetCollection<Sonda>("Sondas");
                return Sondas;
            }
        }
        public IMongoCollection<Link> Links
        {
            get
            {
                var Links = Database.GetCollection<Link>("Links");
                return Links;
            }
        }
        public IMongoCollection<Template> Templates
        {
            get
            {
                var Templates = Database.GetCollection<Template>("Templates");
                return Templates;
            }
        }
        public IMongoCollection<Indisponibilidade> Indisponibilidades
        {
            get
            {
                var Indisponibilidades = Database.GetCollection<Indisponibilidade>("Indisponibilidades");
                return Indisponibilidades;
            }
        }
        public IMongoCollection<HistoricoNotificacao> HistoricoNotificacoes
        {
            get
            {
                var HistoricoNotificacoes = Database.GetCollection<HistoricoNotificacao>("HistoricoNotificacoes");
                return HistoricoNotificacoes;
            }
        }

        public IMongoCollection<Grupo> Grupos
        {
            get
            {
                var grupos = Database.GetCollection<Grupo>("Grupos");
                return grupos;
            }
        }
    }
}