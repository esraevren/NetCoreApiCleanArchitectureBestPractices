using Microsoft.EntityFrameworkCore;

namespace App.Repositories.Categories
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(50);
        }
    }
}
