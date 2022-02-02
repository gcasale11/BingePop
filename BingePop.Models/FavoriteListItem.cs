using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingePop.Models
{
    public class FavoriteListItem
    {
        public int ContentId { get; set; }
        public string Title { get; set; }
        public string ContentType { get; set; }
        public string Content { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }

    }
}
