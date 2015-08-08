using Psub.DataAccess.Abstract;
using Psub.DataService.Abstract;
using Psub.DataService.HandlerPerQuery.PublicationProcess.Entities;
using Psub.Domain.Entities;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;
using AutoMapper;

namespace Psub.DataService.HandlerPerQuery.PublicationProcess.Handlers
{
    public class PublicationEditPostHandler : IQueryHandler<PublicationEditPostQuery, PublicationEditViewModel>
    {
        private readonly IUserService _userService;
        private readonly IRepository<Publication> _publicationRepository;
        private readonly IActionLogService _actionLogService;

        public PublicationEditPostHandler(IUserService userService,
            IRepository<Publication> publicationRepository,
            IActionLogService actionLogService)
        {
            _userService = userService;
            _publicationRepository = publicationRepository;
            _actionLogService = actionLogService;
        }

        public PublicationEditViewModel Handle(PublicationEditPostQuery publication)
        {
            var currentUser = _userService.GetCurrentUser();

            var lastPublication = _publicationRepository.Get(publication.Id);

            lastPublication.Text = publication.Text;
            lastPublication.TextPreview = publication.TextPreview;
            lastPublication.Title = publication.TitleText;
            lastPublication.Keywords = publication.Keywords;

            _publicationRepository.SaveOrUpdate(lastPublication);

            _actionLogService.SetActionLog("редактировал(а) публикацию", lastPublication.Id, typeof(Publication).Name);

            return Mapper.Map<Publication, PublicationEditViewModel>(lastPublication);
        }
    }
}
