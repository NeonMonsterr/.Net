using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebProject.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebProject.DAL.Configrations
{
    internal class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasColumnType("varchar").HasMaxLength(50);
            builder.Property(d => d.Code).IsRequired();
            builder.Property(d => d.Description).HasMaxLength(250); 
            builder.Property(d => d.CreationDate).IsRequired().HasDefaultValueSql("GETDATE()"); ;
           builder.HasMany(d => d.Employees).WithOne(e => e.Department) .HasForeignKey(e => e.DepartmentId).IsRequired();
        }
    }
}
