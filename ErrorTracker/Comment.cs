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
    
    public partial class Comment
    {
        public Nullable<int> BookId { get; set; }
        public int ItemNumber { get; set; }
        public int Page { get; set; }
        public string Comment1 { get; set; }
        public string ValidStatus { get; set; }
        public int ClassCode { get; set; }
        public string Stage { get; set; }
        public int CommentId { get; set; }
    
        public virtual Book Book { get; set; }
    }
}