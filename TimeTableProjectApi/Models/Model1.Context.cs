//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TimeTableProjectApi.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Actual_timetable> Actual_timetable { get; set; }
        public virtual DbSet<Friday> Friday { get; set; }
        public virtual DbSet<Monday> Monday { get; set; }
        public virtual DbSet<Saturday> Saturday { get; set; }
        public virtual DbSet<Thursday> Thursday { get; set; }
        public virtual DbSet<Tuesday> Tuesday { get; set; }
        public virtual DbSet<Wednesday> Wednesday { get; set; }
    }
}
