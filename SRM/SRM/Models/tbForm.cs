//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SRM.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbForm
    {
        public int id { get; set; }
        public string FormName { get; set; }
        public string FormType { get; set; }
        public string module { get; set; }
        public Nullable<decimal> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy_name { get; set; }
        public Nullable<decimal> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedByName { get; set; }
        public Nullable<bool> isDeleted { get; set; }
    }
}
