namespace DeployApp.Application.Dtos
{
    public record ProjectFilterDto(
        string Title = null,
        string Description = null,
        bool? isActive = null
        )
    {
    }
}
