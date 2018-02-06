using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using PortalDSEMonitorizacao.Models;

namespace PortalDSEMonitorizacao.Controllers
{
     public class HomeController : Controller
    {
        private static List<ContactBalcao> ContactsList = new List<ContactBalcao>();
        private static ContactsBalcaoListViewModel viewModel= new ContactsBalcaoListViewModel();
        //Ficheiro que contem informação dos contacos dos balcoes a notificar
        private static String filepath = "C:\\Temp\\database\\contactosbalcoesTeste.csv";
        //Descrição do caminho do ficheiro usado pelo javascript
        private static String filepathalter ="C:/Temp/database/contactosbalcoesTeste.csv";
        private static readonly string PowerShellRunnerBatchFilePath = @"C:\Temp\smsTest.ps1";
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
        
       
        public ActionResult Milleteller()
        {
            ViewBag.Message = "Your Milleteller page.";

            return View();
        }

        public ActionResult ExecutarComunicacoes()
        {

            standardOutput = "";
            errorOutput = "";
            PowershellModel modelps = new PowershellModel(errorOutput, standardOutput);
            return View(modelps);
        }
        public ActionResult ExecutarComunicacoes2()
        {

            standardOutput = "";
            errorOutput = "";
            RunScript(PowerShellRunnerBatchFilePath);
            PowershellModel modelps = new PowershellModel(errorOutput,standardOutput);
            
            return  View("ExecutarComunicacoes",modelps);
        }
        
        public ActionResult balcoes()
        {
            ViewBag.Message = "Your Balcões page.";
            viewModel = Readfromfile2();
            if (ContactsList.Count() == 0)
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt2').style.display='block'; document.getElementById('hidenatt2').innerHTML='Impossivel Carregar dados.. Verifique se o ficheiro "+filepathalter+" existe';" +
                                      "document.getElementById('btngravar').disabled = true;</script>";   
            }
            return View(viewModel);
        }
        
         public void RunScript(string path)
        {
            errorOutput = "";

            System.Diagnostics.Debug.WriteLine(path);
            var scriptOutput = new StringBuilder();
 
            var config = RunspaceConfiguration.Create();
 
            //create Powershell runspace
            using (var runspace = RunspaceFactory.CreateRunspace(config))
            {
                runspace.Open();           
                var results = new Collection<PSObject>();
 
                //create a pipeline and feed it the script text
                using (var pipeline = runspace.CreatePipeline())
                {
                    //create the command to run
                    var cmd = new Command(path, true);
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
                            Console.WriteLine(standardOutput);
                        }
                        // Check for errors in the pipeline and throw an exception if necessary.
                        if (pipeline.Error != null && pipeline.Error.Count > 0)
                        {
                            var pipelineError = new StringBuilder();
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
            // return string.IsNullOrWhiteSpace(errorOutput);
            }
        }
        
     
        private static List<ContactsBalcaoList> Readfromfile()
        {
            List<ContactsBalcaoList> ContactsList = new List<ContactsBalcaoList>();
            var lines = System.IO.File.ReadAllLines(filepath).Skip(1).TakeWhile(t => t != null);
            foreach (string item in lines)
            {
                var values = item.Split(';');
                ContactsList.Add(new ContactsBalcaoList(values[0], values[1], values[2], values[3], values[4],
                    values[5]));
            }
            return ContactsList;
        }
        //new implementation
        private static ContactsBalcaoListViewModel Readfromfile2()
        {
                ContactsList = new List<ContactBalcao>();
                viewModel= new ContactsBalcaoListViewModel();
            if (System.IO.File.Exists(filepath))
            {
                var lines = System.IO.File.ReadAllLines(filepath).Skip(1).TakeWhile(t => t != null);
                int count = 0; 
                foreach (string item in lines)
                {
                    var values = item.Split(';');
                    ContactsList.Add(new ContactBalcao(count.ToString(),values[0], values[1], values[2], values[3], values[4],
                        values[5]));
                    count++;
                }
                
            }
            
            viewModel.Contactbalcao = ContactsList;
            return viewModel;
        }
        
        //Remove contacto selecionado - Comunicacoes com os balcoes
        [HttpPost]
        public ActionResult Remove(String id)
        {
            ContactsList.Remove(ContactsList.First(x => x.Id == id));
            viewModel.Contactbalcao = ContactsList;
            return  View("balcoes",viewModel);
        }
        
        [HttpPost]
        public ActionResult SalvarAlteracoesBalcoes()
        {
           
            if (System.IO.File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }

            StreamWriter sw = new StreamWriter(filepath, true);
            sw.WriteLine("Nome;Equipa;Telefone;Email;Notf_Email;Notf_SMS");
           
            foreach (var line in ContactsList)
            {
                sw.WriteLine(string.Format("{0};{1};{2};{3};{4};{5}",
                    line.Nome,
                    line.Equipa,
                    line.Telefone,
                    line.Email,
                    line.Notf_Email,
                    line.Notf_SMS));
            }
            sw.Close();
            String textoagravar = sw.ToString();
            
            if (textoagravar.Length == 0)
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt').innerHTML = 'Nenhum conteudo a gravar....';document.getElementById('hidenatt').style.display='block';</script>";
            }
            else
            {
                TempData["showmsg"] = "<script type='text/javascript'> document.getElementById('hidenatt').style.display='block';</script>";
            }
            return  View("balcoes",viewModel);
        }
        //Invoca Popup para editar dados do contacto - Comunicacoes com os balcoes
    
        public ActionResult EditBalcoes(String id)
        {
            Debug.WriteLine("ID:"+id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactBalcao contactoencontrado = FindContact(id);
            if (contactoencontrado == null)
            {
                return HttpNotFound();
            }
            ViewBag.Message = "Editar Contactos.";
            return PartialView(contactoencontrado);
        }
        
        //Editar Dados do Contacto - Comunicacoes com os balcoes
        public ActionResult EditBalcoes2(ContactBalcao model)
        {
           
            if (model.Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactBalcao contactoencontrado = FindContact(model.Id);

            if (contactoencontrado == null)
            {
                return HttpNotFound();
            }
            if(contactoencontrado !=null)
            {
                ContactsList.Where(w => w.Id == model.Id).ToList().ForEach(s => s.Telefone = model.Telefone);
                ContactsList.Where(w => w.Id == model.Id).ToList().ForEach(s => s.Email = model.Email);
                ContactsList.Where(w => w.Id == model.Id).ToList().ForEach(s => s.Notf_Email = model.Notf_Email);
                ContactsList.Where(w => w.Id == model.Id).ToList().ForEach(s => s.Notf_SMS = model.Notf_SMS);
                viewModel.Contactbalcao = ContactsList;
                Console.WriteLine("Updated Object");
                return  View("balcoes",viewModel);
            }
            
            return  View("balcoes",viewModel);
        }

        //Procura contacto selecionado pelo ID
        public ContactBalcao FindContact(String id)
        {
            ContactBalcao contact = null;

            foreach (var Contacto in ContactsList)
            {
                if (Contacto.Id == id)
                {
                    contact = Contacto;
                }
            }
            return contact;
        }
       
    }
}