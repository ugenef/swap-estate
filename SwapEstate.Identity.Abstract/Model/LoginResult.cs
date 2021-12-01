namespace SevenSem.Identity.Abstract.Model
{
    public class LoginResult
    {
        public bool Succeeded { get; set; }
        public Role[] Roles { get; set; }
    }
}