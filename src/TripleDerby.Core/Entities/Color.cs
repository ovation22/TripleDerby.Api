using System.ComponentModel.DataAnnotations;

namespace TripleDerby.Core.Entities
{
    public class Color
    {
        [Key]
        public byte Id { get; set; }

        public string Name { get; set; } = default!;
    }
}
