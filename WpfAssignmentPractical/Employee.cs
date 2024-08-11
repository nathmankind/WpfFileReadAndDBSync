using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfAssignmentPractical
{
    internal class Employee
    {
        private int employeeId;
        private string employeeName;
        private string position;
        private decimal hourlyRate;

        public int EmployeeID
        {
            get => employeeId;
            set => employeeId = value;
        }
        public string EmployeeName
        {
            get => employeeName;
            set => employeeName = value;
        }
        public string Position
        {
            get => position;
            set => position = value;
        }
        public decimal HourlyPayRate
        {
            get => hourlyRate;
            set => hourlyRate = value;
        }

        public Employee()
        {
            employeeId = 0;
            employeeName = string.Empty;
            position = string.Empty;
            hourlyRate = 0;
        }
        // Constructor that accepts all properties
        public Employee(int id, string name, string position, decimal hourlyRate)
        {
            EmployeeID = id;
            EmployeeName = name;
            Position = position;
            HourlyPayRate = hourlyRate;
        }

        // Constructor that accepts ID, name, and payRate
        public Employee(int id, string name, decimal hourlypayRate)
        {
            EmployeeID = id;
            EmployeeName = name;
            Position = string.Empty;
            HourlyPayRate = hourlypayRate;
        }

        public Employee ReadEmployeeData(string fileName)
        {
           
            // C:\PROG8011\WpfAssignmentPractical\WpfAssignmentPractical\Employees\Employee1.txt
            string pathToFile =  $"C:\\PROG8011\\WpfAssignmentPractical\\WpfAssignmentPractical\\Employees\\{fileName}.txt";

            using (StreamReader reader = new StreamReader(pathToFile))
            {
                int id = int.Parse(reader.ReadLine());
                string name = reader.ReadLine();
                string position = reader.ReadLine();
                decimal payRate = decimal.Parse(reader.ReadLine());

                return new Employee(id, name, position, payRate);
            }
        }

        public void InsertEmployeeRecord(SqlConnection conn, Employee employee)
        {
            string newQuery = "IF NOT EXISTS (SELECT * FROM Employee WHERE EmployeeID = @EmployeeID) BEGIN INSERT INTO Employee (EmployeeID, EmployeeName, Position, HourlyPayRate) VALUES (@EmployeeID, @EmployeeName, @Position, @HourlyPayRate) END";
            string query = "INSERT INTO Employee (EmployeeID, EmployeeName, Position, HourlyPayRate) VALUES (@EmployeeID, @EmployeeName, @Position, @HourlyPayRate)";
            using (SqlCommand cmd = new SqlCommand(newQuery, conn))
            {
                cmd.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
                cmd.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                cmd.Parameters.AddWithValue("@Position", employee.Position);
                cmd.Parameters.AddWithValue("@HourlyPayRate", employee.HourlyPayRate);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error inserting employee value {employee.EmployeeID}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
