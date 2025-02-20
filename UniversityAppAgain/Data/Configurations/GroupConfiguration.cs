﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniversityAppAgain.Data.Entities;

namespace UniversityAppAgain.Data.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.Property(x => x.No).HasMaxLength(5).IsRequired(true);
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Limit).IsRequired(true);
        }
    }
}
