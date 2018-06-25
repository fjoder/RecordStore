using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RecordStore.Models
{
    public class UserPlaylist
    {
        public UserPlaylist()
        {
            tracks = new HashSet<tracks>();
        }

        public long UserPlaylistId { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }

        public ICollection<tracks> tracks { get; set; }
    }
}
