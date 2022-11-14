using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hi_Tech_Management_System.DataAccess;
using System.Windows.Forms;

namespace Hi_Tech_Management_System.Business
{
    public class Employee
    {
        private int employeeId;
        private string firstName;
        private string lastName;
        private string phoneNumber;
        private string email;
        private string jobTitle;
        private DateTime hireDate;        

        public int EmployeeId { get => employeeId; set => employeeId = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string Email { get => email; set => email = value; }
        public string JobTitle { get => jobTitle; set => jobTitle = value; }
        public DateTime HireDate { get => hireDate; set => hireDate = value; }

        public virtual string DisplayInfo()
        {
            return (employeeId + "," + firstName + "," + lastName);
        }

        public void SaveEmployee(Employee emp)
        {
            EmployeeDA.Save(emp);
        }

        public Employee searchEmployee(int empId)
        {
            return EmployeeDA.Search(empId);
        }

        public List<Employee> SearchEmployee(string input)
        {
            return EmployeeDA.Searchname(input);
        }

        public List<Employee> GetListEmployees()
        {
            return EmployeeDA.GetAllRecords();
        }

        public void DisplayList(ListView listViewE, List<Employee> listE)
        {
            listViewE.Items.Clear();
            foreach (Employee empItem in listE)
            {
                ListViewItem item = new ListViewItem(empItem.EmployeeId.ToString());
                item.SubItems.Add(empItem.FirstName);
                item.SubItems.Add(empItem.LastName);
                item.SubItems.Add(empItem.HireDate.ToString());
                item.SubItems.Add(empItem.phoneNumber);
                item.SubItems.Add(empItem.email);
                item.SubItems.Add(empItem.jobTitle);
                listViewE.Items.Add(item);
            }
        }

        public void DeleteEmployee(int id)
        {
            EmployeeDA.DeleteRecord(id);
        }

        public void UpdateEmployee(Employee emp)
        {
            EmployeeDA.UpdateRecord(emp);
        }

        public bool DuplicatedId(int id)
        {
            return EmployeeDA.IsDuplicated(id);
        }

        public bool JobTitleCheck (int id)
        {
            return EmployeeDA.empJobTitleCheck(id);
        }
    }
}
