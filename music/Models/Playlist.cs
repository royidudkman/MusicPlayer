using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.Models
{
	internal class Playlist
	{
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Song> Songs { get; set; }

        public void AddSongToPlaylist(Song i_Song)
        {
            Songs.Add(i_Song);
        }

        public void RemoveSongFromPlaylist(Song i_Song)
        {
            Songs.Remove(i_Song);
        }
    }
}
