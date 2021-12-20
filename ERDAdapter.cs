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
    public class ERDAdapter
    {

        public static MySqlConnection conn { get; set; }
        static string db = "kit206";
        static string user = "kit206";
        static string pass = "kit206";
        static string server = "alacritas.cis.utas.edu.au";

        public static T ParseEnum<T>(string value)
        {
           return (T)Enum.Parse(typeof(T), value.Replace(" ","_"));
        }


        public List<Research.Researcher> fetchBasicResearcherDetails()
        {
            conn = GetConnection();
           
            List<Researcher> ResearcherList = new List<Researcher>();
            MySqlDataReader rdr = null;
            

            try
            {
                conn.Open();
                
                MySqlCommand cmd = new MySqlCommand("select id, given_name, family_name, level from researcher", conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    // Add the Researchers into ResearcherList as either Staff or Student:
                    // Default is Staff:
                    Staff r = new Staff();
                    r.GivenName = rdr.GetString(1);
                    r.FamilyName = rdr.GetString(2);
                    r.Id = rdr.GetInt32(0);
                   
                    try
                    {
                        r.Level = ERDAdapter.ParseEnum<EmploymentLevel>(rdr.GetString(3));
                        

                    }catch (Exception e)
                    // Note: this works because in the DB, student level is NULL.
                    // So if we get the null exception, make the researcher a Student:
                    {
                        Student r_student=new Student();
                        r_student.Level = EmploymentLevel.Student;
                        r_student.GivenName=rdr.GetString(1);
                        r_student.FamilyName = rdr.GetString(2);
                        r_student.Id = rdr.GetInt32(0);
                        ResearcherList.Add(r_student);
                    }

                    if (r.Level != EmploymentLevel.Student)
                    {
                        r.Level=ERDAdapter.ParseEnum<EmploymentLevel>(rdr.GetString(3));
                        ResearcherList.Add(r);
                    }
                   
                   
                   

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
            //Researcher r = new Researcher();
            MySqlDataReader rdr = null;
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select id, given_name, family_name, level, title, unit, campus, email, photo, degree, supervisor_id, utas_start, current_start from researcher where id=?id_num", conn);
                cmd.Parameters.AddWithValue("id_num",id_num);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    Staff r = new Staff();
                    /*r.GivenName = rdr.GetString(1);
                    r.FamilyName = rdr.GetString(2);
                    r.Id = rdr.GetInt32(0);*/
                   
                    try
                    {
                        r.Level = ERDAdapter.ParseEnum<EmploymentLevel>(rdr.GetString(3));
                        

                    }catch (Exception e)
                    // Note: this works because in the DB, student level is NULL.
                    // So if we get the null exception, make the researcher a Student:
                    {
                        Student r_student=new Student();
                        r_student.Level = EmploymentLevel.Student;
                        r_student.GivenName=rdr.GetString(1);
                        r_student.FamilyName = rdr.GetString(2);
                        r_student.Id = rdr.GetInt32(0);
                        r_student.Id=rdr.GetInt32(0);
                        r_student.GivenName = rdr.GetString(1);
                        r_student.FamilyName = rdr.GetString(2);
                        r_student.Title = rdr.GetString(4);
                        r_student.School = rdr.GetString(5);
                        r_student.Campus = rdr.GetString(6);
                        r_student.Email = rdr.GetString(7);
                        r_student.PhotoURL = rdr.GetString(8);
                        r_student.UtasStart=rdr.GetDateTime(11);
                        r_student.CurrentStart=rdr.GetDateTime(12);
                        r_student.Degree=rdr.GetString(9);
                        r_student.SupervisorID=rdr.GetString(10);
                        
                        return r_student;
                    }

                    if (r.Level != EmploymentLevel.Student)
                    {
                         r.Id=rdr.GetInt32(0);
                         r.GivenName = rdr.GetString(1);
                         r.FamilyName = rdr.GetString(2);
                         r.Title = rdr.GetString(4);
                         r.School = rdr.GetString(5);
                         r.Campus = rdr.GetString(6);
                         r.Email = rdr.GetString(7);
                         r.PhotoURL = rdr.GetString(8);
                         r.UtasStart=rdr.GetDateTime(11);
                         r.CurrentStart=rdr.GetDateTime(12);
                         r.Level=ERDAdapter.ParseEnum<EmploymentLevel>(rdr.GetString(3));
                         return r;
                    }
                   
                   
                   


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
            return null;
        }

        public List<Research.Publication> LoadPublications(int Id)
        {
            conn = GetConnection();
            List<Research.Publication> TestPubList = new List<Research.Publication>();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                //MySqlCommand cmd = new MySqlCommand("select doi from researcher_publication where researcher_id=?id", conn);

                MySqlCommand cmd = new MySqlCommand("select title, year, type, available from publication as pub, researcher_publication as respub where pub.doi = respub.doi and researcher_id=?id", conn);
                cmd.Parameters.AddWithValue("id", Id);

                // 

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    TestPubList.Add(new Publication
                        {Title = rdr.GetString(0), Date=rdr.GetInt32(1) , Type=Publication.ParseEnum<Publication.OutputType>(rdr.GetString(2)),  AvailableDate=rdr.GetDateTime(3)});//
                        //, level = GetTitleFromLevel(ParseEnum<EmploymentLevel>(rdr.GetString(3))
                                                    //{Title = rdr.GetString(0), Year=rdr.GetDateTime(1), Type=Publication.ParseEnum<Publication.OutputType>(rdr.GetString(2)),AvailableDate=rdr.GetString(4)});                                                                                                                                         
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

            return TestPubList;
        }


        public List<Research.Publication> fetchBasicPublicationDetails(Research.Researcher r)
        {
             conn = GetConnection();
           
            List<Publication> PublicationsList = new List<Publication>();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select * from researcher_publication where id=?id_num", conn);
                cmd.Parameters.AddWithValue("id_num",r.Id);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    PublicationsList.Add(new Publication { DOI=rdr.GetString(1) });
                   
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

            

            return PublicationsList;
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
