using System;
using Psub.DataAccess.Abstract;
using Psub.DataService.Abstract;
using Psub.DataService.HandlerPerQuery.PublicationProcess.Entities;
using Psub.Domain.Entities;
using Psub.Shared;
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
                UserName = currentUser.Name,
                Created = DateTime.Now,
                Keywords = publication.Keywords,
                Text = publication.Text,
                TextPreview = publication.TextPreview,
                TitleText = publication.TitleText,
                IsPublic = publication.IsPublic,
                Section = new Section { Id = publication.Section.Id }
            };

            var id = _publicationRepository.SaveOrUpdate(savePublication);

            _actionLogService.SetActionLog("создал(а) публикацию", id, typeof(Publication).Name,
                FormatUtlities.FormatVirtualDetailsUrl(typeof(Publication).Name, savePublication.Id, savePublication.TitleText));

            return new PublicationCreateResult { Id = id };
        }
    }
}
