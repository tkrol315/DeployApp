namespace DeployApp.Application.Dtos
{
    public record InstanceSearchPhraseDto(
        string TypeDescription = null,
        string TagName = null,
        string GroupName = null
        );
   
}
