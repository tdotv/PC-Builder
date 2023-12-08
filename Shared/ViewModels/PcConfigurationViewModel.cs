using System;

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
        public string MotherBoardName { get; set; }
        public string CpuName { get; set; }
        public string GraphicalCardName { get; set; }
        public string CaseName { get; set; }
    }
}