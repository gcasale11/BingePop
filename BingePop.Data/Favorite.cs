using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingePop.Data
{
    public class Favorite
    {
        [Key]
        public int ContentId { get; set; }
        [Required]
        public string ContentType { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public Guid OwnerId { get; set; }
        [Required]
        public DateTimeOffset CreatedUtc { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
