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
                MotherBoards.*,
                CPUs.*,
                GraphicalCards.*,
                ComputerCases.*
            FROM dbo.PcConfigurations 
            INNER JOIN dbo.MotherBoards ON PcConfigurations.MotherBoardId = MotherBoards.MotherBoardId 
            INNER JOIN dbo.CPUs ON PcConfigurations.CpuId = CPUs.CpuId 
            INNER JOIN dbo.GraphicalCards ON PcConfigurations.GraphicalCardId = GraphicalCards.GraphicalCardId 
            INNER JOIN dbo.ComputerCases ON PcConfigurations.CaseId = ComputerCases.ComputerCaseId";

        // var configurations = await _dbService.Connection.QueryAsync<PcConfigurationViewModel, MotherBoards, CPUs, GraphicalCards, ComputerCases, PcConfigurationViewModel>(
        //     query,
        //     (configuration, motherboard, cpu, graphicalCard, computerCase) =>
        //     {
        //         configuration.Motherboard = new SocketsViewModel
        //         {
        //             SocketId = motherboard.SocketId,
        //             Name = motherboard.Name
        //             // Добавьте остальные свойства, если они есть
        //         };

        //         configuration.Cpu = new SocketsViewModel
        //         {
        //             SocketId = cpu.SocketId,
        //             Name = cpu.Name
        //             // Добавьте остальные свойства, если они есть
        //         };

        //         configuration.GraphicalCard = new SocketsViewModel
        //         {
        //             SocketId = graphicalCard.SocketId,
        //             Name = graphicalCard.Name
        //             // Добавьте остальные свойства, если они есть
        //         };

        //         configuration.Case = new SocketsViewModel
        //         {
        //             SocketId = computerCase.SocketId,
        //             Name = computerCase.Name
        //             // Добавьте остальные свойства, если они есть
        //         };

        //         return configuration;
        //     },
        //     splitOn: "MotherBoardId, CpuId, GraphicalCardId, CaseId"
        // );

        // return configurations.ToList();

        var configurations = await _dbService.GetAll<PcConfigurations>(query, new { }); 
        return configurations.Select(configuration => (PcConfigurationViewModel)configuration).ToList(); 
    }

}