using System.Collections.Generic;

namespace Fit2go.Options
{
    public class SportivityOptions
    {
        public int LocationId { get; set; }
        public ICollection<UserOption> Users { get; set; }
    }
}
