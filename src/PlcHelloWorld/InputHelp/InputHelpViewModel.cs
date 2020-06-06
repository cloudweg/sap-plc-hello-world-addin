using PlcHelloWorld.Utils;
using Sap.Plc.AddIn.AddInView.Data.Master;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace PlcHelloWorld.InputHelp
{
    class InputHelpViewModel : ViewModelBase
    {
    # region Text Elements
        public string UpdateFieldButton
        {
            get => Texts.ButtonUpdateField;
        }
        #endregion

        private string _fieldValue;
        public string FieldValue
        {
            get => _fieldValue;
            set
            {
                if (value != null && _fieldValue != value)
                {
                    _fieldValue = value;
                }
                else
                {
                    _fieldValue = "";
                }
                OnPropertyChanged();
            }           
        }

        private bool _materialSelected;
        public bool MaterialSelected
        {
            get => _materialSelected;
            set
            {
                if (_materialSelected != value)
                {
                    _materialSelected = value;
                    OnPropertyChanged();
                }
            }
        }
            
        /// <summary>
        /// Input Value
        /// </summary>
        private ComboBoxItem _selectedInputValue;
        public ComboBoxItem SelectedInputValue
        {
            get => _selectedInputValue;
            set
            {
                if (_selectedInputValue != value)
                {
                    _selectedInputValue = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
