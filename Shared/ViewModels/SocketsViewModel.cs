using System.ComponentModel;
using PC_Designer.Shared;

namespace PC_Designer.ViewModels
{
    
    public class SocketsViewModel : INotifyPropertyChanged
    {        
        private int _socketId { get; set; }
        private string _name { get; set; } = null!;

        public int SocketId
        {
            get => _socketId;
            set
            {
                if (_socketId != value)
                {
                    _socketId = value;
                    OnPropertyChanged(nameof(SocketId));
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

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static implicit operator SocketsViewModel(Sockets sockets)
        {
            return new SocketsViewModel
            {
                SocketId = sockets.SocketId,
                Name = sockets.Name,
            };
        }
    }


}