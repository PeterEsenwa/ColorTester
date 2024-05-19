namespace ColorTesterAPI.Application.DTOs
{
    public class CreateProjectRequest
    {
        public Guid PaletteId { get; set; }
        public string MockupName { get; set; }
    }

    public class UpdateProjectRequest
    {
        public Guid PaletteId { get; set; }
    }
}