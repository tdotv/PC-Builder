using System;
using System.ComponentModel;
using PC_Designer.Shared;

namespace PC_Designer.ViewModels
{
    public class PcConfigurationViewModel : INotifyPropertyChanged
    {
        private int _pcConfigurationId;
        private MotherBoards _motherboard = null!;
        private CPUs _cpu = null!;
        private GraphicalCards _graphicalCard = null!;
        private ComputerCases _case = null!;
        private string _name = null!;
        private int _totalWattage;
        private DateTime _createOn;

        public int PcConfigurationId
        {
            get => _pcConfigurationId;
            set
            {
                if (_pcConfigurationId != value)
                {
                    _pcConfigurationId = value;
                    OnPropertyChanged(nameof(PcConfigurationId));
                }
            }
        }

        public MotherBoards MotherBoard
        {
            get => _motherboard;
            set
            {
                if (_motherboard != value)
                {
                    _motherboard = value;
                    OnPropertyChanged(nameof(MotherBoard));
                }
            }
        }

        public CPUs Cpu
        {
            get => _cpu;
            set
            {
                if (_cpu != value)
                {
                    _cpu = value;
                    OnPropertyChanged(nameof(Cpu));
                }
            }
        }

        public GraphicalCards GraphicalCard
        {
            get => _graphicalCard;
            set
            {
                if (_graphicalCard != value)
                {
                    _graphicalCard = value;
                    OnPropertyChanged(nameof(GraphicalCard));
                }
            }
        }

        public ComputerCases Case
        {
            get => _case;
            set
            {
                if (_case != value)
                {
                    _case = value;
                    OnPropertyChanged(nameof(Case));
                }
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public int TotalWattage
        {
            get => _totalWattage;
            set
            {
                if (_totalWattage != value)
                {
                    _totalWattage = value;
                    OnPropertyChanged(nameof(TotalWattage));
                }
            }
        }

        public DateTime CreateOn
        {
            get => _createOn;
            set
            {
                if (_createOn != value)
                {
                    _createOn = value;
                    OnPropertyChanged(nameof(CreateOn));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static implicit operator PcConfigurationViewModel(PcConfigurations configuration)
        {
            return new PcConfigurationViewModel
            {
                PcConfigurationId = configuration.PcConfigurationId,
                MotherBoard = (MotherBoards)configuration.MotherBoard,
                Cpu = (CPUs)configuration.Cpu,
                GraphicalCard = (GraphicalCards)configuration.GraphicalCard,
                Case = (ComputerCases)configuration.Case,
                Name = configuration.Name ?? string.Empty,
                TotalWattage = configuration.TotalWattage,
                CreateOn = configuration.CreateOn
            };
        }
    }
}