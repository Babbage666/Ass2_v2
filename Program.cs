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
    /* TODO 
       1. Publications list in researcher details needs to be sorted, in time order. NOT DONE. Easier in GUI window probably.
       4. General Error catching (ie, unexpected input etc) NOT DONE
       5. WPF implementation needs to be included and completed NOT DONE
     * 
     */
  
    public class Program
    {
        public static void Main(string[] args)
        {
           
            // Display the basic Researcher Details:
            ResearcherController R_Cont = new ResearcherController();
            PublicationsController P_Cont=new PublicationsController(); // Don't need this now???

            bool quit = false;

            while (!quit)
            {
                Console.WriteLine("\nPlease Select an option: \n 1: Display List \n 2: Filter List by Employment Level \n 3. Filter List by name  \n 4: Display Details \n 5: Achievement Report \n 0: Quit");
                string option = Console.ReadLine();
                Console.WriteLine("{0} selected", option);
                switch (option) 
                {
                    case "1":
                        R_Cont.Display();
                        break;
                    case "2":
                        Console.WriteLine("Please Enter Employment Level to filter by: A, B, C, D, E or S (Student)");
                        string level = Console.ReadLine();
                        switch (level) {
                            case "A":
                                R_Cont.FilterByLevel(EmploymentLevel.A);
                                R_Cont.DisplayFilteredList();
                                break;
                            case "B":
                                R_Cont.FilterByLevel(EmploymentLevel.B);
                                R_Cont.DisplayFilteredList();
                                break;
                            case "C":
                                R_Cont.FilterByLevel(EmploymentLevel.C);
                                R_Cont.DisplayFilteredList();
                                break;
                            case "D":
                                R_Cont.FilterByLevel(EmploymentLevel.D);
                                R_Cont.DisplayFilteredList();
                                break;                            
                            case "E":
                                R_Cont.FilterByLevel(EmploymentLevel.E);
                                R_Cont.DisplayFilteredList();
                                break;
                            case "S":
                                R_Cont.FilterByLevel(EmploymentLevel.Student);
                                R_Cont.DisplayFilteredList();
                                break;
                            default:
                                Console.WriteLine("LEVEL_NOT_FOUND");
                                break;
                        }
                        break;
                    case "3":
                        Console.WriteLine("Please Enter name to filter by:");
                        string name = Console.ReadLine();
                        R_Cont.FilterByName(name);
                        R_Cont.DisplayFilteredList();
                        break;

                    case "4":
                        Console.WriteLine("Please Enter ID number to display details");
                        int ID = Int32.Parse(Console.ReadLine());
                        Console.WriteLine("Details of Researcher ID: {0} \n", ID);
                        R_Cont.LoadResearcherDetails(ID);
                       
                        break;

                    case "5":
                        R_Cont.AchievementReport();
                        break;

                    case "0":
                        quit = true;
                        break;
                    default:
                        Console.WriteLine("Not a valid option, please enter a valid option \n");
                        break;
                }
            }  

            
        }

    }   
}
