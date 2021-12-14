using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KIT206_RAP_Project.Research;
using KIT206_RAP_Project.Control;
using KIT206_RAP_Project.Database;
using MySql.Data.MySqlClient;



namespace KIT206_RAP_Project.Control
{
   

    public class ResearcherController
    {

        
        public void LoadResearchers()
        {

            
            ERDAdapter Adapter1 = new ERDAdapter();
            List<Researcher> ResearcherList2 = new List<Researcher>();
            ResearcherList2 = Adapter1.fetchBasicResearcherDetails();
            foreach(Researcher r1 in ResearcherList2)
            {
                Console.WriteLine("{0}  {1}   {2}", r1.Id, r1.GivenName, r1.FamilyName);
            }
            Console.WriteLine("Press a key...");
            Console.ReadKey();


        }

        public void FilterByLevel(Research.EmploymentLevel level)
        {
              

            return;
        }

        public void FilterByName(string name)
        {
            return;
        }

        public void LoadResearcherDetails(int IDnum)
        {
            ERDAdapter Adapter1 = new ERDAdapter();
            Researcher res1 = Adapter1.fullResearcherDetails(IDnum);
            Console.WriteLine("Press a key...");
            Console.ReadKey(); ;
        }

       


    }
}
