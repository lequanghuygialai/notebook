using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Notebook
{
    public class NoteBookDbContext : DbContext
    {
        public NoteBookDbContext(DbContextOptions<NoteBookDbContext> options)
        : base(options)
        {
        }

        public DbSet<Note> Notes { get; set; }
    }

    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string NoteContent { get; set; }
    }
}
