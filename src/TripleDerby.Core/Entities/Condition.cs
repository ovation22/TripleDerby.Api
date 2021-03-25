using System.ComponentModel.DataAnnotations;
using TripleDerby.Core.Enums;

namespace TripleDerby.Core.Entities
{
    public class Condition
    {
        [Key]
        public ConditionId Id { get; set; }

        public string Name { get; set; } = default!;
    }
}
