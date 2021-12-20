﻿using System;
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
   

    public class PublicationsController
    {

        
        public List<Publication> LoadPublicationsFor(Research.Researcher r)
        {

           return null;
        }

        public List<Research.Publication> LoadPublicationsForID(int id)
        {

            ERDAdapter Adapter1 = new ERDAdapter();
            List<Research.Publication> PublicationList2 = new List<Research.Publication>();
            PublicationList2 = Adapter1.LoadPublications(id);
            return PublicationList2;

        }

        public int calc3yrAvg(List<Research.Publication> inputList)
        {
           
            DateTime thisday = DateTime.Today;
            int thisyear = thisday.Year;
            int count = 0;
            foreach (Publication p in inputList)
            {
                if (thisyear - p.Date < 3)
                    count = count + 1;

            }
            Console.WriteLine("Count is equl to:" + count);
            return count;
        }


    }
}
