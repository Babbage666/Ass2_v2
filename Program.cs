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
           

            ResearcherController R_Cont =new ResearcherController();
            
            R_Cont.LoadResearchers();
            R_Cont.LoadResearcherDetails(123460);

            PublicationsController PC=new PublicationsController();
            List<Research.Publication> pubsList = new List<Research.Publication>();
            pubsList=PC.LoadPublicationsForID(123460);
            foreach(Publication p1 in pubsList)
            {
                Console.WriteLine("{0} {1} {2} {3}", p1.Title, p1.AvailableDate, p1.Type, p1.Date);
            }
           Console.ReadLine();
            
        }

    }   
}
