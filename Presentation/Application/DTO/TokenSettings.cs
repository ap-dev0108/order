namespace OrderManagement.Application.DTO;

public record AuthenticationSettings
    {
        public required string TokenSecret { get; set; }
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
    }