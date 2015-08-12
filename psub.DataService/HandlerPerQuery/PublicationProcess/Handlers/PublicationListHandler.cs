using System.Collections.Generic;
using System.Linq;
using Psub.DataAccess.Abstract;
using Psub.DataService.Abstract;
using Psub.DataService.HandlerPerQuery.PublicationProcess.Entities;
using Psub.Domain.Entities;
using Psub.Shared;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;
using AutoMapper;

namespace Psub.DataService.HandlerPerQuery.PublicationProcess.Handlers
{
    public class PublicationListHandler : IQueryHandler<PublicationListQuery, ListPublication>
    {
        private readonly IRepository<Publication> _publicationRepository;
        private readonly IUserService _userService;

        public PublicationListHandler(IRepository<Publication> publicationRepository,
            IUserService userService)
        {
            _publicationRepository = publicationRepository;
            _userService = userService;
        }

        public ListPublication Handle(PublicationListQuery catalog)
        {
            var pageSize = catalog.PageSize > 0 ? catalog.PageSize : ConfigData.PageSize;
            var publications = _publicationRepository.Query().Where(m => catalog.PublicationSectionId == 0 || (m.Section != null && m.Section.Id == catalog.PublicationSectionId)).OrderByDescending(m => m.Id);
            var currentUser = _userService.GetCurrentUser();
            var selectPublications = publications.Skip((catalog.Page - 1) * pageSize).Take(pageSize).ToList();
            var returnPublication = new List<PublicationListItem>();

            foreach (var publicationListItem in selectPublications)
            {
                var returnPublicationItem = Mapper.Map<Publication, PublicationListItem>(publicationListItem);
                returnPublicationItem.IsView = true;
                returnPublication.Add(returnPublicationItem);
            }

            var result = new ListPublication
            {
                Items = returnPublication,
                Count = publications.Count(),
                PageNumber = catalog.Page,
                PageSize = pageSize,
                IsEditor = _userService.IsAdmin()
            };

            return result;
        }
    }
}
