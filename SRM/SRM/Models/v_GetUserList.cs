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
    
    public partial class v_GetUserList
    {
        public decimal Userid { get; set; }
        public string UserType { get; set; }
        public string UserName { get; set; }
        public string pwd { get; set; }
        public Nullable<decimal> Createdby { get; set; }
        public string CreatedDate { get; set; }
        public string Emp_name { get; set; }
        public Nullable<bool> isActibe { get; set; }
        public Nullable<bool> isDeleted { get; set; }
    }
}
