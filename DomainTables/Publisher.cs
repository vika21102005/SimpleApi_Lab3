using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainTables
{
    public class Publisher
    {
        [Key]
        public int PublisherId { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
