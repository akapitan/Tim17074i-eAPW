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
    
    public partial class Tip
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tip()
        {
            this.Zaposlenik_has_Tip = new HashSet<Zaposlenik_has_Tip>();
        }
    
        public int id { get; set; }
        public string naziv { get; set; }
        public string opis { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Zaposlenik_has_Tip> Zaposlenik_has_Tip { get; set; }

        public override string ToString()
        {
            return naziv;
        }
    }
}
