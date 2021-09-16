using System;
using System.Collections.Generic;

namespace FacuTheRock.Talks.Net.EFTesting.API.Dtos
{
    public class TeamDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int FoundedYear { get; set; }

        public ICollection<PlayerDto> Players { get; set; }
    }
}
