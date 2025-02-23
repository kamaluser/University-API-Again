using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniversityAppAgain.Data.Entities;

namespace UniversityAppAgain.Data.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(x => x.FullName).IsRequired(true).HasMaxLength(35);
            builder.Property(x => x.Email).HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.BirthDate).IsRequired(true).HasColumnType("date");
            builder.HasOne(x => x.Group).WithMany(x=>x.Students).HasForeignKey(x=>x.GroupId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
