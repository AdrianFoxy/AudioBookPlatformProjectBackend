﻿using Re_ABP_Backend.Data.Entities.Base;

namespace Re_ABP_Backend.Data.Entities
{
    public class BookSeries : BaseEntity
    {
        public string Name { get; set; }
        public string EnName { get; set; }
    }
}