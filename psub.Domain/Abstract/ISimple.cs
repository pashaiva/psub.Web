namespace Psub.Domain.Abstract
{
    public class Base : ISimple
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
    }
    public interface ISimple
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}
