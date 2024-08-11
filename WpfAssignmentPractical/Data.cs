using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfAssignmentPractical
{
    internal class Data
    {
        private static string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;
                                         Initial Catalog=Personnel;
                                         Integrated Security=True";

        public static string ConnectionStr { get => connStr; }

        public ObservableCollection<Employee> GetAllEmployees2()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                ObservableCollection<Employee> list = new ObservableCollection<Employee>();
                Employee tblEmployees = new Employee();

                list.Clear();
                string query = "SELECT * FROM Employee";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        Employee employee = new Employee();
                        {
                            //EmployeeID = reader.GetInt32(0)
                            employee.EmployeeID = int.Parse(reader["EmployeeID"].ToString());
                            employee.EmployeeName = reader["EmployeeName"].ToString();
                            employee.Position = reader["Position"].ToString();
                            employee.HourlyPayRate = decimal.Parse(reader["HourlyPayRate"].ToString());

                        }

                        list.Add(employee);
                    }
                }
                return list;
            }
        }

        public List<Employee> GetAllEmployees()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                List<Employee> list = new List<Employee>();
                Employee tblEmployees = new Employee();

                list.Clear();
                string query = "SELECT * FROM Employee";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        Employee employee = new Employee();
                        {
                            //EmployeeID = reader.GetInt32(0)
                            employee.EmployeeID = int.Parse(reader["EmployeeID"].ToString());
                            employee.EmployeeName = reader["EmployeeName"].ToString();
                            employee.Position = reader["Position"].ToString();
                            employee.HourlyPayRate = decimal.Parse(reader["HourlyPayRate"].ToString());

                        }

                        list.Add(employee);
                    }
                }
                return list;
            }
        }
        //public void InsertRecordIntoDB(Employee employee)
        public void InsertRecordIntoDB(List<Employee> employees)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                foreach (var employee in employees)
                {
                    employee.InsertEmployeeRecord(conn, employee);
                }
                //MessageBox.Show("All employee Data inserted into DB successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public List<Employee> UpdateRecordInDB(Employee employee)
        {
            List<Employee> list = new List<Employee>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = $"UPDATE Employee SET EmployeeName = \'{employee.EmployeeName}\', Position = \'{employee.Position}\', HourlyPayRate = {employee.HourlyPayRate} WHERE EmployeeID = {employee.EmployeeID}";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                try
                {
                   
                    cmd.ExecuteNonQuery();
                    list = GetAllEmployees();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating employee with ID: {employee.EmployeeID}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return list;
        }



        public List<Employee> SearchEmployeesByName(string name)
        {
            List<Employee> results = new List<Employee>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = $"SELECT * FROM Employee WHERE EmployeeName LIKE \'%{name}%\'";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Employee employee = new Employee();
                            {
                                employee.EmployeeID = int.Parse(reader["EmployeeID"].ToString());
                                employee.EmployeeName = reader["EmployeeName"].ToString();
                                employee.Position = reader["Position"].ToString();
                                employee.HourlyPayRate = decimal.Parse(reader["HourlyPayRate"].ToString());

                            };
                            results.Add(employee);
                        }
                    }
                }
            }
            return results;
        }

        public Employee GetEmployeesById(int employeeId)
        {
           Employee result = new Employee();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = $"SELECT * FROM Employee WHERE EmployeeID = {employeeId}";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Employee employee = new Employee();
                            {
                                employee.EmployeeID = int.Parse(reader["EmployeeID"].ToString());
                                employee.EmployeeName = reader["EmployeeName"].ToString();
                                employee.Position = reader["Position"].ToString();
                                employee.HourlyPayRate = decimal.Parse(reader["HourlyPayRate"].ToString());

                            };
                            result = (employee);
                        }
                    }
                }
            }
            return result;
        }

    }
}
