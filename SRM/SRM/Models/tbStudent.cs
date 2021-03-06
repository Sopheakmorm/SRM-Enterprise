//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace SRM.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbStudent
    {
        public decimal stdID { get; set; }
        [Required(ErrorMessage = "Input Name")]
        public string Name { get; set; }
        public string Gender { get; set; }
        public Nullable<int> ClassID { get; set; }
        public Nullable<decimal> ParID { get; set; }
        public string Address { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string ContactPhone { get; set; }
        public string CurrenctAddress { get; set; }
        public string PreAddress { get; set; }
        public Nullable<decimal> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string catID { get; set; }
        public string CreatedBy_name { get; set; }
        public Nullable<decimal> ModifiedBy { get; set; }
        public Nullable<int> Photo { get; set; }
        public string HouseNo { get; set; }
        public string Father_name { get; set; }
        public string Mother_name { get; set; }
        public Nullable<bool> isDeleted { get; set; }
    
        public virtual tbClass tbClass { get; set; }
        public virtual tbParent tbParent { get; set; }
        public virtual tbPhoto tbPhoto { get; set; }
        public virtual tbStudentCategory tbStudentCategory { get; set; }
    }
}
