using System;
using Psub.DataAccess.Abstract;
using Psub.DataService.Abstract;
using Psub.DataService.HandlerPerQuery.PublicationProcess.Entities;
using Psub.Domain.Entities;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.PublicationProcess.Handlers
{
    public class PublicationCreateHandler : IQueryHandler<PublicationCreateQuery, PublicationCreateResult>
    {
        private readonly IRepository<Publication> _publicationRepository;
        private readonly IUserService _userService;
        private readonly IActionLogService _actionLogService;

        public PublicationCreateHandler(IRepository<Publication> publicationRepository,
            IUserService userService,
            IActionLogService actionLogService)
        {
            _publicationRepository = publicationRepository;
            _userService = userService;
            _actionLogService = actionLogService;
        }

        public PublicationCreateResult Handle(PublicationCreateQuery publication)
        {
            var currentUser = _userService.GetCurrentUser();
            var savePublication = new Publication
            {
                UserGuid = currentUser.UserGuid,
                Name = currentUser.Name,
                CreateDate = DateTime.Now,
                Keywords = publication.Keywords,
                Text = publication.Text,
                TextPreview = publication.TextPreview,
                Title = publication.TitleText
            };

            var id = _publicationRepository.SaveOrUpdate(savePublication);

            _actionLogService.SetActionLog("создал(а) публикацию", id, typeof(Publication).Name);

            return new PublicationCreateResult { Id = id };
        }
    }
}
