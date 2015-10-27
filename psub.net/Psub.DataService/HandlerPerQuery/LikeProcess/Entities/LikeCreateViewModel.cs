using UESPDataManager.DataService.HandlerPerQuery.Abstract;

namespace Psub.DataService.HandlerPerQuery.LikeProcess.Entities
{
    public class LikeCreateQuery : IQuery<LikeCreateResult>
    {
        public int ObjectId { get; set; }
        public string ObjectType { get; set; }
        public bool IsLike { get; set; }
    }

    public class LikeCreateViewModel : IQuery<LikeCreateResult>
    {
    }

    public class LikeCreateResult : QueryResult
    {
        public int Id { get; set; }
        public bool IsLikeBe { get; set; }
    }
}
