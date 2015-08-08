using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Psub.Shared;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.PublicationProcess.Entities
{
    public class PublicationDetailsQuery : IQuery<PublicationDetailsViewModel>
    {
        public int Id { get; set; }
    }

    public class PublicationDetailsViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = LanguageConstants.Name)]
        public string TitleText { get; set; }

        [Display(Name = LanguageConstants.Content)]
        [AllowHtml]
        [DataType(DataType.Html)]
        public string Text { get; set; }

        [Display(Name = LanguageConstants.Preview)]
        [AllowHtml]
        [DataType(DataType.Html)]
        public string TextPreview { get; set; }

        [Display(Name = LanguageConstants.KeyWords)]
        public string Keywords { get; set; }
        
        public string Creator
        {
            get { return string.Format("{0} {1}", UserName, Created); }
        }
        [HiddenInput(DisplayValue = false)]
        public string UserName { get; set; }
        [HiddenInput(DisplayValue = false)]
        public DateTime Created { get; set; }
        [HiddenInput(DisplayValue = false)]
        public bool IsEditor { get; set; }
    }
}
