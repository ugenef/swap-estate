namespace SevenSem.Rest.Dto
{
    /// <summary>
    /// dto
    /// </summary>
    public class CreateDto
    {
        public CredsDto Creds { get; set; }
        public RoleDto[] Roles { get; set; }
    }
}