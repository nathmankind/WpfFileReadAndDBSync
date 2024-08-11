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
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Page
    {

        private Data data = new Data();
        private Employee initialEmployeeData = new Employee();
        private int selectedEmployeeId = 0;
        public EditWindow(int employeeId)
        {
            InitializeComponent();
            selectedEmployeeId = employeeId;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                initialEmployeeData = data.GetEmployeesById(selectedEmployeeId);
                txtEmployeeID.Text = initialEmployeeData.EmployeeID.ToString();
                txtEmployeeName.Text = initialEmployeeData.EmployeeName;
                txtPosition.Text = initialEmployeeData.Position;
                txtHourlyPayRate.Text = initialEmployeeData.HourlyPayRate.ToString();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdateRecord_Click(object sender, RoutedEventArgs e)
        {
            string employeeID = txtEmployeeID.Text;
            string employeeName = txtEmployeeName.Text;
            string position = txtPosition.Text;
            string hourlyPayRate = txtHourlyPayRate.Text;

            
            
            Employee employee = new Employee();
            employee.EmployeeName = employeeName;
            employee.EmployeeID = int.Parse(employeeID);
            employee.Position = position;
            employee.HourlyPayRate = decimal.Parse(hourlyPayRate);
            try
            {
                data.UpdateRecordInDB(employee);
                this.NavigationService.Navigate(new EmployeeWindow(true));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        
       
    }
}
