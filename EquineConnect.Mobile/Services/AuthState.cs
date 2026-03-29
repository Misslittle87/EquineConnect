namespace EquineConnect.Mobile.Services
{
    public class AuthState
    {
        public string? UserId { get; set; }
        public string? Email { get; set; }
        public List<string> Roles { get; set; } = new();
        public string? Token { get; set; }
        public bool IsAuthenticated => !string.IsNullOrEmpty(Token);

        /// <summary>
        /// Kontrollera om användaren har en specifik roll
        /// </summary>
        public bool HasRole(string role)
        {
            return Roles.Contains(role);
        }

        /// <summary>
        /// Kontrollera om användaren är Admin
        /// </summary>
        public bool IsAdmin => HasRole("Admin");

        /// <summary>
        /// Kontrollera om användaren är Superuser
        /// </summary>
        public bool IsSuperuser => HasRole("Superuser");

        /// <summary>
        /// Kontrollera om användaren är StableStaff
        /// </summary>
        public bool IsStableStaff => HasRole("StableStaff");

        /// <summary>
        /// Kontrollera om användaren är Boarder
        /// </summary>
        public bool IsBoarder => HasRole("Boarder");

        /// <summary>
        /// Kontrollera om användaren är CoRider
        /// </summary>
        public bool IsCoRider => HasRole("CoRider");
    }
}
