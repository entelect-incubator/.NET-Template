namespace Core.Database.EFMapping;

using Common.Entities.V1;

public class PizzaMap : IEntityTypeConfiguration<Pizza>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Pizza> builder)
    {
        // table
        builder.ToTable("Pizza");

        // key
        builder.HasKey(t => t.Id);

        // properties
        builder.Property(t => t.Id)
            .IsRequired()
            .HasColumnName("ID")
            .HasColumnType("int")
            .ValueGeneratedOnAdd();

        builder.Property(t => t.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("varchar(200)")
            .HasMaxLength(200);

        builder.Property(t => t.DateCreated)
            .HasColumnName("DateCreated")
            .HasColumnType("datetime")
            .HasDefaultValueSql("GETDATE()");

        builder.Property(t => t.Disabled)
            .HasColumnName("Disabled")
            .HasColumnType("bool")
            .HasDefaultValueSql("true");
    }

    public struct Table
    {
        public const string Schema = "";
        public const string Name = "Sample";
    }

    public struct Columns
    {
        public const string Id = "Id";
        public const string Name = "Name";
        public const string DateCreated = "DateCreated";
        public const string Disabled = "Disabled";
    }
}
