using System;

namespace FacuTheRock.Talks.Net.EFTesting.API.Dtos
{
    public class PlayerDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Position { get; set; }

        public Guid TeamId { get; set; }
    }
}
