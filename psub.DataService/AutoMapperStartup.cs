using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Psub.DataService.HandlerPerQuery.CommentProcess.Entities;
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

            #endregion
        }
    }
}
