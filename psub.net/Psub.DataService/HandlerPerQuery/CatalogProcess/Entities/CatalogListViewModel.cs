using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Psub.Domain.Entities;
using psub.net.Shared;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.CatalogProcess.Entities
{
    public class CatalogListQuery : ListQuery, IQuery<ListCatalog>
    {
        public int CatalogSectionId { get; set; }
    }


    public class ListCatalog : PagingListQueryResult<CatalogListItem>
    {
        public bool IsEditor { get; set; }
    }

    public class CatalogListItem
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = LanguageConstants.Name)]
        public string TitleText { get; set; }

        [Display(Name = LanguageConstants.Preview)]
        [AllowHtml]
        [DataType(DataType.Html)]
        public string TextPreview { get; set; }

        [Display(Name = LanguageConstants.Price)]
        [DataType(DataType.Currency)]
        [UIHint("Price")]
        public decimal Price { get; set; }

        [Display(Name = LanguageConstants.KeyWords)]
        public string Keywords { get; set; }
        
        public string Creator
        {
            get { return FormatUtlities.FormatCreated(string.Format("{0}", UserName), Created); }
        }

        [HiddenInput(DisplayValue = false)]
        public List<File> Files { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string UserName { get; set; }
        [HiddenInput(DisplayValue = false)]
        public DateTime Created { get; set; }
        [HiddenInput(DisplayValue = false)]
        public bool IsView { get; set; }
        [HiddenInput(DisplayValue = false)]
        public bool IsPublic { get; set; }
    }
}
