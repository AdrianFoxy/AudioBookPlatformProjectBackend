﻿using Re_ABP_Backend.Data.Entities;

namespace Re_ABP_Backend.Data.Interfraces
{
    public interface IRecommendationService
    {
        Task<IReadOnlyList<AudioBook>> GetRecommendationsByPopularity();
        Task<IReadOnlyList<AudioBook>> GetRecommendationsByRating();
        Task<IReadOnlyList<AudioBook>> GetRecentlyWatched(List<int> audioBooksIds);
    }
}
