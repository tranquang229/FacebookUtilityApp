using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace FM.Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = String.Empty;

        public string LastName { get; set; } = String.Empty;

        public string VerificationToken { get; set; } = String.Empty;

        public DateTime? Verified { get; set; }

        public bool IsVerified => Verified.HasValue || PasswordReset.HasValue;

        public string ResetToken { get; set; } = String.Empty;

        public bool? IsValidateResetToken { get; set; }

        public DateTime? ResetTokenExpires { get; set; }

        public DateTime? PasswordReset { get; set; }

        [NotMapped]
        public List<string> Roles { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

        public List<HealthHistory> HealthHistories { get; set; } = new List<HealthHistory>();
    }
}