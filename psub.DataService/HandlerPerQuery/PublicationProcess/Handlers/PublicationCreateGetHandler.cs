using System.Collections.Specialized;
using System.Linq;
using Psub.DataAccess.Abstract;
using Psub.DataService.CommonViewModels;
using Psub.DataService.HandlerPerQuery.PublicationProcess.Entities;
using Psub.Domain.Entities;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.PublicationProcess.Handlers
{
    public class PPublicationCreateGetHandler : IQueryHandler<PublicationCreateGetQuery, PublicationCreateGetViewModel>
    {
        private readonly IRepository<PublicationSection> _sectionRepository;

        public PPublicationCreateGetHandler(IRepository<PublicationSection> sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public PublicationCreateGetViewModel Handle(PublicationCreateGetQuery publication)
        {
            return new PublicationCreateGetViewModel
                {
                    Section = new DropDownSelectorViewModel
                        {
                            Items = _sectionRepository.Query().Select(m => new DropDownItem
                            {
                                Id = m.Id,
                                Name = m.Name
                            }).ToList()
                        }
                };
        }
    }
}
