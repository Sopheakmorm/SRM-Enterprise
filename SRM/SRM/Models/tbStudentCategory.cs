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
    
    public partial class tbStudentCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbStudentCategory()
        {
            this.tbStudents = new HashSet<tbStudent>();
        }
    
        public string catID { get; set; }
        public string Descriptions { get; set; }
        public Nullable<decimal> CreatedBy { get; set; }
        public string CreatedBy_name { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<decimal> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbStudent> tbStudents { get; set; }
    }
}
