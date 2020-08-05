using System;
using System.ComponentModel.DataAnnotations;

namespace TripleDerby.Core.Entities
{
    public class Horse
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        public bool IsMale { get; set; }
    }
}
