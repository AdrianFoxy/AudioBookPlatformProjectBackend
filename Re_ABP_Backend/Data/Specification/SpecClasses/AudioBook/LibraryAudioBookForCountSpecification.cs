﻿using Microsoft.IdentityModel.Tokens;
using Re_ABP_Backend.Data.Entities;

namespace Re_ABP_Backend.Data.Specification.SpecClasses.AudioBooks
{
    public class LibraryAudioBookForCountSpecification : BaseSpecification<AudioBook>
    {
        public LibraryAudioBookForCountSpecification(ABSpecParams abParams)
            : base(x =>
                (string.IsNullOrEmpty(abParams.Search) || x.Name.ToLower().Contains(abParams.Search)) &&
                (x.Rating >= abParams.LowerRating && x.Rating <= abParams.HighRating) &&

                (abParams.AuthorIds == null || abParams.AuthorIds.Count == 0 || x.Author.Any(x => abParams.AuthorIds.Contains(x.Id))) &&
                (abParams.GenreIds == null || abParams.GenreIds.Count == 0 || x.Genre.Any(x => abParams.GenreIds.Contains(x.Id))) &&           
                (abParams.BookSeriesIds == null || abParams.BookSeriesIds.Count == 0 || abParams.BookSeriesIds.Contains(x.BookSeriesId)) &&
                (abParams.BookLanguageIds == null || abParams.BookLanguageIds.Count == 0 || abParams.BookLanguageIds.Contains(x.BookLanguageId)) &&
                (abParams.NarratorIds == null || abParams.NarratorIds.Count == 0 || abParams.NarratorIds.Contains(x.NarratorId)) &&

                (abParams.ExceptAuthorIds == null || abParams.ExceptAuthorIds.Count == 0 || !x.Author.Any(a => abParams.ExceptAuthorIds.Contains(a.Id))) &&
                (abParams.ExceptGenreIds == null || abParams.ExceptGenreIds.Count == 0 || !x.Genre.Any(a => abParams.ExceptGenreIds.Contains(a.Id))) &&
                (abParams.ExceptBookSeriesIds == null || !abParams.ExceptBookSeriesIds.Contains(x.BookSeriesId)) &&
                (abParams.ExceptBookLanguageIds == null || !abParams.ExceptBookLanguageIds.Contains(x.BookLanguageId)) &&
                (abParams.ExceptNarratorIds == null || !abParams.ExceptNarratorIds.Contains(x.NarratorId))

            )
        {

        }

    }
}
