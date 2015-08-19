using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Psub.DataService.CommonViewModels;
using Psub.DataService.HandlerPerQuery.CatalogCommentProcess.Entities;
using Psub.DataService.HandlerPerQuery.CatalogProcess.Entities;
using Psub.DataService.HandlerPerQuery.CommentProcess.Entities;
using Psub.DataService.HandlerPerQuery.PublicationCommentProcess.Entities;
using Psub.DataService.HandlerPerQuery.PublicationProcess.Entities;
using Psub.DataService.HandlerPerQuery.SectionProcess.Entities;
using Psub.Domain.Entities;
using AutoMapper;

namespace Psub.DataService
{
    public static class AutoMapperStartup
    {
        public static void Configure()
        {
            #region comment

            Mapper.CreateMap<CommentDetailsViewModel, Comment>();
            Mapper.CreateMap<List<CommentDetailsViewModel>, List<Comment>>();
            //.ForMember(dest => dest.ApproverNames, opt => opt.MapFrom(src => src.Approvers.Name));

            //Mapper.CreateMap<PublicationComment, PublicationCommentListItem>()
            //        .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Text))
            //        .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.UserName))
            //        .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Created.ToString("dd.MM.yyyy, H:mm:ss")))
            //        .ForMember(dest => dest.Replys, opt => opt.MapFrom(src => src.Replys.OrderByDescending(m => m.Id)))
            //        .ForMember(dest => dest.LikeCount, opt => opt.MapFrom(src => src.Likes != null && src.Likes.Any() ? src.Likes.Count(like => like.IsLike) : 0))
            //        .ForMember(dest => dest.DisLikeCount, opt => opt.MapFrom(src => src.Likes != null && src.Likes.Any() ? src.Likes.Count(like => !like.IsLike) : 0))
            //        .ForMember(dest => dest.DisLikeUsers, opt => opt.MapFrom(src => src.Likes != null && src.Likes.Any() ? src.Likes.Where(like => !like.IsLike).Select(like => like.UserName).Take(5).ToList().Aggregate("", (current, item) => current + string.Format("<li><nobr>{0}</nobr></li>", item)) : string.Empty))
            //        .ForMember(dest => dest.LikeUsers, opt => opt.MapFrom(src => src.Likes != null && src.Likes.Any() ? src.Likes.Where(like => like.IsLike).Select(like => like.UserName).Take(5).ToList().Aggregate("", (current, item) => current + string.Format("<li><nobr>{0}</nobr></li>", item)) : string.Empty));
            
            #endregion

            #region publication

            Mapper.CreateMap<Publication, PublicationDetailsViewModel>();
            Mapper.CreateMap<Publication, PublicationEditViewModel>()
                .ForMember(dest => dest.Section, opt => opt.MapFrom(src => new DropDownSelectorViewModel
                {
                    Id = src.Section.Id
                }));
            Mapper.CreateMap<Publication, PublicationListItem>();
            Mapper.CreateMap<PublicationComment, PublicationCommentListItem>()
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Created.ToString("dd.MM.yyyy, H:mm:ss")))
                .ForMember(dest => dest.Replys, opt => opt.MapFrom(src => src.Replys.OrderByDescending(m => m.Id)))
                .ForMember(dest => dest.LikeCount, opt => opt.MapFrom(src => src.Likes != null && src.Likes.Any() ? src.Likes.Count(like => like.IsLike) : 0))
                .ForMember(dest => dest.DisLikeCount, opt => opt.MapFrom(src => src.Likes != null && src.Likes.Any() ? src.Likes.Count(like => !like.IsLike) : 0))
                .ForMember(dest => dest.DisLikeUsers, opt => opt.MapFrom(src => src.Likes != null && src.Likes.Any() ? src.Likes.Where(like => !like.IsLike).Select(like => like.UserName).Take(5).ToList().Aggregate("", (current, item) => current + string.Format("<li><nobr>{0}</nobr></li>", item)) : string.Empty))
                .ForMember(dest => dest.LikeUsers, opt => opt.MapFrom(src => src.Likes != null && src.Likes.Any() ? src.Likes.Where(like => like.IsLike).Select(like => like.UserName).Take(5).ToList().Aggregate("", (current, item) => current + string.Format("<li><nobr>{0}</nobr></li>", item)) : string.Empty));

            Mapper.CreateMap<MainSection, MainSectionListItem>();

            #endregion
            
            #region catalog

            Mapper.CreateMap<Catalog, CatalogDetailsViewModel>();
            Mapper.CreateMap<Catalog, CatalogEditViewModel>()
                .ForMember(dest => dest.Section, opt => opt.MapFrom(src => new DropDownSelectorViewModel
                {
                    Id = src.Section.Id
                }));
            Mapper.CreateMap<Catalog, CatalogListItem>();
            Mapper.CreateMap<CatalogComment, CatalogCommentListItem>()
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Created.ToString("dd.MM.yyyy, H:mm:ss")))
                .ForMember(dest => dest.Replys, opt => opt.Ignore())//MapFrom(src => src.Replys.OrderByDescending(m => m.Id)))
                .ForMember(dest => dest.LikeCount, opt => opt.MapFrom(src => src.Likes != null && src.Likes.Any() ? src.Likes.Count(like => like.IsLike) : 0))
                .ForMember(dest => dest.DisLikeCount, opt => opt.MapFrom(src => src.Likes != null && src.Likes.Any() ? src.Likes.Count(like => !like.IsLike) : 0))
                .ForMember(dest => dest.DisLikeUsers, opt => opt.MapFrom(src => src.Likes != null && src.Likes.Any() ? src.Likes.Where(like => !like.IsLike).Select(like => like.UserName).Take(5).ToList().Aggregate("", (current, item) => current + string.Format("<li><nobr>{0}</nobr></li>", item)) : string.Empty))
                .ForMember(dest => dest.LikeUsers, opt => opt.MapFrom(src => src.Likes != null && src.Likes.Any() ? src.Likes.Where(like => like.IsLike).Select(like => like.UserName).Take(5).ToList().Aggregate("", (current, item) => current + string.Format("<li><nobr>{0}</nobr></li>", item)) : string.Empty));

            Mapper.CreateMap<MainSection, MainSectionListItem>();

            #endregion
        }
    }
}
