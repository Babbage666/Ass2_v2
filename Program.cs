using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using KIT206_RAP_Project.Research;
using KIT206_RAP_Project.Control;
using KIT206_RAP_Project.Database;

namespace KIT206_RAP_Project
{
  
    public class Program
    {
        public static void Main(string[] args)
        {
           
            // Display the basic Researcher Details:
            ResearcherController R_Cont = new ResearcherController();
            R_Cont.Display();

            // Iterate the list and display the full Researcher Details:
            int[] id_numbers=new int[] {123460, 123461, 123462, 123463, 123464, 123465, 123466, 123467, 123468, 123469};
            foreach (int idnum in id_numbers)
            {
                R_Cont.LoadResearcherDetails(idnum);
            }
            
           
          
            
        }

    }   
}
