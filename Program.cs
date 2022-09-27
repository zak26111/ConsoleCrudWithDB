using ConsoleCrudOperationWithDB.DAL;
using ConsoleCrudOperationWithDB.Models;
using ConsoleCrudOperationWithDB.DAL;
using ConsoleCrudOperationWithDB.Models;
using Microsoft.Extensions.Configuration;

namespace CRUDOperatonWithDNT2
{
    class EmployeeCRUDOperation
    {
        private static IConfiguration _iconfiguration;
        private static readonly EmployeeDAL _employeeDAL;

        static EmployeeCRUDOperation()
        {
            GetAppSettingsFile();
            _employeeDAL = new EmployeeDAL(_iconfiguration);
        }

        static void Main()
        {
            

            bool isExit = false;

            while (!isExit)
            {
                Console.WriteLine("Please enter your choice. 1-Show All Employee,2-Show Single Employee,3-Save Employee,4-Update Employee,5-Delete Employee,6-Exit");
                int userChoice = Convert.ToInt32(Console.ReadLine());

                if (userChoice == 1)
                {
                    ShowALlEmloyees();
                }
                else if (userChoice == 2)
                {
                    Console.WriteLine("Please enter your email Id");
                    string emailId = Console.ReadLine();
                    ShowSigleEmloyee(emailId);
                }
                else if (userChoice == 3)
                {
                    var employee = new Employee();
                    Console.WriteLine("Please enter your name");
                    employee.Name = Console.ReadLine();

                    Console.WriteLine("Please enter your city");
                    employee.City = Console.ReadLine();

                    Console.WriteLine("Please enter your age");
                    employee.Age = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Please enter your email Id");
                    employee.EmailId = Console.ReadLine();

                    SaveEmployee(employee);
                }
                else if (userChoice == 4)
                {
                    var employee = new Employee();
                    Console.WriteLine("Please enter your name");
                    employee.Name = Console.ReadLine();

                    Console.WriteLine("Please enter your city");
                    employee.City = Console.ReadLine();

                    Console.WriteLine("Please enter your age");
                    employee.Age = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Please enter your email Id");
                    employee.EmailId = Console.ReadLine();
                    UpdateEmployee(employee);
                }
                else if (userChoice == 5)
                {

                    Console.WriteLine("Please enter your email Id");
                    string emailId = Console.ReadLine();
                    DeleteEmployee(emailId);
                }
                else if (userChoice == 6)
                {
                    isExit = true;
                    Console.WriteLine("Thank you for visiting");
                }
            }


            

        }

        #region Private Static Methods

        static void GetAppSettingsFile()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json",
                    optional: false, reloadOnChange: true);
            _iconfiguration = builder.Build();
        }

        static void ShowALlEmloyees()
        {
            var listOfEmployees = _employeeDAL.GetAllEmployees();
            listOfEmployees.ForEach(item =>
            {
                Console.WriteLine($"Employee ID: {item.Id}" +
                    $" Name: {item.Name}" +
                    $" City: {item.City}" +
                    $" Age: {item.Age}" +
                    $" Email Id: {item.EmailId}");
            });
        }

        static void ShowSigleEmloyee(string emailId)
        {
            var employee = _employeeDAL.GetSingleEmployee(emailId);

            Console.WriteLine($"Employee ID: {employee.Id}" +
                $" Name: {employee.Name}" +
                $" City: {employee.City}" +
                $" Age: {employee.Age}" +
                $" Email Id: {employee.EmailId}");
        }

        static void SaveEmployee(Employee employee)
        {
            string result = _employeeDAL.SaveEmployee(employee);
            Console.WriteLine(result); ;
        }


        static void UpdateEmployee(Employee employee)
        {
            string result = _employeeDAL.UpdateEmployee(employee);
            Console.WriteLine(result); ;
        }

        static void DeleteEmployee(string emailId)
        {
            string result = _employeeDAL.DeleteEmployee(emailId);
            Console.WriteLine(result); ;
        }

        #endregion
    }
}