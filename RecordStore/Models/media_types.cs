using System;
using System.Collections.Generic;

namespace RecordStore.Models
{
    public partial class media_types
    {
        public media_types()
        {
            tracks = new HashSet<tracks>();
        }

        public long MediaTypeId { get; set; }
        public string Name { get; set; }

        public ICollection<tracks> tracks { get; set; }
    }
}
