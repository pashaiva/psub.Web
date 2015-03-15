using System;

namespace Psub.DTO.Entities
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Name { get; set; }
        public string Guid { get; set; }
        public DateTime Created { get; set; }
    }
}
