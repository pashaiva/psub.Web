using System;
using System.Collections.Generic;

namespace Psub.Domain.Entities
{
    public class File
    {
        public virtual string Name { get; set; }
        public virtual DateTime DateTime { get; set; }
        public virtual List<string> Folders { get; set; }
        public virtual string Folder { get; set; }
        public virtual string Guid { get; set; }
        public virtual string EntityName { get; set; }
    }
}
