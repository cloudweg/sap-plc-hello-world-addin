using Sap.Plc.AddIn.AddInView.Data.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PlcHelloWorld.CustomizingViewModel
{
    class CustomizingModel
    {
        private string _example;

        public CustomizingModel(ConfigurationItem example)
        {
            if (example != null)
            {
                _example = example.Value;
            }
        }
       
        #region Text Elements
        public string ExampleLabel
        {
            get => Texts.ExampleLabel;
        }
        #endregion Text Elements

        public string Example
        {
            get
            {
                return _example;
            }
            set
            {
                if (value != _example)
                {
                    _example = value;
                    //--Raise event that customizing has changed
                    this.OnPropertyChanged();
                }
            }
        }

        //--Event based update of view
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
