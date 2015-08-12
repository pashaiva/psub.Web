using System.Collections.Generic;
using System.Linq;
using Psub.DataAccess.Abstract;
using Psub.DataService.Abstract;
using Psub.DataService.HandlerPerQuery.CommentProcess.Entities;
using Psub.Domain.Entities;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;
using AutoMapper;

namespace Psub.DataService.HandlerPerQuery.CommentProcess.Handlers
{
    public class AlbumDetailsHandler : IQueryHandler<CommentDetailsQuery, List<CommentDetailsViewModel>>
    {
        private readonly IUserService _userService;
        private readonly IRepository<Comment> _commentRepository;

        public AlbumDetailsHandler(IUserService userService,
            IRepository<Comment> commentRepository)
        {
            _userService = userService;
            _commentRepository = commentRepository;
        }

        public List<CommentDetailsViewModel> Handle(CommentDetailsQuery catalog)
        {
            var currentDocumentTemp = _commentRepository.Query().Where(m => m.ObjectId == catalog.ObjectId && m.ObjectName==catalog.ObjectType);
            if (!currentDocumentTemp.Any())
                return null;

            var currentDocument = currentDocumentTemp.ToList();

            var result = Mapper.Map<List<Comment>, List<CommentDetailsViewModel>>(currentDocument);
           
            return result;
        }
    }
}
