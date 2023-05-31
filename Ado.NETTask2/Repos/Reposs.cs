using Ado.NETTask2.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ado.NETTask2.Repos
{
    public class Reposs
    {
        SqlConnection conn;
        string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        DataSet set = new DataSet();

        public Reposs()
        {
            using (conn=new SqlConnection())
            {
                var sda = new SqlDataAdapter();
                conn.ConnectionString = connectionString;
                conn.Open();

                SqlCommand cmd = new SqlCommand("Select * from authors", conn);
                sda.SelectCommand=cmd;
                sda.Fill(set, "AuthorsSet");
            }
        }

        public DataSet GetAll()
        {
            return set;
        }

        public void InsertAuthor(int Id, string Name, string Surname)
        {
            using (conn=new SqlConnection())
            {
                var cmd = new SqlCommand("Insert into authors (Id,FirstName,LastName) values(@Id,@Name,@Surname)", conn);
                conn.ConnectionString=connectionString;
                conn.Open();

                cmd.Parameters.Add(new SqlParameter
                {
                    DbType=DbType.Int32,
                    ParameterName="@Id",
                    Value=Id
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    SqlDbType=SqlDbType.NVarChar,
                    ParameterName="@Name",
                    Value=Name
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    SqlDbType=SqlDbType.NVarChar,
                    ParameterName="@Surname",
                    Value=Surname
                });

                var sda = new SqlDataAdapter();
                sda.InsertCommand=cmd;
                sda.InsertCommand.ExecuteNonQuery();
                sda.Update(set, "AuthorsSet");
                set.Clear();
                sda=new SqlDataAdapter("select * from Authors", conn);

                sda.Fill(set, "AuthorsSet");

            }
        }
        public void DeleteAuthor(int Id)
        {
            using (conn=new SqlConnection())
            {
                var cmd = new SqlCommand("Delete from authors where Id=@Id", conn);
                conn.ConnectionString=connectionString;
                conn.Open();
                cmd.Parameters.Add(new SqlParameter
                {
                    DbType=DbType.Int32,
                    ParameterName="@Id",
                    Value = Id
                });
                var sda = new SqlDataAdapter();
                sda.DeleteCommand=cmd;
                sda.DeleteCommand.ExecuteNonQuery();
                sda.Update(set, "AuthorsSet");
                set.Clear();
                sda=new SqlDataAdapter("select * from Authors", conn);
                sda.Fill(set, "AuthorsSet");
            }




        }
        public void UpdateAuthor(int Id,string Name,string Surname)
        {
            using (conn = new SqlConnection())
            {
                conn.ConnectionString = connectionString;
                conn.Open();

                var command = new SqlCommand("UPDATE Authors SET Firstname=@firstName WHERE Id=@id", conn);

                command.Parameters.Add(new SqlParameter
                {
                    DbType = DbType.Int32,
                    ParameterName = "@id",
                    Value = 4
                });

                command.Parameters.Add(new SqlParameter
                {
                    SqlDbType=SqlDbType.NVarChar,
                    ParameterName="@firstName",
                    Value="Elchin"
                });

                var da = new SqlDataAdapter();
                da.UpdateCommand = command;
                da.UpdateCommand.ExecuteNonQuery();

                da.Update(set, "AuthorsSet");
                set.Clear();

                da = new SqlDataAdapter("SELECT * FROM Authors;SELECT * FROM Books", conn);

                da.Fill(set, "AuthorsSet");

               


            }
        }
    }
}
