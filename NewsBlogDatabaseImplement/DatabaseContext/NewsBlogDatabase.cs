using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Text;
using NewsBlogDatabaseImplement.Models;

namespace NewsBlogDatabaseImplement.DatabaseContext
{
    public partial class NewsBlogDatabase : DbContext
    {
        const string CONFIG_FILE_ADDRESS = "../../../config.txt";

        public NewsBlogDatabase()
        {
        }

        public NewsBlogDatabase(DbContextOptions<NewsBlogDatabase> options)
            : base(options)
        {
        }

        public virtual DbSet<Articles> Articles { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(GetConnectionString());
                optionsBuilder.UseNpgsql("Host=localhost;Database=NewsBlogDB;Username=yura;Password=1234;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Articles>(entity =>
            {
                entity.HasKey(e => e.Idarticle)
                    .HasName("articlespk");

                entity.ToTable("articles");

                entity.HasIndex(e => e.Date)
                    .HasName("ind_articles_date");

                entity.HasIndex(e => e.Title)
                    .HasName("ind_articles_title");

                entity.Property(e => e.Idarticle)
                    .HasColumnName("idarticle")
                    .HasDefaultValueSql("nextval('articles_seq'::regclass)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.Idtheme).HasColumnName("idtheme");

                entity.Property(e => e.Iduser).HasColumnName("iduser");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnName("text")
                    .HasMaxLength(50);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdthemeNavigation)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.Idtheme)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("categoriesfk");

                entity.HasOne(d => d.IduserNavigation)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.Iduser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userfk");
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.Idtheme)
                    .HasName("categoriespk");

                entity.ToTable("categories");

                entity.HasIndex(e => e.Nametheme)
                    .HasName("ind_categories_nametheme");

                entity.Property(e => e.Idtheme)
                    .HasColumnName("idtheme")
                    .HasDefaultValueSql("nextval('categories_seq'::regclass)");

                entity.Property(e => e.Nametheme)
                    .IsRequired()
                    .HasColumnName("nametheme")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Comments>(entity =>
            {
                entity.HasKey(e => e.Idcomment)
                    .HasName("commentspk");

                entity.ToTable("comments");

                entity.HasIndex(e => e.Comment)
                    .HasName("ind_comments_comment");

                entity.HasIndex(e => e.Date)
                    .HasName("ind_comments_date");

                entity.Property(e => e.Idcomment)
                    .HasColumnName("idcomment")
                    .HasDefaultValueSql("nextval('comments_seq'::regclass)");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasColumnName("comment")
                    .HasMaxLength(50);

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.Idarticle).HasColumnName("idarticle");

                entity.Property(e => e.Iduser).HasColumnName("iduser");

                entity.HasOne(d => d.IdarticleNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.Idarticle)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("articlesfk");

                entity.HasOne(d => d.IduserNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.Iduser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userfk");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Idrole)
                    .HasName("rolepk");

                entity.ToTable("role");

                entity.HasIndex(e => e.Namerole)
                    .HasName("ind_role_name");

                entity.Property(e => e.Idrole)
                    .HasColumnName("idrole")
                    .HasDefaultValueSql("nextval('role_seq'::regclass)");

                entity.Property(e => e.Namerole)
                    .IsRequired()
                    .HasColumnName("namerole")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Iduser)
                    .HasName("userpk");

                entity.ToTable("users");

                entity.HasIndex(e => e.Nickname)
                    .HasName("ind_users_nickname");

                entity.Property(e => e.Iduser)
                    .HasColumnName("iduser")
                    .HasDefaultValueSql("nextval('users_seq'::regclass)");

                entity.Property(e => e.Idrole).HasColumnName("idrole");

                entity.Property(e => e.Nickname)
                    .IsRequired()
                    .HasColumnName("nickname")
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdroleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Idrole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rolefk");
            });

            modelBuilder.HasSequence("articles_seq");

            modelBuilder.HasSequence("categories_seq");

            modelBuilder.HasSequence("comments_seq");

            modelBuilder.HasSequence("role_seq");

            modelBuilder.HasSequence("users_seq");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        private string GetConnectionString()
        {
            if (File.Exists(CONFIG_FILE_ADDRESS))
            {
                if (!CheckConfigFile(CONFIG_FILE_ADDRESS))
                {
                    throw new Exception("Неверный формат файла конфигурации");
                }
                StringBuilder str = new StringBuilder();
                using (StreamReader sr = new StreamReader(CONFIG_FILE_ADDRESS, Encoding.GetEncoding(1251)))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        str.Append(line);
                    }
                }
                Console.WriteLine(str.ToString());
                return str.ToString();
            }
            else
            {
                throw new Exception("Файл конфигурации не найден");
            }
        }
        private bool CheckConfigFile(string fileAddress)
        {
            int count = 0;
            using (StreamReader sr = new StreamReader(fileAddress))
            {
                while (sr.ReadLine() != null)
                {
                    count++;
                }
            }
            return count == 5 ? true : false;
        }
    }
}
