using PC_Designer.ViewModels;
using PC_Designer.Shared;

public interface IPcConfigurationService
{
    Task<List<PcConfigurationViewModel>?> GetConfigurationsAsync();

    Task<int> CreateConfigurationsAsync(PcConfigurationViewModel configurationViewModel);

    // Task<PcConfigurationViewModel?> UpdateConfigurationsAsync(PcConfigurationViewModel configurationViewModel);

    Task<List<MotherBoards>?> GetMotherBoardsAsync();
    Task<List<CPUs>?> GetCpusAsync();
    Task<List<GraphicalCards>?> GetGraphicalCardsAsync();
    Task<List<ComputerCases>?> GetComputerCasesAsync();

    Task<bool> DeleteConfigurationsAsync(int key);
}