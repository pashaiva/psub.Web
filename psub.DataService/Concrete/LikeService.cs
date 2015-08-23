using System;
using System.Linq;
using Psub.DataAccess.Abstract;
using Psub.DataService.Abstract;
using Psub.DataService.HandlerPerQuery.LikeProcess.Entities;
using Psub.Domain.Entities;

namespace Psub.DataService.Concrete
{
    public class LikeService : ILikeService
    {
        private readonly IRepository<PublicationCommentLike> _publicationCommentLikeRepository;
        private readonly IRepository<Like> _likeRepository;
        private readonly IRepository<CatalogCommentLike> _catalogCommentLikeRepository;
        private readonly IRepository<CatalogLike> _catalogLikeRepository;
        private readonly IUserService _userService;

        public LikeService(IRepository<Like> likeRepository,
            IUserService userService,
            IRepository<PublicationCommentLike> publicationCommentLikeRepository,
            IRepository<CatalogCommentLike> catalogCommentLikeRepository,
            IRepository<CatalogLike> catalogLikeRepository)
        {
            _likeRepository = likeRepository;
            _userService = userService;
            _publicationCommentLikeRepository = publicationCommentLikeRepository;
            _catalogCommentLikeRepository = catalogCommentLikeRepository;
            _catalogLikeRepository = catalogLikeRepository;
        }

        public LikeCreateResult Save(LikeCreateQuery like)
        {
            var currentUser = _userService.GetCurrentUser();
            var isLikeBe = false;

            if (like.ObjectType == typeof(Publication).Name)
            {
                var likeExist =
         _likeRepository.Query().Where(
             m =>
             m.ObjectId == like.ObjectId && m.ObjectType == like.ObjectType && m.UserGuid == currentUser.UserGuid);

                if (likeExist.Any(m => m.IsLike == !like.IsLike))
                {
                    foreach (var like1 in likeExist.Where(m1 => m1.IsLike == !like.IsLike))
                        _likeRepository.Delete(like1.Id);
                    isLikeBe = true;
                }

                if (likeExist.Any(m => m.IsLike == like.IsLike))
                {
                    foreach (var like1 in likeExist.Where(m1 => m1.IsLike == like.IsLike))
                        _likeRepository.Delete(like1.Id);

                    return new LikeCreateResult { Id = 0, IsLikeBe = isLikeBe };
                }

                var saveLike = new PublicationCommentLike
                {
                    ObjectId = like.ObjectId,
                    ObjectType = like.ObjectType,
                    IsLike = like.IsLike,
                    UserGuid = currentUser.UserGuid,
                    UserName = currentUser.Name,
                    Created = DateTime.Now,
                    Comment = new PublicationComment { Id = like.ObjectId }
                };

                _publicationCommentLikeRepository.SaveOrUpdate(saveLike);

                return new LikeCreateResult { Id = saveLike.Id, IsLikeBe = isLikeBe };
            }

            var likeExist1 =
        _catalogLikeRepository.Query().Where(
            m =>
            m.ObjectId == like.ObjectId && m.ObjectType == like.ObjectType && m.UserGuid == currentUser.UserGuid);

            if (likeExist1.Any(m => m.IsLike == !like.IsLike))
            {
                foreach (var like1 in likeExist1.Where(m1 => m1.IsLike == !like.IsLike))
                    _catalogLikeRepository.Delete(like1.Id);
                isLikeBe = true;
            }

            if (likeExist1.Any(m => m.IsLike == like.IsLike))
            {
                foreach (var like1 in likeExist1.Where(m1 => m1.IsLike == like.IsLike))
                    _catalogLikeRepository.Delete(like1.Id);

                return new LikeCreateResult { Id = 0, IsLikeBe = isLikeBe };
            }

            var saveLike1 = new CatalogCommentLike
            {
                ObjectId = like.ObjectId,
                ObjectType = like.ObjectType,
                IsLike = like.IsLike,
                UserGuid = currentUser.UserGuid,
                UserName = currentUser.Name,
                Created = DateTime.Now,
                Comment = new CatalogComment { Id = like.ObjectId }
            };

            _catalogCommentLikeRepository.SaveOrUpdate(saveLike1);

            return new LikeCreateResult { Id = saveLike1.Id, IsLikeBe = isLikeBe };

        }
    }
}
