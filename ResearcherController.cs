using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
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
        List<Researcher> filteredList = new List<Researcher>();
        public ResearcherController()
        {
                  ResearcherList = LoadResearchers();
        }


        public void Display()
        {
            foreach(Researcher r in ResearcherList)
            {
                Console.WriteLine("{0}  {1}   {2}   {3}", r.Id, r.GivenName, r.FamilyName, r.Level);
            }
        }

        public void DisplayFilteredList()
        {
            foreach(Researcher r in filteredList)
            {
                Console.WriteLine("{0}  {1}   {2}   {3}", r.Id, r.GivenName, r.FamilyName, r.Level);
            }
        }

        public void DisplayDetails(Researcher res1)
        {
            Console.WriteLine("ID:"+res1.Id);
            Console.WriteLine("Family Name:"+res1.FamilyName);
            Console.WriteLine("Given Name:"+res1.GivenName);
            Console.WriteLine("Title:"+res1.Title);
            Console.WriteLine("Campus:"+res1.Campus);
            Console.WriteLine("School:"+res1.School);
            Console.WriteLine("Email:"+res1.Email);
            Console.WriteLine("PhotoURL:"+res1.PhotoURL);
            Console.WriteLine("Level:"+res1.ToTitle(res1.Level));
            Console.WriteLine("Began: {0}", res1.CurrentStart);
            if (res1.Level==EmploymentLevel.Student)
            {
                Console.WriteLine("Degree:" + ((Student)res1).Degree);
                Console.WriteLine("Supervisor ID:" + ((Student)res1).SupervisorID);
            }

           
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
            filteredList.Clear();
            foreach (Researcher r in ResearcherList)
            {
                if (r.Level == level)
                {
                    filteredList.Add(r);
                }
            }
            return;
        }

        public void FilterByName(string name)
        {
            filteredList.Clear();
            foreach (Researcher r in ResearcherList)
            {
                string fullName = r.GivenName + r.FamilyName;
                if (fullName.IndexOf(name, StringComparison.OrdinalIgnoreCase) > -1)
                {
                    filteredList.Add(r);
                }
            }
            return;
        }

        public void LoadResearcherDetails(int IDnum)
        {
            ERDAdapter ad1=new ERDAdapter();
            Researcher r2=ad1.fullResearcherDetails(IDnum);
            DisplayDetails(r2);
        }

       


    }
}