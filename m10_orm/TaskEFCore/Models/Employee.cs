using System;
using System.Collections.Generic;
using System.Text;

namespace TaskEFCore.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int? ReportTo { get; set; }

        public virtual Employee ReportToNavigation { get; set; }
        public virtual ICollection<Employee> InversReportToNavigations { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<EmployeeCreditCard> CreditCards { get; set; }

        public Employee()
        {
            InversReportToNavigations = new HashSet<Employee>();
            Orders = new HashSet<Order>();
            CreditCards = new HashSet<EmployeeCreditCard>();
        }
    }
}
