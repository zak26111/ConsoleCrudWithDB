
using ConsoleCrudOperationWithDB.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ConsoleCrudOperationWithDB.DAL
{
    public class EmployeeDAL
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public EmployeeDAL(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._connectionString = this._configuration.GetConnectionString("DbConnection");
        }

        public List<Employee> GetAllEmployees()
        {
            var lstEmployees = new List<Employee>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    //SqlCommand cmd = new SqlCommand("SELECT * FROM Employee", con);
                    SqlCommand cmd = new SqlCommand("Usp_GetAllEmployees", con);
                    //cmd.CommandType = CommandType.Text;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        lstEmployees.Add(new Employee
                        {
                            Id = rdr.GetInt32("Id"),
                            Name = rdr.GetString("Name"),
                            City = rdr.GetString("City"),
                            Age = rdr.GetInt32("Age"),
                            EmailId = rdr.GetString("EmailId"),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return lstEmployees;
        }

        public Employee GetSingleEmployee(string emailId)
        {
            var employee = new Employee();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    //SqlCommand cmd = new SqlCommand("SELECT * FROM Employee", con);
                    SqlCommand cmd = new SqlCommand("Usp_GetSingleEmployee", con);
                    //cmd.CommandType = CommandType.Text;                   
                    cmd.Parameters.AddWithValue("@EmailId", emailId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        employee.Id = rdr.GetInt32("Id");
                        employee.Name = rdr.GetString("Name");
                        employee.City = rdr.GetString("City");
                        employee.Age = rdr.GetInt32("Age");
                        employee.EmailId = rdr.GetString("EmailId");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return employee;
        }

        public string SaveEmployee(Employee employee)
        {
            string message = string.Empty;
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    //SqlCommand cmd = new SqlCommand("SELECT * FROM Employee", con);
                    SqlCommand cmd = new SqlCommand("Usp_SaveEmployee", con);
                    //cmd.CommandType = CommandType.Text;                   
                    cmd.Parameters.AddWithValue("@Name", employee.Name);
                    cmd.Parameters.AddWithValue("@City", employee.City);
                    cmd.Parameters.AddWithValue("@Age", employee.Age);
                    cmd.Parameters.AddWithValue("@EmailId", employee.EmailId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                        message = "Record successfully insernted";
                    else
                        message = "Sorry! error occured";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return message;
        }
        public string UpdateEmployee(Employee employee)
        {
            string message = string.Empty;
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Usp_UpdateEmployee", con);
                    cmd.Parameters.AddWithValue("@Name", employee.Name);
                    cmd.Parameters.AddWithValue("@City", employee.City);
                    cmd.Parameters.AddWithValue("@Age", employee.Age);
                    cmd.Parameters.AddWithValue("@EmailId", employee.EmailId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                        message = "Record updated successfully";
                    else
                        message = "Sorry ! Error occured.";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return message;
        }

    }
}