//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eAPW
{
    using System;
    using System.Collections.Generic;
    
    public partial class Zaposlenik_has_Tip
    {
        public int id_zaposlenik { get; set; }
        public int id_tip { get; set; }
        public Nullable<System.DateTime> datum { get; set; }
    
        public virtual Tip Tip { get; set; }
        public virtual Zaposlenik Zaposlenik { get; set; }
    }
}
