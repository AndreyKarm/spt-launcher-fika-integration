using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using SPT.Fika.Models;
using System.Reactive;
using Avalonia.Layout;

namespace SPT.Fika.CustomControls
{
    public partial class OnlinePlayers : UserControl
    {
        public static readonly StyledProperty<int> OnlinePlayersCountProperty =
            AvaloniaProperty.Register<OnlinePlayers, int>(nameof(OnlinePlayersCount), defaultValue: 0);

        public int OnlinePlayersCount
        {
            get => GetValue(OnlinePlayersCountProperty);
            set => SetValue(OnlinePlayersCountProperty, value);
        }
        private readonly string?[] activityStrings = { "in Menu", "in Raid", "in Stash", "in Hideout", "is Trading" };
        private readonly Dictionary<string, string> maps = new Dictionary<string, string>
        {
            { "factory4_day", "Factory" },
            { "factory4_night", "Factory" },
            { "bigmap", "Customs" },
            { "Interchange", "Interchange" },
            { "RezervBase", "Reserve" },
            { "Woods", "Woods" },
            { "Shoreline", "Shoreline" },
            { "TarkovStreets", "Streets of Tarkov" },
            { "Sandbox", "Ground Zero" },
            { "laboratory", "Laboratory" },
            { "Lighthouse", "Lighthouse" }
        };

        public OnlinePlayers()
        {
            InitializeComponent();
            
            // Create a default command that updates the UI AND invokes the parent's command
            UpdateOnlinePlayersCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                // This will update the UI
                await UpdatePlayersList();
                
                // Now get and execute the parent's command if it exists
                var parentCommand = GetValue(UpdateOnlinePlayersCommandProperty);
                if (parentCommand != null)
                {
                    parentCommand.Execute(null);
                }
            });

            // Initial load of players
            UpdatePlayersList();
        }

        public static readonly StyledProperty<ICommand> UpdateOnlinePlayersCommandProperty = AvaloniaProperty.Register<OnlinePlayers, ICommand>(nameof(UpdateOnlinePlayersCommand));

        public ICommand UpdateOnlinePlayersCommand
        {
            get => GetValue(UpdateOnlinePlayersCommandProperty);
            set => SetValue(UpdateOnlinePlayersCommandProperty, value);
        }

        // Modified to accept players parameter to avoid duplicate API calls
        public Task UpdatePlayersList(FikaPlayer[]? players = null)
        {
            try
            {
                
                // Clear the existing list first
                var stackPanel = this.FindControl<StackPanel>("onlinePlayersList");
                if (stackPanel != null)
                {
                    stackPanel.Children.Clear();
                }

                // Use provided players or fetch if none provided
                if (players == null)
                {
                    players = FikaController.GetOnlinePlayers();
                }

                foreach (var player in players)
                {
                    if (player != null)
                    {
                        var border = new Border
                        {
                            BorderBrush = Avalonia.Media.Brushes.Black,
                            BorderThickness = new Thickness(1),
                            Margin = new Thickness(5),
                            Padding = new Thickness(10)
                        };

                        var horizontalStack = new StackPanel { Orientation = Orientation.Horizontal };

                        var verticalStack1 = new StackPanel { Orientation = Orientation.Vertical };
                        verticalStack1.Children.Add(new Label { Content = "Player name" });
                        verticalStack1.Children.Add(new Label { Content = "Current activity:" });

                        string activityString = String.Format("{0}", activityStrings[player.activity]);

                        if (player.activity == 1)
                            activityString = String.Format("{0} on {1} as {2} for {3} mins",
                                activityStrings[player.activity],
                                maps[player.RaidInformation.location],
                                player.RaidInformation.side,
                                ((Int32)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds - player.activityStartedTimeStamp) / 60);

                        var verticalStack2 = new StackPanel { Orientation = Orientation.Vertical };
                        verticalStack2.Children.Add(new Label { Content = player.nickname });
                        verticalStack2.Children.Add(new Label { Content = activityString });

                        horizontalStack.Children.Add(verticalStack1);
                        horizontalStack.Children.Add(verticalStack2);

                        border.Child = horizontalStack;

                        stackPanel?.Children.Add(border);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error refreshing online players: {ex.Message}");
            }
            return Task.CompletedTask;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
