using ElosztottLabor.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElosztottLabor.Data
{
    public class HEMDbContext : DbContext
    {
        public HEMDbContext(DbContextOptions<HEMDbContext> options) : base(options) { }

        public DbSet<QuestionForm> QuestionForms { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<FreeTextQuestion> FreeTextQuestions { get; set; }
        public DbSet<MultipleChoiceQuestion> MultipleChoiceQuestions { get; set; }
        public DbSet<TrueOrFalseQuestion> TrueOrFalseQuestions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>().ToTable("Questions");
        }
    }
}
