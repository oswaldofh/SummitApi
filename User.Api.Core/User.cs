namespace User.Api.Core
{
    public class User
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? RawPassword { get; set; } // No se debe serializar
        public DateTime BirthDate { get; set; }
    }
}
