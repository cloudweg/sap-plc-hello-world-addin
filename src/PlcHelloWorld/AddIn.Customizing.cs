using PlcHelloWorld.CustomizingViewModel;
using Sap.Plc.AddIn.AddInView.Data.Config;
using System;
using System.ComponentModel;
using System.Linq;

namespace PlcHelloWorld
{
    public partial class AddIn
    {
        //--Configuration key
        private const string _exampleKey = "Example";

        CustomizingModel _customizingModel;

        /// <summary>
        /// 
        /// </summary>
        private void SetupConfiguration()
        {
            //--Load backend configuration
            ConfigurationItem configItemExample = this.Configuration.Items.FirstOrDefault(item => item.Key == _exampleKey);

            //--Create new configuration model and view
            _customizingModel = new CustomizingModel(configItemExample);
            this.ConfigurationView = new CustomizingView();


            //--Assign model to view
            this.ConfigurationView.DataContext = _customizingModel;

            //--Register Events
            _customizingModel.PropertyChanged += ProcessConfigurationChanged;
            this.Configuration.Reloaded += this.ConfigurationReloaded;
        }

        /// <summary>
        /// Reload customizing from PLC backend
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfigurationReloaded(object sender, EventArgs e)
        {
            ConfigurationItem configItemVersion = this.Configuration.Items.FirstOrDefault(item => item.Key == _exampleKey);
            if (configItemVersion != null)
            {
                _customizingModel.Example = configItemVersion.Value;
            }
        }

        /// <summary>
        /// Add new customizing value to customizing object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessConfigurationChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case _exampleKey:
                    if (_customizingModel.Example != string.Empty)
                    {
                        this.Configuration.Items.Add(new ConfigurationItem(_exampleKey, _customizingModel.Example));
                    }
                    else 
                    {
                        this.Configuration.Items.Add(new ConfigurationItem(_exampleKey, " "));
                    }
                    break;
            }
        }
    }
}
