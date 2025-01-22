namespace Core.Pizzas.V1.Database.EFMapping;

using Core.Pizzas.V1.Database.Entities;

public class PizzaMap : IEntityTypeConfiguration<Pizza>
{
    public void Configure(EntityTypeBuilder<Pizza> builder)
    {
        // table
        builder.ToTable("Pizza");

        // key
        builder.HasKey(t => t.Id);

        // properties
        builder.Property(t => t.Id)
            .IsRequired()
            .HasColumnName("Id")
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
}