using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainTables
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
