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


    }
}
