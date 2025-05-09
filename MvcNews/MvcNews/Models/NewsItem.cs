using System;
using System.ComponentModel.DataAnnotations;

namespace MvcNews.Models
{
    public class NewsItem
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime TimeStamp { get; set; }

        [Required(ErrorMessage = "Tekst jest wymagany.")]
        [StringLength(140, MinimumLength = 5, ErrorMessage = "Tekst musi mieć od 5 do 140 znaków.")]
        public string Text { get; set; } = string.Empty;

        [ConcurrencyCheck]
        public long? RowVersion { get; set; }
    }
}
