﻿using Re_ABP_Backend.Data.Entities.Base;

namespace Re_ABP_Backend.Data.Entities
{
    public class LibraryStatus : BaseEntity
    {
        public string Name { get; set; }
        public string EnName { get; set; }
        public List<UserLibrary> UserLibrary { get; set; }
    }
}
