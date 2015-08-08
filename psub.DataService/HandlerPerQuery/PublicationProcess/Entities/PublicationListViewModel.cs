using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Psub.Shared;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.PublicationProcess.Entities
{
    public class PublicationListQuery :ListQuery, IQuery<ListPublication>
    {
        public int SectionId { get; set; }
    }


    public class ListPublication : PagingListQueryResult<PublicationListItem>
    {
        public bool IsEditor { get; set; }
    }

    public class PublicationListItem
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = LanguageConstants.Name)]
        public string TitleText { get; set; }

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
        public bool IsView { get; set; }
    }
}
