using System.Collections.Generic;
using System.Linq;
using Psub.DataAccess.Abstract;
using Psub.DataService.Abstract;
using Psub.DataService.HandlerPerQuery.SectionProcess.Entities;
using Psub.Domain.Entities;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;
using AutoMapper;

namespace Psub.DataService.HandlerPerQuery.SectionProcess.Handlers
{
    public class MainSectionListHandler : IQueryHandler<MainSectionListQuery, ListMainSection>
    {
        private readonly IRepository<PublicationMainSection> _mainSectionRepository;
        private readonly IUserService _userService;

        public MainSectionListHandler(IRepository<PublicationMainSection> mainSectionRepository,
            IUserService userService)
        {
            _mainSectionRepository = mainSectionRepository;
            _userService = userService;
        }

        public ListMainSection Handle(MainSectionListQuery query)
        {
            var mainSections = _mainSectionRepository.Query().ToList();

            return new ListMainSection
                {
                    Items = Mapper.Map<List<PublicationMainSection>, List<MainSectionListItem>>(mainSections),
                    IsAdminPublications=_userService.IsAdmin()
                };
        }
    }
}
