//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ErrorTracker
{
    using System;
    using System.Collections.Generic;
    
    public partial class Book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Book()
        {
            this.Comments = new HashSet<Comment>();
        }
    
        public int BookId { get; set; }
        public Nullable<int> UserId { get; set; }
        public string Title { get; set; }
        public string PartNumber { get; set; }
        public string ATA { get; set; }
        public string Author { get; set; }
        public Nullable<int> TotalPages { get; set; }
        public Nullable<int> ChangePages { get; set; }
        public Nullable<System.DateTime> P2p { get; set; }
        public Nullable<System.DateTime> RcmGroup { get; set; }
        public Nullable<System.DateTime> RcmQa1 { get; set; }
        public Nullable<System.DateTime> UtasGroup { get; set; }
        public Nullable<System.DateTime> UtasQa1 { get; set; }
        public Nullable<System.DateTime> UtasIhr { get; set; }
        public Nullable<System.DateTime> Validate { get; set; }
        public Nullable<System.DateTime> RcmGroup2 { get; set; }
        public Nullable<System.DateTime> RcmQa2 { get; set; }
        public Nullable<System.DateTime> UtasQa2 { get; set; }
        public Nullable<System.DateTime> RcmDelivery { get; set; }
        public Nullable<bool> Delivered { get; set; }
        public string stage { get; set; }
    
        public virtual UserProfile UserProfile { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
