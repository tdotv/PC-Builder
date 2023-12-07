using PC_Designer.ViewModels;

public interface IPcConfigurationService
{
    Task<List<PcConfigurationViewModel>?> GetConfigurationsAsync();
}