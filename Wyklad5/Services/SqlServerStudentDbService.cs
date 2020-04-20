using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Wyklad5.DTOs.Requests;
using Wyklad5.DTOs.Responses;
using Wyklad5.Models;

namespace Wyklad5.Services
{
    public class SqlServerStudentDbService : IStudentDbService
    {

        private const string StringConstruct = "Data Source=db-mssql;Initial Catalog=s18844;Integrated Security=True";

        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest request)
        {
            int pobranySemestr = 1;
            DateTime pobranadata = DateTime.Today;
            EnrollStudentResponse respond = null;
     
            using (var con = new SqlConnection(StringConstruct))
            using (var com = new SqlCommand())
            {
                com.Connection = con;

                con.Open();
                var tran = con.BeginTransaction();
                
                try
                {
                    //1. Czy studia istnieja?
                    com.CommandText = "select IdStudy from studies where name=@name";
                    com.Parameters.AddWithValue("name", request.Studies);
                    com.Transaction = tran;
                    
                    var dr = com.ExecuteReader();
                    if (!dr.Read())
                    {
                        dr.Close();
                        tran.Rollback();
                        return respond;
                    }
                    int idstudies = (int)dr["IdStudy"];
                    dr.Close();

                    //2.Czy taka Ska juz jest
                    com.CommandText = "select count(*) from Student where IndexNumber=@IndexNumber";
                    com.Parameters.AddWithValue("IndexNumber", request.IndexNumber);
                    dr = com.ExecuteReader();
                    dr.Read();
                    int pobrane = (int)dr[0];

                    if (pobrane==0)
                    {
                        dr.Close();
                        tran.Rollback();
                        return respond;
                    }
                    dr.Close();

                    //3.Czy wpis istnieje
                    com.CommandText = "select count(*) from Enrollment where idStudy = @idstudies";
                    com.Parameters.AddWithValue("idstudies", idstudies);
                    dr = com.ExecuteReader();
                    dr.Read();
                    pobrane = (int)dr[0];
                    DateTime thisDay = DateTime.Today;
                    if (pobrane != 0)
                    {
                        //JEST
                        com.CommandText = "UPDATE Enrollment SET StartDate = @dzisiejszaData WHERE IdStudy = @idstudies AND Semester = 1 ";
                        com.Parameters.AddWithValue("idstudies", idstudies);
                        com.Parameters.AddWithValue("dzisiejszaData", thisDay);
                        dr.Close();
                        com.ExecuteNonQuery();

                    }
                    //NIE MA 
                    com.CommandText = "INSERT INTO Enrollment(IdEnrollment, Semester,IdStudy,StartDate) VALUES(4,4,@idstudies,@dzisiejszaData)";
                    com.Parameters.AddWithValue("idstudies", idstudies);
                    com.Parameters.AddWithValue("dzisiejszaData", thisDay);
                    dr.Close();
                    com.ExecuteNonQuery();

                    com.CommandText = "select * from Enrollment where idStudy = @idstudies";
                    com.Parameters.AddWithValue("idstudies", idstudies);
                    dr = com.ExecuteReader();
                    dr.Read();
                     pobranySemestr = (int)dr["Semester"];
                     pobranadata = (DateTime) dr["StartData"];
                    dr.Close();

                    tran.Commit();
                   

                }
                catch (SqlException exc)
                {
                    tran.Rollback();
                }
            }

            respond = new EnrollStudentResponse();
            respond.LastName = request.LastName;
            respond.Semester = pobranySemestr;
            respond.StartDate = pobranadata;
            return respond;
        }

        public Boolean GetStudent(string index)
        {
            using (var con = new SqlConnection(ConString))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();

                com.CommandText = "Select * from Student where indexNumber = @indexNumber";
                com.Parameters.AddWithValue("indexNumber", index);
                var dr = com.ExecuteReader();
                if (dr.Read())
                {
                   
                    return true;

                }
                dr.Close();

                return false;
            }
        }


        public EnrollStudentResponse PromoteStudents(int semester, string studies)
        {
            int pobranySemestr = 1;
            DateTime pobranadata = DateTime.Today;
            EnrollStudentResponse respond = null;

            using (var con = new SqlConnection(StringConstruct))
            using (var com = new SqlCommand())
            {
                com.Connection = con;

                con.Open();
                var tran = con.BeginTransaction();

                try
                {
                    //1. Czy istnieja?
                    com.CommandText = "select IdStudy from studies where name=@name AND IdStudy=@IdStudy";
                    com.Parameters.AddWithValue("name", studies);
                    com.Parameters.AddWithValue("IdStudy", semester);
                    com.Transaction = tran;

                    var dr = com.ExecuteReader();
                    if (!dr.Read())
                    {
                        dr.Close();
                        tran.Rollback();
                        return respond;
                    }
                    int idstudies = (int)dr["IdStudy"];
                    dr.Close();
                    
                    com.CommandText = "exec procedurka @semester, @studies";
                    com.Parameters.AddWithValue("semester", semester);
                    com.Parameters.AddWithValue("studies", studies);
                    com.ExecuteNonQuery();


                    tran.Commit();


                }
                catch (SqlException exc)
                {
                    tran.Rollback();
                }
            }

            respond = new EnrollStudentResponse();
            return respond;
        }
    }
}
