using GameReaderCommon;
using SimHub.Plugins;
using System;
using System.Windows.Media;
using System.Windows.Forms;

namespace User.PluginIgnitionManager
{
    [PluginDescription("Allows to set buttons to set ignition to on/off for physical switches")]
    [PluginAuthor("Gmasil")]
    [PluginName("Ignition Manager")]
    public class IgnitionManagerPlugin : IPlugin, IDataPlugin, IWPFSettingsV2
    {
        public IgnitionManagerPluginSettings Settings;

        /// <summary>
        /// Instance of the current plugin manager
        /// </summary>
        public PluginManager PluginManager { get; set; }

        /// <summary>
        /// Gets the left menu icon. Icon must be 24x24 and compatible with black and white display.
        /// </summary>
        public ImageSource PictureIcon => this.ToIcon(Properties.Resources.sdkmenuicon);

        /// <summary>
        /// Gets a short plugin title to show in left menu. Return null if you want to use the title as defined in PluginName attribute.
        /// </summary>
        public string LeftMenuTitle => "Ignition Manager";

        private Boolean IgnitionStatus = false;
        private long LastUpdate = DateTimeOffset.Now.ToUnixTimeMilliseconds();

        /// <summary>
        /// Called one time per game data update, contains all normalized game data,
        /// raw data are intentionnally "hidden" under a generic object type (A plugin SHOULD NOT USE IT)
        ///
        /// This method is on the critical path, it must execute as fast as possible and avoid throwing any error
        ///
        /// </summary>
        /// <param name="pluginManager"></param>
        /// <param name="data">Current game data, including current and previous data frame.</param>
        public void DataUpdate(PluginManager pluginManager, ref GameData data)
        {
            long Now = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            if ((Now - LastUpdate) > Settings.DelayInMs)
            {
                if (data.GameRunning)
                {
                    if (data.OldData != null && data.NewData != null)
                    {
                        if (IgnitionStatus != (data.NewData.EngineIgnitionOn != 0))
                        {
                            // toggle ignition
                            SendKeys.SendWait("I");
                            LastUpdate = Now;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Called at plugin manager stop, close/dispose anything needed here !
        /// Plugins are rebuilt at game change
        /// </summary>
        /// <param name="pluginManager"></param>
        public void End(PluginManager pluginManager)
        {
            // Save settings
            this.SaveCommonSettings("GeneralSettings", Settings);
        }

        /// <summary>
        /// Returns the settings control, return null if no settings control is required
        /// </summary>
        /// <param name="pluginManager"></param>
        /// <returns></returns>
        public System.Windows.Controls.Control GetWPFSettingsControl(PluginManager pluginManager)
        {
            return new SettingsControlDemo(this);
        }

        /// <summary>
        /// Called once after plugins startup
        /// Plugins are rebuilt at game change
        /// </summary>
        /// <param name="pluginManager"></param>
        public void Init(PluginManager pluginManager)
        {
            SimHub.Logging.Current.Info("Starting plugin");

            // Load settings
            Settings = this.ReadCommonSettings<IgnitionManagerPluginSettings>("GeneralSettings", () => new IgnitionManagerPluginSettings());

            this.AddAction("SetIgnitionOn", (a, b) =>
            {
                IgnitionStatus = true;
            });

            this.AddAction("SetIgnitionOff", (a, b) =>
            {
                IgnitionStatus = false;
            });
        }
    }
}
