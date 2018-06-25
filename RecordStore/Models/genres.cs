using System;
using System.Collections.Generic;

namespace RecordStore.Models
{
    public partial class genres
    {
        public genres()
        {
            tracks = new HashSet<tracks>();
        }

        public long GenreId { get; set; }
        public string Name { get; set; }

        public ICollection<tracks> tracks { get; set; }
    }
}
