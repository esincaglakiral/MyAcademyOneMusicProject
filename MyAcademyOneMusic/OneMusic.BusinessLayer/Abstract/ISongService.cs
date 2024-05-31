﻿using OneMusic.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.BusinessLayer.Abstract
{
    public interface ISongService : IGenericService<Song>
    {
        public List<Song> TGetSongsWithAlbumAndArtist();
        public List<Song> TGetSongsWithAlbumByUserId(int id); 
        public List<Song> TGetSongsByAlbumId(int id);
        List<Song> TGetSongWithAlbum();

    }
}
