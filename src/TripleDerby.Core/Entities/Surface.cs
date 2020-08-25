using System.ComponentModel.DataAnnotations;
using TripleDerby.Core.Enums;

namespace TripleDerby.Core.Entities
{
    public class Surface
    {
        [Key]
        public SurfaceId Id { get; set; }

        public string Name { get; set; } = default!;
    }
}
