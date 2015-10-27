using Psub.DataAccess.Abstract;
using Psub.DataService.Abstract;
using Psub.DataService.CommonViewModels;
using Psub.DataService.HandlerPerQuery.PublicationProcess.Entities;
using Psub.Domain.Entities;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;
using System.Linq;
using AutoMapper;

namespace Psub.DataService.HandlerPerQuery.PublicationProcess.Handlers
{
    public class PublicationEditGetHandler : IQueryHandler<PublicationEditGetQuery, PublicationEditViewModel>
    {
        private readonly IRepository<Publication> _publicationRepository;
        private readonly IUserService _userService;
        private readonly IRepository<Section> _sectionRepository;

        public PublicationEditGetHandler(IRepository<Publication> publicationRepository,
            IUserService userService,
            IRepository<Section> sectionRepository)
        {
            _publicationRepository = publicationRepository;
            _userService = userService;
            _sectionRepository = sectionRepository;
        }

        public PublicationEditViewModel Handle(PublicationEditGetQuery catalog)
        {
            var currentDocument = _publicationRepository.Get(catalog.Id);

            if (currentDocument == null || !_userService.IsAdmin())
                return null;

            var result = Mapper.Map<Publication, PublicationEditViewModel>(currentDocument);

            result.Section = new DropDownSelectorViewModel
                {
                    Items = _sectionRepository.Query().Select(m => new DropDownItem()
                {
                    Id = m.Id,
                    Name = m.Name
                }).ToList(),
                    Id = result.Section.Id
                };

            return result;
        }
    }
}
