using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
using ClosedXML.Excel;
using Newtonsoft.Json;
using PortalDSEMonitorizacao.Models;
using Spire.Xls;
using System.Net.Mail;
using System.Globalization;
using System.DirectoryServices.AccountManagement;
using System.Web.Hosting;
using System.Security;
using RazorPDF;
using Rotativa;
using System.Web.Security;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;
using PortalDSEMonitorizacao.Models.Libraries;
using Galactic.PowerShell;
using System.Collections;

namespace PortalDSEMonitorizacao.Controllers
{
    public class HomeController : Controller
    {

        private static PortalDSEMonDB Context = new PortalDSEMonDB();
        private static List<Indisponibilidade> IndisponibilidadePendentesList = new List<Indisponibilidade>();
        private static List<Indisponibilidade> IndisponibilidadeResolvidosList = new List<Indisponibilidade>();
        private static List<Indisponibilidade> IndisponibilidadeList = new List<Indisponibilidade>();
        private static List<HistoricoNotificacao> historicoNotifList = new List<HistoricoNotificacao>();
        private static List<Template> templatesList = new List<Template>();
        private static List<Provedor> provedores = new List<Provedor>();
        private static List<Equipa> equipasList = new List<Equipa>();
        private static List<ServicoMobile> servicosMobile = new List<ServicoMobile>();
        private static List<Associacao> associacoes = new List<Associacao>();
        private static List<ContactBalcao> ContactsList = new List<ContactBalcao>();
        private static List<ContactExtranet> ContactsExtranetsList = new List<ContactExtranet>();
        private static List<ContactAPC> ContactsAPCList = new List<ContactAPC>();
        private static List<ContactExchange> ContactsExchangeList = new List<ContactExchange>();
        private static List<Contacto> contactosList = new List<Contacto>();
        private static List<Credencial> credenciaisList = new List<Credencial>();
        private static List<Sonda> sondasList = new List<Sonda>();
        private static List<Link> linksList = new List<Link>();
        private static List<Aplicacao> aplicacoesList = new List<Aplicacao>();
        private static List<CartaoSIM> cartoesSIMList = new List<CartaoSIM>();
        private static List<ContactMilleteller> ContactsListMilleteller = new List<ContactMilleteller>();
        private static ContactsBalcaoListViewModel viewModel = new ContactsBalcaoListViewModel();
        private static ContactsExtranetsListViewModel viewExtranetModel = new ContactsExtranetsListViewModel();
        private static ContactsAPCListViewModel APCviewModel = new ContactsAPCListViewModel();
        private static ContactsExchangeListViewModel ExchangeviewModel = new ContactsExchangeListViewModel();
        private static ContactsListViewModel contactosviewModel = new ContactsListViewModel();
        private static CredenciaisListViewModel credenciaisListViewModel = new CredenciaisListViewModel();
        private static SondasListViewModel sondasListViewModel = new SondasListViewModel();
        private static LinksListViewModel linksListView = new LinksListViewModel();
        private static AplicacoesListViewModel aplicacoesListViewModel = new AplicacoesListViewModel();
        private static CartoesSIMListViewModel cartoesSIMListViewModel = new CartoesSIMListViewModel();
        private static EditarContactoListView editarContactoListView = new EditarContactoListView();
        private static GestaoEquipasListViewModel equipasviewModel = new GestaoEquipasListViewModel();
        private static ContactsMilletellerListViewModel viewModelMilleteller = new ContactsMilletellerListViewModel();
        private static GestaoTemplatesListViewModel templateview = new GestaoTemplatesListViewModel();
        private static List<EstatisticaBalcao> estatisticaBalcoesList = new List<EstatisticaBalcao>();
        private static List<EstatisticaAplicacao> estatisticaAplicacoesList = new List<EstatisticaAplicacao>();
        private static List<Grupo> gruposList = new List<Grupo>();
        public class Computers : List<Computer> { }
        public Computers Computers1 = new Computers();

        //Descrição do caminho do ficheiro usado pelo javascript
        private static readonly string PowerShellRunnerBatchFilePath = @"C:\Temp\RemoteScriptInvoke\COMBalcoes.ps1";
        private static readonly string PowerShellRunnerBatchFilePathM = @"C:\Temp\RemoteScriptInvoke\SERVMILLETELLER.ps1";
        private static readonly string fileEDiscoBalcoes = @"C:\Temp\RemoteScriptInvoke\EDISCOBC.ps1";
        private static readonly string fileEDiscoSC = @"C:\Temp\RemoteScriptInvokep\EDISCOSC.ps1";
        private static readonly string fileATMs = @"C:\Temp\RemoteScriptInvoke\ATMs.ps1";
        private static readonly string fileRAID = @"\C:\Temp\RemoteScriptInvoke\RAID.ps1";
        private static readonly string fileEServSC = @"C:\Temp\RemoteScriptInvoke\ESTADOSERVSC.ps1";
        private static readonly string fileAS400 = @"C:\Temp\RemoteScriptInvoke\AS400.ps1";
        private static readonly string fileMcafeeSC = @"C:\Temp\RemoteScriptInvoke\MCAFEESC.ps1";
        private static readonly string fileMcafeeB = @"\C:\Temp\RemoteScriptInvoke\MCAFEEBC.ps1";
        private static readonly string fileAPC = @"C:\Temp\RemoteScriptInvoke\APC.ps1";
        private static readonly string fileExtranet = @"C:\Temp\RemoteScriptInvoke\COMExtranet.ps1";
        private static readonly string fileAKCP = @"C:\Temp\RemoteScriptInvoke\AKCP.ps1";
        private String standardOutput = "";
        private String errorOutput = "";


        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
        public ActionResult AddServicoMobile()
        {
            return PartialView();
        }
        public ActionResult ADvsPRTG()
        {
            return View();
        }
        [Authorize]
        public ActionResult GestaoCredenciais()
        {
            //Dependentes
            credenciaisListViewModel = new CredenciaisListViewModel();
            credenciaisListViewModel = GetCredenciasFromDB();
            credenciaisListViewModel.temPermissao = VerifyUserAutorization();
            linksListView = GetLinksFromDB();
            aplicacoesListViewModel = GetAplicacoesFromDB();
            sondasListViewModel = GetSondasFromDB();
            return View(credenciaisListViewModel);
        }
        public ActionResult GestaoSondas()
        {
            //Dependentes
            linksListView = GetLinksFromDB();
            aplicacoesListViewModel = GetAplicacoesFromDB();
            credenciaisListViewModel = GetCredenciasFromDB();
            sondasListViewModel = new SondasListViewModel();
            sondasListViewModel = GetSondasFromDB();
            sondasListViewModel.temPermissao = VerifyUserAutorization();
            return View(sondasListViewModel);
        }
        public ActionResult GestaoLinks()
        {
            credenciaisListViewModel = GetCredenciasFromDB();
            sondasListViewModel = GetSondasFromDB();
            linksListView = new LinksListViewModel();
            linksListView = GetLinksFromDB();
            linksListView.temPermissao = VerifyUserAutorization();
            return View(linksListView);
        }
        public ActionResult GestaoAplicacoes()
        {
            credenciaisListViewModel = GetCredenciasFromDB();
            sondasListViewModel = GetSondasFromDB();
            aplicacoesListViewModel = new AplicacoesListViewModel();
            aplicacoesListViewModel = GetAplicacoesFromDB();
            List<Aplicacao> SortedLis2t = aplicacoesList.OrderBy(o => o.Nome).ToList();
            aplicacoesList = SortedLis2t;
            aplicacoesListViewModel.temPermissao = VerifyUserAutorization();
            return View(aplicacoesListViewModel);
        }

        public ActionResult GestaoCartaoSIM()
        {
           
            sondasListViewModel = GetSondasFromDB();
            cartoesSIMListViewModel = new CartoesSIMListViewModel();
            cartoesSIMListViewModel = GetCartoesSIMFromDB();
            List<CartaoSIM> SortedLis2t = cartoesSIMList.OrderBy(o => o.Numero).ToList();
            cartoesSIMList = SortedLis2t;
            cartoesSIMListViewModel.temPermissao = VerifyUserAutorization();
            if (cartoesSIMList != null)
            {
                TempData["showmsg2"] = "<script type='text/javascript'> document.getElementById('hidenbtn').disabled =false;</script>";
            }
            return View(cartoesSIMListViewModel);
        }

        public ActionResult GestaoTemplates()
        {
            GestaoTemplatesListViewModel gestaoView = new GestaoTemplatesListViewModel();
            gestaoView = GetTemplatesFromBD();
            ContactsListViewModel contacts = GetContactosPortalBD();
            GetEquipasFromDB();
            gestaoView.templates = templatesList;
            return View(gestaoView);
        }

      
        public ActionResult Mcafee()
        {
            EstatisticaMcafeeViewModel view = new EstatisticaMcafeeViewModel();
            return View(view);
        }

        public ActionResult MonitorizacaoPRTG()
        {
            MonitorizacaoPRTGListViewModel view = new MonitorizacaoPRTGListViewModel();
            return View(view);
        }
        [HttpPost]
        public ActionResult SearchSC()
        {
            MonitorizacaoPRTGListViewModel view = new MonitorizacaoPRTGListViewModel();
            List<Computer> servidoresSC = new List<Computer>();
            List<String> devicesMonitored = new List<string>();
            string selectOU = "OU=Servers,OU=ITManagement,DC=mz,DC=bcpcorp,DC=net";
            string accountname = @"mz\opsmgrmsaction";
            string password = "Dsemon&123";
            String servidores1 = "http://semzseiptg12:16661/api/table.json?content=devices&output=json&columns=device&id=54&&username=prtgadmin&&passhash=2445698422";
            WebRequest request = HttpWebRequest.Create(servidores1);
            request.Method = "GET";
            using (WebResponse response = request.GetResponse())
            {
                String jsonText = "";
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    jsonText = sr.ReadToEnd();
                    response.Close();
                }
                XmlDocument xmlDoc = (XmlDocument)JsonConvert.DeserializeXmlNode(jsonText, "devices");
                foreach (XmlNode node in xmlDoc.SelectNodes("/devices/devices"))
                {
                    devicesMonitored.Add(node["device"].InnerText.ToUpper());
                }
            }
            String servidores2 = "http://semzseiptg12:16661/api/table.json?content=devices&output=json&columns=device&id=2430&&username=prtgadmin&&passhash=2445698422";
            WebRequest request2 = HttpWebRequest.Create(servidores2);
            request2.Method = "GET";
            using (WebResponse response2 = request2.GetResponse())
            {
                String jsonText2 = "";
                using (var sr = new StreamReader(response2.GetResponseStream()))
                {
                    jsonText2 = sr.ReadToEnd();
                    response2.Close();
                }
                XmlDocument xmlDoc2 = (XmlDocument)JsonConvert.DeserializeXmlNode(jsonText2, "devices");
                foreach (XmlNode node in xmlDoc2.SelectNodes("/devices/devices"))
                {
                    devicesMonitored.Add(node["device"].InnerText.ToUpper());
                }

            }
            String servidores3 = "http://semzseiptg12:16661/api/table.json?content=devices&output=json&columns=device&id=14170&&username=prtgadmin&&passhash=2445698422";
            WebRequest request3 = HttpWebRequest.Create(servidores3);
            request3.Method = "GET";
            using (WebResponse response3 = request3.GetResponse())
            {
                String jsonText3 = "";
                using (var sr = new StreamReader(response3.GetResponseStream()))
                {
                    jsonText3 = sr.ReadToEnd();
                    response3.Close();
                }
                XmlDocument xmlDoc3 = (XmlDocument)JsonConvert.DeserializeXmlNode(jsonText3, "devices");
                foreach (XmlNode node in xmlDoc3.SelectNodes("/devices/devices"))
                {
                    devicesMonitored.Add(node["device"].InnerText.ToUpper());
                }
            }
            // set up domain contextad
            using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "mz.bcpcorp.net", selectOU, accountname, password))
                {
                    ComputerPrincipal cP = new ComputerPrincipal(ctx);
                    cP.Name = "SEMZSEI*";
                    using (PrincipalSearcher searcher = new PrincipalSearcher(cP))
                    {
                        foreach (ComputerPrincipal p in searcher.FindAll())
                        {
                            if(p!= null)
                            {                          
                                    if(devicesMonitored.Exists(device => device.Contains(p.Name.ToUpper())) == false)
                                    {
                                        String ouname = p.DistinguishedName.ToString().Replace(",", "").Replace("DC=mzDC=bcpcorpDC=net", " ").Replace("OU=No-Standard", "").Replace("CN=" + p.Name, "").Replace("OU=", "<--");
                                        servidoresSC.Add(new Computer(p.Name.ToUpper(), p.Description, ouname));
                                    }
                             }
                        }
                    }
                }
            view.servidoresSC = servidoresSC;
            return View("MonitorizacaoPRTG", view);
        }

        [HttpPost]
        public ActionResult SearchBC()
        {
            MonitorizacaoPRTGListViewModel view = new MonitorizacaoPRTGListViewModel();
            List<Computer> servidoresBC = new List<Computer>();
            List<String> devicesMonitored = new List<string>();
            // string selectOU = "OU=Production,OU=Buildings2008,OU=Servers,OU=ITManagement,DC=net,DC=bcpcorp,DC=mz";
            string selectOU = "OU=Branches7, OU=Servers,OU=ITManagement,DC=mz,DC=bcpcorp,DC=net";
            string accountname = @"mz\opsmgrmsaction";
            string password = "Dsemon&123";


            String servidores1 = "http://semzseiptg12:16661/api/table.json?content=devices&output=json&columns=objid,device,host&id=44317&username=prtgadmin&&passhash=2445698422";
            WebRequest request = HttpWebRequest.Create(servidores1);
            request.Method = "GET";
            using (WebResponse response = request.GetResponse())
            {
                String jsonText = "";
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    jsonText = sr.ReadToEnd();
                    response.Close();
                }
                XmlDocument xmlDoc = (XmlDocument)JsonConvert.DeserializeXmlNode(jsonText, "devices");
                foreach (XmlNode node in xmlDoc.SelectNodes("/devices/devices"))
                {
                    devicesMonitored.Add(node["host"].InnerText.ToUpper());
                }
            }

            // set up domain contextad
            using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "mz.bcpcorp.net", selectOU, accountname, password))
            {
                ComputerPrincipal cP = new ComputerPrincipal(ctx);
                cP.Name = "SBMZ*";
                using (PrincipalSearcher searcher = new PrincipalSearcher(cP))
                {
                    foreach (ComputerPrincipal p in searcher.FindAll())
                    {
                        if (p != null)
                        {

                            if (devicesMonitored.Exists(device => device.Contains(p.Name.ToUpper())) == false)
                            {
                                String ouname = p.DistinguishedName.ToString().Replace(",", "").Replace("DC=mzDC=bcpcorpDC=net", " ").Replace("OU=No-Standard", "").Replace("CN=" + p.Name, "").Replace("OU=", "<--");
                                // Convert the "generic" Principal to a UserPrincipal
                                servidoresBC.Add(new Computer(p.Name.ToUpper(), p.Description, ouname));
                            }
                        }
                    }
                }
            }
            view.servidoresBC = servidoresBC;
            return View("MonitorizacaoPRTG", view);
        }


        public ActionResult EnvioNotificacoes()
        {
            EnvioNotificacoesListViewModel viewModel = new EnvioNotificacoesListViewModel();
            ContactsListViewModel contacts = GetContactosPortalBD();
            ReadHistoricoFromFile();
            GestaoTemplatesListViewModel gestaoView = new GestaoTemplatesListViewModel();
            gestaoView = GetTemplatesFromBD();
            viewModel.Contactos = contactosList;
            viewModel.Templates = templatesList;
            viewModel.mensagem = "\nDSE-Monitorização";
            List<String> lista = new List<String>();
            viewModel.SelectedOptions = lista;
            viewModel.historico = historicoNotifList;
            viewModel.nomeTemplate = "";
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EnvioNotificacoes2(EnvioNotificacoesListViewModel model)
        {
            EnvioNotificacoesListViewModel viewModel = new EnvioNotificacoesListViewModel();
            SmtpClient client = new SmtpClient("semzseisrf01");
            client.Credentials = new NetworkCredential("mz/opsmgrmsaction", "Dsemon&123");

            if (model.mensagem != null && model.mensagem != "")
            {
                int countSpaces = model.mensagem.Count(Char.IsWhiteSpace); // 6
                int countWords = model.mensagem.Split().Length;
                int totalChar = countSpaces + countWords;
                String mensagemFormatada = RemoverAcentuacao(model.mensagem);

                if (totalChar > 180)
                {
                    TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='Erro ao enviar Notificação..Mensagem acima do limite de 180 caracteres!';</script>";
                    return RedirectToAction("EnvioNotificacoes");
                }
                else
                {
                    if (model.SelectedOptions == null)
                    {
                        TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='Erro ao enviar Notificação..Nenhum contacto especificado!';</script>";
                        return RedirectToAction("EnvioNotificacoes");
                    }
                    else
                    {
                        List<Contacto> selectedContacts = new List<Contacto>();
                        foreach (String id in model.SelectedOptions)
                        {
                            foreach (Contacto contacto in contactosList)
                            {
                                if (contacto.Id == ObjectId.Parse(id))
                                {
                                    selectedContacts.Add(contacto);
                                }
                            }
                        }
                        foreach (Contacto contacto in selectedContacts)
                        {
                            String url = "http://semzseiweb03/WSSMSNotifications/default.aspx?celnbr=" + contacto.Telefone + "&smscontent=" + mensagemFormatada;
                            WebRequest requestworkf = HttpWebRequest.Create(url);
                            requestworkf.Timeout = 30000;
                            WebResponse myWebResponse = requestworkf.GetResponse();
                            MailMessage mailMessage = new MailMessage();
                            mailMessage.From = new MailAddress("monitorizacao@millenniumbim.co.mz");
                            mailMessage.To.Add(contacto.Email);
                            mailMessage.Subject = "Notificação da DSE Monitorizacao";
                            mailMessage.Body = model.mensagem;
                            client.Send(mailMessage);
                        }
                        ObjectId genrdID = ObjectId.GenerateNewId();

                        string userName = getCurrentUserName();
                        HistoricoNotificacao newHistory = new HistoricoNotificacao(genrdID, DateTime.Now, "Portal de Monitorização", selectedContacts, model.mensagem, userName);
                        Context.HistoricoNotificacoes.InsertOne(newHistory);
                        TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='Notificação enviada com sucesso!';</script>";
                    }
                }
            }
            return RedirectToAction("EnvioNotificacoes");
        }

        public String RemoverAcentuacao(String text)
        {
            return new String(text
                .Normalize(NormalizationForm.FormD)
                .Where(ch => char.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
                .ToArray());
        }

        [HttpPost]
        public ActionResult EnvioNotificacoesMidleUpdate(String idTemplate)
        {
            EnvioNotificacoesListViewModel viewModel = new EnvioNotificacoesListViewModel();
            Template foundTemplate = FindTemplate(idTemplate);
            List<String> lista = new List<String>();
            if (foundTemplate.Contactos != null)
            {
                foreach (Contacto contacto in foundTemplate.Contactos)
                {
                    lista.Add(contacto.Id.ToString());
                }
            }
            viewModel.Contactos = contactosList;
            viewModel.Templates = templatesList;
            viewModel.historico = historicoNotifList;
            viewModel.SelectedOptions = lista;
            viewModel.mensagem = "DSE-Monitorização";
            viewModel.nomeTemplate = "";
            return View("EnvioNotificacoes", viewModel);
        }
        [HttpPost]
        public ActionResult FiltrarMilleteller()
        {
            String nomeFiltro = Request["filtroNome"].ToString();
            viewModelMilleteller = FilterMilleteller(nomeFiltro);
            return View("Milleteller", viewModelMilleteller);
        }
        [HttpPost]
        public ActionResult FiltrarContactBalcoes()
        {
            String nomeFiltro = Request["filtroNome"].ToString();
            viewModel = FilterBalcoes(nomeFiltro);
            return View("balcoes", viewModel);
        }
        [HttpPost]
        public ActionResult FiltrarContactAPC()
        {
            String nomeFiltro = Request["filtroNome"].ToString();
            APCviewModel = FilterAPC(nomeFiltro);
            return View("APC", APCviewModel);
        }

        [HttpPost]
        public ActionResult FiltrarContactExchange()
        {
            String nomeFiltro = Request["filtroNome"].ToString();
            ExchangeviewModel = FilterExchange(nomeFiltro);
            return View("Exchange", ExchangeviewModel);
        }
        [HttpPost]
        public ActionResult FiltrarContactos()
        {
            String nomeFiltro = Request["filtroNome"].ToString();
            contactosviewModel = FilterContactos(nomeFiltro);
            return View("Contactos", contactosviewModel);
        }
        [HttpPost]
        public ActionResult FiltrarCredencial()
        {
            String nomeFiltro = Request["filtroNome"].ToString();
            credenciaisListViewModel = FilterCredenciais(nomeFiltro);
            //Dependentes
            linksListView = GetLinksFromDB();
            aplicacoesListViewModel = GetAplicacoesFromDB();
            sondasListViewModel = GetSondasFromDB();
            credenciaisListViewModel.temPermissao = VerifyUserAutorization();
            return View("GestaoCredenciais", credenciaisListViewModel);
        }
        [HttpPost]
        public ActionResult FiltrarLink()
        {
            String nomeFiltro = Request["filtroNome"].ToString();
            linksListView = FilterLink(nomeFiltro);
            credenciaisListViewModel = GetCredenciasFromDB();
            sondasListViewModel = GetSondasFromDB();
            linksListView.temPermissao = VerifyUserAutorization();
            return View("GestaoLinks", linksListView);
        }
        [HttpPost]
        public ActionResult FiltrarAplicacao()
        {
            String nomeFiltro = Request["filtroNome"].ToString();
            aplicacoesListViewModel = FilterAplicacao(nomeFiltro);
            credenciaisListViewModel = GetCredenciasFromDB();
            sondasListViewModel = GetSondasFromDB();
            aplicacoesListViewModel.temPermissao = VerifyUserAutorization();
            return View("GestaoAplicacoes", aplicacoesListViewModel);
        }
        [HttpPost]
        public ActionResult FiltrarCartaoSIM()
        {
            String nomeFiltro = Request["filtroNumero"].ToString();
            cartoesSIMListViewModel = FilterCartaoSIM(nomeFiltro);
            sondasListViewModel = GetSondasFromDB();
            cartoesSIMListViewModel.temPermissao = VerifyUserAutorization();
            return View("GestaoCartaoSIM", cartoesSIMListViewModel);
        }
        [HttpPost]
        public ActionResult FiltrarSonda()
        {
            String nomeFiltro = Request["filtroNome"].ToString();
            sondasListViewModel = FilterSonda(nomeFiltro);
            //Dependentes
            linksListView = GetLinksFromDB();
            aplicacoesListViewModel = GetAplicacoesFromDB();
            credenciaisListViewModel = GetCredenciasFromDB();
            sondasListViewModel.temPermissao = VerifyUserAutorization();
            return View("GestaoSondas", sondasListViewModel);
        }
        [HttpPost]
        public ActionResult Mcafee2()
        {
            EstatisticaMcafeeViewModel view = new EstatisticaMcafeeViewModel();
            String datain = Request["dataInicio"].ToString();

            if (datain == null)
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='É obrigatorio especificar a Data!';</script>";
            }
            else
            {
                string[] tokens = datain.Split('/');
                String month = tokens[0];
                String year = tokens[1];
                //Balcoes
                List<Mcafee> objectsBalcoes = new List<Mcafee>();
                String path = @"\\semzseiptg12\Temp\database\" + year + @"\" + month + @"\databaseb.json";
                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    objectsBalcoes = JsonConvert.DeserializeObject<List<Mcafee>>(json);
                    if (objectsBalcoes == null)
                    {
                        objectsBalcoes = new List<Mcafee>();
                    }
                }
                //SCentrais
                List<Mcafee> objectssc = new List<Mcafee>();
                String path2 = @"\\semzseiptg12\Temp\database\" + year + @"\" + month + @"\database.json";
                using (StreamReader r = new StreamReader(path2))
                {
                    string json = r.ReadToEnd();
                    objectssc = JsonConvert.DeserializeObject<List<Mcafee>>(json);
                    if (objectssc == null)
                    {
                        objectssc = new List<Mcafee>();
                    }
                }
                if (objectsBalcoes.Count == 0 && objectssc.Count == 0)
                {
                    TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='Não existem ocorrências de Desactualização do Antivirus na Data Especificada!';</script>";
                }
                else if (objectsBalcoes.Count == 0)
                {
                    TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='Não existem ocorrências de Desactualização do Antivirus nos Balcões na Data Especificada!';</script>";
                }
                else if (objectssc.Count == 0)
                {
                    TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='Não existem ocorrências de Desactualização do Antivirus nos Serviços Centrais na Data Especificada!';</script>";
                }
                view.balcoes = objectsBalcoes;
                view.servicosCentrais = objectssc;
                view.periodoEspecificado = datain;
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenbtn').disabled =false;</script>";
            }
            return View("Mcafee", view);
        }
        public ActionResult EstatisticasTransacoesMobile()
        {
            try { GetMobileInfoFromDB(); GetIndisponibilidadeFromDB(); } catch { }
            EstatisticaTransacaoViewModel modelView = new EstatisticaTransacaoViewModel();
            return View(modelView);
        }

        public ActionResult EstatisticasDisponibilidadeMobile()
        {

            try { GetMobileInfoFromDB(); GetIndisponibilidadeFromDB(); } catch { }
            EstatisticaDsiponibilidadeViewModel estatisticaMobile = new EstatisticaDsiponibilidadeViewModel();
            return View(estatisticaMobile);
        }

        public ActionResult EstatisticasDisponibilidadeMobile2()
        {
            GetMobileInfoFromDB();
            String datain = Request["dataInicio"].ToString();
            String datafm = Request["dataFim"].ToString();
            String periodoEspecificado = datain + " - " + datafm;
            List<Disponibilidade> disponibilidadeList = new List<Disponibilidade>();
            List<DisponibilidadeMobile> disponibilidadeMobileList = new List<DisponibilidadeMobile>();
            List<DisponibilidadeMobile> indisponibilidadeList = new List<DisponibilidadeMobile>();
            List<Mdo> mdoList = new List<Mdo>();
            List<Mds> mdsList = new List<Mds>();
            long somatorioIndisponiblidadeTotal = 0;
            String periodoIndsTotal = "0";
            if (datain != "" && datafm != "")
            {
                DateTime DataInicio = DateTime.ParseExact(datain, "dd/MM/yyyy",
                 System.Globalization.CultureInfo.InvariantCulture);
                DateTime DataFim = DateTime.ParseExact(datafm, "dd/MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture);
                if (DataInicio == DataFim)
                {
                    DataInicio= DataInicio.AddHours((Double)0).AddMinutes((Double)0);
                    DataFim= DataFim.AddHours((Double)23).AddMinutes((Double)59);
                }
                else
                {
                    DataFim = DataFim.AddHours((Double)23).AddMinutes((Double)59);
                }
                TimeSpan intervalo = DataFim.Subtract(DataInicio);
                try { GetIndisponibilidadeFromDB(); } catch { }
                foreach (Associacao ass in associacoes)
                {
                    foreach (Indisponibilidade inds in IndisponibilidadeList)
                    {
                        if (inds.Servico != null)
                        {
                            if (ass.Provedor.Id == inds.Operadora.Id && ass.ServicoMobile.Id == inds.Servico.Id)
                            {
                                TimeSpan tempInds = new TimeSpan();
                                if (inds.Resolvido == true)
                                {
                                    if (inds.DataInicio >= DataInicio && inds.DataFim <= DataFim)
                                    {
                                        tempInds = inds.DataFim.Subtract(inds.DataInicio);
                                        AddToDisponibilidadeList(disponibilidadeList, inds, tempInds);
                                    }
                                    if (inds.DataInicio < DataInicio && inds.DataFim >= DataInicio && inds.DataFim <= DataFim)
                                    {
                                        tempInds = inds.DataFim.Subtract(DataInicio);
                                        AddToDisponibilidadeList(disponibilidadeList, inds, tempInds);
                                    }
                                    if (inds.DataInicio >= DataInicio && inds.DataInicio <= DataFim && inds.DataFim > DataFim)
                                    {
                                        tempInds = DataFim.Subtract(inds.DataInicio);
                                        AddToDisponibilidadeList(disponibilidadeList, inds, tempInds);
                                    }
                                }
                                else if (inds.Resolvido == false)
                                {
                                    if (inds.DataInicio <= DataFim)
                                    {
                                        tempInds = DataFim.Subtract(inds.DataInicio);
                                        AddToDisponibilidadeList(disponibilidadeList, inds, tempInds);
                                    }
                                }
                            }
                        }

                    }
                }
                //Calculo de Cada Disponibilidade Por associacao
                foreach (Associacao ass in associacoes)
                {
                    Decimal disponibilidade = 100;
                    int nrOcorrencias = 0;
                    bool indisp = false;
                    double minutes = intervalo.TotalMinutes;
                    String periodo = "-";
                    long minutesRounded = (long)Math.Round(intervalo.TotalMinutes);
                    long somatorioMinutos = 0;
                    long referenciaMinutos = minutesRounded;
                    bool found = false;
                    String diasInd = "";
                    List<String> diasToverify = new List<String>();
                    foreach (Disponibilidade dsp in disponibilidadeList)
                    {
                        if (ass.Provedor.Id.ToString() == dsp.IDProvedor && ass.ServicoMobile.Id.ToString() == dsp.IDServico)
                        {
                            somatorioMinutos = somatorioMinutos + dsp.DisponibilidadeMinutos;
                            if (somatorioMinutos < 0)
                            {
                                somatorioMinutos = 0;
                            }
                            found = true;
                            nrOcorrencias = nrOcorrencias + 1;

                            if (dsp.days.First().Day == dsp.days.Last().Day && dsp.days.First().Month == dsp.days.Last().Month && dsp.days.First().Year == dsp.days.Last().Year)
                            {
                                //diasInd += dsp.days.First().ToString("dd/MM/yyyy")+"; ";
                                diasToverify.Add(dsp.days.First().ToString("dd/MM/yyyy")+" : "+ dsp.Descricao);
                            }
                            else
                            {
                                diasToverify.Add(dsp.days.First().ToString("dd/MM/yyyy") + " a " + dsp.days.Last().ToString("dd/MM/yyyy") + " : " + dsp.Descricao);
                                // diasInd += dsp.days.First().ToString("dd/MM/yyyy")+" a " + dsp.days.Last().ToString("dd/MM/yyyy")+"; ";
                            }
                        }
                    }

                    foreach (String data in diasToverify)
                    {
                        int count = 0;
                        foreach (String data2 in diasToverify)
                        {
                            if (data == data2)
                            {
                                count = count + 1;
                            }
                        }
                        if (!diasInd.Contains(data))
                        {
                            diasInd += data + " (" + count + ");  ";
                        }
                    }
                    if (found == false)
                    {
                        somatorioMinutos = referenciaMinutos;
                        disponibilidade = ((Decimal)somatorioMinutos / (Decimal)referenciaMinutos) * (Decimal)100;
                    }
                    else
                    {
                        disponibilidade = ((Decimal)somatorioMinutos / (Decimal)referenciaMinutos) * (Decimal)100;
                        disponibilidade = (Decimal)100 - disponibilidade;
                    }
                    long days = somatorioMinutos / 1440;
                    long hours = (somatorioMinutos % 1440) / 60;
                    long mins = somatorioMinutos % 60;
                    periodo = days + " dia(s), " + hours + " hora(s) e " + mins + " minuto(s)";
                    if (disponibilidade != 100)
                    {
                        indisp = true;
                    }
                    disponibilidadeMobileList.Add(new DisponibilidadeMobile(indisp, ass, disponibilidade, String.Format("{0:N2}", disponibilidade).Replace(".", ",") + " %", periodo, nrOcorrencias.ToString(), diasInd));
                }

                foreach (DisponibilidadeMobile dsp in disponibilidadeMobileList)
                {
                    if (dsp.Indisponibilidade == true)
                    {
                        indisponibilidadeList.Add(dsp);
                    }
                }

                foreach (Disponibilidade dsp in disponibilidadeList)
                {
                    somatorioIndisponiblidadeTotal = somatorioIndisponiblidadeTotal + dsp.DisponibilidadeMinutos;
                }

                long days2 = somatorioIndisponiblidadeTotal / 1440;
                long hours2 = (somatorioIndisponiblidadeTotal % 1440) / 60;
                long mins2 = somatorioIndisponiblidadeTotal % 60;
                periodoIndsTotal = days2 + " dia(s), " + hours2 + " hora(s) e " + mins2 + " minuto(s)";


                //Calculo das Médias MDO e MDS
                //MDO
                foreach (Provedor prov in provedores)
                {
                    Decimal disponibilidade = 100;
                    Decimal somatorioDisp = 0;
                    String provedor = prov.Id.ToString();
                    String servico = "";
                    int count = 0;
                    foreach (DisponibilidadeMobile dsp in disponibilidadeMobileList)
                    {
                        if (prov.Id == dsp.Associacao.Provedor.Id)
                        {
                            servico = dsp.Associacao.ServicoMobile.Id.ToString();
                            somatorioDisp = somatorioDisp + dsp.Disponibilidade;
                            count = count + 1;
                        }
                    }
                    if (count == 0) { count = 1; }
                    disponibilidade = somatorioDisp / count;
                    mdoList.Add(new Mdo(provedor, servico, String.Format("{0:N2}", disponibilidade).Replace(".",",") + " %"));
                }
                //MDS
                foreach (ServicoMobile serv in servicosMobile)
                {
                    Decimal disponibilidade = 100;
                    Decimal somatorioDisp = 0;
                    String provedor = "";
                    String servico = serv.Id.ToString();
                    int count = 0;
                    foreach (DisponibilidadeMobile dsp in disponibilidadeMobileList)
                    {
                        if (serv.Id == dsp.Associacao.ServicoMobile.Id)
                        {
                            provedor = dsp.Associacao.Provedor.Id.ToString();
                            somatorioDisp = somatorioDisp + dsp.Disponibilidade;
                            count = count + 1;
                        }
                    }
                    if (count == 0) { count = 1; }
                    disponibilidade = somatorioDisp / count;
                    mdsList.Add(new Mds(provedor, servico, String.Format("{0:N2}", disponibilidade).Replace(".", ",") + " %"));
                }
            }
            else
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='É obrigatório especificar a data de Início e Fim!';</script>";
            }



            //sequencia fix

            List<String> SequenciaServicosMobile = new List<string>();
            List<ServicoMobile> servicosMobileSequencedLast = new List<ServicoMobile>();
            SequenciaServicosMobile.Add("CREDELEC");
            SequenciaServicosMobile.Add("DSTV / GOTV");
            SequenciaServicosMobile.Add("ZAP TV");
            SequenciaServicosMobile.Add("STARTIMES TV");
            SequenciaServicosMobile.Add("IZI / USSD");
            SequenciaServicosMobile.Add("SMART IZI");
            SequenciaServicosMobile.Add("Notificações");
            SequenciaServicosMobile.Add("Recargas Automaticas");
            SequenciaServicosMobile.Add("SIM SWAP");
            List<ServicoMobile> servicosMobileSequenced = new List<ServicoMobile>();
            foreach (String nome in SequenciaServicosMobile)
            {
                foreach (ServicoMobile servico in servicosMobile)
                {
                    if(servico.Nome == nome)
                    {
                        servicosMobileSequenced.Add(servico);
                    }
                }
            }

            foreach (ServicoMobile serv in servicosMobile)
            {
                if(servicosMobileSequenced.Find(x => x == serv) == null)
                {
                    servicosMobileSequencedLast.Add(serv);
                }
            }

            servicosMobileSequenced.AddRange(servicosMobileSequencedLast);
            Dictionary<String, String> cores = new Dictionary<string, string>();

            List<String> sequenciaProvedores = new List<string>();
            sequenciaProvedores.Add("MCEL");
            cores.Add("MCEL", "#fceb05");
            sequenciaProvedores.Add("VODACOM");
            cores.Add("VODACOM", "#fc3605");
            sequenciaProvedores.Add("MOVITEL");
            cores.Add("MOVITEL", "#fca905");
            sequenciaProvedores.Add("Multi-choice");
            cores.Add("Multi-choice", "#66a0ff");
            sequenciaProvedores.Add("ZAP");
            cores.Add("ZAP", "#fc057c");
            sequenciaProvedores.Add("STARTIMES");
            cores.Add("STARTIMES", "#d17c0e");
            sequenciaProvedores.Add("EDM");
            cores.Add("EDM", "#fc7805");
            sequenciaProvedores.Add("BIM");
            cores.Add("BIM", "#aa0b46");
            List<Provedor> provedoresSequenced = new List<Provedor>();
            foreach (String nome in sequenciaProvedores)
            {
                foreach (Provedor provedor in provedores)
                {
                    if (provedor.Nome == nome)
                    {
                        provedoresSequenced.Add(provedor);
                    }

                }
            }
            EstatisticaDsiponibilidadeViewModel estatisticaMobile = new EstatisticaDsiponibilidadeViewModel();
            estatisticaMobile.Associacoes = associacoes;
            estatisticaMobile.Provedores = provedoresSequenced;
            estatisticaMobile.DisponibilidadeMobileList = disponibilidadeMobileList;
            estatisticaMobile.MdoList = mdoList;
            estatisticaMobile.MdsList = mdsList;
            estatisticaMobile.ServicosMobile = servicosMobileSequenced;
            estatisticaMobile.IndisponibilidadesList = indisponibilidadeList;
            estatisticaMobile.periodoEspecificado = periodoEspecificado;
            estatisticaMobile.tempoIndsTotal = periodoIndsTotal;
            estatisticaMobile.cores = cores;
            TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenbtn').disabled =false;</script>";
            return View("EstatisticasDisponibilidadeMobile", estatisticaMobile);
        }


        public ActionResult EstatisticasTransacoes2()
        {
            EstatisticaTransacaoViewModel modelView = new EstatisticaTransacaoViewModel();
            List<Transacao> transacoes = new List<Transacao>();
            List<Transacao> transacoesErro = new List<Transacao>();
            Hashtable htSuccess = new Hashtable();
            Hashtable htError = new Hashtable();
            String datain = Request["dataInicio"].ToString();
            String datafm = Request["dataFim"].ToString();
            String periodoEspecificado = datain + " - " + datafm;
            var filterBuilder = Builders<HistoricoMobile>.Filter;
            var filterBuilder2 = Builders<HistoricoMobileError>.Filter;
            if (datain != "" && datafm != "")
            {
                DateTime DataInicio = DateTime.ParseExact(datain, "dd/MM/yyyy",
                 System.Globalization.CultureInfo.InvariantCulture);
                DateTime DataFim = DateTime.ParseExact(datafm, "dd/MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture);
                DataInicio = DataInicio.AddHours((Double)0).AddMinutes((Double)0).AddSeconds((Double)0);
                DataFim = DataFim.AddHours((Double)23).AddMinutes((Double)59).AddSeconds((Double)59);
                //filtro na BD por periodo especificado 
                var filter7 = filterBuilder.Gte(x => x.dataLog, new BsonDateTime(DataInicio)) & filterBuilder.Lte(x => x.dataLog, new BsonDateTime(DataFim));
                List<HistoricoMobile> historicoTransacoes = Context.HistoricoMobile.Find(filter7).ToList();
                var filter8 = filterBuilder2.Gte(x => x.dataLog, new BsonDateTime(DataInicio)) & filterBuilder2.Lte(x => x.dataLog, new BsonDateTime(DataFim));
                List<HistoricoMobileError> historicoTransacoesErro = Context.HistoricoMobileError.Find(filter8).ToList();

                Debug.WriteLine(historicoTransacoes.Count);
                Debug.WriteLine(historicoTransacoesErro.Count);
                //Successfuls Transactions 
                foreach (HistoricoMobile historicoMobile in historicoTransacoes)
                {
                    if(historicoMobile != null)
                    {
                        if (historicoMobile.HistoricoTransacoes != null)
                        {
                            foreach (HistoricoTransacaoMobile historico in historicoMobile.HistoricoTransacoes)
                            {
                                if (historico != null)
                                {
                                    if (htSuccess.ContainsKey(historico.Nome) == true)
                                    {
                                        String old = (String)htSuccess[historico.Nome];
                                        long count = 0;
                                        long.TryParse(old, out count);
                                        count = count + historico.nrTransacoes;
                                        htSuccess[historico.Nome] = count.ToString();
                                    }
                                    else
                                    {
                                        htSuccess[historico.Nome] = historico.nrTransacoes.ToString();
                                    }
                                }
                            }
                        }
                    }

                }


                foreach (var key in htSuccess.Keys)
                {
                    long count = 0;
                    long.TryParse((string)htSuccess[key], out count);
                    transacoes.Add(new Transacao(key.ToString(), count));
                }
                List<Transacao> SortedLis2t = transacoes.OrderByDescending(o => o.nrTransacao).ToList();
                transacoes = SortedLis2t;

                //Transactions with errors
                foreach (HistoricoMobileError historicoMobile in historicoTransacoesErro)
                {
                    if (historicoMobile != null)
                    {
                        if (historicoMobile.HistoricoTransacoes != null)
                        {
                            foreach (HistoricoTransacaoMobile historico in historicoMobile.HistoricoTransacoes)
                            {
                                if (historico != null)
                                {
                                    if (htError.ContainsKey(historico.Nome) == true)
                                    {
                                        String old = (String)htError[historico.Nome];
                                        long count = 0;
                                        long.TryParse(old, out count);
                                        count = count + historico.nrTransacoes;
                                        htError[historico.Nome] = count.ToString();
                                    }
                                    else
                                    {
                                        htError[historico.Nome] = historico.nrTransacoes.ToString();
                                    }
                                }
                            }
                        }
                    }
                }


                foreach (var key in htError.Keys)
                {
                    long count = 0;
                    long.TryParse((string)htError[key], out count);
                    transacoesErro.Add(new Transacao(key.ToString(), count));
                }
                List<Transacao> SortedLis2t2 = transacoesErro.OrderByDescending(o => o.nrTransacao).ToList();
                transacoesErro = SortedLis2t2;

                modelView.transacoesComsucesso = transacoes;
                modelView.transacoesSemsucesso = transacoesErro;
                modelView.periodoEspecificado = periodoEspecificado;
            }
                
                return View("EstatisticasTransacoesMobile", modelView);
        }
            private static void AddToDisponibilidadeList(List<Disponibilidade> disponibilidadeList, Indisponibilidade inds, TimeSpan tempInds)
        {
            double minutes = tempInds.TotalMinutes;
            long minutesRounded = (long)Math.Round(tempInds.TotalMinutes);
            List<DateTime> days = new List<DateTime>();
            days.Add(inds.DataInicio);
            days.Add(inds.DataFim);
            disponibilidadeList.Add(new Disponibilidade(inds.Operadora.Id.ToString(), inds.Servico.Id.ToString(), minutesRounded, tempInds, inds.Resolvido, days,inds.DescricaoProblema));
        }

        public ActionResult AssociarServico(String id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GetMobileInfoFromDB();
            ServicoMobile servicoEncontrado = FindServicoMobile(id.ToString());
            if (servicoEncontrado == null)
            {
                return HttpNotFound();
            }
            AssociarServicoListViewModel viewMod = new AssociarServicoListViewModel();
            viewMod.provedoras = provedores;
            viewMod.servicoMobile = servicoEncontrado;
            return PartialView(viewMod);
        }


        [HttpPost]
        public ActionResult AssociarServico2(String id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Provedor provedorEncontrado = FindProvedor(Request[id.ToString()].ToString());
            ServicoMobile servicoAassociar = FindServicoMobile(id.ToString());
            if (provedorEncontrado == null || servicoAassociar == null)
            {
                return HttpNotFound();
            }
            try
            {
                ObjectId genrdID = ObjectId.GenerateNewId();
                Associacao associacao = new Associacao(genrdID, provedorEncontrado, servicoAassociar);
                Context.Associacoes.InsertOne(associacao);
            }
            catch
            {

            }
            GetMobileInfoFromDB();
            GestaoServicosMobileListViewModel gestao = UpdateGestaoInfo();
            return View("GestaoServicosMobile", gestao);
        }

        [HttpPost]
        public ActionResult EditServicoMobile2(String id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (Request["Nome"].ToString() != "")
            {
                try
                {
                    Expression<Func<ServicoMobile, bool>> filter6 = x => x.Id == ObjectId.Parse(id);
                    ServicoMobile returnedObject = FindServicoMobile(id);
                    returnedObject.Nome = Request["Nome"].ToString();
                    Context.ServicosMobile.ReplaceOne(filter6, returnedObject);
                    try
                    {
                        Expression<Func<Indisponibilidade, bool>> filter7 = x => x.Servico.Id == returnedObject.Id;
                        List<Indisponibilidade> objectosEncontrados = Context.Indisponibilidades.Find(filter7).ToList();
                        List<Indisponibilidade> objectosModificados = new List<Indisponibilidade>();
                        foreach (Indisponibilidade objecto in objectosEncontrados)
                        {
                            objecto.Servico = returnedObject;
                            objectosModificados.Add(objecto);
                        }
                        foreach (Indisponibilidade objecto in objectosModificados)
                        {
                            Expression<Func<Indisponibilidade, bool>> filter8 = x => x.Id == objecto.Id;
                            Context.Indisponibilidades.ReplaceOne(filter8, objecto);
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        Expression<Func<Associacao, bool>> filter7 = x => x.ServicoMobile.Id == returnedObject.Id;
                        List<Associacao> objectosEncontrados = Context.Associacoes.Find(filter7).ToList();
                        List<Associacao> objectosModificados = new List<Associacao>();
                        foreach (Associacao objecto in objectosEncontrados)
                        {
                            objecto.ServicoMobile = returnedObject;
                            objectosModificados.Add(objecto);
                        }
                        foreach (Associacao objecto in objectosModificados)
                        {
                            Expression<Func<Associacao, bool>> filter8 = x => x.Id == objecto.Id;
                            Context.Associacoes.ReplaceOne(filter8, objecto);
                        }
                    }
                    catch
                    {
                    }
                }
                catch { }
            }
            else
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='É obrigatório especificar o Nome do Serviço!';</script>";
            }
            GetMobileInfoFromDB();
            GestaoServicosMobileListViewModel gestao = UpdateGestaoInfo();
            return View("GestaoServicosMobile", gestao);
        }
        [HttpPost]
        public ActionResult EditProvedor2(String id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Request["Nome"].ToString() != "")
            {
                try {
                    Expression<Func<Provedor, bool>> filter6 = x => x.Id == ObjectId.Parse(id);
                    Provedor returnedObject = FindProvedor(id);
                    returnedObject.Nome = Request["Nome"].ToString();
                    Context.Provedores.ReplaceOne(filter6, returnedObject);
                    try
                    {
                        Expression<Func<Indisponibilidade, bool>> filter7 = x => x.Operadora.Id == returnedObject.Id;
                        List<Indisponibilidade> objectosEncontrados = Context.Indisponibilidades.Find(filter7).ToList();
                        List<Indisponibilidade> objectosModificados = new List<Indisponibilidade>();
                        foreach (Indisponibilidade objecto in objectosEncontrados)
                        {
                            objecto.Operadora = returnedObject;
                            objectosModificados.Add(objecto);
                        }
                        foreach (Indisponibilidade objecto in objectosModificados)
                        {
                            Expression<Func<Indisponibilidade, bool>> filter8 = x => x.Id == objecto.Id;
                            Context.Indisponibilidades.ReplaceOne(filter8, objecto);
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        Expression<Func<Associacao, bool>> filter7 = x => x.Provedor.Id == returnedObject.Id;
                        List<Associacao> objectosEncontrados = Context.Associacoes.Find(filter7).ToList();
                        List<Associacao> objectosModificados = new List<Associacao>();
                        foreach (Associacao objecto in objectosEncontrados)
                        {
                            objecto.Provedor = returnedObject;
                            objectosModificados.Add(objecto);
                        }
                        foreach (Associacao objecto in objectosModificados)
                        {
                            Expression<Func<Associacao, bool>> filter8 = x => x.Id == objecto.Id;
                            Context.Associacoes.ReplaceOne(filter8, objecto);
                        }
                    }
                    catch
                    {
                    }
                } catch { }
            }
            else
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='É obrigatório especificar o Nome do Provedor!';</script>";
            }
            GetMobileInfoFromDB();
            GestaoServicosMobileListViewModel gestao = UpdateGestaoInfo();
            return View("GestaoServicosMobile", gestao);
        }

        [HttpPost]
        public ActionResult EditCredencial2(Credencial credencial)
        {
            if (credencial.Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (credencial.User != "" && credencial.User != null)
            {
                bool visibilidade = false;
                if (credencial.Visibilidade == true) { visibilidade = true; }
                try
                {
                    Expression<Func<Credencial, bool>> filter5 = (x => x.Id == credencial.Id);
                    credencial.Visibilidade = visibilidade;
                    credencial.DataActualizacao = DateTime.Now;
                    Context.Credenciais.ReplaceOne(filter5, credencial);
                    updateCredecialsOnDependencies(credencial);
                }
                catch { }
            }
            else
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='É obrigatório especificar o User!';</script>";
            }
            credenciaisListViewModel = GetCredenciasFromDB();
            credenciaisListViewModel.temPermissao = false;
            string userName = getCurrentUserName();
            credenciaisListViewModel.temPermissao = VerifyUserAutorization();
            return View("GestaoCredenciais", credenciaisListViewModel);
        }

        private static void updateCredecialsOnDependencies(Credencial credencial)
        {
            try
            {
                Expression<Func<Sonda, bool>> filter6 = x => x.Credencial.Any(z => z.Id == credencial.Id);
                List<Sonda> objectosEncontrados = Context.Sondas.Find(filter6).ToList();
                List<Sonda> objectosModificadas = new List<Sonda>();
                foreach (Sonda objecto in objectosEncontrados)
                {
                    var index = objecto.Credencial.FindIndex(c => c.Id == credencial.Id);
                    credencial.DataActualizacao = DateTime.Now;
                    objecto.Credencial[index] = credencial;
                    objectosModificadas.Add(objecto);
                }
                foreach (Sonda objecto in objectosModificadas)
                {
                    Expression<Func<Sonda, bool>> filter7 = x => x.Id == objecto.Id;
                    Context.Sondas.ReplaceOne(filter7, objecto);
                }
            }
            catch
            {
            }

            try
            {
                Expression<Func<Link, bool>> filter6 = x => x.Credencial.Any(z => z.Id == credencial.Id);
                List<Link> objectosEncontrados = Context.Links.Find(filter6).ToList();
                List<Link> objectosModificadas = new List<Link>();
                foreach (Link objecto in objectosEncontrados)
                {
                    var index = objecto.Credencial.FindIndex(c => c.Id == credencial.Id);
                    credencial.DataActualizacao = DateTime.Now;
                    objecto.Credencial[index] = credencial;
                    objectosModificadas.Add(objecto);
                }
                foreach (Link objecto in objectosModificadas)
                {
                    Expression<Func<Link, bool>> filter7 = x => x.Id == objecto.Id;
                    Context.Links.ReplaceOne(filter7, objecto);
                }
            }
            catch
            {
            }

            try
            {
                Expression<Func<Aplicacao, bool>> filter6 = x => x.Credencial.Any(z => z.Id == credencial.Id);
                List<Aplicacao> objectosEncontrados = Context.Aplicacoes.Find(filter6).ToList();
                List<Aplicacao> objectosModificadas = new List<Aplicacao>();
                foreach (Aplicacao objecto in objectosEncontrados)
                {
                    var index = objecto.Credencial.FindIndex(c => c.Id == credencial.Id);
                    credencial.DataActualizacao = DateTime.Now;
                    objecto.Credencial[index] = credencial;
                    objectosModificadas.Add(objecto);
                }
                foreach (Aplicacao objecto in objectosModificadas)
                {
                    Expression<Func<Aplicacao, bool>> filter7 = x => x.Id == objecto.Id;
                    Context.Aplicacoes.ReplaceOne(filter7, objecto);
                }
            }
            catch
            {
            }
        }

        [HttpPost]
        public ActionResult EditSonda2(EditSondaListViewModel model)
        {
            if (model.Sonda.Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (model.Sonda.Dns != "" && model.Sonda.Dns != null)
            {
                try
                {
                    GetCredenciasFromDB();
                    Expression<Func<Sonda, bool>> filter5 = (x => x.Id == model.Sonda.Id);
                    List<Credencial> credSelecionados = new List<Credencial>();
                    if (model.SelectedOptions != null)
                    {
                        foreach (String id in model.SelectedOptions)
                        {
                            foreach (Credencial cred in credenciaisList)
                            {
                                if (cred.Id == ObjectId.Parse(id))
                                {
                                    credSelecionados.Add(cred);
                                }
                            }
                        }
                    }
                    model.Sonda.Credencial = credSelecionados.ToList();
                    Context.Sondas.ReplaceOne(filter5, model.Sonda);
                    updateSondasOnDependencies(model.Sonda);
                }
                catch { }
            }
            else
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='É obrigatório especificar o DNS!';</script>";
            }
            sondasListViewModel = GetSondasFromDB();
            sondasListViewModel.temPermissao = VerifyUserAutorization();
            return View("GestaoSondas", sondasListViewModel);
        }
        private static void updateSondasOnDependencies(Sonda sonda)
        {
            try
            {
                Expression<Func<Link, bool>> filter6 = x => x.Sonda.Any(z => z.Id == sonda.Id);
                List<Link> objectosEncontrados = Context.Links.Find(filter6).ToList();
                List<Link> objectosModificadas = new List<Link>();
                foreach (Link objecto in objectosEncontrados)
                {
                    var index = objecto.Sonda.FindIndex(c => c.Id == sonda.Id);
                    objecto.Sonda[index] = sonda;
                    objectosModificadas.Add(objecto);
                }
                foreach (Link objecto in objectosModificadas)
                {
                    Expression<Func<Link, bool>> filter7 = x => x.Id == objecto.Id;
                    Context.Links.ReplaceOne(filter7, objecto);
                }
            }
            catch
            {
            }
            try
            {
                Expression<Func<Aplicacao, bool>> filter6 = x => x.Sonda.Any(z => z.Id == sonda.Id);
                List<Aplicacao> objectosEncontrados = Context.Aplicacoes.Find(filter6).ToList();
                List<Aplicacao> objectosModificadas = new List<Aplicacao>();
                foreach (Aplicacao objecto in objectosEncontrados)
                {
                    var index = objecto.Sonda.FindIndex(c => c.Id == sonda.Id);
                    objecto.Sonda[index] = sonda;
                    objectosModificadas.Add(objecto);
                }
                foreach (Aplicacao objecto in objectosModificadas)
                {
                    Expression<Func<Aplicacao, bool>> filter7 = x => x.Id == objecto.Id;
                    Context.Aplicacoes.ReplaceOne(filter7, objecto);
                }
            }
            catch
            {
            }
        }
        [HttpPost]
        public ActionResult EditLink2(EditLinkListViewModel model)
        {
            if (model.Link.Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (model.Link.Endereco != "" && model.Link.Endereco != null)
            {

                try
                {
                    GetCredenciasFromDB();
                    Expression<Func<Link, bool>> filter5 = (x => x.Id == model.Link.Id);
                    List<Credencial> selectedCred = new List<Credencial>();
                    if (model.SelectedOptions != null)
                    {
                        foreach (String id in model.SelectedOptions)
                        {
                            foreach (Credencial cred in credenciaisList)
                            {
                                if (cred.Id == ObjectId.Parse(id))
                                {
                                    selectedCred.Add(cred);
                                }
                            }
                        }
                    }
                    GetSondasFromDB();
                    List<Sonda> selectedSondas = new List<Sonda>();
                    if (model.SelectedOptions2 != null)
                    {
                        foreach (String id in model.SelectedOptions2)
                        {
                            foreach (Sonda cred in sondasList)
                            {
                                if (cred.Id == ObjectId.Parse(id))
                                {
                                    selectedSondas.Add(cred);
                                }
                            }
                        }
                    }

                    model.Link.Credencial = selectedCred.ToList();
                    model.Link.Sonda = selectedSondas.ToList();
                    Context.Links.ReplaceOne(filter5, model.Link);
                }
                catch { }
            }
            else
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='É obrigatório especificar o Endereço!';</script>";
            }
            linksListView = GetLinksFromDB();
            linksListView.temPermissao = VerifyUserAutorization();
            return View("GestaoLinks", linksListView);
        }

        [HttpPost]
        public ActionResult EditAplicacao2(EditAplicacaoListViewModel model)
        {
            if (model.Aplicacao.Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (model.Aplicacao.Nome != "" && model.Aplicacao.Nome != null)
            {

                try
                {
                    GetCredenciasFromDB();
                    Expression<Func<Aplicacao, bool>> filter5 = (x => x.Id == model.Aplicacao.Id);
                    List<Credencial> selectedCred = new List<Credencial>();
                    if (model.SelectedOptions != null)
                    {
                        foreach (String id in model.SelectedOptions)
                        {
                            foreach (Credencial cred in credenciaisList)
                            {
                                if (cred.Id == ObjectId.Parse(id))
                                {
                                    selectedCred.Add(cred);
                                }
                            }
                        }
                    }
                    GetSondasFromDB();
                    List<Sonda> selectedSondas = new List<Sonda>();
                    if (model.SelectedOptions2 != null)
                    {
                        foreach (String id in model.SelectedOptions2)
                        {
                            foreach (Sonda cred in sondasList)
                            {
                                if (cred.Id == ObjectId.Parse(id))
                                {
                                    selectedSondas.Add(cred);
                                }
                            }
                        }
                    }

                    model.Aplicacao.Credencial = selectedCred.ToList();
                    model.Aplicacao.Sonda = selectedSondas.ToList();
                    Context.Aplicacoes.ReplaceOne(filter5, model.Aplicacao);
                    try
                    {
                        Expression<Func<Indisponibilidade, bool>> filter7 = x => x.Aplicacao.Id == model.Aplicacao.Id;
                        List<Indisponibilidade> objectosEncontrados = Context.Indisponibilidades.Find(filter7).ToList();
                        List<Indisponibilidade> objectosModificados = new List<Indisponibilidade>();
                        foreach (Indisponibilidade objecto in objectosEncontrados)
                        {
                            objecto.Aplicacao = model.Aplicacao;
                            objectosModificados.Add(objecto);
                        }
                        foreach (Indisponibilidade objecto in objectosModificados)
                        {
                            Expression<Func<Indisponibilidade, bool>> filter8 = x => x.Id == objecto.Id;
                            Context.Indisponibilidades.ReplaceOne(filter8, objecto);
                        }
                    }
                    catch
                    {
                    }
                }
                catch { }
            }
            else
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt2').innerHTML='Erro..Certifique-se de que preencheu devidamente os campos obrigatórios!';</script>";
            }
            aplicacoesListViewModel = GetAplicacoesFromDB();
            aplicacoesListViewModel.temPermissao = VerifyUserAutorization();
            return View("GestaoAplicacoes", aplicacoesListViewModel);
        }


        [HttpPost]
        public ActionResult EditCartaoSIM2(EditCartaoSIMListViewModel model)
        {
            if (model.cartaoSIM.Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            String data = Request["dataRegisto"].ToString();
            DateTime DataRegisto = DateTime.ParseExact(data, "d/M/yyyy HH:mm",
                   System.Globalization.CultureInfo.InvariantCulture);
            model.cartaoSIM.DataRegisto = DataRegisto;

            if (model.cartaoSIM.Numero != null && model.cartaoSIM.DataRegisto != null && model.cartaoSIM.Operadora != null)
            {
                try
                {
                    GetSondasFromDB();
                    List<Sonda> selectedSondas = new List<Sonda>();
                    if (model.SelectedOptions2 != null)
                    {
                        foreach (String id in model.SelectedOptions2)
                        {
                            foreach (Sonda cred in sondasList)
                            {
                                if (cred.Id == ObjectId.Parse(id))
                                {
                                    selectedSondas.Add(cred);
                                }
                            }
                        }
                    }

                    model.cartaoSIM.Sonda = selectedSondas.ToList();
                    Expression<Func<CartaoSIM, bool>> filter5 = (x => x.Id == model.cartaoSIM.Id);
                    ReplaceOneResult replaceReult = Context.cartoesSIM.ReplaceOne(filter5, model.cartaoSIM);
                }
                catch { }
            }
            else
            {
                Debug.WriteLine("Missing required fields");
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='Erro..Certifique-se de que preencheu devidamente os campos obrigatórios!';</script>";
            }
            cartoesSIMListViewModel = GetCartoesSIMFromDB();
            cartoesSIMListViewModel.temPermissao = VerifyUserAutorization();
            if (cartoesSIMList != null)
            {
                TempData["showmsg2"] = "<script type='text/javascript'> document.getElementById('hidenbtn').disabled =false;</script>";
            }
            return View("GestaoCartaoSIM", cartoesSIMListViewModel);
        }

        [HttpPost]
        public ActionResult ClonarAplicacao2(EditAplicacaoListViewModel model)
        {
            if (model.Aplicacao.Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (model.Aplicacao.Nome != "" && model.Aplicacao.Nome != null)
            {

                try {
                    GetCredenciasFromDB();
                    List<Credencial> selectedCred = new List<Credencial>();
                    if (model.SelectedOptions != null)
                    {
                        foreach (String id in model.SelectedOptions)
                        {
                            foreach (Credencial cred in credenciaisList)
                            {
                                if (cred.Id == ObjectId.Parse(id))
                                {
                                    selectedCred.Add(cred);
                                }
                            }
                        }
                    }
                    GetSondasFromDB();
                    List<Sonda> selectedSondas = new List<Sonda>();
                    if (model.SelectedOptions2 != null)
                    {
                        foreach (String id in model.SelectedOptions2)
                        {
                            foreach (Sonda cred in sondasList)
                            {
                                if (cred.Id == ObjectId.Parse(id))
                                {
                                    selectedSondas.Add(cred);
                                }
                            }
                        }
                    }
                    model.Aplicacao.Credencial = selectedCred.ToList();
                    model.Aplicacao.Sonda = selectedSondas.ToList();
                    model.Aplicacao.Id = ObjectId.GenerateNewId();
                    Context.Aplicacoes.InsertOne(model.Aplicacao);
                }
                catch {
                }
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt2').innerHTML='Aplicação (clonada) adicionada com sucesso!';</script>";
            }
            else
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt2').innerHTML='Erro..Certifique-se de que preencheu devidamente os campos obrigatórios!';</script>";
            }
            aplicacoesListViewModel = GetAplicacoesFromDB();
            aplicacoesListViewModel.temPermissao = VerifyUserAutorization();
            return View("GestaoAplicacoes", aplicacoesListViewModel);
        }

        [HttpPost]
        public ActionResult ClonarCartaoSIM2(EditCartaoSIMListViewModel model)
        {
            if (model.cartaoSIM.Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            String data = Request["dataRegisto"].ToString();
            DateTime DataRegisto = DateTime.ParseExact(data, "d/M/yyyy HH:mm",
                   System.Globalization.CultureInfo.InvariantCulture);
            model.cartaoSIM.DataRegisto = DataRegisto;

            if (model.cartaoSIM.Numero != null && model.cartaoSIM.Operadora != null && model.cartaoSIM.DataRegisto != null)
            {

                try
                {
                    GetSondasFromDB();
                    List<Sonda> selectedSondas = new List<Sonda>();
                    if (model.SelectedOptions2 != null)
                    {
                        foreach (String id in model.SelectedOptions2)
                        {
                            foreach (Sonda cred in sondasList)
                            {
                                if (cred.Id == ObjectId.Parse(id))
                                {
                                    selectedSondas.Add(cred);
                                }
                            }
                        }
                    }
                    model.cartaoSIM.Sonda = selectedSondas.ToList();
                    model.cartaoSIM.Id = ObjectId.GenerateNewId();
                    Context.cartoesSIM.InsertOne(model.cartaoSIM);
                }
                catch
                {
                }
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='Cartão SIM (clonado) adicionado com sucesso!';</script>";
            }
            else
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='Erro..Certifique-se de que preencheu devidamente os campos obrigatórios!';</script>";
            }
            cartoesSIMListViewModel = GetCartoesSIMFromDB();
            cartoesSIMListViewModel.temPermissao = VerifyUserAutorization();
            if (cartoesSIMList != null)
            {
                TempData["showmsg2"] = "<script type='text/javascript'> document.getElementById('hidenbtn').disabled =false;</script>";
            }
            return View("GestaoCartaoSIM", cartoesSIMListViewModel);
        }
        [HttpPost]
        public ActionResult EditEquipa2(String id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Request["Nome"].ToString() != "")
            {
                Expression<Func<Equipa, bool>> filter6 = x => x.Id == ObjectId.Parse(id);
                Equipa returnedObject = FindEquipa(id);
                returnedObject.Nome = Request["Nome"].ToString();
                Context.Equipas.ReplaceOne(filter6, returnedObject);
                UpdateEquipasonContactos(returnedObject);
            }
            else
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='Erro..Certifique-se de que preencheu devidamente os campos obrigatórios!';</script>";
            }
           
            equipasviewModel = GetEquipasFromDB();
            List<Equipa> SortedLis2t = equipasList.OrderBy(o => o.Nome).ToList();
            equipasList = SortedLis2t;
            equipasviewModel.equipas = equipasList;
            return View("GestaoEquipas", equipasviewModel);
        }

        private void UpdateEquipasonContactos(Equipa equipa)
        {
            try
            {
                Expression<Func<Contacto, bool>> filter6 = x => x.IDEquipa == equipa.Id;
                List<Contacto> objectosEncontrados = Context.Contactos.Find(filter6).ToList();
                List<Contacto> objectosModificadas = new List<Contacto>();
                foreach (Contacto objecto in objectosEncontrados)
                {
                    objecto.IDEquipa = equipa.Id;
                    objecto.Equipa = equipa.Nome;
                    objectosModificadas.Add(objecto);
                }
                foreach (Contacto objecto in objectosModificadas)
                {
                    Expression<Func<Contacto, bool>> filter7 = x => x.Id == objecto.Id;
                    Context.Contactos.ReplaceOne(filter7, objecto);
                }
            }
            catch
            {
            }
        }

        [HttpPost]
        public ActionResult EditTemplate2(EditTemplateListViewModel model)
        {
            if (model.template.Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (model.template.NomeTemplate != "")
            {
                Expression<Func<Template, bool>> filter5 = (x => x.Id == model.template.Id);
                Template modifyT = Context.Templates.Find(filter5).FirstOrDefault();
                try
                {
                    modifyT.NomeTemplate = model.template.NomeTemplate;
                    GetContactosPortalBD();
                    GetEquipasFromDB();
                    modifyT.Contactos = new List<Contacto>();
                    List<Equipa> equipasToTemplate = new List<Equipa>();
                    foreach (String id in model.SelectedOptions.ToList())
                    {
                        foreach (Contacto contacto in contactosList.ToList())
                        {
                            if (contacto.Id == ObjectId.Parse(id))
                            {
                                modifyT.Contactos.Add(contacto);
                            }
                            bool exist2 = false;
                                foreach (Equipa equ in equipasToTemplate)
                                {
                                    if (equ.Id == contacto.IDEquipa)
                                    {
                                        exist2 = true;
                                    }

                                }
                                if (exist2 == false)
                                {
                                    equipasToTemplate.Add(FindEquipa(contacto.IDEquipa.ToString()));
                                }
                        }

                    }
                    modifyT.Equipas = equipasToTemplate;
                    Context.Templates.ReplaceOne(filter5, modifyT);
                }
                catch
                {
                }
            }
            else
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='Erro..Certifique-se de que preencheu devidamente os campos obrigatórios!';</script>";
            }
            GetTemplatesFromBD();
            templateview = GetTemplatesFromBD();
            return View("GestaoTemplates", templateview);
        }

        [HttpPost]
        public ActionResult AddServicoMobile2()
        {
            if (Request["frmNome"].ToString() != "")
            {
                ObjectId Id = ObjectId.GenerateNewId();
                Context.ServicosMobile.InsertOne(new ServicoMobile(Id, Request["frmNome"].ToString()));
            }
            else
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='Erro..Certifique-se de que preencheu devidamente os campos obrigatórios!';</script>";
            }
            GetMobileInfoFromDB();
            GestaoServicosMobileListViewModel gestao = UpdateGestaoInfo();
            return View("GestaoServicosMobile", gestao);
        }

        [HttpPost]
        public ActionResult AddProvedor2()
        {
            if (Request["frmNome"].ToString() != "")
            {
                ObjectId Id = ObjectId.GenerateNewId();
                Context.Provedores.InsertOne(new Provedor(Id, Request["frmNome"].ToString()));
            }
            else
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='Erro..Certifique-se de que preencheu devidamente os campos obrigatórios!';</script>";
            }
            GetMobileInfoFromDB();
            GestaoServicosMobileListViewModel gestao = UpdateGestaoInfo();
            return View("GestaoServicosMobile", gestao);
        }

        [HttpPost]
        public ActionResult AddEquipa2()
        {
            if (Request["frmNome"].ToString() != "")
            {
                ObjectId Id = ObjectId.GenerateNewId();
                Context.Equipas.InsertOne(new Equipa(Id, Request["frmNome"].ToString()));
            }
            else
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='Erro..Certifique-se de que preencheu devidamente os campos obrigatórios!';</script>";
            }
            equipasviewModel = GetEquipasFromDB();
           
            return View("GestaoEquipas", equipasviewModel);
        }

        [HttpPost]
        public ActionResult AddGrupo2(AddGrupoListViewModel model)
        {
            if (Request["frmNome"].ToString() != "")
            {
                Grupo grupo = new Grupo();
                grupo.Id = ObjectId.GenerateNewId();
                grupo.NomeGrupo = Request["frmNome"].ToString();
                grupo.Equipas = new List<Equipa>();
                GetEquipasFromDB();
                List<String> equipassemcontactos = new List<String>();
                if (model.SelectedOptions != null)
                {
                    foreach (String id in model.SelectedOptions)
                    {
                        
                        Equipa equipa = FindEquipa(id);
                        if(equipa.Id != null)
                        {
                            grupo.Equipas.Add(equipa);
                        }
                       
                    }
                }
                try { Context.Grupos.InsertOne(grupo);} catch { }
                
            }
            else
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='Erro..Certifique-se de que preencheu devidamente os campos obrigatórios!';</script>";
                return View("GestaoEquipas", GetEquipasFromDB());
            }
            return View("GestaoEquipas", GetEquipasFromDB());
        }

        [HttpPost]
        public ActionResult EditGrupo2(EditGrupoListViewModel model)
        {
            if (model.grupo.NomeGrupo != "")
            {
                Expression<Func<Grupo, bool>> filter5 = (x => x.Id == model.grupo.Id);
                Grupo modifyT = Context.Grupos.Find(filter5).FirstOrDefault();
                try
                {
                    modifyT.NomeGrupo = model.grupo.NomeGrupo;
                    GetEquipasFromDB();
                    modifyT.Equipas = new List<Equipa>();
                    foreach (String id in model.SelectedOptions.ToList())
                    {
                        foreach (Equipa equipa in equipasList.ToList())
                        {
                            if (equipa.Id == ObjectId.Parse(id))
                            {
                                modifyT.Equipas.Add(equipa);
                            }
                        }
                    }
                    Context.Grupos.ReplaceOne(filter5, modifyT);
                }
                catch
                {
                }
            }
            else
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='Erro..Certifique-se de que preencheu devidamente os campos obrigatórios!';</script>";
                return View("GestaoEquipas", GetEquipasFromDB());
            }
            return View("GestaoEquipas", GetEquipasFromDB());
        }


        [HttpPost]
        public ActionResult AddTemplate2(AddTemplateListViewModel model)
        {
            String templateName = "";
            if (Request["frmNome"].ToString() != "" && model.selectedEntidade != null)
            {
                templateName = model.selectedEntidade + "_" + Request["frmNome"].ToString();
                ObjectId Id = ObjectId.GenerateNewId();
                GetContactosPortalBD();
                GetEquipasFromDB();
                List<Contacto> selectedContacts = new List<Contacto>();
                List<String> equipassemcontactos = new List<String>();
                List<Equipa> equipasToTemplate = new List<Equipa>();
                if (model.SelectedEquipasOptions != null)
                {
                    foreach (String id in model.SelectedEquipasOptions)
                    {
                        List<Contacto> selectedContactsTemp = new List<Contacto>();
                        Equipa equipa = FindEquipa(id);
                        foreach (Contacto contacto in contactosList)
                        {
                            if (contacto.Equipa == equipa.Nome)
                            {
                                selectedContactsTemp.Add(contacto);
                            }
                        }
                        if (selectedContactsTemp.Count == 0)
                        {
                            equipassemcontactos.Add(equipa.Nome);
                        }

                        selectedContacts.AddRange(selectedContactsTemp);
                    }
                    String equpas = "";
                    foreach (String eq in equipassemcontactos)
                    {
                        if (equipassemcontactos.Count == 1)
                        {
                            equpas += eq;
                        }
                        else
                        {
                            int peneultimo = equipassemcontactos.Count() - 2;
                            if (equipassemcontactos.First() == eq)
                            {
                                equpas += eq + ", ";
                            }
                            else if (equipassemcontactos.ElementAt(peneultimo) == eq)
                            {
                                equpas += eq;
                            }
                            else if (equipassemcontactos.Last() == eq)
                            {
                                equpas += " e " + eq;
                            }
                        }
                    }
                    if (equpas != "")
                    {
                        TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='A(s) equipa(s) " + equpas + " selecionada(s) não tem contactos associados !';</script>";
                    }
                }
                if (model.SelectedOptions != null)
                {
                    foreach (String id in model.SelectedOptions)
                    {
                        foreach (Contacto contacto in contactosList)
                        {
                            bool exist = false;
                            foreach (Contacto contacto2 in selectedContacts)
                            {
                                if (contacto.Id == contacto2.Id)
                                {
                                    exist = true;
                                }
                            }
                            if (contacto.Id == ObjectId.Parse(id) && exist == false)
                            {
                                selectedContacts.Add(contacto);
                            }
                        }
                    }
                }

                if (model.SelectedGruposOptions != null)
                {

                    Debug.WriteLine("Not null");
                    foreach (String id in model.SelectedGruposOptions)
                    {
                        Debug.WriteLine(id);
                        foreach (Grupo grupo in gruposList)
                        {
                            if(grupo.Id == ObjectId.Parse(id))
                            {
                                Debug.WriteLine("match with "+ grupo.Id.ToString());
                                foreach (Equipa equipa in grupo.Equipas){
                                    Debug.WriteLine(grupo.Equipas.Count +" contactos to Add");
                                    foreach (Contacto contacto in contactosList)
                                    {
                                        if (contacto.IDEquipa == equipa.Id)
                                        {
                                            Debug.WriteLine(contacto.Nome + " pertence ao grupo");
                                            bool exist = false;
                                            foreach (Contacto contacto2 in selectedContacts)
                                            {
                                                if (contacto.Id == contacto2.Id)
                                                {
                                                    exist = true;
                                                }
                                            }
                                            if (exist == false)
                                            {
                                                selectedContacts.Add(contacto);
                                            }
                                            bool exist2 = false;
                                            foreach (Equipa equ in equipasToTemplate)
                                            {
                                                if(equ.Id == equipa.Id)
                                                {
                                                    exist2 = true;
                                                }

                                            }
                                            if(exist2 == false)
                                            {
                                                equipasToTemplate.Add(equipa);
                                            }
                                           
                                        }

                                    }

                                }
                            }
                        }
                    }
                }

                if (selectedContacts.Count == 0)
                {
                    TempData["showmsg2"] = "<script type='text/javascript'> document.getElementById('hidenatt4').style.display='block'; document.getElementById('hidenatt4').innerHTML='Erro! Nenhum contacto selecionado!';</script>";
                    return View("GestaoTemplates", templateview);
                }
                try { Context.Templates.InsertOne(new Template(Id, templateName, selectedContacts, equipasToTemplate)); } catch { }
               
            }
            else
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='Erro..Certifique-se de que preencheu devidamente os campos obrigatórios!';</script>";
                return View("GestaoTemplates", templateview);
            }
            GetTemplatesFromBD();
            return View("GestaoTemplates", templateview);
        }

        [HttpPost]
        public ActionResult AddCredencial2()
        {
            if (Request["frmUser"].ToString() != "")
            {
                ObjectId Id = ObjectId.GenerateNewId();

                bool visibilidade = false;
                if (Request["frmvisibilidade"].ToString() == "true") { visibilidade = true; }
                Context.Credenciais.InsertOne(new Credencial(Id, Request["frmUser"].ToString(), Request["frmPassword"].ToString(), Request["frmDescricao"].ToString(), DateTime.Now, visibilidade));
            }
            else
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='Erro..Certifique-se de que preencheu devidamente os campos obrigatórios!';</script>";
            }
            GetCredenciasFromDB();
            credenciaisListViewModel.temPermissao = false;
            credenciaisListViewModel.temPermissao = VerifyUserAutorization();
            return View("GestaoCredenciais", credenciaisListViewModel);
        }

        [HttpPost]
        public ActionResult AddSonda2(AddSondaListViewModel model)
        {
            if (Request["frmDns"].ToString() != "")
            {
                ObjectId Id = ObjectId.GenerateNewId();

                GetCredenciasFromDB();
                List<Credencial> selectedCred = new List<Credencial>();
                if (model.SelectedOptions != null)
                {
                    foreach (String id in model.SelectedOptions)
                    {
                        foreach (Credencial cred in credenciaisList)
                        {
                            if (cred.Id == ObjectId.Parse(id))
                            {
                                selectedCred.Add(cred);
                            }
                        }
                    }
                }
                Context.Sondas.InsertOne(new Sonda(Id, Request["frmDns"].ToString(), Request["frmIP"].ToString(), Request["frmSistemaOp"].ToString(), selectedCred, Request["frmDescricao"].ToString()));
            }
            else
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='Erro..Certifique-se de que preencheu devidamente os campos obrigatórios!';</script>";
            }
            GetSondasFromDB();
            sondasListViewModel.temPermissao = VerifyUserAutorization();
            return View("GestaoSondas", sondasListViewModel);
        }

        [HttpPost]
        public ActionResult AddLink2(AddLinkListViewModel model)
        {
            if (Request["frmendereco"].ToString() != "")
            {
                ObjectId Id = ObjectId.GenerateNewId();

                GetCredenciasFromDB();
                List<Credencial> selectedCred = new List<Credencial>();
                if (model.SelectedOptions != null)
                {
                    foreach (String id in model.SelectedOptions)
                    {
                        foreach (Credencial cred in credenciaisList)
                        {
                            if (cred.Id == ObjectId.Parse(id))
                            {
                                selectedCred.Add(cred);
                            }
                        }
                    }
                }
                GetSondasFromDB();
                List<Sonda> selectedSondas = new List<Sonda>();
                if (model.SelectedOptions2 != null)
                {
                    foreach (String id in model.SelectedOptions2)
                    {
                        foreach (Sonda cred in sondasList)
                        {
                            if (cred.Id == ObjectId.Parse(id))
                            {
                                selectedSondas.Add(cred);
                            }
                        }
                    }
                }
                Context.Links.InsertOne(new Link(Id, Request["frmendereco"].ToString(), Request["frmDescricao"].ToString(), selectedCred, selectedSondas));
            }
            else
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='Erro..Certifique-se de que preencheu devidamente os campos obrigatórios!';</script>";
            }
            GetLinksFromDB();
            linksListView.temPermissao = VerifyUserAutorization();
            return View("GestaoLinks", linksListView);
        }

        [HttpPost]
        public ActionResult AddAplicacao2(AddAplicacaoListViewModel model)
        {
            if (Request["frmNome"].ToString() != "")
            {
                ObjectId Id = ObjectId.GenerateNewId();

                GetCredenciasFromDB();
                List<Credencial> selectedCred = new List<Credencial>();
                if (model.SelectedOptions != null)
                {
                    foreach (String id in model.SelectedOptions)
                    {
                        foreach (Credencial cred in credenciaisList)
                        {
                            if (cred.Id == ObjectId.Parse(id))
                            {
                                selectedCred.Add(cred);
                            }
                        }
                    }
                }
                GetSondasFromDB();
                List<Sonda> selectedSondas = new List<Sonda>();
                if (model.SelectedOptions2 != null)
                {
                    foreach (String id in model.SelectedOptions2)
                    {
                        foreach (Sonda cred in sondasList)
                        {
                            if (cred.Id == ObjectId.Parse(id))
                            {
                                selectedSondas.Add(cred);
                            }
                        }
                    }
                }

                Context.Aplicacoes.InsertOne(new Aplicacao(Id, Request["frmNome"].ToString(), Request["frmPreRequisito"].ToString(), selectedCred, Request["frmPerfil"].ToString(), Request["frmendereco"].ToString(), Request["frmsuporte"].ToString(), selectedSondas));
            }
            else
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt2').style.display='block'; document.getElementById('hidenatt3').innerHTML='Erro..Certifique-se de que preencheu devidamente os campos obrigatórios!';</script>";
            }
            GetAplicacoesFromDB();
            aplicacoesListViewModel.temPermissao = VerifyUserAutorization();
            return View("GestaoAplicacoes", aplicacoesListViewModel);
        }

        [HttpPost]
        public ActionResult AddCartaoSIM2(AddCartaoSIMListViewModel model)
        {
            if (Request["frmNumero"].ToString() != "" && Request["dataRegisto"].ToString() != "" && Request["frmOperadora"].ToString() != "")
            {
                ObjectId Id = ObjectId.GenerateNewId();
                GetSondasFromDB();
                String data = Request["dataRegisto"].ToString();
                DateTime DataRegisto = DateTime.ParseExact(data, "d/M/yyyy HH:mm",
                       System.Globalization.CultureInfo.InvariantCulture);
                List<Sonda> selectedSondas = new List<Sonda>();
                if (model.SelectedOptions2 != null)
                {
                    foreach (String id in model.SelectedOptions2)
                    {
                        foreach (Sonda cred in sondasList)
                        {
                            if (cred.Id == ObjectId.Parse(id))
                            {
                                selectedSondas.Add(cred);
                            }
                        }
                    }
                }

                Context.cartoesSIM.InsertOne(new CartaoSIM(Id, Request["frmNumero"].ToString(), Request["frmOperadora"].ToString(), DataRegisto, Request["frmFuncao"].ToString(), selectedSondas));
            }
            else
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='Erro..Certifique-se de que preencheu devidamente os campos obrigatórios!';</script>";
            }
            cartoesSIMListViewModel = GetCartoesSIMFromDB();
            cartoesSIMListViewModel.temPermissao = VerifyUserAutorization();
            if (cartoesSIMList != null)
            {
                TempData["showmsg2"] = "<script type='text/javascript'> document.getElementById('hidenbtn').disabled =false;</script>";
            }
            return View("GestaoCartaoSIM", cartoesSIMListViewModel);
        }


        public ActionResult AddTemplate()
        {
            AddTemplateListViewModel addView = new AddTemplateListViewModel();
            List<String> servicosToSelect = new List<String>();
            contactosviewModel = GetContactosPortalBD();
            equipasviewModel = GetEquipasFromDB();
            GetAplicacoesFromDB();
            GetMobileInfoFromDB();
            foreach (ServicoMobile serv in servicosMobile)
            {
                servicosToSelect.Add(serv.Nome);
            }
            foreach (Aplicacao app in aplicacoesList)
            {
                servicosToSelect.Add(app.Nome);
            }
            addView.servicos = servicosToSelect;
            addView.entidades = provedores;
            addView.Contactos = contactosList;
            addView.equipas = equipasList;
            addView.grupos = gruposList;
            List<String> lista = new List<String>();
            addView.SelectedOptions = lista;
            addView.SelectedEquipasOptions = lista;
            return PartialView(addView);
        }
        public ActionResult AddCredencial()
        {
            return PartialView();
        }

        public ActionResult AddSonda()
        {
            credenciaisListViewModel = GetCredenciasFromDB();
            AddSondaListViewModel addview = new AddSondaListViewModel();
            addview.credenciais = credenciaisList;
            List<String> lista = new List<String>();
            addview.SelectedOptions = lista;
            return PartialView(addview);
        }
        public ActionResult AddLink()
        {
            credenciaisListViewModel = GetCredenciasFromDB();
            sondasListViewModel = GetSondasFromDB();
            AddLinkListViewModel addview = new AddLinkListViewModel();
            linksListView = GetLinksFromDB();
            addview.credenciais = credenciaisList;
            addview.sondas = sondasList;
            List<String> lista = new List<String>();
            addview.SelectedOptions = lista;
            addview.SelectedOptions2 = lista;
            return PartialView(addview);
        }
        public ActionResult AddAplicacao()
        {
            credenciaisListViewModel = GetCredenciasFromDB();
            sondasListViewModel = GetSondasFromDB();
            AddAplicacaoListViewModel addview = new AddAplicacaoListViewModel();
            aplicacoesListViewModel = GetAplicacoesFromDB();
            addview.credenciais = credenciaisList;
            addview.sondas = sondasList;
            List<String> lista = new List<String>();
            addview.SelectedOptions = lista;
            addview.SelectedOptions2 = lista;
            return PartialView(addview);
        }
        public ActionResult AddCartaoSIM()
        {
            sondasListViewModel = GetSondasFromDB();
            AddCartaoSIMListViewModel addview = new AddCartaoSIMListViewModel();
            List<String> lista = new List<String>();
            addview.SelectedOptions2 = lista;
            addview.sondas = sondasList;
            return PartialView(addview);
        }
        public ActionResult AddProvedor()
        {
            return PartialView();
        }
        public ActionResult AddEquipa()
        {
            return PartialView();
        }
        public ActionResult AddGrupo()
        {
            GetEquipasFromDB();
            AddGrupoListViewModel view = new AddGrupoListViewModel();
            List<String> selectedOptions = new List<string>();
            view.equipas = equipasList;
            view.SelectedOptions = selectedOptions;
            return PartialView(view);
        }
        public ActionResult GestaoEquipas()
        {
            contactosviewModel = GetContactosPortalBD();
            equipasviewModel = GetEquipasFromDB();
            List<Equipa> SortedLis2t = equipasList.OrderBy(o => o.Nome).ToList();
            equipasList = SortedLis2t;
            equipasviewModel.equipas = equipasList;
            return View(equipasviewModel);
        }
        public ActionResult GestaoServicosMobile()
        {
            GestaoServicosMobileListViewModel gestao = UpdateGestaoInfo();
            return View(gestao);
        }

        private GestaoServicosMobileListViewModel UpdateGestaoInfo()
        {
            GetMobileInfoFromDB();
            GestaoServicosMobileListViewModel gestao = new GestaoServicosMobileListViewModel();
            associacoes = Context.Associacoes.Find(new BsonDocument()).ToList();
            List<Associacao> SortedLis2t = associacoes.OrderBy(o => o.Provedor.Nome).ToList();
            associacoes = SortedLis2t;
            gestao.Associacoes = associacoes;
            provedores = Context.Provedores.Find(new BsonDocument()).ToList();
            List<Provedor> SortedLis3t = provedores.OrderBy(o => o.Nome).ToList();
            provedores = SortedLis3t;
            gestao.Provedores = provedores;
            servicosMobile = Context.ServicosMobile.Find(new BsonDocument()).ToList();
            List<ServicoMobile> SortedLis4t = servicosMobile.OrderBy(o => o.Nome).ToList();
            servicosMobile = SortedLis4t;
            gestao.ServicosMobile = servicosMobile;
            return gestao;
        }

        private void GetMobileInfoFromDB()
        {
            provedores = Context.Provedores.Find(new BsonDocument()).ToList();
            servicosMobile = Context.ServicosMobile.Find(new BsonDocument()).ToList();
            associacoes = Context.Associacoes.Find(new BsonDocument()).ToList();
            IndisponibilidadeList = Context.Indisponibilidades.Find(new BsonDocument()).ToList();
        }

        public ActionResult RegistoIndisponibilidade()
        {
            GetMobileInfoFromDB();
            GetAplicacoesFromDB();
            IndisponibilidadesListViewModel registroIndisp = GetIndisponibilidadeFromDB();
            return View(registroIndisp);
        }

        private IndisponibilidadesListViewModel GetIndisponibilidadeFromDB()
        {
            IndisponibilidadesListViewModel registroIndisp = new IndisponibilidadesListViewModel();
            IndisponibilidadeResolvidosList = new List<Indisponibilidade>();
            IndisponibilidadePendentesList = new List<Indisponibilidade>();

            foreach (Indisponibilidade dsp in IndisponibilidadeList)
            {
                if (dsp.Resolvido == true)
                {
                    IndisponibilidadeResolvidosList.Add(dsp);
                }
                if (dsp.Resolvido == false)
                {
                    IndisponibilidadePendentesList.Add(dsp);
                }
            }
            List<Indisponibilidade> SortedLis2t = IndisponibilidadeResolvidosList.OrderByDescending(o => o.DataInicio).ToList();
            IndisponibilidadeResolvidosList = SortedLis2t;
            List<Indisponibilidade> SortedLis3t = IndisponibilidadePendentesList.OrderByDescending(o => o.DataInicio).ToList();
            IndisponibilidadePendentesList = SortedLis3t;
            registroIndisp.inidsponibilidadesPendentes = IndisponibilidadePendentesList;
            registroIndisp.inidsponibilidadesResolvidas = IndisponibilidadeResolvidosList;
            return registroIndisp;
        }

        [HttpPost]
        public ActionResult RemoveIndisponibilidade(ObjectId id)
        {
            //DeleteResult delresult = colNews.DeleteOne(filter);
            try
            {
                Expression<Func<Indisponibilidade, bool>> filter = x => x.Id == id;
                Context.Indisponibilidades.DeleteOne(filter);
            }
            catch
            {
            }
            GetAplicacoesFromDB();
            GetMobileInfoFromDB();
            return View("RegistoIndisponibilidade", GetIndisponibilidadeFromDB());
        }
        [HttpPost]
        public ActionResult EditarIndisponibilidade2(ObjectId id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indisponibilidade indisponibilidadeEncontrado = FindIndisponibilidade(id.ToString());

            if (indisponibilidadeEncontrado == null)
            {
                return HttpNotFound();
            }
            if (indisponibilidadeEncontrado != null)
            {
                bool Resolvido = false;

                if (Request["indisponibilidade.Resolvido"].ToString() == "true")
                {
                    Resolvido = true;
                }
                if (Request["indisponibilidade.Resolvido"].ToString() == "false")
                {
                    Resolvido = false;
                }
                String Servico = "";
                String aplicacao = "";
                String datain = Request["DataInicio"].ToString();
                String datafm = Request["DataFim"].ToString();
                String Operadora = Request["indisponibilidade.Operadora.Id"].ToString();
                try
                {
                    Servico = Request["indisponibilidade.Servico.Id"].ToString();
                }
                catch
                {
                }
                try
                {
                    aplicacao = Request["indisponibilidade.Aplicacao.Id"].ToString();
                }
                catch
                {
                }
                String DescricaoProblema = Request["indisponibilidade.DescricaoProblema"].ToString();
                Provedor provedor = FindProvedor(Operadora);
                ServicoMobile servicoMobile = FindServicoMobile(Servico);
                Aplicacao aplicacaoEncontrada = FindAplicacao(aplicacao);
                if (Resolvido == true)
                {
                    if (provedor == null && servicoMobile == null && aplicacaoEncontrada == null)
                    {
                        return HttpNotFound();
                    }
                else if (datain != "" && datafm != "")
                    {
                        DateTime DataInicio = DateTime.ParseExact(datain, "dd/MM/yyyy HH:mm",
                        System.Globalization.CultureInfo.InvariantCulture);
                        DateTime DataFim = DateTime.ParseExact(datafm, "dd/MM/yyyy HH:mm",
                        System.Globalization.CultureInfo.InvariantCulture);
                        String userName = getCurrentUserName();
                        if (Servico == "")
                        {
                            EditIndisponibilidadeOnBD(id, DataInicio, DataFim, Resolvido, provedor, null, aplicacaoEncontrada, DescricaoProblema, userName);
                        }
                        if (aplicacao == "")
                        {
                            EditIndisponibilidadeOnBD(id, DataInicio, DataFim, Resolvido, provedor, servicoMobile, null, DescricaoProblema, userName);
                        }
                    }
                    else
                    {
                        TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='Para Indisponibilidade já Resolvida é obrigatório especificar a data de Início e Fim!';</script>";
                    }
                }
                else if (Resolvido == false)
                {
                    if (datain != "")
                    {
                        DateTime DataInicio = DateTime.ParseExact(datain, "dd/MM/yyyy HH:mm",
                         System.Globalization.CultureInfo.InvariantCulture);
                        String userName = getCurrentUserName();
                        if (Servico == "")
                        {
                            EditIndisponibilidadeOnBD(id, DataInicio, DateTime.Now, Resolvido, provedor, null, aplicacaoEncontrada, DescricaoProblema, userName);
                        }
                        if (aplicacao == "")
                        {
                            EditIndisponibilidadeOnBD(id, DataInicio, DateTime.Now, Resolvido, provedor, servicoMobile, null, DescricaoProblema, userName);
                        }
                    }
                    else
                    {
                        TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='Para Indisponibilidade não Resolvida é obrigatório especificar a data de Início!';</script>";
                    }
                }
                GetAplicacoesFromDB();
                GetMobileInfoFromDB();
                return View("RegistoIndisponibilidade", GetIndisponibilidadeFromDB());
            }

            IndisponibilidadesListViewModel registroIndisp = GetIndisponibilidadeFromDB();
            return View("RegistoIndisponibilidade", registroIndisp);
        }

        private static void EditIndisponibilidadeOnBD(ObjectId id, DateTime DataInicio, DateTime DataFim, bool Resolvido, Provedor Operadora, ServicoMobile Servico, Aplicacao aplicacao, string DescricaoProblema, String userName)
        {

            Expression<Func<Indisponibilidade, bool>> filter = x => x.Id.Equals(id);
            Indisponibilidade indisponibilidade = Context.Indisponibilidades.Find(filter).FirstOrDefault();

            if (indisponibilidade.Id != null)
            {

                if (Resolvido == true)
                {
                    indisponibilidade.DataFim = DataFim;
                    TimeSpan intervalo = DataFim.Subtract(DataInicio);
                    indisponibilidade.Intervalo = intervalo;
                }
                if (Resolvido == false)
                {
                    indisponibilidade.DataFim = DateTime.Now;
                }

                indisponibilidade.Operadora = Operadora;
                indisponibilidade.DataInicio = DataInicio;
                indisponibilidade.Servico = Servico;
                indisponibilidade.Aplicacao = aplicacao;

                indisponibilidade.Resolvido = Resolvido;
                indisponibilidade.DescricaoProblema = DescricaoProblema;
               // indisponibilidade.User = userName;
                Context.Indisponibilidades.ReplaceOne(filter, indisponibilidade);
            }
        }
        [HttpPost]
        public ActionResult RegistarIndisponibilidade2()
        {
            ObjectId Id = ObjectId.GenerateNewId();

            bool Resolvido = false;
            if (Request["lstResolvido"].ToString() == "true")
            {
                Resolvido = true;
            }
            if (Request["lstResolvido"].ToString() == "false")
            {
                Resolvido = false;
            }
            String datain = Request["dataInicio"].ToString();
            String datafm = Request["dataFim"].ToString();
            String operadora = Request["lstoperadoras"].ToString();
            String Servico = Request["lstServicos"].ToString();
            String DescricaoProblema = Request["txtDetalhes"].ToString();
            Provedor provedor = FindProvedor(operadora);
            ServicoMobile servicoMobile = FindServicoMobile(Servico);

            if (Resolvido == true)
            {
                if (provedor == null && servicoMobile == null)
                {
                    return HttpNotFound();
                }
                else if (datain != "" && datafm != "")
                {
                    DateTime DataInicio = DateTime.ParseExact(datain, "dd/MM/yyyy HH:mm",
                     System.Globalization.CultureInfo.InvariantCulture);
                    DateTime DataFim = DateTime.ParseExact(datafm, "dd/MM/yyyy HH:mm",
                    System.Globalization.CultureInfo.InvariantCulture);
                    String userName = getCurrentUserName();
                    TimeSpan intervalo = DataFim.Subtract(DataInicio);
                    addNewIndisponibilidade(Id, Resolvido, DataInicio, DataFim, intervalo, provedor, servicoMobile, DescricaoProblema, userName);
                }
                else
                {
                    TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='Para Indisponibilidade já Resolvida é obrigatório especificar a data de Início e Fim!';</script>";
                }
            }
            else if (Resolvido == false)
            {
                if (datain != "")
                {
                    DateTime DataInicio = DateTime.ParseExact(datain, "dd/MM/yyyy HH:mm",
                     System.Globalization.CultureInfo.InvariantCulture);
                    String userName = getCurrentUserName();
                    //Add controlo de indisponibilidades adbertas....
                    bool found = false;
                    foreach (Indisponibilidade dsp in IndisponibilidadeList)
                    {
                        if (dsp.Servico != null)
                        {
                            if (dsp.Operadora.Id == provedor.Id && dsp.Servico.Id == servicoMobile.Id && dsp.Resolvido == false)
                            {
                                found = true;
                                break;
                            }
                        }
                    }

                    if (found == false)
                    {
                        addNewIndisponibilidade(Id, Resolvido, DataInicio, DateTime.Now, new TimeSpan(), provedor, servicoMobile, DescricaoProblema, userName);
                    }
                    else if (found == true)
                    {
                        TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='Indisponiblidade aberta do mesmo tipo já existente. Por Favor, feche a indisponibilidade aberta antes de criar uma nova!';</script>";
                        GetAplicacoesFromDB();
                        GetMobileInfoFromDB();
                        return View("RegistoIndisponibilidade", GetIndisponibilidadeFromDB());
                    }
                }
                else
                {
                    TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='Para Indisponibilidade não Resolvida é obrigatório especificar a data de Início!';</script>";
                }
            }
            GetAplicacoesFromDB();
            GetMobileInfoFromDB();
            return View("RegistoIndisponibilidade", GetIndisponibilidadeFromDB());
        }

        [HttpPost]
        public ActionResult FiltrarIndisponibilidade2()
        {
            String datain = Request["dataInicio"].ToString() + " 00:00";
            String datafm = Request["dataFim"].ToString() +" 23:59";
            IndisponibilidadesListViewModel modelView = GetIndisponibilidadeFromDB();

            if (datain != "" && datafm != "")
            {

                DateTime DataInicio = DateTime.ParseExact(datain, "dd/MM/yyyy HH:mm",
                     System.Globalization.CultureInfo.InvariantCulture);
                DateTime DataFim = DateTime.ParseExact(datafm, "dd/MM/yyyy HH:mm",
                System.Globalization.CultureInfo.InvariantCulture);
                String userName = getCurrentUserName();
                TimeSpan intervalo = DataFim.Subtract(DataInicio);
                double tempoInd = 0;
                Double.TryParse(Request["frmTempoInd"].ToString(), out tempoInd);
                String operadora = Request["lstoperadoras"].ToString();
                String Servico = Request["lstServicos"].ToString();
                Provedor provedor = FindProvedor(operadora);
                ServicoMobile servicoMobile = FindServicoMobile(Servico);
                List<Indisponibilidade> indisponibilidadesFiltradasResolvidas = new List<Indisponibilidade>();
               
                foreach (Indisponibilidade inds in modelView.inidsponibilidadesResolvidas.ToList())
                {

                    if (inds.DataInicio >= DataInicio && inds.DataFim <= DataFim)
                    {

                        if (inds.Servico != null && inds.Operadora != null)
                        {
                            if (servicoMobile != null && provedor != null)
                            {
                                if (inds.Servico.Nome == servicoMobile.Nome && inds.Operadora.Nome == provedor.Nome)
                                {
                                    if (inds.Intervalo != null)
                                    {
                                        if (inds.Intervalo.TotalMinutes >= tempoInd)
                                        {
                                            indisponibilidadesFiltradasResolvidas.Add(inds);
                                        }
                                    }

                                }
                            }
                            else if (servicoMobile == null && provedor == null)
                            {
                                if (inds.Intervalo != null)
                                {
                                    if (inds.Intervalo.TotalMinutes >= tempoInd)
                                    {
                                        indisponibilidadesFiltradasResolvidas.Add(inds);
                                    }
                                }
                            }
                            else if (provedor == null && servicoMobile != null)
                            {
                                if (inds.Servico.Nome == servicoMobile.Nome)
                                {
                                    if (inds.Intervalo != null)
                                    {
                                        if (inds.Intervalo.TotalMinutes >= tempoInd)
                                        {
                                            indisponibilidadesFiltradasResolvidas.Add(inds);
                                        }
                                    }

                                }

                            }
                            else if (servicoMobile == null && provedor != null)
                            {
                                if (inds.Operadora.Nome == provedor.Nome)
                                {
                                    if (inds.Intervalo != null)
                                    {
                                        if (inds.Intervalo.TotalMinutes >= tempoInd)
                                        {
                                            indisponibilidadesFiltradasResolvidas.Add(inds);
                                        }
                                    }

                                }

                            }
                        }

                    }
                }
                List<Indisponibilidade> SortedLis2t = indisponibilidadesFiltradasResolvidas.OrderByDescending(o => o.DataInicio).ToList();
                indisponibilidadesFiltradasResolvidas = SortedLis2t;
                modelView.inidsponibilidadesResolvidas = indisponibilidadesFiltradasResolvidas;
            }
            return View("RegistoIndisponibilidade", modelView);
        }

        [HttpPost]
        public ActionResult FiltrarIndisponibilidadeApp2()
        {
            String datain = Request["dataInicio"].ToString() + " 00:00";
            String datafm = Request["dataFim"].ToString() + " 23:59";
            DateTime DataInicio = DateTime.ParseExact(datain, "dd/MM/yyyy HH:mm",
                     System.Globalization.CultureInfo.InvariantCulture);
            DateTime DataFim = DateTime.ParseExact(datafm, "dd/MM/yyyy HH:mm",
            System.Globalization.CultureInfo.InvariantCulture);
            String userName = getCurrentUserName();
            TimeSpan intervalo = DataFim.Subtract(DataInicio);
            double tempoInd = 0;
            Double.TryParse(Request["frmTempoInd"].ToString(), out tempoInd);
            String operadora = Request["lstoperadoras"].ToString();
            String Servico = Request["lstServicos"].ToString();
            Provedor provedor = FindProvedor(operadora);
            Aplicacao aplicacao = FindAplicacao(Servico);
            List<Indisponibilidade> indisponibilidadesFiltradasResolvidas = new List<Indisponibilidade>();
            IndisponibilidadesListViewModel modelView = GetIndisponibilidadeFromDB();
            foreach (Indisponibilidade inds in modelView.inidsponibilidadesResolvidas.ToList())
            {
                if (inds.DataInicio >= DataInicio && inds.DataFim <= DataFim)
                {

                    if (inds.Aplicacao != null && inds.Operadora != null)
                    {
                        if (aplicacao != null && provedor != null)
                        {
                            if (inds.Aplicacao.Nome == aplicacao.Nome && inds.Operadora.Nome == provedor.Nome)
                            {
                                if (inds.Intervalo != null)
                                {
                                    if (inds.Intervalo.TotalMinutes >= tempoInd)
                                    {
                                        indisponibilidadesFiltradasResolvidas.Add(inds);
                                    }
                                }

                            }
                        }
                        else if (aplicacao == null && provedor == null)
                        {
                            if (inds.Intervalo != null)
                            {
                                if (inds.Intervalo.TotalMinutes >= tempoInd)
                                {
                                    indisponibilidadesFiltradasResolvidas.Add(inds);
                                }
                            }
                        }
                        else if (provedor == null && aplicacao != null)
                        {
                            if (inds.Aplicacao.Nome == aplicacao.Nome)
                            {
                                if (inds.Intervalo != null)
                                {
                                    if (inds.Intervalo.TotalMinutes >= tempoInd)
                                    {
                                        indisponibilidadesFiltradasResolvidas.Add(inds);
                                    }
                                }

                            }

                        }
                        else if (aplicacao == null && provedor != null)
                        {
                            if (inds.Operadora.Nome == provedor.Nome)
                            {
                                if (inds.Intervalo != null)
                                {
                                    if (inds.Intervalo.TotalMinutes >= tempoInd)
                                    {
                                        indisponibilidadesFiltradasResolvidas.Add(inds);
                                    }
                                }

                            }

                        }
                    }

                }
            }
            List<Indisponibilidade> SortedLis2t = indisponibilidadesFiltradasResolvidas.OrderByDescending(o => o.DataInicio).ToList();
            indisponibilidadesFiltradasResolvidas = SortedLis2t;
            modelView.inidsponibilidadesResolvidas = indisponibilidadesFiltradasResolvidas;
            return View("RegistoIndisponibilidade", modelView);
        }

        [HttpPost]
        public ActionResult RegistarIndisponibilidadeApps2()
        {
            ObjectId Id = ObjectId.GenerateNewId();

            bool Resolvido = false;
            if (Request["lstResolvido"].ToString() == "true")
            {
                Resolvido = true;
            }
            if (Request["lstResolvido"].ToString() == "false")
            {
                Resolvido = false;
            }
            String datain = Request["dataInicio"].ToString();
            String datafm = Request["dataFim"].ToString();
            String Operadora = Request["lstoperadoras"].ToString();
            String app = Request["lstaplicacoes"].ToString();
            String DescricaoProblema = Request["txtDetalhes"].ToString();
            Provedor provedor = FindProvedor(Operadora);
            Aplicacao Aplicacao = FindAplicacao(app);
            if (Resolvido == true)
            {
                if (provedor == null && Aplicacao == null)
                {
                    return HttpNotFound();
                }
                else if (datain != "" && datafm != "")
                {
                    DateTime DataInicio = DateTime.ParseExact(datain, "dd/MM/yyyy HH:mm",
                     System.Globalization.CultureInfo.InvariantCulture);
                    DateTime DataFim = DateTime.ParseExact(datafm, "dd/MM/yyyy HH:mm",
                    System.Globalization.CultureInfo.InvariantCulture);
                    String userName = getCurrentUserName();
                    TimeSpan intervalo = DataFim.Subtract(DataInicio);
                    addNewIndisponibilidadeApp(Id, Resolvido, DataInicio, DataFim, intervalo, provedor, Aplicacao, DescricaoProblema, userName);
                }
                else
                {
                    TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='Para Indisponibilidade já Resolvida é obrigatório especificar a data de Início e Fim!';</script>";
                }
            }
            else if (Resolvido == false)
            {
                if (datain != "")
                {
                    DateTime DataInicio = DateTime.ParseExact(datain, "dd/MM/yyyy HH:mm",
                     System.Globalization.CultureInfo.InvariantCulture);
                    String userName = getCurrentUserName();
                    //Add controlo de indisponibilidades adbertas....
                    bool found = false;
                    foreach (Indisponibilidade dsp in IndisponibilidadeList)
                    {
                        if (dsp.Aplicacao != null)
                        {
                            if (dsp.Operadora.Id == provedor.Id && dsp.Aplicacao.Id == Aplicacao.Id && dsp.Resolvido == false)
                            {
                                found = true;
                                break;
                            }
                        }
                    }

                    if (found == false)
                    {
                        addNewIndisponibilidadeApp(Id, Resolvido, DataInicio, DateTime.Now, new TimeSpan(), provedor, Aplicacao, DescricaoProblema, userName);
                    }
                    else if (found == true)
                    {
                        TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='Indisponiblidade aberta do mesmo tipo já existente. Por Favor, feche a indisponibilidade aberta antes de criar uma nova!';</script>";
                        GetAplicacoesFromDB();
                        GetMobileInfoFromDB();
                        return View("RegistoIndisponibilidade", GetIndisponibilidadeFromDB());
                    }
                }
                else
                {
                    TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='Para Indisponibilidade não Resolvida é obrigatório especificar a data de Início!';</script>";
                }
            }
            GetAplicacoesFromDB();
            GetMobileInfoFromDB();
            return View("RegistoIndisponibilidade", GetIndisponibilidadeFromDB());
        }

        private void addNewIndisponibilidade(ObjectId Id, bool Resolvido, DateTime DataInicio, DateTime DataFim, TimeSpan intervalo, Provedor Operadora, ServicoMobile Servico, string DescricaoProblema, string userName)
        {
            Indisponibilidade indisp = new Indisponibilidade(Id, DataInicio, DataFim, Operadora, Servico, DescricaoProblema, Resolvido, userName, intervalo);
            try { Context.Indisponibilidades.InsertOne(indisp);
                } catch { }
        }

        private void addNewIndisponibilidadeApp(ObjectId Id, bool Resolvido, DateTime DataInicio, DateTime DataFim, TimeSpan intervalo, Provedor Operadora, Aplicacao app, string DescricaoProblema, string userName)
        {
            Indisponibilidade indisp = new Indisponibilidade(Id, DataInicio, DataFim, Operadora, app, DescricaoProblema, Resolvido, userName, intervalo);
            try { Context.Indisponibilidades.InsertOne(indisp);
            } catch { }
        }

        public static String generateID()
        {
            StringBuilder builder = new StringBuilder();
            Enumerable
               .Range(65, 26)
                .Select(e => ((char)e).ToString())
                .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
                .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
                .OrderBy(e => Guid.NewGuid())
                .Take(11)
                .ToList().ForEach(e => builder.Append(e));
            string id = builder.ToString();
            return id;
        }
        public ActionResult Milleteller()
        {
            ViewBag.Message = "Your Milleteller page.";
            viewModelMilleteller = GetContactosMilletellerBD();
            if (ContactsListMilleteller.Count() == 0)
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt2').style.display='block'; document.getElementById('hidenatt2').innerHTML='Impossivel Carregar dados da BD.. Verifica a disponiblidade da BD ou se existem registos do objecto';" +
                                      "document.getElementById('btngravar').disabled = true;</script>";
            }
            return View(viewModelMilleteller);
        }
        [HttpPost]
        public ActionResult EstatisticaAplicacoes2()
        {
            EstatisticaAplicacaoViewModel estatisticaView2 = new EstatisticaAplicacaoViewModel();
            ServicePointManager.DefaultConnectionLimit = 6000;
            String dataInicio = Request["dataInicio"].ToString() + "-00-00-00";
            String dataFim = Request["dataFim"].ToString() + "-23-59-00";
            String periodoEspecificado = Request["dataInicio"].ToString() + " - " + Request["dataFim"].ToString();
            Decimal sladefinidoP = 98;
            Decimal sladefinidoD = 99;
            Decimal disponibilidadeGeral = 0;
            Decimal performanceGeral = 0;
            estatisticaAplicacoesList = new List<EstatisticaAplicacao>();
            //verify if date as been choosen
            if (Request["dataInicio"].ToString() == "" || Request["dataFim"].ToString() == "")
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='Por Favor, É obrigatório especificar a data de Início e Fim!';</script>";
                return View("EstatisticaAplicacoes", estatisticaView2);
            }
            else if (Request["sladefinidoD"].ToString() == "" || Request["sladefinidoP"].ToString() == "")
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='Por Favor, É obrigatório especificar o SLA (Threshold)!';</script>";
                return View("EstatisticaAplicacoes", estatisticaView2);
            }
            else if (Request["dataInicio"].ToString() != "" && Request["dataFim"].ToString() != "" && Request["sladefinidoD"].ToString() != "" && Request["sladefinidoP"].ToString() != "")
            {
                sladefinidoP = Decimal.Parse(Request["sladefinidoP"].ToString());
                sladefinidoD = Decimal.Parse(Request["sladefinidoD"].ToString());
                //Get All APlications sensors (id 43093) monitored by PRTG
                //workflows
                String workflows = "http://semzseiptg12:16661/api/table.json?content=sensors&columns=objid,type,group,device,sensor,status,uptimesince&id=19733&filter_type=exexml&sortby=priority&username=prtgadmin&&passhash=2445698422";
                WebRequest request = HttpWebRequest.Create(workflows);
                request.Method = "GET";
                using (WebResponse response = request.GetResponse())
                {
                    processAppStatistics(dataInicio, dataFim, response);
                }
                //Millenet e Outras Apps
                String apps2 = "http://semzseiptg12:16661/api/table.json?content=sensors&columns=objid,type,group,device,sensor,status,uptimesince&id=39184&filter_type=exexml&sortby=priority&username=prtgadmin&&passhash=2445698422";
                WebRequest request2 = HttpWebRequest.Create(apps2);
                request2.Method = "GET";
                using (WebResponse response = request2.GetResponse())
                {
                    processAppStatistics(dataInicio, dataFim, response);
                }
                //Outlook e Intranet
                String outeintra = "http://semzseiptg12:16661/api/table.json?content=sensors&columns=objid,type,group,device,sensor,status,uptimesince&id=19726&filter_type=exexml&sortby=priority&username=prtgadmin&&passhash=2445698422";
                WebRequest request3 = HttpWebRequest.Create(outeintra);
                request3.Method = "GET";
                using (WebResponse response = request3.GetResponse())
                {
                    processAppStatistics(dataInicio, dataFim, response);
                }
                //JavaApps - icbs swift imex financa
                String javaapps = "http://semzseiptg12:16661/api/table.json?content=sensors&columns=objid,type,group,device,sensor,status,uptimesince&id=38686&filter_type=exexml&sortby=priority&username=prtgadmin&&passhash=2445698422";
                WebRequest request4 = HttpWebRequest.Create(javaapps);
                request4.Method = "GET";
                using (WebResponse response = request4.GetResponse())
                {
                    processAppStatistics(dataInicio, dataFim, response);
                }
                //JavaApps - icbs swift imex financa
                String millepac = "http://semzseiptg12:16661/api/table.json?content=sensors&columns=objid,type,group,device,sensor,status,uptimesince&id=39315&filter_type=exexml&sortby=priority&username=prtgadmin&&passhash=2445698422";
                WebRequest request5 = HttpWebRequest.Create(millepac);
                request5.Method = "GET";
                using (WebResponse response = request5.GetResponse())
                {
                    processAppStatistics(dataInicio, dataFim, response);
                }
            }

            List<String> sequenciadeApps = new List<string>();
            sequenciadeApps.Add("Millenet Particulares");
            sequenciadeApps.Add("Millenet Prestige");
            sequenciadeApps.Add("Millenet Empresas");
            sequenciadeApps.Add("Millenet Institucional");
            sequenciadeApps.Add("ICBS");
            sequenciadeApps.Add("SWIFT");
            sequenciadeApps.Add("Outlook");
            sequenciadeApps.Add("Outlook Web_v2");
            sequenciadeApps.Add("WebBank");
            sequenciadeApps.Add("WF WebCel");
            sequenciadeApps.Add("WF SIM MyGIS");
            sequenciadeApps.Add("FINANCA");
            sequenciadeApps.Add("IMEX");
            sequenciadeApps.Add("XLTI");
            sequenciadeApps.Add("WF Remotas");
            sequenciadeApps.Add("WF Leasing");
            sequenciadeApps.Add("WF Crédito Nova Vida");
            sequenciadeApps.Add("WF Crédito Habitação");
            sequenciadeApps.Add("WF Câmbios");
            sequenciadeApps.Add("WF Linha BIM");
            sequenciadeApps.Add("WF Pedido Cartões");
            sequenciadeApps.Add("MillePac");
            sequenciadeApps.Add("WF SIM Seguros Paz");
            sequenciadeApps.Add("WF Transf. Imex Estrangeiro");
            sequenciadeApps.Add("WF Transf. OIC e Int");
            sequenciadeApps.Add("WF SPC");
            sequenciadeApps.Add("WF C. Aplicações Financeiras");
            sequenciadeApps.Add("WF Digitalização");
            sequenciadeApps.Add("WF Factoring e Confirming");
            sequenciadeApps.Add("WF Leitura de Cheques");
            sequenciadeApps.Add("WF Limites de Crédito");



            
            List<EstatisticaAplicacao> estatisticasFiltradas = new List<EstatisticaAplicacao>();
            foreach (EstatisticaAplicacao estatis in estatisticaAplicacoesList)
            {
                if(sequenciadeApps.Exists(app => app == estatis.Nome))
                {
                    estatisticasFiltradas.Add(estatis);
                }
            }
            

            //Média das Páginas Millepac
            Decimal dispoMillepac = 0;
            Decimal performanceMillepac = 0;
            Decimal countMp = 0;
            
            foreach (EstatisticaAplicacao estatis in estatisticaAplicacoesList)
            {
                if (estatis.Nome.Contains("Millepac"))
                {
                    countMp = countMp + 1;
                    dispoMillepac += Decimal.Parse(estatis.Disponibilidade.Replace(" %", "").Replace(",", "."));
                    performanceMillepac += Decimal.Parse(estatis.Performance.Replace(" %", "").Replace(",", "."));
                }
            }
            if (countMp == 0)
            {
                dispoMillepac = 100;
                performanceMillepac = 100;
            }
            else
            {
                performanceMillepac = ((decimal)performanceMillepac / (decimal)countMp) * (decimal)100;
                dispoMillepac = ((decimal)dispoMillepac / (decimal)countMp) * (decimal)100;
            }


            EstatisticaAplicacao estatisMP = new EstatisticaAplicacao();
            estatisMP.Nome = "MillePac";
            estatisMP.Disponibilidade = String.Format("{0:N2}", dispoMillepac).Replace(".", ",") + " %";
            estatisMP.Performance = String.Format("{0:N2}", performanceMillepac).Replace(".", ",") + " %";
            estatisticasFiltradas.Add(estatisMP);

            long countdsp = 0;
            Decimal somaDispo = 0;
            Decimal somaPerf = 0;
            foreach (EstatisticaAplicacao estatis in estatisticasFiltradas)
            {
                if (sequenciadeApps.Exists(app => app == estatis.Nome))
                {
                    countdsp = countdsp + 100;
                    somaDispo += Decimal.Parse(estatis.Disponibilidade.Replace(" %", "").Replace(",", "."));
                    somaPerf += Decimal.Parse(estatis.Performance.Replace(" %", "").Replace(",", "."));
                }
            }
            if (countdsp == 0)
            {
                disponibilidadeGeral = 100;
                performanceGeral = 100;
            }
            else
            {
                disponibilidadeGeral = ((decimal)somaDispo / (decimal)countdsp) * (decimal)100;
                performanceGeral = ((decimal)somaPerf / (decimal)countdsp) * (decimal)100;
            }
            // List<EstatisticaAplicacao> SortedLis2t = estatisticaAplicacoesList.OrderBy(o => o.Nome).ToList();
            List<EstatisticaAplicacao> SortedLis2t = new List<EstatisticaAplicacao>();
            foreach (String app in sequenciadeApps)
            {
                foreach (EstatisticaAplicacao ind in estatisticasFiltradas)
                {
                    if(app == ind.Nome)
                    {
                        SortedLis2t.Add(ind);
                    }
                }
            }
            //List<EstatisticaAplicacao> SortedLis2t = estatisticaAplicacoesList.OrderBy(d => sequenciadeApps.IndexOf(d.Nome)).ToList();
            estatisticaAplicacoesList = SortedLis2t;
            estatisticaView2.disponibilidadeGeral = String.Format("{0:N2}", disponibilidadeGeral).Replace(".", ",") + " %";
            estatisticaView2.performanceGeral = String.Format("{0:N2}", performanceGeral).Replace(".", ",") + " %";
            estatisticaView2.numeroAplicacoes = estatisticasFiltradas.Count.ToString();
            estatisticaView2.periodoEspecificado = periodoEspecificado;
            estatisticaView2.sladefinidoD = sladefinidoD.ToString();
            estatisticaView2.sladefinidoP = sladefinidoP.ToString();
            estatisticaView2.EstatisticasAplicacoes = estatisticaAplicacoesList;

            TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenbtn').disabled =false;</script>";
            return View("EstatisticaAplicacoes", estatisticaView2);
        }

        private void processAppStatistics(string dataInicio, string dataFim, WebResponse response)
        {
            String jsonText = "";
            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                jsonText = sr.ReadToEnd();
                response.Close();
            }
            XmlDocument xmlDoc = (XmlDocument)JsonConvert.DeserializeXmlNode(jsonText, "sensors");
            foreach (XmlNode node in xmlDoc.SelectNodes("/sensors/sensors"))
            {
                EstatisticaAplicacao app = new EstatisticaAplicacao();
                app.Id = node["objid"].InnerText;
                app.Nome = node["sensor"].InnerText;
                string url = "http://semzseiptg12:16661/api/historicdata.json?id=" + app.Id + "&avg=0&sdate=" + dataInicio + "&edate=" + dataFim + "&usecaption=1&username=prtgadmin&&passhash=2445698422";
                WebRequest requestworkf = HttpWebRequest.Create(url);
                requestworkf.ContentType = "application/json";
                requestworkf.Timeout = 600000;

                using (WebResponse myWebResponse = requestworkf.GetResponse())
                {

                    String jsonText2 = "";
                    using (var sr = new StreamReader(myWebResponse.GetResponseStream()))
                    {
                        jsonText2 = sr.ReadToEnd();
                    }
                    XmlDocument xmlDoc2 = (XmlDocument)JsonConvert.DeserializeXmlNode(jsonText2, "histdata");
                    XmlNodeList docToAnalise = xmlDoc2.SelectNodes("/histdata/histdata");
                    List<String> disponibilidadeList = new List<string>();
                    List<String> performanceList = new List<string>();
                    if (docToAnalise.Count == 0 || docToAnalise == null)
                    {
                    }
                    else
                    {
                        long countinc = 0;
                        long countincp = 0;
                        foreach (XmlNode node2 in docToAnalise)
                        {
                            //Fetch the Node values and assign it to Model.
                            //Disponibilidade
                            if (node2 != null)
                            {

                                if (node2.ChildNodes != null)
                                {
                                    foreach (XmlNode nodeItem in node2.ChildNodes)
                                    {
                                        if (nodeItem.Name.Contains("Disponibilidade") || nodeItem.Name.Contains("Resultado"))
                                        {
                                            if (nodeItem.InnerText != "")
                                            {
                                                if (nodeItem.InnerText == "1")
                                                {
                                                    disponibilidadeList.Add("100");
                                                    countinc = countinc + 100;
                                                }
                                                else if (nodeItem.InnerText == "0")
                                                {
                                                    disponibilidadeList.Add("0");
                                                    countinc = countinc + 100;
                                                }
                                            }
                                        }
                                        if (nodeItem.Name.Contains("Performance"))
                                        {
                                            if (nodeItem.InnerText != "")
                                            {
                                                if (nodeItem.InnerText == "0.0000")
                                                {
                                                    performanceList.Add("0");
                                                    countincp = countincp + 100;
                                                }
                                                else if (nodeItem.InnerText == "100.0000")
                                                {
                                                    performanceList.Add("100");
                                                    countincp = countincp + 100;
                                                }
                                                else
                                                {
                                                    performanceList.Add(String.Format("{0:N2}", nodeItem.InnerText));
                                                    countincp = countincp + 100;
                                                }

                                            }
                                        }
                                    }
                                }
                            }
                        }
                        //Process Statistics
                        Decimal disponibilidade = 0;
                        Decimal performance = 0;
                        Decimal SomaDisponibilidade = 0;
                        Decimal SomaPerformance = 0;
                        foreach (String disp in disponibilidadeList)
                        {
                            SomaDisponibilidade += Decimal.Parse(disp);
                        }
                        foreach (String perf in performanceList)
                        {
                            if (!perf.Contains("-"))
                            {
                                SomaPerformance += Decimal.Parse(perf, System.Globalization.CultureInfo.InvariantCulture);
                            }
                        }
                        disponibilidade = ((decimal)SomaDisponibilidade / (decimal)countinc) * (decimal)100;
                        if (disponibilidade > 100)
                        {
                            disponibilidade = 100;
                        }
                        performance = ((decimal)SomaPerformance / (decimal)countincp) * (decimal)100;
                        if (performance > 100)
                        {
                            performance = (decimal)100.000;
                        }
                        app.Disponibilidade = String.Format("{0:N2}", disponibilidade).Replace(".", ",") + " %";
                        app.Performance = String.Format("{0:N2}", performance).Replace(".", ",") + " %";
                        estatisticaAplicacoesList.Add(app);
                    }
                }
            }
        }
        public ActionResult EstatisticaAplicacoes()
        {
            EstatisticaAplicacaoViewModel estatisticaView2 = new EstatisticaAplicacaoViewModel();
            return View(estatisticaView2);
        }
        public ActionResult EstatisticasComunicacoes()
        {
            EstatisticaBalcaoViewModel estatisticaView = new EstatisticaBalcaoViewModel();
            return View(estatisticaView);
        }
        public ActionResult EstatisticasExtranet()
        {
            EstatisticaExtranetListViewModel estatisticaView = new EstatisticaExtranetListViewModel();
            return View(estatisticaView);
        }

        [HttpPost]
        public ActionResult EstatisticasExtranet2()
        {
            //Get start and End date specified by user
            ServicePointManager.DefaultConnectionLimit = 1000;
            String dataInicio = Request["dataInicio"].ToString() + "-00-00-00";
            String dataFim = Request["dataFim"].ToString() + "-23-59-00";
            String periodoEspecificado = Request["dataInicio"].ToString() + " - " + Request["dataFim"].ToString();
            Decimal sladefinido = 95;
            Decimal disponibilidadeGeral = 0;
            List<EstatisticaExtranet>  estatisticaExtranetsList = new List<EstatisticaExtranet>();
            EstatisticaExtranetListViewModel estatisticaView = new EstatisticaExtranetListViewModel();

            //verify if date as been choosen
            if (Request["dataInicio"].ToString() == "" || Request["dataFim"].ToString() == "")
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='Por Favor, É obrigatório especificar a data de Início e Fim!';</script>";
                return View("EstatisticasExtranet", estatisticaView);
            }
            else if (Request["sladefinido"].ToString() == "")
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='Por Favor, É obrigatório especificar o SLA (Threshold)!';</script>";
                return View("EstatisticasExtranet", estatisticaView);
            }
            else if (Request["dataInicio"].ToString() != "" && Request["dataFim"].ToString() != "" && Request["sladefinido"].ToString() != "")
            {
                //Get All Ping sensors on Balcoes Group (id 43093) monitored by PRTG
                String devicesToScan = "http://semzseiptg12:16661/api/table.json?content=sensors&columns=objid,group,device,sensor,status,uptimesince&filter_type=Ping&id=51143&sortby=priority&username=prtgadmin&&passhash=2445698422";
                WebRequest request = HttpWebRequest.Create(devicesToScan);
                request.Method = "GET";
                using (WebResponse response = request.GetResponse())
                {
                    String jsonText = "";
                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        jsonText = sr.ReadToEnd();
                    }
                    XmlDocument xmlDoc = (XmlDocument)JsonConvert.DeserializeXmlNode(jsonText, "sensors");
                    sladefinido = Decimal.Parse(Request["sladefinido"].ToString());
                    //Loop through the selected Nodes.
                    foreach (XmlNode node in xmlDoc.SelectNodes("/sensors/sensors"))
                    {
                        //Fetch the Node values and assign it to Model.
                        //Creating new EstatisticaBalcao object
                        EstatisticaExtranet extranet = new EstatisticaExtranet();
                        extranet.Id = node["objid"].InnerText;
                        extranet.Nome = node["device"].InnerText;
                        extranet.Sensor = node["sensor"].InnerText;
                        string url = "http://semzseiptg12:16661/api/historicdata.json?id=" + extranet.Id + "&avg=0&sdate=" + dataInicio + "&edate=" + dataFim + "&usecaption=1&username=prtgadmin&&passhash=2445698422";
                        WebRequest request2 = HttpWebRequest.Create(url);
                        request2.Timeout = 300000;
                        using (WebResponse myWebResponse = request2.GetResponse())
                        {
                            String jsonText2 = "";
                            using (var sr = new StreamReader(myWebResponse.GetResponseStream()))
                            {
                                jsonText2 = sr.ReadToEnd();
                            }
                            XmlDocument xmlDoc2 = (XmlDocument)JsonConvert.DeserializeXmlNode(jsonText2, "histdata");
                            XmlNodeList docToAnalise = xmlDoc2.SelectNodes("/histdata/histdata");
                            List<String> PerdaPacotes = new List<string>();
                            if (docToAnalise.Count == 0 || docToAnalise == null)
                            {
                            }
                            else
                            {
                                long countinc = 0;
                                foreach (XmlNode node2 in docToAnalise)
                                {
                                    //Fetch the Node values and assign it to Model.
                                    if (node2 != null)
                                    {
                                        countinc = countinc + 100;
                                        if (node2.ChildNodes.Item(4).InnerText != "")
                                        {
                                            PerdaPacotes.Add("0");
                                        }
                                        else
                                        {
                                            PerdaPacotes.Add("100");
                                        }
                                    }
                                    else
                                    {
                                    }
                                }
                                //Process Statistics
                                Decimal disponibilidade = 0;
                                Decimal indisponibilidade = 0;
                                Decimal SomaPerda = 0;
                                foreach (String perda in PerdaPacotes)
                                {
                                    SomaPerda += Decimal.Parse(perda);
                                }
                                indisponibilidade = ((decimal)SomaPerda / (decimal)countinc) * (decimal)100;
                                disponibilidade = (decimal)100 - indisponibilidade;
                                extranet.Disponibilidade = String.Format("{0:N2}", disponibilidade) + " %";
                                extranet.Indisponibilidade = String.Format("{0:N2}", indisponibilidade) + " %";
                                estatisticaExtranetsList.Add(extranet);
                            }
                        } // Your response will be disposed of here
                    }
                    //Top indisponíveis de acordo com o SLA especificado
                    long countdsp = 0;
                    Decimal somaDispo = 0;
                    foreach (EstatisticaExtranet estatis in estatisticaExtranetsList)
                    {
                        countdsp = countdsp + 100;
                        somaDispo += Decimal.Parse(estatis.Disponibilidade.Replace(" %", ""));
                    }
                    if (countdsp == 0)
                    {
                        disponibilidadeGeral = 100;
                    }
                    else
                    {
                        disponibilidadeGeral = ((decimal)somaDispo / (decimal)countdsp) * (decimal)100;
                    }
                    //order
                    List<EstatisticaExtranet> SortedList = estatisticaExtranetsList.OrderByDescending(o => Decimal.Parse(o.Disponibilidade.Replace(" %", ""))).ToList();
                    estatisticaExtranetsList = SortedList;
                    //
                    estatisticaView.EstatisticasExtranet = estatisticaExtranetsList;
                    estatisticaView.numeroExtranets = estatisticaExtranetsList.Count().ToString();
                    estatisticaView.sladefinido = sladefinido.ToString() + " %";
                    estatisticaView.periodoEspecificado = periodoEspecificado;
                    estatisticaView.disponibilidadeGeral = String.Format("{0:N2}", disponibilidadeGeral) + " %";
                    TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenbtn').disabled =false;</script>";
                }
            }
            return View("EstatisticasExtranet", estatisticaView);
        }
        [HttpPost]
        public ActionResult EstatisticasComunicacoes2()
        {
            //Get start and End date specified by user
            ServicePointManager.DefaultConnectionLimit = 1000;
            String dataInicio = Request["dataInicio"].ToString() + "-00-00-00";
            String dataFim = Request["dataFim"].ToString() + "-23-59-00";
            String periodoEspecificado = Request["dataInicio"].ToString() + " - " + Request["dataFim"].ToString();
            Decimal sladefinido = 95;
            Decimal disponibilidadeGeral = 0;
            estatisticaBalcoesList = new List<EstatisticaBalcao>();
            List<EstatisticaBalcao> TopIndisponiveis = new List<EstatisticaBalcao>();
            EstatisticaBalcaoViewModel estatisticaView = new EstatisticaBalcaoViewModel();

            //verify if date as been choosen
            if (Request["dataInicio"].ToString() == "" || Request["dataFim"].ToString() == "")
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='Por Favor, É obrigatório especificar a data de Início e Fim!';</script>";
                return View("EstatisticasComunicacoes", estatisticaView);
            }
            else if (Request["sladefinido"].ToString() == "")
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block'; document.getElementById('hidenatt3').innerHTML='Por Favor, É obrigatório especificar o SLA (Threshold)!';</script>";
                return View("EstatisticasComunicacoes", estatisticaView);
            }
            else if (Request["dataInicio"].ToString() != "" && Request["dataFim"].ToString() != "" && Request["sladefinido"].ToString() != "")
            {
                //Get All Ping sensors on Balcoes Group (id 43093) monitored by PRTG
                String devicesToScan = "http://semzseiptg12:16661/api/table.json?content=sensors&columns=objid,group,device,sensor,status,uptimesince&filter_type=Ping&id=43093&sortby=priority&username=prtgadmin&&passhash=2445698422";
                WebRequest request = HttpWebRequest.Create(devicesToScan);
                request.Method = "GET";
                using (WebResponse response = request.GetResponse())
                {
                    String jsonText = "";
                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        jsonText = sr.ReadToEnd();
                    }
                    XmlDocument xmlDoc = (XmlDocument)JsonConvert.DeserializeXmlNode(jsonText, "sensors");
                    sladefinido = Decimal.Parse(Request["sladefinido"].ToString());
                    //Loop through the selected Nodes.
                    foreach (XmlNode node in xmlDoc.SelectNodes("/sensors/sensors"))
                    {
                        //Fetch the Node values and assign it to Model.
                        //Creating new EstatisticaBalcao object
                        EstatisticaBalcao balcao = new EstatisticaBalcao();
                        balcao.Id = node["objid"].InnerText;
                        balcao.Provincia = node["group"].InnerText;
                        balcao.Nome = node["device"].InnerText;
                        balcao.Sensor = node["sensor"].InnerText;
                        string url = "http://semzseiptg12:16661/api/historicdata.json?id=" + balcao.Id + "&avg=0&sdate=" + dataInicio + "&edate=" + dataFim + "&usecaption=1&username=prtgadmin&&passhash=2445698422";
                        WebRequest request2 = HttpWebRequest.Create(url);
                        request2.Timeout = 300000;
                        using (WebResponse myWebResponse = request2.GetResponse())
                        {
                            String jsonText2 = "";
                            using (var sr = new StreamReader(myWebResponse.GetResponseStream()))
                            {
                                jsonText2 = sr.ReadToEnd();
                            }
                            XmlDocument xmlDoc2 = (XmlDocument)JsonConvert.DeserializeXmlNode(jsonText2, "histdata");
                            XmlNodeList docToAnalise = xmlDoc2.SelectNodes("/histdata/histdata");
                            List<String> PerdaPacotes = new List<string>();
                            if (docToAnalise.Count == 0 || docToAnalise == null)
                            {
                            }
                            else
                            {
                                long countinc = 0;
                                foreach (XmlNode node2 in docToAnalise)
                                {
                                    //Fetch the Node values and assign it to Model.
                                    if (node2 != null)
                                    {
                                        countinc = countinc + 100;
                                        if (node2.ChildNodes.Item(4).InnerText != "")
                                        {
                                            PerdaPacotes.Add("0");
                                        }
                                        else
                                        {
                                            PerdaPacotes.Add("100");
                                        }
                                    }
                                    else
                                    {
                                    }
                                }
                                //Process Statistics
                                Decimal disponibilidade = 0;
                                Decimal indisponibilidade = 0;
                                Decimal SomaPerda = 0;
                                foreach (String perda in PerdaPacotes)
                                {
                                    SomaPerda += Decimal.Parse(perda);
                                }
                                indisponibilidade = ((decimal)SomaPerda / (decimal)countinc) * (decimal)100;
                                disponibilidade = (decimal)100 - indisponibilidade;
                                balcao.Disponibilidade = String.Format("{0:N2}", disponibilidade) + " %";
                                balcao.Indisponibilidade = String.Format("{0:N2}", indisponibilidade) + " %";
                                estatisticaBalcoesList.Add(balcao);
                            }
                        } // Your response will be disposed of here
                    }
                    //Top indisponíveis de acordo com o SLA especificado
                    long countdsp = 0;
                    Decimal somaDispo = 0;
                    foreach (EstatisticaBalcao estatis in estatisticaBalcoesList)
                    {
                        countdsp = countdsp + 100;
                        if (Decimal.Parse(estatis.Disponibilidade.Replace(" %", "")) < sladefinido)
                        {
                            TopIndisponiveis.Add(estatis);
                        }
                        somaDispo += Decimal.Parse(estatis.Disponibilidade.Replace(" %", ""));
                    }
                    if (countdsp == 0)
                    {
                        disponibilidadeGeral = 100;
                    }
                    else
                    {
                        disponibilidadeGeral = ((decimal)somaDispo / (decimal)countdsp) * (decimal)100;
                    }
                    //order
                    List<EstatisticaBalcao> SortedList = estatisticaBalcoesList.OrderByDescending(o => Decimal.Parse(o.Disponibilidade.Replace(" %", ""))).ToList();
                    estatisticaBalcoesList = SortedList;
                    List<EstatisticaBalcao> SortedLis2t = TopIndisponiveis.OrderBy(o => Decimal.Parse(o.Disponibilidade.Replace(" %", ""))).ToList();
                    TopIndisponiveis = SortedLis2t;
                    //
                    estatisticaView.EstatisticasBalcoes = estatisticaBalcoesList;
                    estatisticaView.numeroBalcoes = estatisticaBalcoesList.Count().ToString();
                    estatisticaView.TopIndisponiveis = TopIndisponiveis;
                    estatisticaView.sladefinido = sladefinido.ToString() + " %";
                    estatisticaView.periodoEspecificado = periodoEspecificado;
                    estatisticaView.disponibilidadeGeral = String.Format("{0:N2}", disponibilidadeGeral) + " %";
                    TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenbtn').disabled =false;</script>";
                }
            }
            return View("EstatisticasComunicacoes", estatisticaView);
        }

        public ActionResult AddContactoMilleteller()
        {
            contactosviewModel = GetContactosPortalBD();
            List<Contacto> filtredList = new List<Contacto>();
            filtredList = contactosList;
            foreach (ContactMilleteller contact in ContactsListMilleteller)
            {
                foreach (Contacto cnt in filtredList.ToList())
                {
                    if (contact.Contacto.Id == cnt.Id)
                    {
                        filtredList.Remove(cnt);
                    }
                }
            }
            contactosviewModel.Contactos = filtredList;
            return PartialView(contactosviewModel);
        }
        public ActionResult RegistrarIndisponibilidade()
        {
            RegistrarIndisponibilidadeViewModel regView = new RegistrarIndisponibilidadeViewModel();
            GetMobileInfoFromDB();
            regView.provedoras = provedores;
            regView.servicosMobile = servicosMobile;
            return PartialView(regView);
        }
        public ActionResult RegistrarIndisponibilidadeApps()
        {
            RegistrarIndisponibilidadeViewModel regView = new RegistrarIndisponibilidadeViewModel();
            GetMobileInfoFromDB();
            GetAplicacoesFromDB();
            regView.provedoras = provedores;
            regView.aplicacoes = aplicacoesList;
            return PartialView(regView);
        }
        public ActionResult FiltrarIndPorServico()
        {
            RegistrarIndisponibilidadeViewModel regView = new RegistrarIndisponibilidadeViewModel();
            GetMobileInfoFromDB();
            //Adding option of all objects in List
            regView.provedoras = new List<Provedor>();
            List < Provedor > tempP = new List<Provedor>();
            tempP.Add(new Provedor(ObjectId.GenerateNewId(), "Todos os Provedores"));
            tempP.AddRange(provedores);
            regView.provedoras = tempP;

            regView.servicosMobile = new List<ServicoMobile>();
            List<ServicoMobile> tempS = new List<ServicoMobile>();
            tempS.Add(new ServicoMobile(ObjectId.GenerateNewId(), "Todos os Serviços Mobile"));
            tempS.AddRange(servicosMobile);
            regView.servicosMobile = tempS;
            return PartialView(regView);
        }
        public ActionResult FiltrarIndPorAplicacao()
        {
            RegistrarIndisponibilidadeViewModel regView = new RegistrarIndisponibilidadeViewModel();
            GetMobileInfoFromDB();
            GetAplicacoesFromDB();
            //Adding option of all objects in List
            regView.provedoras = new List<Provedor>();
            List<Provedor> tempP = new List<Provedor>();
            tempP.Add(new Provedor(ObjectId.GenerateNewId(), "Todos os Provedores"));
            tempP.AddRange(provedores);
            regView.provedoras = tempP;

            regView.aplicacoes = new List<Aplicacao>();
            List<Aplicacao> tempS = new List<Aplicacao>();
            tempS.Add(new Aplicacao(ObjectId.GenerateNewId(), "Todas as Aplicações",null,null,null,null,null,null));
            tempS.AddRange(aplicacoesList);
            regView.aplicacoes = tempS;
            return PartialView(regView);
        }
        [HttpPost]
        public ActionResult AddContactoMilleteller2()
        {
            ContactMilleteller novoContacto = new ContactMilleteller();
            String id = Request["frmContactos"].ToString();
            Contacto findedContacto = FindContacto(id);
            novoContacto.Id = findedContacto.Id;
            novoContacto.Contacto = findedContacto;
            novoContacto.Notf_SMS = Request["drpListNotfSMS"].ToString();
            novoContacto.Notf_Email = Request["drpListNotfEmail"].ToString();
            novoContacto.Fim = Request["drpListFim"].ToString();
            Context.ContactosMilleteller.InsertOne(novoContacto);
            GetContactosMilletellerBD();
            List<ContactMilleteller> SortedLis2t = ContactsListMilleteller.OrderBy(o => o.Contacto.Nome).ToList();
            ContactsListMilleteller = SortedLis2t;
            viewModelMilleteller.ContactMilleteller = ContactsListMilleteller;
            TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt2').style.display='block'; document.getElementById('hidenatt2').innerHTML='Contacto " + novoContacto.Contacto.Nome + " adicionado com sucesso.';" +
                                      "document.getElementById('btngravar').disabled = true;</script>";
            return View("Milleteller", viewModelMilleteller);
        }

        public ActionResult ExecutarComunicacoes()
        {
            standardOutput = "";
            errorOutput = "";
            PowershellModel modelps = new PowershellModel(errorOutput, standardOutput);
            return View(modelps);
        }
        public ActionResult ExecutarExtranets()
        {
            standardOutput = "";
            errorOutput = "";
            PowershellModel modelps = new PowershellModel(errorOutput, standardOutput);
            return View(modelps);
        }
        public ActionResult ExecutarExtranets2()
        {
            standardOutput = "";
            errorOutput = "";
            RunScript(fileExtranet);
            PowershellModel modelps = new PowershellModel(errorOutput, standardOutput);
            return View("ExecutarExtranets", modelps);
        }
        public ActionResult ExecutarAPC()
        {
            standardOutput = "";
            errorOutput = "";
            PowershellModel modelps = new PowershellModel(errorOutput, standardOutput);
            return View(modelps);
        }

        public ActionResult ExecutarAPC2()
        {
            standardOutput = "";
            errorOutput = "";
            RunScript(fileAPC);
            PowershellModel modelps = new PowershellModel(errorOutput, standardOutput);
            return View("ExecutarAPC", modelps);
        }

        public ActionResult ExecutarAKCP()
        {
            standardOutput = "";
            errorOutput = "";
            PowershellModel modelps = new PowershellModel(errorOutput, standardOutput);
            return View(modelps);
        }

        public ActionResult ExecutarAKCP2()
        {
            standardOutput = "";
            errorOutput = "";
            RunScript(fileAKCP);
            PowershellModel modelps = new PowershellModel(errorOutput, standardOutput);
            return View("ExecutarAKCP", modelps);
        }


        public ActionResult ExecutarComunicacoes2()
        {
            standardOutput = "";
            errorOutput = "";
            RunScript(PowerShellRunnerBatchFilePath);
            PowershellModel modelps = new PowershellModel(errorOutput, standardOutput);
            return View("ExecutarComunicacoes", modelps);
        }

        public ActionResult ExecutarMilleteller()
        {
            standardOutput = "";
            errorOutput = "";
            PowershellModel modelps = new PowershellModel(errorOutput, standardOutput);
            return View(modelps);
        }
        public ActionResult ExecutarMilleteller2()
        {
            standardOutput = "";
            errorOutput = "";
            RunScript(PowerShellRunnerBatchFilePathM);
            PowershellModel modelps = new PowershellModel(errorOutput, standardOutput);
            return View("ExecutarMilleteller", modelps);
        }
        public ActionResult ExecutarATMs()
        {
            standardOutput = "";
            errorOutput = "";
            PowershellModel modelps = new PowershellModel(errorOutput, standardOutput);
            return View(modelps);
        }
        public ActionResult ExecutarATMs2()
        {
            standardOutput = "";
            errorOutput = "";
            RunScript(fileATMs);
            PowershellModel modelps = new PowershellModel(errorOutput, standardOutput);
            return View("ExecutarATMs", modelps);
        }
        public ActionResult ExecutarAS400()
        {
            standardOutput = "";
            errorOutput = "";
            PowershellModel modelps = new PowershellModel(errorOutput, standardOutput);
            return View(modelps);
        }
        public ActionResult ExecutarAS4002()
        {
            standardOutput = "";
            errorOutput = "";
            RunScript(fileAS400);
            PowershellModel modelps = new PowershellModel(errorOutput, standardOutput);
            return View("ExecutarAS400", modelps);
        }
        public ActionResult ExecutarEDiscoB()
        {
            standardOutput = "";
            errorOutput = "";
            PowershellModel modelps = new PowershellModel(errorOutput, standardOutput);
            return View(modelps);
        }
        public ActionResult ExecutarEDiscoB2()
        {
            standardOutput = "";
            errorOutput = "";
            RunScript(fileEDiscoBalcoes);
            PowershellModel modelps = new PowershellModel(errorOutput, standardOutput);
            return View("ExecutarEDiscoB", modelps);
        }
        public ActionResult ExecutarEDiscoSC()
        {
            standardOutput = "";
            errorOutput = "";
            PowershellModel modelps = new PowershellModel(errorOutput, standardOutput);
            return View(modelps);
        }
        public ActionResult ExecutarEDiscoSC2()
        {
            standardOutput = "";
            errorOutput = "";
            RunScript(fileEDiscoSC);
            PowershellModel modelps = new PowershellModel(errorOutput, standardOutput);
            return View("ExecutarEDiscoSC", modelps);
        }
        public ActionResult ExecutarEServSC()
        {
            standardOutput = "";
            errorOutput = "";
            PowershellModel modelps = new PowershellModel(errorOutput, standardOutput);
            return View(modelps);
        }
        public ActionResult ExecutarEServSC2()
        {
            standardOutput = "";
            errorOutput = "";
            RunScript(fileEServSC);
            PowershellModel modelps = new PowershellModel(errorOutput, standardOutput);
            return View("ExecutarEServSC", modelps);
        }
        public ActionResult ExecutarRAID()
        {
            standardOutput = "";
            errorOutput = "";
            PowershellModel modelps = new PowershellModel(errorOutput, standardOutput);
            return View(modelps);
        }
        public ActionResult ExecutarRAID2()
        {
            standardOutput = "";
            errorOutput = "";
            RunScript(fileRAID);
            PowershellModel modelps = new PowershellModel(errorOutput, standardOutput);
            return View("ExecutarRAID", modelps);
        }
        public ActionResult ExecutarMcafeesc()
        {
            standardOutput = "";
            errorOutput = "";
            PowershellModel modelps = new PowershellModel(errorOutput, standardOutput);
            return View(modelps);
        }
        public ActionResult ExecutarMcafeesc2()
        {
            standardOutput = "";
            errorOutput = "";
            RunScript(fileMcafeeSC);
            PowershellModel modelps = new PowershellModel(errorOutput, standardOutput);
            return View("ExecutarMcafeesc", modelps);
        }
        public ActionResult ExecutarMcafeebc()
        {
            standardOutput = "";
            errorOutput = "";
            PowershellModel modelps = new PowershellModel(errorOutput, standardOutput);
            return View(modelps);
        }
        public ActionResult ExecutarMcafeebc2()
        {
            standardOutput = "";
            errorOutput = "";
            RunScript(fileMcafeeB);
            PowershellModel modelps = new PowershellModel(errorOutput, standardOutput);
            return View("ExecutarMcafeebc", modelps);
        }
        public ActionResult AddContactoBalcoes()
        {
            contactosviewModel = GetContactosPortalBD();
            List<Contacto> filtredList = new List<Contacto>();
            filtredList = contactosList;
            foreach (ContactBalcao contact in ContactsList)
            {
                foreach (Contacto cnt in filtredList.ToList())
                {
                    if (contact.Contacto.Id == cnt.Id)
                    {
                        filtredList.Remove(cnt);
                    }
                }
            }
            contactosviewModel.Contactos = filtredList;
            return PartialView(contactosviewModel);
        }
        public ActionResult AddContactoExtranet()
        {
            contactosviewModel = GetContactosPortalBD();
            List<Contacto> filtredList = new List<Contacto>();
            filtredList = contactosList;
            foreach (ContactExtranet contact in ContactsExtranetsList)
            {
                foreach (Contacto cnt in filtredList.ToList())
                {
                    if (contact.Contacto.Id == cnt.Id)
                    {
                        filtredList.Remove(cnt);
                    }
                }
            }
            contactosviewModel.Contactos = filtredList;
            return PartialView(contactosviewModel);
        }
        public ActionResult AddContactoAPC()
        {
            contactosviewModel = GetContactosPortalBD();
            List<Contacto> filtredList = new List<Contacto>();
            filtredList = contactosList;
            foreach (ContactAPC contact in ContactsAPCList)
            {
                foreach (Contacto cnt in filtredList.ToList())
                {
                    if (contact.Contacto.Id == cnt.Id)
                    {
                        filtredList.Remove(cnt);
                    }
                }
            }
            contactosviewModel.Contactos = filtredList;
            return PartialView(contactosviewModel);
        }
        public ActionResult AddContactoExchange()
        {
            contactosviewModel = GetContactosPortalBD();
            List<Contacto> filtredList = new List<Contacto>();
            filtredList = contactosList;
            foreach (ContactExchange contact in ContactsExchangeList)
            {
                foreach (Contacto cnt in filtredList.ToList())
                {
                    if (contact.Contacto.Id == cnt.Id)
                    {
                        filtredList.Remove(cnt);
                    }
                }
            }
            contactosviewModel.Contactos = filtredList;
            return PartialView(contactosviewModel);
        }

        public ActionResult AddContacto()
        {
            GetEquipasFromDB();
            AddContactoListViewModel viewModel = new AddContactoListViewModel();
            String associar = "";
            viewModel.equipas = equipasList;
            viewModel.associar = associar;
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult AddContacto2(AddContactoListViewModel model)
        {
            Contacto novoContacto = new Contacto();
            novoContacto.Id = ObjectId.GenerateNewId();
            novoContacto.Nome = Request["frmNome"].ToString();
            novoContacto.Telefone = Request["frmTelefone"].ToString();
            novoContacto.Email = Request["frmEmail"].ToString();
            String id = Request["drpListaEquipa"].ToString();
          

            if(id != "" )
            {
                Equipa equipa = FindEquipa(id);
                novoContacto.Equipa = equipa.Nome;
                novoContacto.IDEquipa = equipa.Id;
                Context.Contactos.InsertOne(novoContacto);
                contactosviewModel = GetContactosPortalBD();
                List<Contacto> SortedLis2t = contactosList.OrderBy(o => o.Nome).ToList();
                contactosList = SortedLis2t;
                if(model.associar == "Sim")
                {
                    GetTemplatesFromBD();
                    foreach(Template template in templatesList)
                    {
                        foreach(Equipa equipa2 in template.Equipas)
                        {
                            if(equipa2.Id == equipa.Id)
                            {
                                Expression<Func<Template, bool>> filter5 = (x => x.Id == template.Id);
                                Template modifyT = Context.Templates.Find(filter5).FirstOrDefault();
                                try
                                {
                                        modifyT.Contactos.Add(novoContacto);
                                        Context.Templates.ReplaceOne(filter5, modifyT);
                                }
                                catch { }
                            }
                        }
                    }

                }
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt2').style.display='block'; document.getElementById('hidenatt2').innerHTML='Contacto " + novoContacto.Nome + " adicionado com sucesso.';" +
                                   "document.getElementById('btngravar').disabled = true;</script>";
            }
            else
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt2').style.display='block'; document.getElementById('hidenatt2').innerHTML='É obrigatório especificar a Equipa!';" +
                                  "document.getElementById('btngravar').disabled = false;</script>";
            }
          
           
            return View("Contactos", contactosviewModel);
        }

        [HttpPost]
        public ActionResult AddContactoBalcoes2()
        {
            ContactBalcao novoContacto = new ContactBalcao();
            String id = Request["frmContactos"].ToString();
            Contacto findedContacto = FindContacto(id);
            novoContacto.Id = findedContacto.Id;
            novoContacto.Contacto = findedContacto;
            novoContacto.Notf_SMS = Request["drpListNotfSMS"].ToString();
            novoContacto.Notf_Email = Request["drpListNotfEmail"].ToString();
            novoContacto.Zona = Request["drpZona"].ToString();
            Context.ContactosBalcoes.InsertOne(novoContacto);
            GetContactosBalcoesBD();
            List<ContactBalcao> SortedLis2t = ContactsList.OrderBy(o => o.Contacto.Nome).ToList();
            ContactsList = SortedLis2t;
            viewModel.Contactbalcao = ContactsList;
            TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt2').style.display='block'; document.getElementById('hidenatt2').innerHTML='Contacto " + novoContacto.Contacto.Nome + " adicionado com sucesso.';" +
                                      "document.getElementById('btngravar').disabled = true;</script>";
            return View("balcoes", viewModel);
        }

        [HttpPost]
        public ActionResult AddContactoExtranet2()
        {
            ContactExtranet novoContacto = new ContactExtranet();
            String id = Request["frmContactos"].ToString();
            Contacto findedContacto = FindContacto(id);
            novoContacto.Id = findedContacto.Id;
            novoContacto.Contacto = findedContacto;
            novoContacto.Notf_SMS = Request["drpListNotfSMS"].ToString();
            novoContacto.Notf_Email = Request["drpListNotfEmail"].ToString();
            novoContacto.Fim = Request["drpListFim"].ToString();
            Context.ContactosExtranet.InsertOne(novoContacto);
            GetContactosExtranetBD();
            List<ContactExtranet> SortedLis2t = ContactsExtranetsList.OrderBy(o => o.Contacto.Nome).ToList();
            ContactsExtranetsList = SortedLis2t;
            viewExtranetModel.ContactsExtranets = ContactsExtranetsList;
            TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt2').style.display='block'; document.getElementById('hidenatt2').innerHTML='Contacto " + novoContacto.Contacto.Nome + " adicionado com sucesso.';" +
                                      "document.getElementById('btngravar').disabled = true;</script>";
            return View("Extranets", viewExtranetModel);
        }

        [HttpPost]
        public ActionResult AddContactoAPC2()
        {
            ContactAPC novoContacto = new ContactAPC();
            String id = Request["frmContactos"].ToString();
            Contacto findedContacto = FindContacto(id);
            novoContacto.Id = findedContacto.Id;
            novoContacto.Contacto = findedContacto;
            novoContacto.Notf_SMS = Request["drpListNotfSMS"].ToString();
            Context.ContactosAPC.InsertOne(novoContacto);
            GetContactosAPCBD();
            List<ContactAPC> SortedLis2t = ContactsAPCList.OrderBy(o => o.Contacto.Nome).ToList();
            ContactsAPCList = SortedLis2t;
            APCviewModel.ContactsAPC = ContactsAPCList;
            TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt2').style.display='block'; document.getElementById('hidenatt2').innerHTML='Contacto " + novoContacto.Contacto.Nome + " adicionado com sucesso.';" +
                                      "document.getElementById('btngravar').disabled = true;</script>";
            return View("APC", APCviewModel);
        }

        [HttpPost]
        public ActionResult AddContactoExchange2()
        {
            ContactExchange novoContacto = new ContactExchange();
            String id = Request["frmContactos"].ToString();
            Contacto findedContacto = FindContacto(id);
            novoContacto.Id = findedContacto.Id;
            novoContacto.Contacto = findedContacto;
            novoContacto.Notf_SMS = Request["drpListNotfSMS"].ToString();
            Context.ContactosExchange.InsertOne(novoContacto);
            GetContactosExchangeBD();
            List<ContactExchange> SortedLis2t = ContactsExchangeList.OrderBy(o => o.Contacto.Nome).ToList();
            ContactsExchangeList = SortedLis2t;
            ExchangeviewModel.ContactsExchange = ContactsExchangeList;
            TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt2').style.display='block'; document.getElementById('hidenatt2').innerHTML='Contacto " + novoContacto.Contacto.Nome + " adicionado com sucesso.';" +
                                      "document.getElementById('btngravar').disabled = true;</script>";
            return View("Exchange", ExchangeviewModel);
        }
        public ActionResult balcoes()
        {
            ViewBag.Message = "Your Balcões page.";
            viewModel = GetContactosBalcoesBD();
            if (ContactsList.Count() == 0)
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt2').style.display='block'; document.getElementById('hidenatt2').innerHTML='Impossivel Carregar dados.. Possivelmente a Base de dados esteja indisponivel. (Verificar serviço MongoDB se está a correr no servidor SEMZSEIPTG11';" +
                                     "document.getElementById('btngravar').disabled = true;</script>";
            }
            return View(viewModel);
        }
        public ActionResult Extranets()
        {
            ViewBag.Message = "Your Balcões page.";
            viewExtranetModel = GetContactosExtranetBD();
            if (ContactsExtranetsList.Count() == 0)
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt2').style.display='block'; document.getElementById('hidenatt2').innerHTML='Impossivel Carregar dados.. Possivelmente a Base de dados esteja indisponivel. (Verificar serviço MongoDB se está a correr no servidor SEMZSEIPTG11';" +
                                     "document.getElementById('btngravar').disabled = true;</script>";
            }
            return View(viewExtranetModel);
        }

        public ActionResult APC()
        {
            ViewBag.Message = "Your APC page.";
            APCviewModel = GetContactosAPCBD();
            if (ContactsAPCList.Count() == 0)
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt2').style.display='block'; document.getElementById('hidenatt2').innerHTML='Impossivel Carregar dados.. Possivelmente a Base de dados esteja indisponivel. (Verificar serviço MongoDB se está a correr no servidor SEMZSEIPTG11';" +
                                     "document.getElementById('btngravar').disabled = true;</script>";
            }
            return View(APCviewModel);
        }


        public ActionResult Exchange()
        {
            ViewBag.Message = "Your Exchange page.";
            ExchangeviewModel = GetContactosExchangeBD();
            if (ContactsExchangeList.Count() == 0)
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt2').style.display='block'; document.getElementById('hidenatt2').innerHTML='Impossivel Carregar dados.. Possivelmente a Base de dados esteja indisponivel. (Verificar serviço MongoDB se está a correr no servidor SEMZSEIPTG11';" +
                                     "document.getElementById('btngravar').disabled = true;</script>";
            }
            return View(ExchangeviewModel);
        }
        public ActionResult Contactos()
        {
            ViewBag.Message = "Your Balcões page.";
            contactosviewModel = GetContactosPortalBD();
            // viewModel = await Readfromfile2();
            //viewModelMilleteller = await ReadfromfileMilleteller2();
            //APCviewModel = await ReadfromfileAPC2();
            //templateview = await ReadTemplatesfromfile();

            if (contactosList.Count() == 0)
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt2').style.display='block'; document.getElementById('hidenatt2').innerHTML='Impossivel Carregar dados.. Possivelmente a Base de dados esteja indisponivel. (Verificar serviço MongoDB se está a correr no servidor SEMZSEIPTG11';" +
                                     "document.getElementById('btngravar').disabled = true;</script>";
            }
            return View(contactosviewModel);
        }

        public ActionResult ContactosReport()
        {
            ViewBag.Message = "Your Balcões page.";
            contactosviewModel = GetContactosPortalBD();
            viewModel = GetContactosBalcoesBD();
            viewModelMilleteller = GetContactosMilletellerBD();
            APCviewModel = GetContactosAPCBD();
            templateview = GetTemplatesFromBD();
            if (contactosList.Count() == 0)
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt2').style.display='block'; document.getElementById('hidenatt2').innerHTML='Impossivel Carregar dados.. Possivelmente a Base de dados esteja indisponivel. (Verificar serviço MongoDB se está a correr no servidor SEMZSEIPTG11';" +
                                      "document.getElementById('btngravar').disabled = true;</script>";
            }
            return new Rotativa.PartialViewAsPdf("ContactosReport", contactosviewModel);
        }


        public ActionResult orderNomeContactos()
        {
            ViewBag.Message = "Your Balcões page.";

            contactosviewModel = GetContactosPortalBD();
            viewModel = GetContactosBalcoesBD();
            viewModelMilleteller = GetContactosMilletellerBD();
            APCviewModel = GetContactosAPCBD();
            templateview = GetTemplatesFromBD();
            List<Contacto> SortedLis2t = contactosList.OrderBy(o => o.Nome).ToList();
            contactosviewModel.Contactos = SortedLis2t;
            if (contactosList.Count() == 0)
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt2').style.display='block'; document.getElementById('hidenatt2').innerHTML='Impossivel Carregar dados.. Possivelmente a Base de dados esteja indisponivel. (Verificar serviço MongoDB se está a correr no servidor SEMZSEIPTG11';" +
                                      "document.getElementById('btngravar').disabled = true;</script>";
            }
            return View("Contactos", contactosviewModel);
        }
        public ActionResult orderEquipaContactos()
        {
            ViewBag.Message = "Your Balcões page.";
            contactosviewModel = GetContactosPortalBD();
            viewModel = GetContactosBalcoesBD();
            viewModelMilleteller = GetContactosMilletellerBD();
            APCviewModel = GetContactosAPCBD();
            templateview = GetTemplatesFromBD();
            List<Contacto> SortedLis2t = contactosList.OrderBy(o => o.Equipa).ToList();
            contactosviewModel.Contactos = SortedLis2t;
            if (contactosList.Count() == 0)
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt2').style.display='block'; document.getElementById('hidenatt2').innerHTML='Impossivel Carregar dados.. Possivelmente a Base de dados esteja indisponivel. (Verificar serviço MongoDB se está a correr no servidor SEMZSEIPTG11';" +
                                      "document.getElementById('btngravar').disabled = true;</script>";
            }
            return View("Contactos", contactosviewModel);
        }
        public ActionResult EstatisticasEMC()
        {
            EstatisticaEMCListViewModel result = new EstatisticaEMCListViewModel();
            return View(result);
        }
        public ActionResult EstatisticasEMC2(FormCollection formCollection)
        {
            EstatisticaEMCListViewModel result = new EstatisticaEMCListViewModel();
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {

                    string fileName = file.FileName;
                    string fileNamewte = Path.GetFileNameWithoutExtension(fileName);
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    file.SaveAs(Server.MapPath("~/App_Data/") + fileName);
                    string filepath = Server.MapPath("~/App_Data/") + fileName;

                    var EstatisticasEMC = new List<EstatisticaEMC>();

                    //Convert File if extension xls
                    if (fileName.EndsWith(".csv") || fileName.EndsWith(".CSV"))
                    {
                        if (System.IO.File.Exists(filepath))
                        {
                            var lines = System.IO.File.ReadAllLines(filepath).Skip(1).TakeWhile(t => t != null);
                            foreach (string item in lines)
                            {
                                var values = item.Split(',');
                                
                                EstatisticasEMC.Add(new EstatisticaEMC(values[0].Replace('"',' '), values[1].Replace('"', ' '), values[2].Replace('"', ' ').Replace(".", ","), values[3].Replace('"', ' ').Replace(".", ","), values[4].Replace('"', ' ').Replace(".", ","), values[5].Replace('"', ' ').Replace(".", ",")));
                            }
                            result.EstatisticasEMC = EstatisticasEMC;
                        }
                        else
                        {
                            TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block';</script>";
                        }
                    }
                    else
                    {
                        TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt2').style.display='block';</script>";
                    }
                }
            }
            return View("EstatisticasEMC", result);
        }

        public ActionResult EstatisticasEnvioRecepcao()
        {
            ResultadoProcessamento result = new ResultadoProcessamento("", "");
            return View(result);
        }
        public ActionResult EstatisticasEnvioRecepcao2(FormCollection formCollection)
        {
            ResultadoProcessamento result = new ResultadoProcessamento("", "");
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileNamewte = Path.GetFileNameWithoutExtension(fileName);
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    file.SaveAs(Server.MapPath("~/App_Data/") + fileName);
                    string filepath = Server.MapPath("~/App_Data/") + fileName;
                    var EstatisticaMcel = new List<EstatisticaEnvioRecepcao>();
                    var EstatisticaVodacom = new List<EstatisticaEnvioRecepcao>();
                    var EstatisticaMovitel = new List<EstatisticaEnvioRecepcao>();
                    //Convert File if extension xls
                    if (fileName.EndsWith(".xls") && fileName.Contains("MonitorSIG_Operadoras"))
                    {
                        Workbook book = new Workbook();
                        //load a document from file
                        book.LoadFromStream(file.InputStream);
                        //Save the file to the version you want
                        filepath = Server.MapPath("~/App_Data/") + fileNamewte + ".xlsx";
                        book.SaveToFile(filepath, ExcelVersion.Version2013);
                        if (System.IO.File.Exists(filepath))
                        {
                            processarEstatisticaEnvioRecepcao(result, out EstatisticaMcel, out EstatisticaVodacom, out EstatisticaMovitel, filepath);
                        }
                        else
                        {
                            TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block';</script>";
                        }
                    }
                    else if (fileName.EndsWith(".xlsx") && fileName.Contains("MonitorSIG_Operadoras"))
                    {
                        if (System.IO.File.Exists(filepath))
                        {
                            processarEstatisticaEnvioRecepcao(result, out EstatisticaMcel, out EstatisticaVodacom, out EstatisticaMovitel, filepath);
                        }
                        else
                        {
                            TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block';</script>";
                        }
                    }
                    else
                    {
                        TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt2').style.display='block';</script>";
                    }
                }
            }
            return View("EstatisticasEnvioRecepcao", result);
        }
        private static void processarEstatisticaEnvioRecepcao(ResultadoProcessamento result, out List<EstatisticaEnvioRecepcao> EstatisticaMcel, out List<EstatisticaEnvioRecepcao> EstatisticaVodacom, out List<EstatisticaEnvioRecepcao> EstatisticaMovitel, string filepath)
        {
            using (XLWorkbook workbook = new XLWorkbook(filepath))
            {
                //MCEL
                var workSheet = workbook.Worksheet(1);
                EstatisticaMcel = ProcessarFicheiroEstatisticaEnvioRecepcao(workSheet);
                //VODACOM
                var workSheet2 = workbook.Worksheet(2);
                EstatisticaVodacom = ProcessarFicheiroEstatisticaEnvioRecepcao(workSheet2);
                //MOVITEL
                var workSheet3 = workbook.Worksheet(3);
                EstatisticaMovitel = ProcessarFicheiroEstatisticaEnvioRecepcao(workSheet3);
                //Processamento de Informação
                //Mcel
                int countMcelAbaixo = 0;
                long somaMcelAbaixo = 0;
                int countMcelAcima = 0;
                long somaMcelAcima = 0;
                foreach (EstatisticaEnvioRecepcao item in EstatisticaMcel)
                {
                    //Abaixo do SLA
                    if (item.Acima_Abaixo.Contains("ABAIXO"))
                    {
                        somaMcelAbaixo = somaMcelAbaixo + long.Parse(item.Duracao);
                        countMcelAbaixo = countMcelAbaixo + 1;
                    }
                    //Acima do SLA
                    if (item.Acima_Abaixo.Contains("ACIMA"))
                    {
                        somaMcelAcima = somaMcelAcima + long.Parse(item.Duracao);
                        countMcelAcima = countMcelAcima + 1;
                    }
                }
                //Vodacom
                int countVodacomAbaixo = 0;
                long somaVodacomAbaixo = 0;
                int countVodacomAcima = 0;
                long somaVodacomAcima = 0;
                foreach (EstatisticaEnvioRecepcao item in EstatisticaVodacom)
                {
                    //Abaixo do SLA
                    if (item.Acima_Abaixo.Contains("ABAIXO"))
                    {
                        somaVodacomAbaixo = somaVodacomAbaixo + long.Parse(item.Duracao);
                        countVodacomAbaixo = countVodacomAbaixo + 1;
                    }
                    //Acima do SLA
                    if (item.Acima_Abaixo.Contains("ACIMA"))
                    {
                        somaVodacomAcima = somaVodacomAcima + long.Parse(item.Duracao);
                        countVodacomAcima = countVodacomAcima + 1;
                    }
                }
                //Movitel
                int countMovitelAbaixo = 0;
                long somaMovitelAbaixo = 0;
                int countMovitelAcima = 0;
                long somaMovitelAcima = 0;
                foreach (EstatisticaEnvioRecepcao item in EstatisticaMovitel)
                {
                    //Abaixo do SLA
                    if (item.Acima_Abaixo.Contains("ABAIXO"))
                    {
                        somaMovitelAbaixo = somaMovitelAbaixo + long.Parse(item.Duracao);
                        countMovitelAbaixo = countMovitelAbaixo + 1;
                    }
                    //Acima do SLA
                    if (item.Acima_Abaixo.Contains("ACIMA"))
                    {
                        somaMovitelAcima = somaMovitelAcima + long.Parse(item.Duracao);
                        countMovitelAcima = countMovitelAcima + 1;
                    }
                }
                //calculo de Médias
                //Abaixo do SLA
                Decimal mediaMcelAbaixo = (Decimal)somaMcelAbaixo / (Decimal)countMcelAbaixo;
                Decimal mediaVodacomAbaixo = (Decimal)somaVodacomAbaixo / (Decimal)countVodacomAbaixo;
                Decimal mediaMovitelAbaixo = (Decimal)somaMovitelAbaixo / (Decimal)countMovitelAbaixo;
                var stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("----------------------------------------------------------------------------------------------------------");
                stringBuilder.AppendLine("TEMPO MÉDIO DE ENVIO VS RECEPÇÃO POR OPERADORA <--- Este são os tempos a considerar para o IT DASHBOARD:");
                stringBuilder.AppendLine("MCEL = " + String.Format("{0:N2}", mediaMcelAbaixo).Replace(",", ".") + " segundos");
                stringBuilder.AppendLine("VODACOM = " + String.Format("{0:N2}", mediaVodacomAbaixo).Replace(",", ".") + " segundos");
                stringBuilder.AppendLine("MOVITEL = " + String.Format("{0:N2}", mediaMovitelAbaixo).Replace(",", ".") + " segundos");
                stringBuilder.AppendLine("----------------------------------------------------------------------------------------------------------");
                stringBuilder.AppendLine("TOTAL DE MENSAGENS ENVIADAS COM SUCESSO:");
                stringBuilder.AppendLine("MCEL = " + countMcelAbaixo + " mensagens");
                stringBuilder.AppendLine("VODACOM = " + countVodacomAbaixo + " mensagens");
                stringBuilder.AppendLine("MOVITEL = " + countMovitelAbaixo + " mensagens");
                stringBuilder.AppendLine("----------------------------------------------------------------------------------------------------------");
                stringBuilder.AppendLine("TOTAL DE MENSAGENS ENVIADAS SEM SUCESSO:");
                stringBuilder.AppendLine("MCEL = " + countMcelAcima + " mensagens");
                stringBuilder.AppendLine("VODACOM = " + countVodacomAcima + " mensagens");
                stringBuilder.AppendLine("MOVITEL = " + countMovitelAcima + " mensagens");
                stringBuilder.AppendLine("----------------------------------------------------------------------------------------------------------");
                result.Output = stringBuilder.ToString();
            }
        }

        private static List<EstatisticaEnvioRecepcao> ProcessarFicheiroEstatisticaEnvioRecepcao(IXLWorksheet workSheet)
        {
            long noOfCol = workSheet.ColumnCount();
            long noOfRow = workSheet.RowCount();
            List<EstatisticaEnvioRecepcao> lista = new List<EstatisticaEnvioRecepcao>();
            for (int rowIterator = 11; rowIterator <= noOfRow; rowIterator++)
            {
                bool linhaVazia = workSheet.Cell(rowIterator, 8).IsEmpty();
                if (linhaVazia == false)
                {
                    var Estatistica = new EstatisticaEnvioRecepcao();
                    Estatistica.Cellnbr = workSheet.Cell(rowIterator, 1).Value.ToString();
                    Estatistica.Saida = workSheet.Cell(rowIterator, 2).Value.ToString();
                    Estatistica.Entrada = workSheet.Cell(rowIterator, 3).Value.ToString();
                    Estatistica.Duracao = workSheet.Cell(rowIterator, 4).Value.ToString();
                    Estatistica.Hora = workSheet.Cell(rowIterator, 5).Value.ToString();
                    Estatistica.Intervalo = workSheet.Cell(rowIterator, 6).Value.ToString();
                    Estatistica.Operadora = workSheet.Cell(rowIterator, 7).Value.ToString();
                    Estatistica.Acima_Abaixo = workSheet.Cell(rowIterator, 8).Value.ToString();
                    lista.Add(Estatistica);
                }
            }
            return lista;
        }
        public ActionResult EstatisticasMobile()
        {
            ResultadoProcessamento result = new ResultadoProcessamento("", "");
            return View(result);
        }
        public ActionResult EstatisticasMobile2(FormCollection formCollection)
        {
            ResultadoProcessamento result = new ResultadoProcessamento("", "");
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileNamewte = Path.GetFileNameWithoutExtension(fileName);
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    var EstatisticaList = new List<EstatisticaMobile>();
                    file.SaveAs(Server.MapPath("~/App_Data/") + fileName);
                    string filepath = Server.MapPath("~/App_Data/") + fileName;
                    //Convert File if extension xls
                    if (fileName.EndsWith(".xls") && fileName.Contains("IZI_Estatisticas"))
                    {
                        Workbook book = new Workbook();
                        //load a document from file
                        book.LoadFromStream(file.InputStream);
                        //Save the file to the version you want
                        filepath = Server.MapPath("~/App_Data/") + fileNamewte + ".xlsx";
                        book.SaveToFile(filepath, ExcelVersion.Version2013);
                        if (System.IO.File.Exists(filepath))
                        {
                            processarEstatisticaMobile(result, EstatisticaList, filepath);
                        }
                        else
                        {
                            TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block';</script>";
                        }
                    }
                    else if (fileName.EndsWith(".xlsx") && fileName.Contains("IZI_Estatisticas"))
                    {
                        if (System.IO.File.Exists(filepath))
                        {
                            processarEstatisticaMobile(result, EstatisticaList, filepath);
                        }
                        else
                        {
                            TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt3').style.display='block';</script>";
                        }
                    }
                    else
                    {
                        TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt2').style.display='block';</script>";
                    }
                }
            }
            return View("EstatisticasMobile", result);
        }

        private static void processarEstatisticaMobile(ResultadoProcessamento result, List<EstatisticaMobile> EstatisticaList, string filepath)
        {
            using (XLWorkbook workbook = new XLWorkbook(filepath))
            {
                var workSheet = workbook.Worksheet(1);
                long noOfCol = workSheet.ColumnCount();
                long noOfRow = workSheet.RowCount();
                for (int rowIterator = 11; rowIterator <= noOfRow; rowIterator++)
                {
                    bool linhaVazia = workSheet.Cell(rowIterator, 5).IsEmpty();
                    if (linhaVazia == false)
                    {
                        var Estatistica = new EstatisticaMobile();
                        Estatistica.Dia = workSheet.Cell(rowIterator, 1).Value.ToString();
                        Estatistica.NumeroSMS = workSheet.Cell(rowIterator, 2).Value.ToString();
                        Estatistica.TipoSMS = workSheet.Cell(rowIterator, 3).Value.ToString();
                        Estatistica.Operadora = workSheet.Cell(rowIterator, 4).Value.ToString();
                        Estatistica.Canal = workSheet.Cell(rowIterator, 5).Value.ToString();
                        EstatisticaList.Add(Estatistica);
                    }
                }
                //Processamento de Informação
                float somatorioSMARTIZIMCEL = 0;
                float somatorioSMARTIZIMOVITEL = 0;
                float somatorioSMARTIZIVODACOM = 0;
                float somatorioIZIMCEL = 0;
                float somatorioIZIMOVITEL = 0;
                float somatorioIZIVODACOM = 0;
                foreach (EstatisticaMobile item in EstatisticaList)
                {
                    // PROCESS SMARTIZI
                    if (item.Operadora.Contains("MCEL") && item.Canal.Contains("SMART IZI"))
                    {
                        somatorioSMARTIZIMCEL = somatorioSMARTIZIMCEL + long.Parse(item.NumeroSMS);
                    }
                    if (item.Operadora.Contains("VDCOM") && item.Canal.Contains("SMART IZI"))
                    {
                        somatorioSMARTIZIVODACOM = somatorioSMARTIZIVODACOM + long.Parse(item.NumeroSMS);
                    }
                    if (item.Operadora.Contains("MVTEL") && item.Canal.Contains("SMART IZI"))
                    {
                        somatorioSMARTIZIMOVITEL = somatorioSMARTIZIMOVITEL + long.Parse(item.NumeroSMS);
                    }
                    ///PROCESS IZI
                    if (item.Operadora.Contains("MCEL") && item.Canal.Contains("IZI"))
                    {
                        somatorioIZIMCEL = somatorioIZIMCEL + long.Parse(item.NumeroSMS);
                    }
                    if (item.Operadora.Contains("VDCOM") && item.Canal.Contains("IZI"))
                    {
                        somatorioIZIVODACOM = somatorioIZIVODACOM + long.Parse(item.NumeroSMS);
                    }
                    if (item.Operadora.Contains("MVTEL") && item.Canal.Contains("IZI"))
                    {
                        somatorioIZIMOVITEL = somatorioIZIMOVITEL + long.Parse(item.NumeroSMS);
                    }
                }
                //Output
                float somatotal = somatorioSMARTIZIMCEL + somatorioSMARTIZIMOVITEL + somatorioSMARTIZIVODACOM + somatorioIZIMCEL + somatorioIZIMOVITEL + somatorioIZIVODACOM;
                float somaizi = somatorioIZIMCEL + somatorioIZIMOVITEL + somatorioIZIVODACOM;
                float somasmartizi = somatorioSMARTIZIMCEL + somatorioSMARTIZIMOVITEL + somatorioSMARTIZIVODACOM;
                var stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("----------------------------------");
                stringBuilder.AppendLine("TOTAL DE MENSAGENS:");
                stringBuilder.AppendLine("Serviços Mobile(IZI e SMARTIZI) = " + String.Format("{0:N3}", Convert.ToDecimal(somatotal) / 1000).Replace("  ", string.Empty).Replace(" ", ".").Replace(",", ".") + " mensagens");
                stringBuilder.AppendLine("----------------------------------");
                stringBuilder.AppendLine("TOTAL DE MENSAGENS POR CANAL (IZI | SMARTIZI):");
                stringBuilder.AppendLine("IZI = " + String.Format("{0:N3}", Convert.ToDecimal(somaizi) / 1000).Replace("  ", string.Empty).Replace(" ", ".").Replace(",", ".") + " mensagens");
                stringBuilder.AppendLine("SMARTIZI = " + String.Format("{0:N3}", Convert.ToDecimal(somasmartizi) / 1000).Replace("  ", string.Empty).Replace(" ", ".").Replace(",", ".") + " mensagens");
                stringBuilder.AppendLine("----------------------------------");
                stringBuilder.AppendLine("TOTAL DE MENSAGENS POR CANAL & OPERADORA (IZI | SMARTIZI & OPERADORA):");
                stringBuilder.AppendLine("SMARTIZI MCEL = " + String.Format("{0:N3}", Convert.ToDecimal(somatorioSMARTIZIMCEL) / 1000).Replace("  ", string.Empty).Replace(" ", ".").Replace(",", ".") + " mensagens");
                stringBuilder.AppendLine("SMARTIZI VODACOM = " + String.Format("{0:N3}", Convert.ToDecimal(somatorioSMARTIZIVODACOM) / 1000).Replace("  ", string.Empty).Replace(" ", ".").Replace(",", ".") + " mensagens");
                stringBuilder.AppendLine("SMARTIZI MOVITEL = " + String.Format("{0:N3}", Convert.ToDecimal(somatorioSMARTIZIMOVITEL) / 1000).Replace("  ", string.Empty).Replace(" ", ".").Replace(",", ".") + " mensagens");
                stringBuilder.AppendLine("IZI MCEL = " + String.Format("{0:N3}", Convert.ToDecimal(somatorioIZIMCEL) / 1000).Replace("  ", string.Empty).Replace(" ", ".").Replace(",", ".") + " mensagens");
                stringBuilder.AppendLine("IZI VODACOM = " + String.Format("{0:N3}", Convert.ToDecimal(somatorioIZIVODACOM) / 1000).Replace("  ", string.Empty).Replace(" ", ".").Replace(",", ".") + " mensagens");
                stringBuilder.AppendLine("IZI MOVITEL = " + String.Format("{0:N3}", Convert.ToDecimal(somatorioIZIMOVITEL) / 1000).Replace("  ", string.Empty).Replace(" ", ".").Replace(",", ".") + " mensagens");
                stringBuilder.AppendLine("----------------------------------");
                result.Output = stringBuilder.ToString();
            }
        }

        private static void GIncognito(params object[] args)
        {
            RunspaceInvoke scriptInvoker = new RunspaceInvoke();
            scriptInvoker.Invoke("Set-ExecutionPolicy Unrestricted");
        }


        public void RunScript2(string path) {

            errorOutput = "";
            StringBuilder scriptOutput = new StringBuilder();
            WSManConnectionInfo connectioninfo = new WSManConnectionInfo();
            connectioninfo.Credential = new PSCredential(@"semzseiptg12\Prtgadmin", ConvertToSecureString("DseMonUsr16"));
            connectioninfo.ComputerName = "semzseiptg12";
            //RunspaceConfiguration config = RunspaceConfiguration.Create();
            //create Powershell runspace
            using (var runspace = RunspaceFactory.CreateRunspace(connectioninfo))
            {
                runspace.Open();
                Collection<PSObject> results = new Collection<PSObject>();
                //create a pipeline and feed it the script text

                using (Pipeline pipeline = runspace.CreatePipeline())
                {
                    //create the command to run
         
                    pipeline.Commands.Add("Get - Process");
                    //execute the script
                    try
                    {
                        results = pipeline.Invoke();

                        if (results.Count != 0)
                        {
                            StringBuilder stringBuilder = new StringBuilder();
                            foreach (PSObject obj in results)
                            {
                                stringBuilder.AppendLine(obj.ToString());
                            }
                            standardOutput = stringBuilder.ToString();
                        }
                        // Check for errors in the pipeline and throw an exception if necessary.
                        if (pipeline.Error != null && pipeline.Error.Count > 0)
                        {
                            StringBuilder pipelineError = new StringBuilder();
                            pipelineError.AppendFormat("Pipeline Errors from Powershell Script: ");
                            foreach (var item in pipeline.Error.ReadToEnd())
                            {
                                pipelineError.AppendFormat("{0}\n", item);
                            }
                            errorOutput = pipelineError.ToString();
                        }
                    }
                    catch (System.Exception ex)
                    {
                        errorOutput = ex.ToString();
                    }
                }
                //close the runspace
                runspace.Close();
            }
        }
        public void RunScript(String path)
        {
            errorOutput = "";
            StringBuilder scriptOutput = new StringBuilder();
            WSManConnectionInfo connectioninfo = new WSManConnectionInfo();
          
            connectioninfo.Credential = new PSCredential(@"mz\Opsmgrmsaction", ConvertToSecureString("Dsemon&123"));
            connectioninfo.ComputerName = "semzseiptg12";
            RunspaceConfiguration config = RunspaceConfiguration.Create();
            //create Powershell runspace
            //path = @"C:\Temp\ScriptsTeste\testeRemotePTG12.ps1";
            using (var runspace = RunspaceFactory.CreateRunspace(connectioninfo))
            {
                runspace.Open();
               
                Collection<PSObject> results = new Collection<PSObject>();
                //create a pipeline and feed it the script text

                using (Pipeline pipeline = runspace.CreatePipeline())
                {
                    //create the command to run
                    Command cmd = new Command(path, true);
                    pipeline.Commands.Add(cmd);
                    //execute the script
                    try
                    {
                        results = pipeline.Invoke();

                        if (results.Count != 0)
                        {
                            StringBuilder stringBuilder = new StringBuilder();
                            foreach (PSObject obj in results)
                            {
                                stringBuilder.AppendLine(obj.ToString());
                            }
                            standardOutput = stringBuilder.ToString();
                        }
                        // Check for errors in the pipeline and throw an exception if necessary.
                        if (pipeline.Error != null && pipeline.Error.Count > 0)
                        {
                            StringBuilder pipelineError = new StringBuilder();
                            pipelineError.AppendFormat("Pipeline Errors from Powershell Script: ");
                            foreach (var item in pipeline.Error.ReadToEnd())
                            {
                                pipelineError.AppendFormat("{0}\n", item);
                            }
                            errorOutput = pipelineError.ToString();
                        }
                    }
                    catch (System.Exception ex)
                    {
                        errorOutput = ex.ToString();
                    }
                }
                //close the runspace
                runspace.Close();
            }
        }

        private SecureString ConvertToSecureString(string password)
        {
            if (password == null)
                throw new ArgumentNullException("password");

            var securePassword = new SecureString();

            foreach (char c in password)
                securePassword.AppendChar(c);

            securePassword.MakeReadOnly();
            return securePassword;
        }

        private GestaoEquipasListViewModel GetEquipasFromDB()
        {
            equipasList = new List<Equipa>();
            equipasviewModel = new GestaoEquipasListViewModel();

            equipasList = Context.Equipas.Find(new BsonDocument()).ToList();
            gruposList = Context.Grupos.Find(new BsonDocument()).ToList();
            if (equipasList != null)
            {
                List<Equipa> SortedLis2t = equipasList.OrderBy(o => o.Nome).ToList();
                equipasList = SortedLis2t;
                List<Grupo> SortedLis2t2 = gruposList.OrderBy(o => o.NomeGrupo).ToList();
                gruposList = SortedLis2t2;
            }
            equipasviewModel.equipas = equipasList;
            equipasviewModel.grupos = gruposList;
            return equipasviewModel;
        }

        private GestaoTemplatesListViewModel GetTemplatesFromBD()
        {
            templatesList = new List<Template>();
            templateview = new GestaoTemplatesListViewModel();
            templatesList = Context.Templates.Find(new BsonDocument()).ToList();
            List<Template> SortedLis2t = templatesList.OrderBy(o => o.NomeTemplate).ToList();
            templatesList = SortedLis2t;
            templateview.templates = templatesList;
            return templateview;
        }

        private CredenciaisListViewModel GetCredenciasFromDB()
        {
            credenciaisList = new List<Credencial>();
            credenciaisListViewModel = new CredenciaisListViewModel();
            credenciaisList = Context.Credenciais.Find(new BsonDocument()).ToList();
            List<Credencial> SortedLis2t = credenciaisList.OrderBy(o => o.User).ToList();
            credenciaisList = SortedLis2t;
            credenciaisListViewModel.Credenciais = credenciaisList;
            credenciaisListViewModel.temPermissao = VerifyUserAutorization();
            return credenciaisListViewModel;
        }
        private void ReadHistoricoFromFile()
        {
            historicoNotifList = new List<HistoricoNotificacao>();
            historicoNotifList = Context.HistoricoNotificacoes.Find(new BsonDocument()).ToList();
            List<HistoricoNotificacao> SortedLis2t = historicoNotifList.OrderByDescending(o => o.DataNotf).ToList();
            historicoNotifList = SortedLis2t;
        }

        private SondasListViewModel GetSondasFromDB()
        {
            sondasList = new List<Sonda>();
            sondasListViewModel = new SondasListViewModel();
            sondasList = Context.Sondas.Find(new BsonDocument()).ToList();
            List<Sonda> SortedLis2t = sondasList.OrderBy(o => o.Dns).ToList();
            sondasList = SortedLis2t;
            sondasListViewModel.Sondas = sondasList;
            return sondasListViewModel;
        }



        private LinksListViewModel GetLinksFromDB()
        {
            linksList = new List<Link>();
            linksListView = new LinksListViewModel();
            linksList = Context.Links.Find(new BsonDocument()).ToList();
            List<Link> SortedLis2t = linksList.OrderBy(o => o.Endereco).ToList();
            linksList = SortedLis2t;
            linksListView.Links = linksList;
            return linksListView;
        }

        private AplicacoesListViewModel GetAplicacoesFromDB()
        {
            aplicacoesList = new List<Aplicacao>();
            aplicacoesListViewModel = new AplicacoesListViewModel();
            aplicacoesList = Context.Aplicacoes.Find(new BsonDocument()).ToList();
            List<Aplicacao> SortedLis2t = aplicacoesList.OrderBy(o => o.Nome).ToList();
            aplicacoesList = SortedLis2t;
            aplicacoesListViewModel.Aplicacoes = aplicacoesList;
            return aplicacoesListViewModel;
        }

        private CartoesSIMListViewModel GetCartoesSIMFromDB()
        {
            cartoesSIMList = new List<CartaoSIM>();
            cartoesSIMListViewModel = new CartoesSIMListViewModel();
            cartoesSIMList = Context.cartoesSIM.Find(new BsonDocument()).ToList();
            List<CartaoSIM> SortedLis2t = cartoesSIMList.OrderBy(o => o.Numero).ToList();
            cartoesSIMList = SortedLis2t;
            cartoesSIMListViewModel.CartoesSIM = cartoesSIMList;
            return cartoesSIMListViewModel;
        }

        private ContactsListViewModel GetContactosPortalBD()
        {
            contactosList = new List<Contacto>();
            contactosviewModel = new ContactsListViewModel();
            contactosList = Context.Contactos.Find(new BsonDocument()).ToList();
            List<Contacto> SortedLis2t = contactosList.OrderBy(o => o.Nome).ToList();
            contactosList = SortedLis2t;
            contactosviewModel.Contactos = contactosList;
            return contactosviewModel;
        }
        //new implementation
        private ContactsBalcaoListViewModel GetContactosBalcoesBD()
        {
            ContactsList = new List<ContactBalcao>();
            viewModel = new ContactsBalcaoListViewModel();
            ContactsList = Context.ContactosBalcoes.Find(new BsonDocument()).ToList();
            List<ContactBalcao> SortedLis2t = ContactsList.OrderBy(o => o.Contacto.Nome).ToList();
            ContactsList = SortedLis2t;
            viewModel.Contactbalcao = ContactsList;
            return viewModel;
        }

        //new implementation
        private ContactsExtranetsListViewModel GetContactosExtranetBD()
        {

            ContactsExtranetsList = new List<ContactExtranet>();
            viewExtranetModel = new ContactsExtranetsListViewModel();
            ContactsExtranetsList = Context.ContactosExtranet.Find(new BsonDocument()).ToList();
            List<ContactExtranet> SortedLis2t = ContactsExtranetsList.OrderBy(o => o.Contacto.Nome).ToList();
            ContactsExtranetsList = SortedLis2t;
            viewExtranetModel.ContactsExtranets = ContactsExtranetsList;
            return viewExtranetModel;
        }
        private ContactsAPCListViewModel GetContactosAPCBD()
        {

            ContactsAPCList = new List<ContactAPC>();
            APCviewModel = new ContactsAPCListViewModel();
            ContactsAPCList = Context.ContactosAPC.Find(new BsonDocument()).ToList();
            List<ContactAPC> SortedLis2t = ContactsAPCList.OrderBy(o => o.Contacto.Nome).ToList();
            ContactsAPCList = SortedLis2t;
            APCviewModel.ContactsAPC = ContactsAPCList;
            return APCviewModel;
        }

        private ContactsExchangeListViewModel GetContactosExchangeBD()
        {

            ContactsExchangeList = new List<ContactExchange>();
            ExchangeviewModel = new ContactsExchangeListViewModel();
            ContactsExchangeList = Context.ContactosExchange.Find(new BsonDocument()).ToList();
            List<ContactExchange> SortedLis2t = ContactsExchangeList.OrderBy(o => o.Contacto.Nome).ToList();
            ContactsExchangeList = SortedLis2t;
            ExchangeviewModel.ContactsExchange = ContactsExchangeList;
            return ExchangeviewModel;
        }
        //new implementation
        private ContactsMilletellerListViewModel FilterMilleteller(String name)
        {
            ContactsListMilleteller = new List<ContactMilleteller>();
            viewModelMilleteller = new ContactsMilletellerListViewModel();
            ContactsListMilleteller = Context.ContactosMilleteller.Find(new BsonDocument()).ToList();
            //Filtro
            List<ContactMilleteller> ContactsListMilleteller2 = ContactsListMilleteller;
            foreach (ContactMilleteller contact in ContactsListMilleteller2.ToList())
            {
                if (!contact.Contacto.Nome.ToLower().Contains(name.ToLower()))
                {
                    ContactsListMilleteller2.Remove(contact);
                }
            }
            List<ContactMilleteller> SortedLis2t = ContactsListMilleteller2.OrderBy(o => o.Contacto.Nome).ToList();
            ContactsListMilleteller2 = SortedLis2t;
            viewModelMilleteller.ContactMilleteller = ContactsListMilleteller2;
            return viewModelMilleteller;
        }

        private ContactsBalcaoListViewModel FilterBalcoes(String name)
        {
            ContactsList = new List<ContactBalcao>();
            viewModel = new ContactsBalcaoListViewModel();
            ContactsList = Context.ContactosBalcoes.Find(new BsonDocument()).ToList();
            //Filtro
            List<ContactBalcao> ContactsList2 = ContactsList;
            foreach (ContactBalcao contact in ContactsList2.ToList())
            {
                if (!contact.Contacto.Nome.ToLower().Contains(name.ToLower()))
                {
                    ContactsList2.Remove(contact);
                }
            }
            List<ContactBalcao> SortedLis2t = ContactsList2.OrderBy(o => o.Contacto.Nome).ToList();
            ContactsList2 = SortedLis2t;
            viewModel.Contactbalcao = ContactsList2;
            return viewModel;
        }
        private ContactsAPCListViewModel FilterAPC(String name)
        {
            ContactsAPCList = new List<ContactAPC>();
            APCviewModel = new ContactsAPCListViewModel();
            ContactsAPCList = Context.ContactosAPC.Find(new BsonDocument()).ToList();
            //Filtro
            List<ContactAPC> ContactsList2 = ContactsAPCList;
            foreach (ContactAPC contact in ContactsList2.ToList())
            {
                if (!contact.Contacto.Nome.ToLower().Contains(name.ToLower()))
                {
                    ContactsList2.Remove(contact);
                }
            }
            List<ContactAPC> SortedLis2t = ContactsList2.OrderBy(o => o.Contacto.Nome).ToList();
            ContactsList2 = SortedLis2t;
            APCviewModel.ContactsAPC = ContactsList2;
            return APCviewModel;
        }
        private ContactsExchangeListViewModel FilterExchange(String name)
        {
            ContactsExchangeList = new List<ContactExchange>();
            ExchangeviewModel = new ContactsExchangeListViewModel();
            ContactsExchangeList = Context.ContactosExchange.Find(new BsonDocument()).ToList();
            //Filtro
            List<ContactExchange> ContactsList2 = ContactsExchangeList;
            foreach (ContactExchange contact in ContactsList2.ToList())
            {
                if (!contact.Contacto.Nome.ToLower().Contains(name.ToLower()))
                {
                    ContactsList2.Remove(contact);
                }
            }
            List<ContactExchange> SortedLis2t = ContactsList2.OrderBy(o => o.Contacto.Nome).ToList();
            ContactsList2 = SortedLis2t;
            ExchangeviewModel.ContactsExchange = ContactsList2;
            return ExchangeviewModel;
        }
        private ContactsListViewModel FilterContactos(String name)
        {
            contactosList = new List<Contacto>();
            contactosviewModel = new ContactsListViewModel();
            contactosList = Context.Contactos.Find(new BsonDocument()).ToList();
            //Filtro
            List<Contacto> ContactsList2 = contactosList;
            foreach (Contacto contact in ContactsList2.ToList())
            {
                if (!contact.Nome.ToLower().Contains(name.ToLower()))
                {
                    ContactsList2.Remove(contact);
                }
            }
            List<Contacto> SortedLis2t = ContactsList2.OrderBy(o => o.Nome).ToList();
            ContactsList2 = SortedLis2t;
            contactosviewModel.Contactos = ContactsList2;
            return contactosviewModel;
        }

        private CredenciaisListViewModel FilterCredenciais(String name)
        {
            credenciaisList = new List<Credencial>();
            credenciaisListViewModel = new CredenciaisListViewModel();
            credenciaisList = Context.Credenciais.Find(new BsonDocument()).ToList();
            //Filtro
            List<Credencial> credList2 = credenciaisList;
            foreach (Credencial cred in credList2.ToList())
            {
                if (!cred.User.ToLower().Contains(name.ToLower()))
                {
                    credList2.Remove(cred);
                }
            }
            List<Credencial> SortedLis2t = credList2.OrderBy(o => o.User).ToList();
            credList2 = SortedLis2t;
            credenciaisListViewModel.Credenciais = credList2;
            return credenciaisListViewModel;
        }
        private SondasListViewModel FilterSonda(String name)
        {
            sondasList = new List<Sonda>();
            sondasListViewModel = new SondasListViewModel();
            sondasList = Context.Sondas.Find(new BsonDocument()).ToList();
            //Filtro
            List<Sonda> credList2 = sondasList;
            foreach (Sonda cred in credList2.ToList())
            {
                if (!cred.Dns.ToLower().Contains(name.ToLower()))
                {
                    credList2.Remove(cred);
                }
            }
            List<Sonda> SortedLis2t = credList2.OrderBy(o => o.Dns).ToList();
            credList2 = SortedLis2t;
            sondasListViewModel.Sondas = credList2;
            return sondasListViewModel;
        }
        private LinksListViewModel FilterLink(String name)
        {
            linksList = new List<Link>();
            linksListView = new LinksListViewModel();
            linksList = Context.Links.Find(new BsonDocument()).ToList();
            //Filtro
            List<Link> credList2 = linksList;
            foreach (Link cred in credList2.ToList())
            {
                if (!cred.Endereco.ToLower().Contains(name.ToLower()))
                {
                    credList2.Remove(cred);
                }
            }
            List<Link> SortedLis2t = credList2.OrderBy(o => o.Endereco).ToList();
            credList2 = SortedLis2t;
            linksListView.Links = credList2;
            return linksListView;
        }
        private AplicacoesListViewModel FilterAplicacao(String name)
        {
            aplicacoesList = new List<Aplicacao>();
            aplicacoesListViewModel = new AplicacoesListViewModel();
            aplicacoesList = Context.Aplicacoes.Find(new BsonDocument()).ToList();
            //Filtro
            List<Aplicacao> credList2 = aplicacoesList;
            foreach (Aplicacao cred in credList2.ToList())
            {
                if (!cred.Nome.ToLower().Contains(name.ToLower()))
                {
                    credList2.Remove(cred);
                }
            }
            List<Aplicacao> SortedLis2t = credList2.OrderBy(o => o.Nome).ToList();
            credList2 = SortedLis2t;
            aplicacoesListViewModel.Aplicacoes = credList2;
            return aplicacoesListViewModel;
        }

        private CartoesSIMListViewModel FilterCartaoSIM(String cartao)
        {
            cartoesSIMList = new List<CartaoSIM>();
            cartoesSIMListViewModel = new CartoesSIMListViewModel();
            cartoesSIMList = Context.cartoesSIM.Find(new BsonDocument()).ToList();
            //Filtro
            List<CartaoSIM> credList2 = cartoesSIMList;
            foreach (CartaoSIM cred in credList2.ToList())
            {
                if (!cred.Numero.ToLower().Contains(cartao.ToLower()))
                {
                    credList2.Remove(cred);
                }
            }
            List<CartaoSIM> SortedLis2t = credList2.OrderBy(o => o.Numero).ToList();
            credList2 = SortedLis2t;
            cartoesSIMListViewModel.CartoesSIM = credList2;
            return cartoesSIMListViewModel;
        }

        private ContactsMilletellerListViewModel GetContactosMilletellerBD()
        {
            ContactsListMilleteller = new List<ContactMilleteller>();
            viewModelMilleteller = new ContactsMilletellerListViewModel();
            ContactsListMilleteller = Context.ContactosMilleteller.Find(new BsonDocument()).ToList();
            List<ContactMilleteller> SortedLis2t = ContactsListMilleteller.OrderBy(o => o.Contacto.Nome).ToList();
            ContactsListMilleteller = SortedLis2t;
            viewModelMilleteller.ContactMilleteller = ContactsListMilleteller;
            return viewModelMilleteller;
        }

        //Remove contacto selecionado - Comunicacoes com os balcoes
        [HttpPost]
        public ActionResult RemoveExtranet(String id)
        {
            try
            {
                Expression<Func<ContactExtranet, bool>> filter = x => x.Id == ObjectId.Parse(id);
                Context.ContactosExtranet.DeleteOne(filter);

            }
            catch
            {

            }
            GetContactosExtranetBD();
            List<ContactExtranet> SortedLis2t = ContactsExtranetsList.OrderBy(o => o.Contacto.Nome).ToList();
            ContactsExtranetsList = SortedLis2t;
            viewExtranetModel.ContactsExtranets = ContactsExtranetsList;
            return View("Extranets", viewExtranetModel);
        }
        [HttpPost]
        public ActionResult Remove(String id)
        {
            try
            {
                Expression<Func<ContactBalcao, bool>> filter = x => x.Id == ObjectId.Parse(id);
                Context.ContactosBalcoes.DeleteOne(filter);

            }
            catch
            {

            }
            GetContactosBalcoesBD();
            List<ContactBalcao> SortedLis2t = ContactsList.OrderBy(o => o.Contacto.Nome).ToList();
            ContactsList = SortedLis2t;
            viewModel.Contactbalcao = ContactsList;
            return View("balcoes", viewModel);
        }
        [HttpPost]
        public ActionResult RemoveAPC(String id)
        {
            try
            {
                Expression<Func<ContactAPC, bool>> filter = x => x.Id ==  ObjectId.Parse(id);
                Context.ContactosAPC.DeleteOne(filter);

            }
            catch
            {

            }
            GetContactosAPCBD();
            List<ContactAPC> SortedLis2t = ContactsAPCList.OrderBy(o => o.Contacto.Nome).ToList();
            ContactsAPCList = SortedLis2t;
            APCviewModel.ContactsAPC = ContactsAPCList;
            return View("APC", APCviewModel);
        }

        [HttpPost]
        public ActionResult RemoveExchange(String id)
        {
            try
            {
                Expression<Func<ContactExchange, bool>> filter = x => x.Id == ObjectId.Parse(id);
                Context.ContactosExchange.DeleteOne(filter);

            }
            catch
            {

            }
            GetContactosExchangeBD();
            List<ContactExchange> SortedLis2t = ContactsExchangeList.OrderBy(o => o.Contacto.Nome).ToList();
            ContactsExchangeList = SortedLis2t;
            ExchangeviewModel.ContactsExchange = ContactsExchangeList;
            return View("Exchange", ExchangeviewModel);
        }
        [HttpPost]
        public ActionResult RemoveContacto(String id)
        {
            try
            {
                Contacto contacto = FindContacto(id);
                Expression<Func<Contacto, bool>> filter = x => x.Id == contacto.Id;
                DeleteResult result = Context.Contactos.DeleteOne(filter);
                if (result.DeletedCount != 0)
                {
                    ContactAPC contactoApc = FindContactAPC(id);
                    ContactBalcao contactBalcao = FindContact(id);
                    ContactMilleteller contactMilleteller = FindContactMilleteller(id);
                    try
                    {
                    if (contactBalcao.Id != null)
                    {
                        Expression<Func<ContactBalcao, bool>> filter2 = x => x.Id == contacto.Id;
                        Context.ContactosBalcoes.DeleteOne(filter2);
                    }
                    }
                    catch {}
                    try
                    {
                     if (contactMilleteller.Id != null)
                    {
                        Expression<Func<ContactMilleteller, bool>> filter3 = x => x.Id == contacto.Id;
                        Context.ContactosMilleteller.DeleteOne(filter3);
                    }
                    }
                    catch { }
                    try
                    {
                    if (contactoApc.Id != null)
                    {
                        Expression<Func<ContactAPC, bool>> filter4 = x => x.Id == contacto.Id;
                        Context.ContactosAPC.DeleteOne(filter4);
                    }
                    }
                    catch { }
                    try
                    {
                        Expression<Func<Template, bool>> filter6 = x => x.Contactos.Any(z => z.Id == contacto.Id);
                        List<Template> templatesEncontrados = Context.Templates.Find(filter6).ToList();
                        List<Template> templatesModificados = new List<Template>();
                        foreach (Template template in templatesEncontrados)
                        {
                            var index = template.Contactos.FindIndex(c => c.Id == contacto.Id);
                            template.Contactos.RemoveAt(index);
                            templatesModificados.Add(template);
                        }
                        foreach (Template template in templatesModificados)
                        {
                            Expression<Func<Template, bool>> filter7 = x => x.Id == template.Id;
                            Context.Templates.ReplaceOne(filter7, template);
                        }
                    }
                    catch { }
                   
                }
            }
            catch
            {

            }
            GetContactosPortalBD();
            GetContactosMilletellerBD();
            GetContactosExtranetBD();
            GetContactosAPCBD();
            GetContactosBalcoesBD();
            List<Contacto> SortedLis2t = contactosList.OrderBy(o => o.Nome).ToList();
            contactosList = SortedLis2t;
            contactosviewModel.Contactos = contactosList;
            viewModel.Contactbalcao = ContactsList;
            viewModelMilleteller.ContactMilleteller = ContactsListMilleteller;
            APCviewModel.ContactsAPC = ContactsAPCList;
            return View("Contactos", contactosviewModel);
        }


        [HttpPost]
        public ActionResult RemoveCredencial(String id)
        {
            Credencial credencial = FindCredencial(id);
            if (credencial.Id != null)
            {
                try
                {
                    Expression<Func<Credencial, bool>> filter = x => x.Id == ObjectId.Parse(id);
                    DeleteResult result = Context.Credenciais.DeleteOne(filter);
                    if (result.DeletedCount != 0)
                    {
                        try {   Expression<Func<Sonda, bool>> filter2 = (x => x.Credencial.Any(cred => cred.Id == ObjectId.Parse(id)));
                        Sonda modifySonda = Context.Sondas.Find(filter2).FirstOrDefault();
                        modifySonda.Credencial.Remove(modifySonda.Credencial.ToList().First(x => x.Id == credencial.Id));
                        Context.Sondas.ReplaceOne(filter2, modifySonda);} catch { }

                        try {Expression<Func<Link, bool>> filter3 = (x => x.Credencial.Any(cred => cred.Id == ObjectId.Parse(id)));
                        Link modifyLink = Context.Links.Find(filter3).FirstOrDefault();
                        modifyLink.Credencial.Remove(modifyLink.Credencial.ToList().First(x => x.Id == credencial.Id));
                        Context.Links.ReplaceOne(filter3, modifyLink); } catch { }

                        try {  Expression<Func<Aplicacao, bool>> filter4 = (x => x.Credencial.Any(cred => cred.Id == ObjectId.Parse(id)));
                        Aplicacao modifyApp = Context.Aplicacoes.Find(filter4).FirstOrDefault();
                        modifyApp.Credencial.Remove(modifyApp.Credencial.ToList().First(x => x.Id == credencial.Id));
                        Context.Aplicacoes.ReplaceOne(filter4, modifyApp); } catch { }
                      
                    }
                }
                catch
                {

                }

            }
            GetCredenciasFromDB();
            List<Credencial> SortedLis2t = credenciaisList.OrderBy(o => o.User).ToList();
            credenciaisList = SortedLis2t;
            credenciaisListViewModel.Credenciais = credenciaisList;
            credenciaisListViewModel.temPermissao = VerifyUserAutorization();
            return View("GestaoCredenciais", credenciaisListViewModel);
        }
        [HttpPost]
        public ActionResult RemoveLink(String id)
        {

            Link found = FindLink(id);
            if (found.Id != null)
            {
                try
                {
                    Expression<Func<Link, bool>> filter = x => x.Id == ObjectId.Parse(id);
                    DeleteResult result = Context.Links.DeleteOne(filter);
                }
                catch
                {

                }

            }
            GetLinksFromDB();
            List<Link> SortedLis2t = linksList.OrderBy(o => o.Endereco).ToList();
            linksList = SortedLis2t;
            linksListView.Links = linksList;
            linksListView.temPermissao = VerifyUserAutorization();
            return View("GestaoLinks", linksListView);
        }
        [HttpPost]
        public ActionResult RemoveAplicacao(String id)
        {

            Aplicacao found = FindAplicacao(id);
            if (found.Id != null)
            {
                try
                {
                    Expression<Func<Aplicacao, bool>> filter = x => x.Id == ObjectId.Parse(id);
                    DeleteResult result = Context.Aplicacoes.DeleteOne(filter);
                }
                catch
                {

                }

            }
            GetAplicacoesFromDB();
            List<Aplicacao> SortedLis2t = aplicacoesList.OrderBy(o => o.Nome).ToList();
            aplicacoesList = SortedLis2t;
            aplicacoesListViewModel.Aplicacoes = aplicacoesList;
            aplicacoesListViewModel.temPermissao = VerifyUserAutorization();
            return View("GestaoAplicacoes", aplicacoesListViewModel);
        }
        [HttpPost]
        public ActionResult RemoveCartaoSIM(String id)
        {

            CartaoSIM found = FindCartao(id);
            if (found.Id != null)
            {
                try
                {
                    Expression<Func<CartaoSIM, bool>> filter = x => x.Id == ObjectId.Parse(id);
                    DeleteResult result = Context.cartoesSIM.DeleteOne(filter);
                }
                catch
                {

                }

            }
            GetCartoesSIMFromDB();
            List<CartaoSIM> SortedLis2t = cartoesSIMList.OrderBy(o => o.Numero).ToList();
            cartoesSIMList = SortedLis2t;
            cartoesSIMListViewModel.CartoesSIM = cartoesSIMList;
            cartoesSIMListViewModel.temPermissao = VerifyUserAutorization();
            return View("GestaoCartaoSIM", cartoesSIMListViewModel);
        }
        [HttpPost]
        public ActionResult RemoveSonda(String id)
        {
            Sonda found = FindSonda(id);
            if (found.Id != null)
            {
                try
                {
                    Expression<Func<Sonda, bool>> filter = x => x.Id == ObjectId.Parse(id);
                    DeleteResult result = Context.Sondas.DeleteOne(filter);
                    if (result.DeletedCount != 0)
                    {
                        try
                        {
                            Expression<Func<Aplicacao, bool>> filter2 = (x => x.Sonda.Any(sonda => sonda.Id == ObjectId.Parse(id)));
                            Aplicacao modifyApp = Context.Aplicacoes.Find(filter2).FirstOrDefault();
                            modifyApp.Sonda.Remove(modifyApp.Sonda.ToList().First(x => x.Id == found.Id));
                            Context.Aplicacoes.ReplaceOne(filter2, modifyApp);
                        }
                        catch
                        {
                            Expression<Func<Link, bool>> filter3 = (x => x.Sonda.Any(sonda => sonda.Id == ObjectId.Parse(id)));
                            Link modifyLink = Context.Links.Find(filter3).FirstOrDefault();
                            modifyLink.Sonda.Remove(modifyLink.Sonda.ToList().First(x => x.Id == found.Id));
                            Context.Links.ReplaceOne(filter3, modifyLink);
                        }
                    }
                }
                catch
                {

                }

            }

            GetSondasFromDB();
            GetLinksFromDB();
            GetAplicacoesFromDB();
            List<Sonda> SortedLis2t = sondasList.OrderBy(o => o.Dns).ToList();
            sondasList = SortedLis2t;
            sondasListViewModel.Sondas = sondasList;
            sondasListViewModel.temPermissao = VerifyUserAutorization();
            return View("GestaoSondas", sondasListViewModel);
        }
        [HttpPost]
        public ActionResult RemoveServicoMobile(String id)
        {
            try
            {
                Expression<Func<ServicoMobile, bool>> filter = x => x.Id == ObjectId.Parse(id);
                DeleteResult result = Context.ServicosMobile.DeleteOne(filter);
                if (result.DeletedCount != 0)
                {
                    Expression<Func<Associacao, bool>> filter2 = x => x.ServicoMobile.Id == ObjectId.Parse(id);
                    Context.Associacoes.DeleteOne(filter2);
                }
            }
            catch
            {

            }

            GetMobileInfoFromDB();
            GestaoServicosMobileListViewModel gestao = UpdateGestaoInfo();
            return View("GestaoServicosMobile", gestao);
        }
        [HttpPost]
        public ActionResult RemoveAssociacoes(String id)
        {
            try
            {
                Expression<Func<Associacao, bool>> filter = x => x.Id == ObjectId.Parse(id);
                DeleteResult result = Context.Associacoes.DeleteOne(filter);
            }
            catch
            {

            }
            GetMobileInfoFromDB();
            GestaoServicosMobileListViewModel gestao = UpdateGestaoInfo();
            return View("GestaoServicosMobile", gestao);
        }
        [HttpPost]
        public ActionResult RemoveProvedoras(String id)
        {
            try
            {
                Expression<Func<Provedor, bool>> filter = x => x.Id == ObjectId.Parse(id);
                DeleteResult result = Context.Provedores.DeleteOne(filter);
                if (result.DeletedCount != 0)
                {
                    Expression<Func<Associacao, bool>> filter2 = x => x.Provedor.Id == ObjectId.Parse(id);
                    Context.Associacoes.DeleteOne(filter2);
                }
            }
            catch
            {

            }
            GetMobileInfoFromDB();
            GestaoServicosMobileListViewModel gestao = UpdateGestaoInfo();
            return View("GestaoServicosMobile", gestao);
        }
        [HttpPost]
        public ActionResult VisualizarEquipa(String id)
        {
            contactosviewModel = GetContactosPortalBD();
            equipasviewModel = GetEquipasFromDB();
            Equipa assToremove = FindEquipa(id);
            List<Contacto> filteredEquipaMembers = new List<Contacto>();
            foreach (Contacto ass in contactosList)
            {
                if (ass.IDEquipa == assToremove.Id)
                {
                    filteredEquipaMembers.Add(ass);
                }
            }
            equipasviewModel.membrosdeequipa = filteredEquipaMembers;
            return View("GestaoEquipas", equipasviewModel);
        }
        [HttpPost]
        public ActionResult RemoveEquipa(String id)
        {
            try
            {
                Expression<Func<Equipa, bool>> filter = x => x.Id == ObjectId.Parse(id);
                DeleteResult result = Context.Equipas.DeleteOne(filter);
            }
            catch
            {

            }
            equipasviewModel = GetEquipasFromDB();
            return View("GestaoEquipas", equipasviewModel);
        }

        [HttpPost]
        public ActionResult RemoveGrupo(String id)
        {
            try
            {
                Expression<Func<Grupo, bool>> filter = x => x.Id == ObjectId.Parse(id);
                DeleteResult result = Context.Grupos.DeleteOne(filter);
            }
            catch
            {

            }
            equipasviewModel = GetEquipasFromDB();
            return View("GestaoEquipas", equipasviewModel);
        }
        [HttpPost]
        public ActionResult RemoveTemplate(String id)
        {
            try
            {
                Expression<Func<Template, bool>> filter = x => x.Id == ObjectId.Parse(id);
                DeleteResult result = Context.Templates.DeleteOne(filter);
            }
            catch
            {

            }
            templateview = GetTemplatesFromBD();
            return View("GestaoTemplates", templateview);
        }
        [HttpPost]
        public ActionResult RemoveMilleteller(String id)
        {
            try
            {
                Expression<Func<ContactMilleteller, bool>> filter = x => x.Id == ObjectId.Parse(id);
                DeleteResult result = Context.ContactosMilleteller.DeleteOne(filter);
            }
            catch
            {

            }
            GetContactosMilletellerBD();
            viewModelMilleteller.ContactMilleteller = ContactsListMilleteller;
            return View("Milleteller", viewModelMilleteller);
        }


        private static Boolean VerifyUserAutorization()
        {
            String userName = getCurrentUserName();
            string accountname = @"mz\opsmgrmsaction";
            string password = "Dsemon&123";
            // set up domain context
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "mz.bcpcorp.net", accountname, password);
            // find a user
            Debug.WriteLine(userName.ToUpper());
                using (UserPrincipal user = UserPrincipal.FindByIdentity(ctx, userName.ToUpper()))
                {
                    
                // find the group in question
                    GroupPrincipal group = GroupPrincipal.FindByIdentity(ctx, "YGDLBIM23110Mon");
                    if (user != null)
                    {
                    Debug.WriteLine(user.Name);
                    // check if user is member of that group
                    if (user.IsMemberOf(group))
                        {
                        Debug.WriteLine("IS MEMBER OF MZ");
                        return true;
                        }
                    }
                }
            if (userName.ToLower() == "opsmgrmsaction" || userName.ToLower() == "prtgadmin")
            {
                return true;
            }
            return false;
        }


        private static string getCurrentUserName()
        {
            String userLogged = System.Web.HttpContext.Current.User.Identity.Name;
            string[] stringSeparators = new string[] { @"\" };
            string[] result = userLogged.Split(stringSeparators, StringSplitOptions.None);
            String userName = result[1];
            return userName;
        }

        //Invoca Popup para editar dados do contacto - Comunicacoes com os balcoes
        public ActionResult EditBalcoes(String id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactBalcao contactoencontrado = FindContact(id);
            if (contactoencontrado.Id == null)
            {
                return HttpNotFound();
            }
            ViewBag.Message = "Editar Contactos.";
            return PartialView(contactoencontrado);
        }
        public ActionResult EditExtranets(String id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactExtranet contactoencontrado = FindContactExtranet(id);
            if (contactoencontrado.Id == null)
            {
                return HttpNotFound();
            }
            ViewBag.Message = "Editar Contactos.";
            return PartialView(contactoencontrado);
        }
        public ActionResult EditAPC(String id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactAPC contactoencontrado = FindContactAPC(id);
            if (contactoencontrado.Id == null)
            {
                return HttpNotFound();
            }
            ViewBag.Message = "Editar Contactos.";
            return PartialView(contactoencontrado);
        }
        public ActionResult EditExchange(String id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactExchange contactoencontrado = FindContactExchange(id);
            if (contactoencontrado.Id == null)
            {
                return HttpNotFound();
            }
            ViewBag.Message = "Editar Contactos.";
            return PartialView(contactoencontrado);
        }
        public ActionResult EditContacto(String id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contacto contactoencontrado = FindContacto(id);
            if (contactoencontrado.Id == null)
            {
                return HttpNotFound();
            }
            GetTemplatesFromBD();
            Boolean exist = false;
            foreach (Template template in templatesList)
            {
                foreach (Contacto contacto in template.Contactos)
                {
                    if(contacto.Id == contactoencontrado.Id)
                    {
                        exist = true;
                        break;
                    }
                }
            }
            if(exist == true)
            {
                editarContactoListView.associar = "Sim";
            }
            else
            {
                editarContactoListView.associar = "Não";
            }
            equipasviewModel = GetEquipasFromDB();
            editarContactoListView.equipas = equipasList;
            editarContactoListView.contacto = contactoencontrado;
            ViewBag.Message = "Editar Contactos.";
            return PartialView(editarContactoListView);
        }
        public ActionResult EditContactoEq(String id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contacto contactoencontrado = FindContacto(id);
            if (contactoencontrado.Id == null)
            {
                return HttpNotFound();
            }
            equipasviewModel = GetEquipasFromDB();
            editarContactoListView.equipas = equipasList;
            editarContactoListView.contacto = contactoencontrado;
            ViewBag.Message = "Editar Contactos.";
            return PartialView(editarContactoListView);
        }
        public ActionResult EditCredencial(String id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Credencial credEncontrado = FindCredencial(id);
            if (credEncontrado.Id == null)
            {
                return HttpNotFound();
            }

            ViewBag.Message = "Editar Credencial.";
            return PartialView(credEncontrado);
        }

        public ActionResult EditSonda(String id)
        {
            EditSondaListViewModel tempView = new EditSondaListViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sonda sondaEncontrado = FindSonda(id);
            if (sondaEncontrado.Id == null)
            {
                return HttpNotFound();
            }
            tempView.Sonda = sondaEncontrado;
            tempView.credenciais = credenciaisList;
            List<String> selectedOptions = new List<String>();
            foreach (Credencial contacto in sondaEncontrado.Credencial.ToList())
            {
                foreach (Credencial cont in credenciaisList)
                {
                    if (contacto.Id == cont.Id)
                    {
                        selectedOptions.Add(contacto.Id.ToString());
                    }
                }
            }
            tempView.SelectedOptions = selectedOptions;
            ViewBag.Message = "Editar Sonda.";
            return PartialView(tempView);
        }

        public ActionResult EditLink(String id)
        {
            EditLinkListViewModel tempView = new EditLinkListViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Link linkEncontrado = FindLink(id);
            if (linkEncontrado.Id == null)
            {
                return HttpNotFound();
            }
            GetCredenciasFromDB();
            GetSondasFromDB();
            tempView.Link = linkEncontrado;
            tempView.credenciais = credenciaisList;
            List<String> selectedOptions = new List<String>();
            foreach (Credencial contacto in linkEncontrado.Credencial.ToList())
            {
                foreach (Credencial cont in credenciaisList)
                {
                    if (contacto.Id == cont.Id)
                    {
                        selectedOptions.Add(contacto.Id.ToString());
                    }
                }
            }
            List<String> selectedOptions2 = new List<String>();
            foreach (Sonda sonda in linkEncontrado.Sonda.ToList())
            {
                foreach (Sonda cont in sondasList)
                {
                    if (sonda.Id == cont.Id)
                    {
                        selectedOptions2.Add(sonda.Id.ToString());
                    }
                }
            }
            tempView.SelectedOptions = selectedOptions;
            tempView.SelectedOptions2 = selectedOptions2;
            tempView.sondas = sondasList;
            ViewBag.Message = "Editar Sonda.";
            return PartialView(tempView);
        }

        public ActionResult EditAplicacao(String id)
        {
            EditAplicacaoListViewModel tempView = new EditAplicacaoListViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aplicacao AplicacaoEncontrado = FindAplicacao(id);
            if (AplicacaoEncontrado.Id == null)
            {
                return HttpNotFound();
            }
            GetCredenciasFromDB();
            GetSondasFromDB();
            tempView.Aplicacao = AplicacaoEncontrado;
            tempView.credenciais = credenciaisList;
            List<String> selectedOptions = new List<String>();
            foreach (Credencial contacto in AplicacaoEncontrado.Credencial.ToList())
            {
                foreach (Credencial cont in credenciaisList)
                {
                    if (contacto.Id == cont.Id)
                    {
                        selectedOptions.Add(contacto.Id.ToString());
                    }
                }
            }
            List<String> selectedOptions2 = new List<String>();
            foreach (Sonda sonda in AplicacaoEncontrado.Sonda.ToList())
            {
                foreach (Sonda cont in sondasList)
                {
                    if (sonda.Id == cont.Id)
                    {
                        selectedOptions2.Add(sonda.Id.ToString());
                    }
                }
            }
            tempView.SelectedOptions2 = selectedOptions2;
            tempView.SelectedOptions = selectedOptions;
            tempView.sondas = sondasList;
            ViewBag.Message = "Editar Sonda.";
            return PartialView(tempView);
        }

        public ActionResult EditCartaoSIM(String id)
        {
            EditCartaoSIMListViewModel tempView = new EditCartaoSIMListViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartaoSIM cartaoEncontrado = FindCartao(id);
            if (cartaoEncontrado.Id == null)
            {
                return HttpNotFound();
            }
            GetSondasFromDB();
            tempView.cartaoSIM = cartaoEncontrado;
            
            List<String> selectedOptions2 = new List<String>();
            foreach (Sonda sonda in cartaoEncontrado.Sonda.ToList())
            {
                foreach (Sonda cont in sondasList)
                {
                    if (sonda.Id == cont.Id)
                    {
                        selectedOptions2.Add(sonda.Id.ToString());
                    }
                }
            }
            tempView.SelectedOptions2 = selectedOptions2;
            tempView.sondas = sondasList;
            ViewBag.Message = "Editar Cartao SIM.";
            return PartialView(tempView);
        }
        public ActionResult ClonarAplicacao(String id)
        {
            EditAplicacaoListViewModel tempView = new EditAplicacaoListViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aplicacao AplicacaoEncontrado = FindAplicacao(id);
            if (AplicacaoEncontrado.Id == null)
            {
                return HttpNotFound();
            }
            GetCredenciasFromDB();
            GetSondasFromDB();
            tempView.Aplicacao = AplicacaoEncontrado;
            tempView.credenciais = credenciaisList;
            List<String> selectedOptions = new List<String>();
            foreach (Credencial contacto in AplicacaoEncontrado.Credencial.ToList())
            {
                foreach (Credencial cont in credenciaisList)
                {
                    if (contacto.Id == cont.Id)
                    {
                        selectedOptions.Add(contacto.Id.ToString());
                    }
                }
            }
            List<String> selectedOptions2 = new List<String>();
            foreach (Sonda sonda in AplicacaoEncontrado.Sonda.ToList())
            {
                foreach (Sonda cont in sondasList)
                {
                    if (sonda.Id == cont.Id)
                    {
                        selectedOptions2.Add(sonda.Id.ToString());
                    }
                }
            }
            tempView.SelectedOptions2 = selectedOptions2;
            tempView.SelectedOptions = selectedOptions;
            tempView.sondas = sondasList;
            ViewBag.Message = "Clonar Sonda.";
            return PartialView(tempView);
        }

        public ActionResult ClonarCartaoSIM(String id)
        {
            EditCartaoSIMListViewModel tempView = new EditCartaoSIMListViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartaoSIM cartaoEncontrado = FindCartao(id);

            if (cartaoEncontrado.Id == null)
            {
                return HttpNotFound();
            }
            GetSondasFromDB();
            tempView.cartaoSIM = cartaoEncontrado;
            
            List<String> selectedOptions2 = new List<String>();
            foreach (Sonda sonda in cartaoEncontrado.Sonda.ToList())
            {
                foreach (Sonda cont in sondasList)
                {
                    if (sonda.Id == cont.Id)
                    {
                        selectedOptions2.Add(sonda.Id.ToString());
                    }
                }
            }
            tempView.SelectedOptions2 = selectedOptions2;
            tempView.sondas = sondasList;
            ViewBag.Message = "Clonar Cartao.";
            return PartialView(tempView);
        }
        //Editar Dados do Contacto - Comunicacoes com os balcoes
        [HttpPost]
        public ActionResult EditContacto2(EditarContactoListView model)
        {
            if (model.contacto.Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expression<Func<Contacto, bool>> filter = x => x.Id == model.contacto.Id;
            Contacto contactoencontrado = Context.Contactos.Find(filter).FirstOrDefault();
            if (contactoencontrado.Id == null)
            {
                return HttpNotFound();
            }
            if (contactoencontrado.Id != null)
            {
                contactoencontrado.Nome = model.contacto.Nome;
                contactoencontrado.Equipa = model.contacto.Equipa;
                contactoencontrado.IDEquipa = model.contacto.IDEquipa;
                contactoencontrado.Telefone = model.contacto.Telefone;
                contactoencontrado.Email = model.contacto.Email;
                try
                {
                    ReplaceOneResult result = Context.Contactos.ReplaceOne(filter, contactoencontrado);
                    if (result.ModifiedCount != 0)
                    {
                        try
                        {
                            Expression<Func<ContactBalcao, bool>> filter2 = x => x.Id == model.contacto.Id;
                            ContactBalcao contactoencontradoB = Context.ContactosBalcoes.Find(filter2).FirstOrDefault();
                            contactoencontradoB.Contacto = contactoencontrado;
                            Context.ContactosBalcoes.ReplaceOne(filter2, contactoencontradoB);
                        }
                        catch
                        {
                        }
                        try
                        {
                            Expression<Func<ContactMilleteller, bool>> filter3 = x => x.Id == model.contacto.Id;
                            ContactMilleteller contactoencontradoM = Context.ContactosMilleteller.Find(filter3).FirstOrDefault();
                            contactoencontradoM.Contacto = contactoencontrado;
                            Context.ContactosMilleteller.ReplaceOne(filter3, contactoencontradoM);
                        }
                        catch
                        {
                        }
                        try
                        {
                            Expression<Func<ContactAPC, bool>> filter4 = x => x.Id == model.contacto.Id;
                            ContactAPC contactoencontradoA = Context.ContactosAPC.Find(filter4).FirstOrDefault();
                            contactoencontradoA.Contacto = contactoencontrado;
                            Context.ContactosAPC.ReplaceOne(filter4, contactoencontradoA);
                        }
                        catch
                        {
                        }
                        try
                        {
                            Expression<Func<ContactExtranet, bool>> filter5 = x => x.Id == model.contacto.Id;
                            ContactExtranet contactoencontradoE = Context.ContactosExtranet.Find(filter5).FirstOrDefault();
                            contactoencontradoE.Contacto = contactoencontrado;
                            Context.ContactosExtranet.ReplaceOne(filter5, contactoencontradoE);
                        }
                        catch
                        {
                        }

                        try
                        {
                            Expression<Func<Template, bool>> filter6 = x => x.Contactos.Any(z => z.Id == model.contacto.Id);
                            List<Template> templatesEncontrados = Context.Templates.Find(filter6).ToList();
                            List<Template> templatesModificados = new List<Template>();
                            foreach (Template template in templatesEncontrados)
                            {
                                        var index = template.Contactos.FindIndex(c => c.Id == contactoencontrado.Id);
                                        template.Contactos[index] = contactoencontrado;
                                        templatesModificados.Add(template);
                            }
                            foreach (Template template in templatesModificados) {
                                Expression<Func<Template, bool>> filter7 = x => x.Id == template.Id;
                                Context.Templates.ReplaceOne(filter7, template);
                            }
                        }
                        catch
                        {
                        }
                    }
                }
                catch
                {

                }

            }
            viewModel = GetContactosBalcoesBD();
            GetContactosPortalBD();
            contactosviewModel = new ContactsListViewModel();
            contactosviewModel.Contactos = contactosList;
            return View("Contactos", contactosviewModel);
        }
        public ActionResult EditContactoEq2(EditarContactoListView model)
        {
            if (model.contacto.Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Expression<Func<Contacto, bool>> filter = x => x.Id == model.contacto.Id;
            Contacto contactoencontrado = Context.Contactos.Find(filter).FirstOrDefault();

            if (contactoencontrado.Id == null)
            {
                return HttpNotFound();
            }
            if (contactoencontrado.Id != null)
            {
                contactoencontrado.Nome = model.contacto.Nome;
                contactoencontrado.Equipa = model.contacto.Equipa;
                contactoencontrado.IDEquipa = model.contacto.IDEquipa;
                contactoencontrado.Telefone = model.contacto.Telefone;
                contactoencontrado.Email = model.contacto.Email;

                try
                {
                    ReplaceOneResult result = Context.Contactos.ReplaceOne(filter, contactoencontrado);
                    if (result.ModifiedCount != 0)
                    {
                        try
                        {
                            Expression<Func<ContactBalcao, bool>> filter2 = x => x.Id == model.contacto.Id;
                            ContactBalcao contactoencontradoB = Context.ContactosBalcoes.Find(filter2).FirstOrDefault();
                            contactoencontradoB.Contacto = contactoencontrado;
                            Context.ContactosBalcoes.ReplaceOne(filter2, contactoencontradoB);
                        }
                        catch
                        {
                        }
                        try
                        {
                            Expression<Func<ContactMilleteller, bool>> filter3 = x => x.Id == model.contacto.Id;
                            ContactMilleteller contactoencontradoM = Context.ContactosMilleteller.Find(filter3).FirstOrDefault();
                            contactoencontradoM.Contacto = contactoencontrado;
                            Context.ContactosMilleteller.ReplaceOne(filter3, contactoencontradoM);
                        }
                        catch
                        {
                        }
                        try
                        {
                            Expression<Func<ContactAPC, bool>> filter4 = x => x.Id == model.contacto.Id;
                            ContactAPC contactoencontradoA = Context.ContactosAPC.Find(filter4).FirstOrDefault();
                            contactoencontradoA.Contacto = contactoencontrado;
                            Context.ContactosAPC.ReplaceOne(filter4, contactoencontradoA);
                        }
                        catch
                        {
                        }
                        try
                        {
                            Expression<Func<Template, bool>> filter6 = x => x.Contactos.Any(z => z.Id == model.contacto.Id);
                            List<Template> templatesEncontrados = Context.Templates.Find(filter6).ToList();
                            List<Template> templatesModificados = new List<Template>();
                            foreach (Template template in templatesEncontrados)
                            {
                                var index = template.Contactos.FindIndex(c => c.Id == contactoencontrado.Id);
                                template.Contactos[index] = contactoencontrado;
                                templatesModificados.Add(template);
                            }
                            foreach (Template template in templatesModificados)
                            {
                                Expression<Func<Template, bool>> filter7 = x => x.Id == template.Id;
                                Context.Templates.ReplaceOne(filter7, template);
                            }
                        }
                        catch
                        {
                        }
                    }
                }
                catch
                {
                }

            }
            viewModel = GetContactosBalcoesBD();
            contactosviewModel = GetContactosPortalBD();
            contactosviewModel.Contactos = contactosList;
            equipasviewModel = GetEquipasFromDB();
            return View("GestaoEquipas", equipasviewModel);
        }

        public ActionResult EditarIndisponibilidade(String id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GetIndisponibilidadeFromDB();
            Debug.WriteLine(id);
            Indisponibilidade indspEncontrado = FindIndisponibilidade(id);
            if (indspEncontrado.Id == null)
            {
                return HttpNotFound();
            }
            ViewBag.Message = "Editar Indisponibilidade.";
            EditarIndisponibilidadeListView editView = new EditarIndisponibilidadeListView();
            GetAplicacoesFromDB();
            GetMobileInfoFromDB();
            editView.indisponibilidade = indspEncontrado;
            editView.servicosMobile = servicosMobile;
            editView.provedores = provedores;
            editView.aplicacoes = aplicacoesList;
            return PartialView(editView);
        }
        public ActionResult EditProvedor(String id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GetMobileInfoFromDB();
            Provedor provedorEncontrado = FindProvedor(id);
            if (provedorEncontrado.Id == null)
            {
                return HttpNotFound();
            }
            ViewBag.Message = "Editar Indisponibilidade.";
            return PartialView(provedorEncontrado);
        }
        public ActionResult EditEquipa(String id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GetEquipasFromDB();
            Equipa equipaEncontrada = FindEquipa(id);
            if (equipaEncontrada.Id == null)
            {
                return HttpNotFound();
            }
            ViewBag.Message = "Editar Indisponibilidade.";
            return PartialView(equipaEncontrada);
        }
        public ActionResult EditTemplate(String id)
        {
            EditTemplateListViewModel tempView = new EditTemplateListViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GetTemplatesFromBD();
            Template templateEncontrado = FindTemplate(id);
            if (templateEncontrado.Id == null)
            {
                return HttpNotFound();
            }
            tempView.template = templateEncontrado;
            tempView.Contactos = contactosList;
            List<String> selectedOptions = new List<String>();
            foreach (Contacto contacto in templateEncontrado.Contactos.ToList())
            {
                foreach (Contacto cont in contactosList)
                {
                    if (contacto.Id == cont.Id)
                    {
                        selectedOptions.Add(contacto.Id.ToString());
                    }
                }
            }
            tempView.SelectedOptions = selectedOptions;
            ViewBag.Message = "Editar Template.";
            //Todo 

            return PartialView(tempView);
        }

        public ActionResult EditGrupo(String id)
        {
            EditGrupoListViewModel tempView = new EditGrupoListViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GetEquipasFromDB();
            Grupo grupoEncontrado = FindGrupo(id);
            if (grupoEncontrado.Id == null)
            {
                return HttpNotFound();
            }
            tempView.grupo = grupoEncontrado;
            List<String> selectedOptions = new List<String>();
            foreach (Equipa equipa in grupoEncontrado.Equipas.ToList())
            {
                foreach (Equipa cont in equipasList)
                {
                    if (equipa.Id == cont.Id)
                    {
                        selectedOptions.Add(equipa.Id.ToString());
                    }
                }
            }
            tempView.SelectedOptions = selectedOptions;
            tempView.equipas = equipasList;
            ViewBag.Message = "Editar Template.";
            //Todo 

            return PartialView(tempView);
        }
        public ActionResult EditServicoMobile(String id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GetMobileInfoFromDB();
            ServicoMobile servicoEncontrado = FindServicoMobile(id);
            if (servicoEncontrado.Id == null)
            {
                return HttpNotFound();
            }
            ViewBag.Message = "Editar Indisponibilidade.";
            return PartialView(servicoEncontrado);
        }

        public ActionResult EditMilleteller2(ContactMilleteller model)
        {
            if (model.Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            viewModelMilleteller = GetContactosMilletellerBD();
            Expression<Func<ContactMilleteller, bool>> filter = x => x.Id == model.Id;
            ContactMilleteller contactoencontrado = Context.ContactosMilleteller.Find(filter).FirstOrDefault();
            if (contactoencontrado.Id == null)
            {
                return HttpNotFound();
            }
            if (contactoencontrado.Id != null)
            {
                contactoencontrado.Notf_Email = model.Notf_Email;
                contactoencontrado.Notf_SMS = model.Notf_SMS;
                contactoencontrado.Fim = model.Fim;
                try {  Context.ContactosMilleteller.ReplaceOne(filter, contactoencontrado);} catch { }
               
            }
            viewModelMilleteller = new ContactsMilletellerListViewModel();
            viewModelMilleteller.ContactMilleteller = ContactsListMilleteller;
            return View("Milleteller", viewModelMilleteller);
        }

        //Editar Dados do Contacto - Comunicacoes com os balcoes
        public ActionResult EditBalcoes2(ContactBalcao model)
        {
            if (model.Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expression<Func<ContactBalcao, bool>> filter = x => x.Id == model.Id;
            ContactBalcao contactoencontrado = Context.ContactosBalcoes.Find(filter).FirstOrDefault();

            if (contactoencontrado.Id == null)
            {
                return HttpNotFound();
            }
            if (contactoencontrado.Id != null)
            {
                contactoencontrado.Notf_Email = model.Notf_Email;
                contactoencontrado.Notf_SMS = model.Notf_SMS;
                contactoencontrado.Zona = model.Zona;
                try {Context.ContactosBalcoes.ReplaceOne(filter, contactoencontrado); } catch { }
                
            }
            GetContactosBalcoesBD();
            viewModel = new ContactsBalcaoListViewModel();
            viewModel.Contactbalcao = ContactsList;
            return View("balcoes", viewModel);
        }

        public ActionResult EditExtranets2(ContactExtranet model)
        {
            if (model.Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            viewExtranetModel = GetContactosExtranetBD();
            Expression<Func<ContactExtranet, bool>> filter = x => x.Id == model.Id;
            ContactExtranet contactoencontrado = Context.ContactosExtranet.Find(filter).FirstOrDefault();
            if (contactoencontrado.Id == null)
            {
                return HttpNotFound();
            }
            if (contactoencontrado.Id != null)
            {
               
                contactoencontrado.Notf_Email = model.Notf_Email;
                contactoencontrado.Notf_SMS = model.Notf_SMS;
                contactoencontrado.Fim = model.Fim;
                try {Context.ContactosExtranet.ReplaceOne(filter, contactoencontrado); } catch { }
                
            }
            GetContactosExtranetBD();
            viewExtranetModel = new ContactsExtranetsListViewModel();
            viewExtranetModel.ContactsExtranets = ContactsExtranetsList;
            return View("Extranets", viewExtranetModel);
        }


        //Editar Dados do Contacto -APC e AKCP
        public ActionResult EditAPC2(ContactAPC model)
        {
            if (model.Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            viewModel = GetContactosBalcoesBD();

            Expression<Func<ContactAPC, bool>> filter = x => x.Id == model.Id;
            ContactAPC contactoencontrado = Context.ContactosAPC.Find(filter).FirstOrDefault();
            if (contactoencontrado.Id == null)
            {
                return HttpNotFound();
            }
            if (contactoencontrado.Id != null)
            {
                contactoencontrado.Notf_SMS = model.Notf_SMS;
                try {Context.ContactosAPC.ReplaceOne(filter, contactoencontrado); } catch { }
                
            }
            APCviewModel = new ContactsAPCListViewModel();
            GetContactosAPCBD();
            APCviewModel.ContactsAPC = ContactsAPCList;
            return View("APC", APCviewModel);
        }

        //Editar Dados do Contacto -Exchange
        public ActionResult EditExchange2(ContactAPC model)
        {
            if (model.Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            viewModel = GetContactosBalcoesBD();

            Expression<Func<ContactExchange, bool>> filter = x => x.Id == model.Id;
            ContactExchange contactoencontrado = Context.ContactosExchange.Find(filter).FirstOrDefault();
            if (contactoencontrado.Id == null)
            {
                return HttpNotFound();
            }
            if (contactoencontrado.Id != null)
            {
                contactoencontrado.Notf_SMS = model.Notf_SMS;
                try { Context.ContactosExchange.ReplaceOne(filter, contactoencontrado); } catch { }

            }
            ExchangeviewModel = new ContactsExchangeListViewModel();
            GetContactosExchangeBD();
            ExchangeviewModel.ContactsExchange = ContactsExchangeList;
            return View("Exchange", ExchangeviewModel);
        }




        public ActionResult EditMilleteller(String id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GetContactosMilletellerBD();
            ContactMilleteller contactoencontrado = FindContactMilleteller(id);
            if (contactoencontrado.Id == null)
            {
                return HttpNotFound();
            }
            ViewBag.Message = "Editar Contactos.";
            return PartialView(contactoencontrado);
        }

        //Procura contacto selecionado pelo ID
        public ContactBalcao FindContact(String id)
        {
            ContactBalcao contact = new ContactBalcao();
            Expression<Func<ContactBalcao, bool>> filter = x => x.Id == ObjectId.Parse(id);
            contact = Context.ContactosBalcoes.Find(filter).FirstOrDefault();
            return contact;
        }

        //Procura contacto selecionado pelo ID
        public ContactExtranet FindContactExtranet(String id)
        {
            ContactExtranet contact = new ContactExtranet();
            Expression<Func<ContactExtranet, bool>> filter = x => x.Id == ObjectId.Parse(id);
            contact = Context.ContactosExtranet.Find(filter).FirstOrDefault();
            return contact;
        }
        //Procura contacto selecionado pelo ID
        public ContactAPC FindContactAPC(String id)
        {
            ContactAPC contact = new ContactAPC();
            Expression<Func<ContactAPC, bool>> filter = x => x.Id == ObjectId.Parse(id);
            contact = Context.ContactosAPC.Find(filter).FirstOrDefault();
            return contact;
        }

        //Procura contacto selecionado pelo ID
        public ContactExchange FindContactExchange(String id)
        {
            ContactExchange contact = new ContactExchange();
            Expression<Func<ContactExchange, bool>> filter = x => x.Id == ObjectId.Parse(id);
            contact = Context.ContactosExchange.Find(filter).FirstOrDefault();
            return contact;
        }

        public Contacto FindContacto(String id)
        {
            Contacto contact = new Contacto();
            Expression<Func<Contacto, bool>> filter = x => x.Id == ObjectId.Parse(id);
            contact = Context.Contactos.Find(filter).FirstOrDefault();
            return contact;
        }
        public Credencial FindCredencial(String id)
        {
            Credencial cred = new Credencial();
            Expression<Func<Credencial, bool>> filter = x => x.Id == ObjectId.Parse(id);
            cred = Context.Credenciais.Find(filter).FirstOrDefault();
            return cred;
        }

        public Sonda FindSonda(String id)
        {
            Sonda cred = new Sonda();
            Expression<Func<Sonda, bool>> filter = x => x.Id == ObjectId.Parse(id);
            cred = Context.Sondas.Find(filter).FirstOrDefault();
            return cred;
        }
        public Link FindLink(String id)
        {
            Link cred = new Link();
            Expression<Func<Link, bool>> filter = x => x.Id == ObjectId.Parse(id);
            cred = Context.Links.Find(filter).FirstOrDefault();
            return cred;
        }

        public Grupo FindGrupo(String id)
        {
            Grupo cred = new Grupo();
            Expression<Func<Grupo, bool>> filter = x => x.Id == ObjectId.Parse(id);
            cred = Context.Grupos.Find(filter).FirstOrDefault();
            return cred;
        }
        public Aplicacao FindAplicacao(String id)
        {
            Aplicacao cred = new Aplicacao();
            Expression<Func<Aplicacao, bool>> filter = x => x.Id == ObjectId.Parse(id);
            if (id != "")
            {
                cred = Context.Aplicacoes.Find(filter).FirstOrDefault();
            }
            return cred;
        }

        public CartaoSIM FindCartao(String id)
        {
            CartaoSIM cred = new CartaoSIM();
            Expression<Func<CartaoSIM, bool>> filter = x => x.Id == ObjectId.Parse(id);
            if (id != "")
            {
                cred = Context.cartoesSIM.Find(filter).FirstOrDefault();
            }
            return cred;
        }
        public Indisponibilidade FindIndisponibilidade(String id)
        {
            Indisponibilidade indisp = new Indisponibilidade();
            Expression<Func<Indisponibilidade, bool>> filter = x => x.Id == ObjectId.Parse(id);
            indisp = Context.Indisponibilidades.Find(filter).FirstOrDefault();
            return indisp;
        }
        public Equipa FindEquipa(String id)
        {
            Equipa eqp = new Equipa();
            Expression<Func<Equipa, bool>> filter = x => x.Id == ObjectId.Parse(id);
            eqp = Context.Equipas.Find(filter).FirstOrDefault();
            return eqp;
        }
        public Template FindTemplate(String id)
        {
            Template eqp = new Template();
            Expression<Func<Template, bool>> filter = x => x.Id == ObjectId.Parse(id);
            eqp = Context.Templates.Find(filter).FirstOrDefault();
            return eqp;
        }
        public Provedor FindProvedor(String id)
        {
            Provedor indisp = new Provedor();
            Expression<Func<Provedor, bool>> filter = x => x.Id == ObjectId.Parse(id);
            indisp = Context.Provedores.Find(filter).FirstOrDefault();
            return indisp;
        }
        public ServicoMobile FindServicoMobile(String id)
        {
            ServicoMobile indisp = new ServicoMobile();
            Expression<Func<ServicoMobile, bool>> filter = x => x.Id == ObjectId.Parse(id);
            if (id != "")
            {
                indisp = Context.ServicosMobile.Find(filter).FirstOrDefault();
            }
            return indisp;
        }
        //Procura contacto selecionado pelo ID
        public ContactMilleteller FindContactMilleteller(String id)
        {
            ContactMilleteller contact = new ContactMilleteller();
            Expression<Func<ContactMilleteller, bool>> filter = x => x.Id == ObjectId.Parse(id);
            contact = Context.ContactosMilleteller.Find(filter).FirstOrDefault();
            return contact;
        }
    }

}