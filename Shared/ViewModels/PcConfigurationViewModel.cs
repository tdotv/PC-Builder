using System;
using PC_Designer.Shared;

namespace PC_Designer.ViewModels
{
    public class PcConfigurationViewModel
    {
        public int PcConfigurationId { get; set; }

        public string Name { get; set; }

        public string About { get; set; }

        public decimal Cost { get; set; }

        public int TotalWattage { get; set; }

        public DateTime CreatedOn { get; set; }

        public int MotherBoardId { get; set; }
        public MotherBoards motherBoard { get; set; }

        public string MotherBoardName { get; set; }

        public int CpuId { get; set; }
        public string CpuName { get; set; }

        public int GraphicalCardId { get; set; }
        public string GraphicalCardName { get; set; }

        public int CaseId { get; set; }
        public string CaseName { get; set; }

        public int UserId { get; set; }
    }
}