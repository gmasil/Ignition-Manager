using System.Windows.Controls;

namespace User.PluginIgnitionManager
{
    /// <summary>
    /// Logique d'interaction pour SettingsControlDemo.xaml
    /// </summary>
    public partial class SettingsControlDemo : UserControl
    {
        public IgnitionManagerPlugin Plugin { get; }

        public SettingsControlDemo()
        {
            InitializeComponent();
        }

        public SettingsControlDemo(IgnitionManagerPlugin plugin) : this()
        {
            this.Plugin = plugin;
        }

        private void GlobalEnabledToggle_IsEnabledChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            Plugin.Settings.Enabled = GlobalEnabledToggle.IsChecked == true;
        }

        private void GlobalEnabledToggle_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            GlobalEnabledToggle.IsChecked = Plugin.Settings.Enabled;
        }
    }
}
