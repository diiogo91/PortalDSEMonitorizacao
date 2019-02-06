using System;

namespace PortalDSEMonitorizacao.Models
{
    public class ContactsBalcaoList
    {
        
        public ContactsBalcaoList (String nome, String equipa, String telefone, String email, String notf_Email,
            String notf_SMS)
        {
            this.Nome = nome;
            this.Equipa = equipa;
            this.Telefone = telefone;
            this.Email = email;
            this.Notf_Email = notf_Email;
            this.Notf_SMS = notf_SMS;
        }
    
        public String Nome { set; get;}
        public String Equipa{ set; get;}
        public String Telefone{ set; get;}
        public String Email{ set; get;}
        public String Notf_Email{ set; get;}
        public String Notf_SMS{ set; get;}
        
    }

}
        
