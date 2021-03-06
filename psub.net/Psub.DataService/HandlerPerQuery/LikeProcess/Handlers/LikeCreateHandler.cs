﻿using Psub.DataService.Abstract;
using Psub.DataService.HandlerPerQuery.LikeProcess.Entities;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.LikeProcess.Handlers
{
    public class LikeCreateHandler : IQueryHandler<LikeCreateQuery, LikeCreateResult>
    {
        private readonly ILikeService _likeService;

        public LikeCreateHandler(ILikeService likeService)
        {
            _likeService = likeService;
        }

        public LikeCreateResult Handle(LikeCreateQuery catalog)
        {
            return _likeService.Save(catalog);
        }
    }
}
