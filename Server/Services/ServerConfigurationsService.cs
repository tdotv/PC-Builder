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
                PcConfigurations.About,
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

    // public async Task<int> CreateConfigurationsAsync(PcConfigurationViewModel configurationViewModel)
    // {
    //     var command = @"
    //         INSERT INTO dbo.PcConfigurations (MotherBoardId, CpuId, GraphicalCardId, CaseId, Name, TotalWattage, CreatedOn)
    //         VALUES (@MotherBoardId, @CpuId, @GraphicalCardId, @CaseId, @Name, @TotalWattage, @CreatedOn);
    //         SELECT SCOPE_IDENTITY();";

    //     var id = await _dbService.Insert<int>(command, configurationViewModel);
    //     return id;
    // }

    // public async Task<bool> UpdateConfigurationAsync(PcConfigurations configuration)
    // {
    //     var command = @"
    //         UPDATE dbo.PcConfigurations
    //         SET MotherBoardId = @MotherBoardId, CpuId = @CpuId, GraphicalCardId = @GraphicalCardId,
    //             CaseId = @CaseId, Name = @Name, TotalWattage = @TotalWattage, CreatedOn = @CreatedOn
    //         WHERE PcConfigurationId = @PcConfigurationId";

    //     await _dbService.Update<int>(command, configuration);
    //     return true;
    // }

    public async Task<bool> DeleteConfigurationsAsync(int id)
    {
        var command = "DELETE FROM dbo.PcConfigurations WHERE PcConfigurationId = @Id";
        await _dbService.Delete<int>(command, new { Id = id });
        return true;
    }
}