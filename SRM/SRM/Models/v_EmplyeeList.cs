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
    
    public partial class v_EmplyeeList
    {
        public decimal empID { get; set; }
        public string Name { get; set; }
        public string IDCard { get; set; }
        public string DOB { get; set; }
        public string ContactPhone { get; set; }
        public string Gender { get; set; }
        public string CurrentAddress { get; set; }
        public string PreviousAddress { get; set; }
        public string Position { get; set; }
        public Nullable<bool> isActivated { get; set; }
        public Nullable<bool> isDeleted { get; set; }
    }
}