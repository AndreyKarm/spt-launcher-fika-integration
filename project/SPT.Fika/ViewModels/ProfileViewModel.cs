using ReactiveUI;
using System.Reactive;
using Avalonia.Media;

namespace SPT.Fika.ViewModels
{
    public class ProfileViewModel : ReactiveObject
    {
        // Online Players
        private int _onlinePlayersCount;
        public int OnlinePlayersCount
        {
            get => _onlinePlayersCount;
            set => this.RaiseAndSetIfChanged(ref _onlinePlayersCount, value);
        }

        // Dedicated Server Status
        private string _dediAvailabilityText;
        public string DediAvailabilityText
        {
            get => _dediAvailabilityText;
            set => this.RaiseAndSetIfChanged(ref _dediAvailabilityText, value);
        }

        private SolidColorBrush _dediAvailabilityColor;
        public SolidColorBrush DediAvailabilityColor
        {
            get => _dediAvailabilityColor;
            set => this.RaiseAndSetIfChanged(ref _dediAvailabilityColor, value);
        }

        // Commands
        public ReactiveCommand<Unit, Unit> UpdateOnlinePlayersCommand { get; }

        public ProfileViewModel()
        {
            // Initialize with defaults
            _dediAvailabilityText = "Online players";
            _dediAvailabilityColor = new SolidColorBrush(Colors.Gray);
            _onlinePlayersCount = 0;

            // Create commands
            UpdateOnlinePlayersCommand = ReactiveCommand.CreateFromTask(RefreshOnlinePlayersAsync);

            // Initial update
            _ = RefreshStatusAsync();
        }

        private async Task RefreshOnlinePlayersAsync()
        {
            try
            {
                // Add await to make this properly async
                var players = await Task.Run(() => FikaController.GetOnlinePlayers());
                OnlinePlayersCount = players?.Length ?? 0;
            }
            catch (Exception ex)
            {
                // Log error
                Console.WriteLine($"Error refreshing online players: {ex.Message}");
            }
        }

        private async Task RefreshStatusAsync()
        {
            try
            {
                // Update dedicated server status
                var dedicatedData = FikaController.GetDedicatedData();
                if (dedicatedData != null && dedicatedData.Available != null)
                {
                    DediAvailabilityText = "Dedicated server is available";
                    DediAvailabilityColor = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    DediAvailabilityText = "Dedicated server is unavailable";
                    DediAvailabilityColor = new SolidColorBrush(Colors.Red);
                }

                // Update online players
                await RefreshOnlinePlayersAsync();
            }
            catch (Exception ex)
            {
                DediAvailabilityText = "Error updating status";
                DediAvailabilityColor = new SolidColorBrush(Colors.Red);

                // Log error
                Console.WriteLine($"Error updating status: {ex.Message}");
            }
        }
    }
}