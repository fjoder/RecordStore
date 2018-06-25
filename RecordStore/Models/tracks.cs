using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace RecordStore.Models
{
    public partial class tracks
    {
        public long TrackId { get; set; }
        public string Name { get; set; }
        public long? AlbumId { get; set; }
        public long MediaTypeId { get; set; }
        public long? GenreId { get; set; }
        public string Composer { get; set; }
        public long Milliseconds { get; set; }
        public long? Bytes { get; set; }
        public string UnitPrice { get; set; }

        public albums album { get; set; }
        public genres genre { get; set; }
        public media_types media_type { get; set; }
        //public UserPlaylist UserPlaylist { get; set; }

    }
}
