using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAD.Models
{
    public class Requisition
    {
        public int Id { get; set; }
        
        public int EmployeeId { get; set; }
        public DateTime dateOfRequest { get; set; }
        public DateTime dateOfAuthorizing { get; set; }
        [ForeignKey("Employee")]
        public int AuthorizerId { get; set; }
        public string status { get; set; }
        public string comment { get; set; }
        public virtual Employee Employee { get; set; }



    }
}