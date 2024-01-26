﻿using Re_ABP_Backend.Data.Entities;
using Re_ABP_Backend.Data.Specification.Params;

namespace Re_ABP_Backend.Data.Specification.SpecClasses.AdminAuthor
{
    public class AuthorCountSpecification : BaseSpecification<Author>
    {
        public AuthorCountSpecification(PagAndSearchParams abParams)
            : base(x => (string.IsNullOrEmpty(abParams.Search) || x.Name.ToLower().Contains(abParams.Search)))
        {

        }
    }
}