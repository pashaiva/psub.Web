using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Psub.Shared;
using UESPDataManager.DataService.HandlerPerQuery.Abstract;
using System.Data;

namespace Psub.DataService.HandlerPerQuery.PublicationProcess.Entities
{
    public class HomeIndexQuery : IQuery<HomeIndexViewModel>
    {
        public int Id { get; set; }
    }

    public class HomeIndexViewModel
    {
        public System.Collections.Generic.IList<PublicationDetailsViewModel> Publications { get; set; }
    }
}
