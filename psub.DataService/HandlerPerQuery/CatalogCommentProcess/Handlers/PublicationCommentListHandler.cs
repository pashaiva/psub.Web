using System.Collections.Generic;
using System.Linq;
using Psub.DataAccess.Abstract;
using Psub.DataService.Abstract;
using Psub.DataService.HandlerPerQuery.CatalogCommentProcess.Entities;
using Psub.Domain.Entities;
using Psub.Shared;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;
using AutoMapper;
using NHibernate.Linq;

namespace Psub.DataService.HandlerPerQuery.CatalogCommentProcess.Handlers
{
    public class CatalogCommentListHandler : IQueryHandler<CatalogCommentListQuery, ListCatalogComment>
    {
        private readonly IRepository<CatalogComment> _CatalogCommentRepository;
        private readonly IUserService _userService;

        public CatalogCommentListHandler(IRepository<CatalogComment> CatalogCommentRepository,
            IUserService userService)
        {
            _CatalogCommentRepository = CatalogCommentRepository;
            _userService = userService;
        }

        public ListCatalogComment Handle(CatalogCommentListQuery catalog)
        {
            var commentList = _CatalogCommentRepository.Query().Where(m => m.Catalog.Id == catalog.Id && m.AnswerTo == null).Fetch(m => m.Replys).OrderByDescending(m => m.Id).ToList();
            var result = new ListCatalogComment
            {
                Items = commentList.Any()
                ? Mapper.Map<List<CatalogComment>, List<CatalogCommentListItem>>(commentList.ToList())
                            : new List<CatalogCommentListItem>(),
                Count = commentList.Count()
            };

            foreach (var item in result.Items)
            {
                var comment = item;
                item.UserAvatar = SetAvatar(ref comment);
            }

            return result;
        }

        string SetAvatar(ref CatalogCommentListItem comment)
        {
            if (comment.Replys.Any())
                foreach (var reply in comment.Replys)
                {
                    var reply2 = reply;
                    reply.UserAvatar = SetAvatar(ref reply2);
                }
            var user = _userService.GetUserByGuid(comment.Guid);

            return FormatUtlities.FormatGravatarImageLink(user != null ? user.Email : string.Empty);
        }
    }
}
