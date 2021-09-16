using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FacuTheRock.Talks.Net.EFTesting.Database.Models
{
    public class Team
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int FoundedYear { get; set; }

        public ICollection<Player> Players { get; set; }
    }
}
