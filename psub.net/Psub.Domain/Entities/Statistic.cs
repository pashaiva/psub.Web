using System;
using Psub.Domain.Abstract;

namespace Psub.Domain.Entities
{
    public class Statistic : Base
    {
        public virtual string IP { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Url { get; set; }
        public virtual string UrlReferrer { get; set; }
        public virtual DateTime Date { get; set; }
    }
}
