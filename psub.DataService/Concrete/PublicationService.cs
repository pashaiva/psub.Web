using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Psub.DTO.Entities;
using Psub.DataAccess.Abstract;
using Psub.DataService.Abstract;
using Psub.Domain.Entities;
using Psub.Shared.Abstract;

namespace Psub.DataService.Concrete
{
    public class PublicationService : IPublicationService
    {
        private readonly IRepository<Publication> _publicationRepository;
        private readonly IUserService _userService;
        private readonly IPublicationMainSectionService _publicationMainSectionService;
        private readonly IPublicationSectionService _publicationSectionService;
        private readonly IFileService _fileService;

        public PublicationService(IRepository<Publication> publicationRepository,
                IUserService userService,
                IPublicationMainSectionService publicationMainSectionService,
                IPublicationSectionService publicationSectionService,
            IFileService fileService)
        {
            _publicationRepository = publicationRepository;
            _userService = userService;
            _publicationMainSectionService = publicationMainSectionService;
            _publicationSectionService = publicationSectionService;
            _fileService = fileService;
        }

        //public IEnumerable<PublicationDTO> GetPublicationList(int publicationSectionId, int publicationMainSectionId)
        //{
        //    return _publicationRepository.Query()
        //        .Where(publication => (publication.PublicationMainSection.Id == publicationMainSectionId || publicationSectionId <= 0) && (publication.PublicationSection.Id == publicationSectionId || publicationSectionId <= 0))
        //        .Select(m => new PublicationDTO
        //        {
        //            Id = m.Id,
        //            Name = m.Name,
        //            UserGuid = m.UserGuid,
        //            Title = m.Title,
        //            Text = m.Text,
        //            CreateDate = m.CreateDate,
        //            EditDate = m.EditDate,
        //            PublicationMainSectionName = m.PublicationMainSection.Name,
        //            PublicationSectionName = m.PublicationSection.Name
        //        });
        //}

        //public IEnumerable<PublicationDTO> GetPublicationListTop(int count = 10)
        //{
        //    return _publicationRepository.Query().Select(m => new PublicationDTO
        //    {
        //        Id = m.Id,
        //        Name = m.Name,
        //        UserGuid = m.UserGuid,
        //        Title = m.Title,
        //        Text = m.Text,
        //        CreateDate = m.CreateDate,
        //        EditDate = m.EditDate,
        //        PublicationMainSectionName = m.PublicationMainSection.Name,
        //        PublicationSectionName = m.PublicationSection.Name
        //    }).OrderByDescending(m => m.Id).Take(count);
        //}

        //public int SaveOrUpdate(PublicationDTO publication)
        //{
        //    var savePublicarion = publication.Id == 0 ? new Publication() : _publicationRepository.Get(publication.Id);

        //    if (publication.Id > 0)
        //    {
        //        savePublicarion.Id = publication.Id;
        //        savePublicarion.EditDate = DateTime.Now;
        //    }
        //    else
        //    {
        //        savePublicarion.CreateDate = DateTime.Now;
        //    }
        //    savePublicarion.Name = _userService.GetCurrentUser().NickName;
        //    savePublicarion.UserGuid = _userService.GetCurrentUser().UserGuid;
        //    savePublicarion.Title = publication.Title;
        //    savePublicarion.Text = publication.Text;
        //    savePublicarion.PublicationMainSection = new PublicationMainSection { Id = publication.PublicationMainSectionId };
        //    savePublicarion.PublicationSection = new PublicationSection { Id = publication.PublicationSectionId };
        //    savePublicarion.Keywords = publication.Keywords;
        //    savePublicarion.Guid = !string.IsNullOrEmpty(publication.Guid) ? publication.Guid : Guid.NewGuid().ToString();

        //    return _publicationRepository.SaveOrUpdate(savePublicarion);
        //}

        //public PublicationDTO GetPublicationById(int id)
        //{
        //    var publicationMainSectionDTOList = _publicationMainSectionService.GetPublicationSectionDTOList();
        //    var publicationSectionDTOList = _publicationSectionService.GetPublicationSectionDTOList();

        //    if (id > 0)
        //    {
        //        var publication = _publicationRepository.Get(id);

        //        return new PublicationDTO
        //            {
        //                Id = publication.Id,
        //                Name = publication.Name,
        //                Title = publication.Title,
        //                Text = publication.Text,
        //                Keywords = publication.Keywords,
        //                Guid = !string.IsNullOrEmpty(publication.Guid) ? publication.Guid : Guid.NewGuid().ToString(),
        //                CreateDate = publication.CreateDate,
        //                EditDate = publication.EditDate,
        //                PublicationMainSectionId = publication.PublicationMainSection.Id,
        //                PublicationSectionId = publication.PublicationSection.Id,
        //                PublicationMainSection = publicationMainSectionDTOList.ToList(),
        //                PublicationSection = publicationSectionDTOList.Where(m => m.PublicationMainSection.Id == publication.PublicationMainSection.Id).ToList(),
        //                PublicationMainSectionName = publication.PublicationMainSection.Name,
        //                PublicationSectionName = publication.PublicationSection.Name,
        //                Files = _fileService.GetFileList(typeof(Publication).Name, publication.Id),
        //            };
        //    }
        //    return new PublicationDTO
        //        {
        //            Guid = Guid.NewGuid().ToString(),
        //            PublicationMainSection = publicationMainSectionDTOList.ToList(),
        //            PublicationSection = publicationSectionDTOList.Where(m => m.Id == publicationMainSectionDTOList.First().Id).ToList()
        //        };
        //}

        //public void DeletePublication(int id)
        //{
        //    var publicationId = _publicationRepository.Get(id).Id;

        //    _publicationRepository.Delete(id);

        //    foreach (var file in _fileService.GetFileList(typeof(Publication).Name, publicationId))
        //    {
        //        _fileService.DeleteFile(typeof(Publication).Name, id, file.Guid);
        //    }

        //}
        public IEnumerable<PublicationDTO> GetPublicationList(int publicationSectionId, int publicationMainSectionId)
        {
            throw new NotImplementedException();
        }

        public int SaveOrUpdate(PublicationDTO publication)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PublicationDTO> GetPublicationListTop(int count = 10)
        {
            throw new NotImplementedException();
        }

        public PublicationDTO GetPublicationById(int id)
        {
            throw new NotImplementedException();
        }

        public void DeletePublication(int id)
        {
            throw new NotImplementedException();
        }
    }
}
