using Microsoft.EntityFrameworkCore;
using ThoughtBroker.Domain.Opinions;
using ThoughtBroker.Domain.Thoughts;
using ThoughtBroker.Domain.Users;

namespace ThoughtBroker.Infrastructure.Context;

public partial class EfDbContext : DbContext
{
    public EfDbContext()
    {
    }

    public EfDbContext(DbContextOptions<EfDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Opinion> Opinions { get; set; }

    public virtual DbSet<Thought> Thoughts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("Server=localhost,1433; Database=msdb; User=sa; Password=yourStrong(!)Password;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Opinion>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ThoughtId }).HasName("Opinion_pk");

            entity.ToTable("Opinion");

            entity.Property(e => e.UserId).HasColumnName("User_Id");
            entity.Property(e => e.ThoughtId).HasColumnName("Thought_Id");

            entity.HasOne(d => d.Thought).WithMany(p => p.Opinions)
                .HasForeignKey(d => d.ThoughtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Like_Thought");

            entity.HasOne(d => d.User).WithMany(p => p.Opinions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Like_User");
        });

        modelBuilder.Entity<Thought>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Thought_pk");

            entity.ToTable("Thought");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Content)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.ParentId).HasColumnName("Parent_Id");
            entity.Property(e => e.Timestamp).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("Thought_Thought");

            entity.HasOne(d => d.User).WithMany(p => p.Thoughts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Thought_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pk");

            entity.ToTable("User");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasMany(d => d.User2s).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "Observation",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("User2Id")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Observe_User2"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Observe_User"),
                    j =>
                    {
                        j.HasKey("UserId", "User2Id").HasName("Observation_pk");
                        j.ToTable("Observation");
                        j.IndexerProperty<Guid>("UserId").HasColumnName("User_Id");
                        j.IndexerProperty<Guid>("User2Id").HasColumnName("User_2_Id");
                    });

            entity.HasMany(d => d.Users).WithMany(p => p.User2s)
                .UsingEntity<Dictionary<string, object>>(
                    "Observation",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Observe_User"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("User2Id")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Observe_User2"),
                    j =>
                    {
                        j.HasKey("UserId", "User2Id").HasName("Observation_pk");
                        j.ToTable("Observation");
                        j.IndexerProperty<Guid>("UserId").HasColumnName("User_Id");
                        j.IndexerProperty<Guid>("User2Id").HasColumnName("User_2_Id");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
