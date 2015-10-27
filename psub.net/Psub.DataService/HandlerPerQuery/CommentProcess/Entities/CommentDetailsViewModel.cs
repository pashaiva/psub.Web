using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Psub.Domain.Entities;
using psub.net.Shared;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.CommentProcess.Entities
{
    public class CommentDetailsQuery : IQuery<List<CommentDetailsViewModel>>
    {
        public int ObjectId { get; set; }
        public string ObjectType { get; set; }
    }

    public class CommentDetailsViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Display(Name = LanguageConstants.Text)]
        public string Text { get; set; }

        [Display(Name = LanguageConstants.Author)]
        public string UserName { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string UserGuid { get; set; }
        public DateTime Created { get; set; }

        [Display(Name = LanguageConstants.ReplyToComment)]
        public Comment ChildrenComment { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ObjectId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ObjectName { get; set; }
    }
}
