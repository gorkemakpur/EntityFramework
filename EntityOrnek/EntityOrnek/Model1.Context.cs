//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntityOrnek
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class SinavOgrenciEntities : DbContext
    {
        public SinavOgrenciEntities()
            : base("name=SinavOgrenciEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Ders> Ders { get; set; }
        public virtual DbSet<Notlar> Notlar { get; set; }
        public virtual DbSet<Ogrenci> Ogrenci { get; set; }
        public virtual DbSet<Kulup> Kulup { get; set; }
    
        public virtual ObjectResult<NotListesi_Result> NotListesi()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<NotListesi_Result>("NotListesi");
        }
    }
}
