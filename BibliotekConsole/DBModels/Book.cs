using System;
using System.Collections.Generic;

namespace BibliotekConsole.DBModels
{
    public partial class Book
    {
        public int Id { get; set; }
        public string? Isbn { get; set; }
        public string? Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public bool? EVersion { get; set; }
        public bool? IsBooked { get; set; }
        public int? IsBookedAmount { get; set; }
        public int? AuthorId { get; set; }

        public virtual Author? Author { get; set; }
    }
}
