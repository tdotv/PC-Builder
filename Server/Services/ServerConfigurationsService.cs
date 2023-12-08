using PC_Designer.Shared;
using PC_Designer.ViewModels;

public class ServerConfigurationService : IPcConfigurationService
{
    private readonly IDbService _dbService;

    public ServerConfigurationService(IDbService dbService)
    { 
        _dbService = dbService ;
    }

    public async Task<List<PcConfigurationViewModel>?> GetConfigurationsAsync() 
    {
        var query = @"
            SELECT 
                PcConfigurations.PcConfigurationId,
                PcConfigurations.Name,
                PcConfigurations.TotalWattage,
                PcConfigurations.CreatedOn,
                MotherBoards.MotherBoardName AS MotherboardName,
                CPUs.CpuName AS CpuName,
                GraphicalCards.GraphicalCardName AS GraphicalCardName,
                ComputerCases.CaseName AS CaseName
            FROM dbo.PcConfigurations 
            INNER JOIN dbo.MotherBoards ON PcConfigurations.MotherBoardId = MotherBoards.MotherBoardId 
            INNER JOIN dbo.CPUs ON PcConfigurations.CpuId = CPUs.CpuId 
            INNER JOIN dbo.GraphicalCards ON PcConfigurations.GraphicalCardId = GraphicalCards.GraphicalCardId 
            INNER JOIN dbo.ComputerCases ON PcConfigurations.CaseId = ComputerCases.ComputerCaseId";

        var configurations = await _dbService.GetAll<PcConfigurationViewModel>(query, new { }); 
        return configurations.Select(configuration => (PcConfigurationViewModel)configuration).ToList(); 
    }
}