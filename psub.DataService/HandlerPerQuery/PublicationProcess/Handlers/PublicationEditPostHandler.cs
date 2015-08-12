using Psub.DataAccess.Abstract;
using Psub.DataService.Abstract;
using Psub.DataService.HandlerPerQuery.PublicationProcess.Entities;
using Psub.Domain.Entities;
using Psub.Shared;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;
using AutoMapper;

namespace Psub.DataService.HandlerPerQuery.PublicationProcess.Handlers
{
    public class PublicationEditPostHandler : IQueryHandler<PublicationEditPostQuery, PublicationEditViewModel>
    {
        private readonly IUserService _userService;
        private readonly IRepository<Publication> _publicationRepository;
        private readonly IActionLogService _actionLogService;
        private readonly IRepository<Section> _sectionRepository;

        public PublicationEditPostHandler(IUserService userService,
            IRepository<Publication> publicationRepository,
            IActionLogService actionLogService,
            IRepository<Section> sectionRepository)
        {
            _userService = userService;
            _publicationRepository = publicationRepository;
            _actionLogService = actionLogService;
            _sectionRepository = sectionRepository;
        }

        public PublicationEditViewModel Handle(PublicationEditPostQuery publication)
        {
            var currentUser = _userService.GetCurrentUser();

            if (!_userService.IsAdmin())
                return null;

            var lastPublication = _publicationRepository.Get(publication.Id);

            lastPublication.Text = publication.Text;
            lastPublication.TextPreview = publication.TextPreview;
            lastPublication.TitleText = publication.TitleText;
            lastPublication.Keywords = publication.Keywords;
            lastPublication.IsPublic = publication.IsPublic;
            lastPublication.Section = new Section { Id = publication.Section.Id };

            _publicationRepository.SaveOrUpdate(lastPublication);

            _actionLogService.SetActionLog("редактировал(а) публикацию", lastPublication.Id, typeof(Publication).Name,
                FormatUtlities.FormatVirtualDetailsUrl(typeof(Publication).Name, lastPublication.Id, lastPublication.TitleText));

            return Mapper.Map<Publication, PublicationEditViewModel>(lastPublication);
        }
    }
}
