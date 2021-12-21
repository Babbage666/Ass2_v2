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

        public Researcher Use (int id)
        {
            foreach (Researcher currentResearcher in ResearcherList)
            {
                if (currentResearcher.Id == id)
                {
                    return currentResearcher;
                }
            }

            return null;
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
            Console.WriteLine("Began current role: {0}", res1.CurrentStart);
            Console.WriteLine("Tenure:" + (res1.Tenure()).ToString("N2") + " years");
            if (res1.Level==EmploymentLevel.Student)
            {
                Console.WriteLine("Degree:" + ((Student)res1).Degree);
                Console.WriteLine("Supervisor ID:" + ((Student)res1).SupervisorID);
                int superID = Int32.Parse(((Student)res1).SupervisorID);
                var superName = from rs in ResearcherList
                                    where rs.Id == superID
                                    select rs;
                //Console.WriteLine("Supervisor Name:");
                superName.ToList().ForEach(a => Console.WriteLine("Supervisor Name: " + a.GivenName + " "+ a.FamilyName));
               
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
            PublicationsController P_Cont2 = new PublicationsController();
            r2.Publications=P_Cont2.LoadPublicationsForID(IDnum);
            
            DisplayDetails(r2);
            if (r2.Level != EmploymentLevel.Student)
            {
                double threeYrAvg = ((Staff)r2).calc3yrAvg(r2.Publications);
                double staff_perf = ((Staff)r2).performance(r2.Level,threeYrAvg);

                Console.WriteLine("Performance for this staff member:" +  staff_perf.ToString("N1") + "%");

            }
            Console.WriteLine("\nPublications of Researcher ID: {0} \n", IDnum);
            foreach (Publication p in r2.Publications)
            {
                Console.WriteLine("{0} : {1}", p.Date , p.Title);
            }

            Console.WriteLine("Would you like the Cumulative Count? (y)es or (n)o.");
            string option = Console.ReadLine();
            if (option == "y")
            {
                CumulativeCount(r2);
            }
        }

        public void CumulativeCount(Researcher r, int startYear = 2015, int endYear = 2021)
        {
            Console.WriteLine("Cumulative Count of Research by Year:\n");

            for (int i = 0; i <= endYear-startYear; i++)
            {
                List<Publication> rPubs = r.Publications;
                var counter =
                    from pub in rPubs
                    where pub.Date == startYear + i
                    select pub;
                int count = counter.Count();
                //List<Publication> count = r.Publications.FindAll(delegate (Publication pub) { return pub.Date == startYear + i; });
                //int yearCount = count.Count();
                Console.WriteLine("Year: {0}           |  Count: {1}       ", startYear+i, count);
            }

            return;
        }

        public void AchievementReport()
        {
            PublicationsController P_Cont3 = new PublicationsController();
            Console.WriteLine("\nAchievement Report\n");
            foreach (Researcher r in ResearcherList)
            {
                if (r.Level != EmploymentLevel.Student)
                {
                    r.Publications = P_Cont3.LoadPublicationsForID(r.Id);
                    double threeYAvg = ((Staff)r).calc3yrAvg(r.Publications);
                    double perf = ((Staff)r).performance(r.Level, threeYAvg);
                    Console.WriteLine("Name:" + r.GivenName + " " + r.FamilyName);
                    Console.WriteLine("Performance Metric:" + perf + "\n");
                }

            }
        }





    }
}