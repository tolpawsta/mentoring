using System;
using System.Collections.Generic;
using System.Text;

namespace TaskEFCore.Models
{
    public class EmployeeCreditCard
    {
        public ulong CardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string CardHolderName { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
