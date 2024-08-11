using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAssignmentPractical
{
    /// <summary>
    /// Interaction logic for EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Page
    {

        private Employee employee = new Employee();
        private Data data = new Data();
        private bool isDataUpdate = false;

        private List<Employee> employees = new List<Employee>();
        public EmployeeWindow(bool? isUpdate = false)
        {
            InitializeComponent();
            if ((bool)isUpdate)
            {
                isDataUpdate = true;
            }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<Employee> list = new List<Employee>();
            try
            {

                Employee employee1 = employee.ReadEmployeeData("Employee1");
                Employee employee2 = employee.ReadEmployeeData("Employee2");
                Employee employee3 = employee.ReadEmployeeData("Employee3");
                list.Add(employee1);
                list.Add(employee2);
                list.Add(employee3);
                
                if (!isDataUpdate)
                {
                    data.InsertRecordIntoDB(list);
                }
                employees = data.GetAllEmployees();
                grdEmployees.ItemsSource = employees;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            if(grdEmployees.SelectedItem is Employee employee)
            {
               
                this.NavigationService.Navigate(new EditWindow(employee.EmployeeID));
                grdEmployees.SelectedItem = null;
            }
            else
            {
                MessageBox.Show($"Select an item in the list of employees", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        public void Refresh()
        {

        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnFindEmployee_Click(object sender, RoutedEventArgs e)
        {
            string name = txtFindEmployee.Text;
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show($"Please enter a name to search", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                List<Employee> employee = data.SearchEmployeesByName(name);
                grdEmployees.ItemsSource = employee;
                grdEmployees.Items.Refresh();
                txtFindEmployee.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void grdEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


    }
}
