using Psub.DataService.HandlerPerQuery.LikeProcess.Entities;

namespace Psub.DataService.Abstract
{
    public interface ILikeService
    {
        LikeCreateResult Save(LikeCreateQuery like);
    }
}
