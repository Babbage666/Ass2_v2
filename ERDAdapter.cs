using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using KIT206_RAP_Project.Research;
using KIT206_RAP_Project.Control;
using KIT206_RAP_Project.Database;

namespace KIT206_RAP_Project.Database
{
    class ERDAdapter
    {

        public static MySqlConnection conn { get; set; }
        static string db = "kit206";
        static string user = "kit206";
        static string pass = "kit206";
        static string server = "alacritas.cis.utas.edu.au";


        public List<Research.Researcher> fetchBasicResearcherDetails()
        {
            conn = GetConnection();
           
            List<Researcher> ResearcherList = new List<Researcher>();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select id, given_name, family_name from researcher", conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ResearcherList.Add(new Researcher { GivenName = rdr.GetString(1), FamilyName=rdr.GetString(2), Id = rdr.GetInt32(0) });
                   
                }
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            

            return ResearcherList;

        }

       
        public Research.Researcher completeResearcherDetails(Research.Researcher r)
        {
            return null;
        }

        public Research.Researcher fullResearcherDetails(int id_num)
        {
            Researcher r = new Researcher();
            MySqlDataReader rdr = null;
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select * from researcher where researcher_id=?id_num", conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    r.Id=rdr.GetInt32(0);
                    r.GivenName = rdr.GetString(1);
                    r.FamilyName = rdr.GetString(2);
                    r.Title = rdr.GetString(3);


                }
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return r;
        }




        public List<Research.Publication> fetchBasicPublicationDetails(Research.Researcher r)
        {
            return null;
        }
        
        public List<Research.Publication> completePublicationDetails(Publication p)
        {
            return null;
        }

        public Publication fetchPublicationCounts(DateTime from, DateTime to)
        {
            return null;
        }

        private static MySqlConnection GetConnection()
        {
            if (conn == null)
            {
                string connectionString = String.Format("Database={0};Data Source={1};User Id={2}; Password={3}", db, server, user, pass);
                conn = new MySqlConnection(connectionString);
            }
            return conn;
        }

    }
}
