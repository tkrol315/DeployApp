namespace DeployApp.Application.Dtos
{
    public record InstanceFilterDto(
        string TypeDescription = null,
        string TagName = null,
        string GroupName = null,
        string ActualVersion = null
        );
   
}
