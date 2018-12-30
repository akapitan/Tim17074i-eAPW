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
    
    public partial class Djelovi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Djelovi()
        {
            this.Lokacija_has_djelovi = new HashSet<Lokacija_has_djelovi>();
            this.Narudzba_has_Djelovi = new HashSet<Narudzba_has_Djelovi>();
            this.Racun_Has_Djelovi = new HashSet<Racun_Has_Djelovi>();
            this.Rezervacija_has_Djelovi = new HashSet<Rezervacija_has_Djelovi>();
        }
    
        public int id { get; set; }
        public string naziv { get; set; }
        public int proizodac { get; set; }
        public int modelVozila { get; set; }
        public int kategorija { get; set; }
        public int maloprodajnaCijena { get; set; }
        public int veleprodajnaCijena { get; set; }
    
        public virtual Kategorija Kategorija1 { get; set; }
        public virtual Model_vozila Model_vozila { get; set; }
        public virtual Proizvodac Proizvodac { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Lokacija_has_djelovi> Lokacija_has_djelovi { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Narudzba_has_Djelovi> Narudzba_has_Djelovi { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Racun_Has_Djelovi> Racun_Has_Djelovi { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rezervacija_has_Djelovi> Rezervacija_has_Djelovi { get; set; }
    }
}