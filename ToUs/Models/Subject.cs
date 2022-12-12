//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ToUs.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Subject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Subject()
        {
            this.SubjectManagers = new HashSet<SubjectManager>();
        }
    
        public string Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> NumberOfDigits { get; set; }
        public string HTGD { get; set; }
        public string System { get; set; }
        public string Faculity { get; set; }
        public Nullable<bool> IsLab { get; set; }
        public string Note { get; set; }
        public string Language { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubjectManager> SubjectManagers { get; set; }
    }
}
