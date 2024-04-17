using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace _1XBetParser.Models;

public partial class ParserbdContext : DbContext
{
    public ParserbdContext()
    {
    }

    public ParserbdContext(DbContextOptions<ParserbdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BetsTable> BetsTables { get; set; }

    public virtual DbSet<ChampTable> ChampTables { get; set; }

    public virtual DbSet<MatchTable> MatchTables { get; set; }

    public virtual DbSet<SportTable> SportTables { get; set; }

    public virtual DbSet<TypeBetTable> TypeBetTables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySQL("server=localhost; Port=3306; uid=root; pwd=12761276Kain!; database=parserbd;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BetsTable>(entity =>
        {
            entity.HasKey(e => new { e.MatchId, e.BetTypeId }).HasName("PRIMARY");

            entity.ToTable("bets_table");

            entity.HasIndex(e => e.BetTypeId, "bet_type_FK_idx");

            entity.Property(e => e.MatchId).HasColumnName("match_id");
            entity.Property(e => e.BetTypeId).HasColumnName("bet_type_id");
            entity.Property(e => e.BetValue)
                .HasPrecision(10)
                .HasColumnName("bet_value");

            entity.HasOne(d => d.BetType).WithMany(p => p.BetsTables)
                .HasForeignKey(d => d.BetTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("bet_type_FK");

            entity.HasOne(d => d.Match).WithMany(p => p.BetsTables)
                .HasForeignKey(d => d.MatchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("mathc_FK");
        });

        modelBuilder.Entity<ChampTable>(entity =>
        {
            entity.HasKey(e => e.ChampId).HasName("PRIMARY");

            entity.ToTable("champ_table");

            entity.HasIndex(e => e.SportId, "sport_FK_idx");

            entity.Property(e => e.ChampId).HasColumnName("champ_Id");
            entity.Property(e => e.ChampEuName)
                .HasMaxLength(100)
                .HasColumnName("champ_eu_name");
            entity.Property(e => e.ChampRuName)
                .HasMaxLength(100)
                .HasColumnName("champ_ru_name");
            entity.Property(e => e.LocationName)
                .HasMaxLength(45)
                .HasColumnName("location_name");
            entity.Property(e => e.SportId).HasColumnName("sport_ID");

            entity.HasOne(d => d.Sport).WithMany(p => p.ChampTables)
                .HasForeignKey(d => d.SportId)
                .HasConstraintName("sport_FK");
        });

        modelBuilder.Entity<MatchTable>(entity =>
        {
            entity.HasKey(e => e.MatchId).HasName("PRIMARY");

            entity.ToTable("match_table");

            entity.HasIndex(e => e.ChampId, "champ_FK_idx");

            entity.Property(e => e.MatchId).HasColumnName("match_id");
            entity.Property(e => e.ChampId).HasColumnName("champ_id");
            entity.Property(e => e.Opponent1)
                .HasMaxLength(45)
                .HasColumnName("opponent_1");
            entity.Property(e => e.Opponent2)
                .HasMaxLength(45)
                .HasColumnName("opponent_2");

            entity.HasOne(d => d.Champ).WithMany(p => p.MatchTables)
                .HasForeignKey(d => d.ChampId)
                .HasConstraintName("champ_FK");
        });

        modelBuilder.Entity<SportTable>(entity =>
        {
            entity.HasKey(e => e.SportId).HasName("PRIMARY");

            entity.ToTable("sport_table");

            entity.Property(e => e.SportId).HasColumnName("sport_id");
            entity.Property(e => e.SportName)
                .HasMaxLength(45)
                .HasColumnName("sport_name");
        });

        modelBuilder.Entity<TypeBetTable>(entity =>
        {
            entity.HasKey(e => e.BetTypeId).HasName("PRIMARY");

            entity.ToTable("type_bet_table");

            entity.Property(e => e.BetTypeId).HasColumnName("bet_type_id");
            entity.Property(e => e.BetTypeName)
                .HasMaxLength(45)
                .HasColumnName("bet_type_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
