using System.Collections.Generic;
using System.Linq;
using Psub.DataAccess.Abstract;
using Psub.DataService.Abstract;
using Psub.DataService.HandlerPerQuery.PublicationCommentProcess.Entities;
using Psub.Domain.Entities;
using Psub.Shared;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;
using AutoMapper;
using NHibernate.Linq;

namespace Psub.DataService.HandlerPerQuery.PublicationCommentProcess.Handlers
{
    public class PublicationCommentListHandler : IQueryHandler<PublicationCommentListQuery, ListPublicationComment>
    {
        private readonly IRepository<PublicationComment> _publicationCommentRepository;
        private readonly IUserService _userService;

        public PublicationCommentListHandler(IRepository<PublicationComment> publicationCommentRepository,
            IUserService userService)
        {
            _publicationCommentRepository = publicationCommentRepository;
            _userService = userService;
        }

        public ListPublicationComment Handle(PublicationCommentListQuery query)
        {
            var commentList = _publicationCommentRepository.Query().Where(m => m.Publication.Id == query.Id && m.AnswerTo == null).Fetch(m => m.Replys).OrderByDescending(m => m.Id).ToList();
            var result = new ListPublicationComment
            {
                Items = commentList.Any()
                ? Mapper.Map<List<PublicationComment>, List<PublicationCommentListItem>>(commentList.ToList())
                            : new List<PublicationCommentListItem>(),
                Count = commentList.Count()
            };

            foreach (var item in result.Items)
            {
                var comment = item;
                item.UserAvatar = SetAvatar(ref comment);
            }

            return result;
        }

        string SetAvatar(ref PublicationCommentListItem comment)
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
