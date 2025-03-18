using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SPT.Fika.CustomControls
{
    public partial class DedicatedStatus : UserControl
    {
        public static readonly StyledProperty<string> DediAvailabilityTextProperty = AvaloniaProperty.Register<DedicatedStatus, string>(
            "DediAvailabilityText");

        public string DediAvailabilityText
        {
            get => GetValue(DediAvailabilityTextProperty);
            set => SetValue(DediAvailabilityTextProperty, value);
        }

        public static readonly StyledProperty<string> DediAvailabilityColorProperty = AvaloniaProperty.Register<DedicatedStatus, string>(
            "DediAvailabilityColor");

        public string DediAvailabilityColor
        {
            get => GetValue(DediAvailabilityColorProperty);
            set => SetValue(DediAvailabilityColorProperty, value);
        }

        public static readonly StyledProperty<ICommand> UpdateDedicatedStatusCommandProperty =
            AvaloniaProperty.Register<DedicatedStatus, ICommand>(nameof(UpdateDedicatedStatusCommand));

        public ICommand UpdateDedicatedStatusCommand
        {
            get => GetValue(UpdateDedicatedStatusCommandProperty);
            set => SetValue(UpdateDedicatedStatusCommandProperty, value);
        }

        public DedicatedStatus()
        {
            InitializeComponent();


            // Initialize with current status if there are no bindings
            if (string.IsNullOrEmpty(DediAvailabilityText))
            {
                var dediData = FikaController.GetDedicatedData();
                bool isDediAvailable = dediData.Available != null;
                DediAvailabilityText = isDediAvailable ? "Available" : "Unavailable";
                DediAvailabilityColor = isDediAvailable ? "Green" : "Red";
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

// using System;
// using System.Windows.Input;
// using Avalonia;
// using Avalonia.Controls;
// using Avalonia.Markup.Xaml;
// using ReactiveUI;

// namespace SPT.Fika.CustomControls
// {
//     public partial class DedicatedStatus : UserControl
//     {
//         public static readonly StyledProperty<string> DediAvailabilityTextProperty = AvaloniaProperty.Register<DedicatedStatus, string>(
//             "DediAvailabilityText");

//         public string DediAvailabilityText
//         {
//             get => GetValue(DediAvailabilityTextProperty);
//             set => SetValue(DediAvailabilityTextProperty, value);
//         }

//         public static readonly StyledProperty<string> DediAvailabilityColorProperty = AvaloniaProperty.Register<DedicatedStatus, string>(
//             "DediAvailabilityColor");

//         public string DediAvailabilityColor
//         {
//             get => GetValue(DediAvailabilityColorProperty);
//             set => SetValue(DediAvailabilityColorProperty, value);
//         }

//         public static readonly StyledProperty<ICommand> UpdateDedicatedStatusCommandProperty =
//             AvaloniaProperty.Register<DedicatedStatus, ICommand>(nameof(UpdateDedicatedStatusCommand));

//         public ICommand UpdateDedicatedStatusCommand
//         {
//             get
//             {
//                 var command = GetValue(UpdateDedicatedStatusCommandProperty);
//                 // If no command is bound, use the default refresh command
//                 return command ?? _defaultRefreshCommand;
//             }
//             set => SetValue(UpdateDedicatedStatusCommandProperty, value);
//         }

//         private readonly ICommand _defaultRefreshCommand;

//         public DedicatedStatus()
//         {
//             InitializeComponent();

//             // Create a default command that updates the UI when no external command is provided
//             _defaultRefreshCommand = ReactiveCommand.Create(() =>
//             {
//                 try
//                 {
//                     var dediData = FikaController.GetDedicatedData();
//                     bool isDediAvailable = dediData != null && dediData.Available != null;
//                     DediAvailabilityText = isDediAvailable ? "Available" : "Unavailable";
//                     DediAvailabilityColor = isDediAvailable ? "Green" : "Red";
//                 }
//                 catch (Exception ex)
//                 {
//                     DediAvailabilityText = "Error";
//                     DediAvailabilityColor = "Red";
//                     Console.WriteLine($"Error refreshing dedicated status: {ex.Message}");
//                 }
//             });

//             // Initialize with current status if there are no bindings
//             if (string.IsNullOrEmpty(DediAvailabilityText))
//             {
//                 try
//                 {
//                     var dediData = FikaController.GetDedicatedData();
//                     bool isDediAvailable = dediData != null && dediData.Available != null;
//                     DediAvailabilityText = isDediAvailable ? "Available" : "Unavailable";
//                     DediAvailabilityColor = isDediAvailable ? "Green" : "Red";
//                 }
//                 catch
//                 {
//                     DediAvailabilityText = "Unknown";
//                     DediAvailabilityColor = "Gray";
//                 }
//             }
//         }

//         private void InitializeComponent()
//         {
//             AvaloniaXamlLoader.Load(this);
//         }
//     }
// }