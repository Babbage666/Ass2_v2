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

        List<Researcher> ResearcherList=new List<Researcher>();
        public ResearcherController()
        {
                  ResearcherList = LoadResearchers();
        }


        public void Display()
        {
            foreach(Researcher r1 in ResearcherList)
            {
                Console.WriteLine("{0}  {1}   {2}   {3}", r1.Id, r1.GivenName, r1.FamilyName, r1.Level);
            }
            Console.WriteLine("Press a key...");
            Console.ReadKey();
        }

        public void DisplayDetails(Researcher res1)
        {
            Console.WriteLine(res1.Id);
            Console.WriteLine(res1.FamilyName);
            Console.WriteLine(res1.GivenName);
            Console.WriteLine(res1.Title);
            Console.WriteLine(res1.Campus);
            Console.WriteLine(res1.School);
            Console.WriteLine(res1.Email);
            Console.WriteLine(res1.PhotoURL);
        }

        public List<Researcher> LoadResearchers()
        { 
            
            ERDAdapter Adapter1 = new ERDAdapter();
            List<Researcher> tempList = new List<Researcher>();
            tempList = Adapter1.fetchBasicResearcherDetails();

            return tempList;

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
            DisplayDetails(res1);
            
        }

       


    }
}