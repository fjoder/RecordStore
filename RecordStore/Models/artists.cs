using System;
using System.Collections.Generic;

namespace RecordStore.Models
{
    public partial class artists
    {
        public artists()
        {
            Albums = new HashSet<albums>();
        }

        public long ArtistId { get; set; }
        public string Name { get; set; }

        public ICollection<albums> Albums { get; set; }
    }
}
