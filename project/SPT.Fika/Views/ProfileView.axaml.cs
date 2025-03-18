using SPT.Fika.ViewModels;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace SPT.Fika.Views
{
    public partial class ProfileView : ReactiveUserControl<ProfileViewModel>
    {
        public ProfileView()
        {
            InitializeComponent();

            // Explicitly set the DataContext to the correct ViewModel
            DataContext = new ProfileViewModel();

            // Optional: Set up view activation if using ReactiveUI patterns
            this.WhenActivated(disposables =>
            {
                // Activate any view-specific logic here
            });
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}