using System;
using System.Collections.Generic;
namespace PortalDSEMonitorizacao.Models
{
    public class Computer
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public String Category { get; set; }

        public Computer( String Name, String Description, String Category)
        {
         
            this.Name = Name;
            this.Description = Description;
            this.Category = Category;
        }
        public List<String> Properties()
        {
            return new List<String> { Name, Description, Category };
        }
        public int UserPropertiesTotal =3;
        public static string[] StringArrayComputerProperties = {"Name", "Description" ,"Category"};
    }
}