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
            List<Publication> sortedPubslist=PublicationList2.OrderBy(p => p.Date).ThenBy(p => p.Title).ToList();
            return sortedPubslist;

        }

       


    }
}
