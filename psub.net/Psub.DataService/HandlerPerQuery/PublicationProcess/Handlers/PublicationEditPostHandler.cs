using Psub.DataAccess.Abstract;
using Psub.DataService.Abstract;
using Psub.DataService.HandlerPerQuery.PublicationProcess.Entities;
using Psub.Domain.Entities;
using psub.net.Shared;
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

        public PublicationEditViewModel Handle(PublicationEditPostQuery catalog)
        {
            var currentUser = _userService.GetCurrentUser();

            if (!_userService.IsAdmin())
                return null;

            var lastPublication = _publicationRepository.Get(catalog.Id);

            lastPublication.Text = catalog.Text;
            lastPublication.TextPreview = catalog.TextPreview;
            lastPublication.TitleText = catalog.TitleText;
            lastPublication.Keywords = catalog.Keywords;
            lastPublication.IsPublic = catalog.IsPublic;
            lastPublication.Section = new Section { Id = catalog.Section.Id };

            _publicationRepository.SaveOrUpdate(lastPublication);

            _actionLogService.SetActionLog("редактировал(а) публикацию", lastPublication.Id, typeof(Publication).Name,
                FormatUtlities.FormatVirtualDetailsUrl(typeof(Publication).Name, lastPublication.Id, lastPublication.TitleText));

            return Mapper.Map<Publication, PublicationEditViewModel>(lastPublication);
        }
    }
}
