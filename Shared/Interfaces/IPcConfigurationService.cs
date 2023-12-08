using PC_Designer.ViewModels;

public interface IPcConfigurationService
{
    Task<List<PcConfigurationViewModel>?> GetConfigurationsAsync();

    // Task<int> CreateConfigurationsAsync(PcConfigurationViewModel configurationViewModel);

    // Task<PcConfigurationViewModel?> UpdateConfigurationsAsync(PcConfigurationViewModel configurationViewModel);

    Task<bool> DeleteConfigurationsAsync(int key);
}