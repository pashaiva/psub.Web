using System;
using Psub.Domain.Abstract;

namespace Psub.Domain.Entities
{
    public class CatalogLike : Base
    {
        public virtual string ObjectType { get; set; }
        public virtual int ObjectId { get; set; }
        public virtual string UserName { get; set; }
        public virtual string UserGuid { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual bool IsLike { get; set; }
    }

    public class CatalogCommentLike : CatalogLike
    {
        public virtual CatalogComment Comment { get; set; }
    }
}
