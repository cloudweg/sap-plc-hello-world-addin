using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PlcHelloWorld.InputHelp;
using Sap.Plc.AddIn.AddInView.Data;
using Sap.Plc.AddIn.AddInView.Data.Master;
using Sap.Plc.AddIn.AddInView.UserInterface;
using Sap.Plc.AddIn.AddInView.UserInterface.Messaging;

namespace PlcHelloWorld
{
    public partial class AddIn
    {
        private InputHelpViewModel viewModel;

        private void SetupSidePanel()
        {

            //--Create view and model
            FrameworkElement view = new InputHelpView();
            viewModel = new InputHelpViewModel();
            view.DataContext = viewModel;

            //--Create side panel
            var inputSidePanel = new SidePanel(Texts.SidePanelCaption, view);

            //--Add side panel
            this.CalculationTab.SidePanels.Add(inputSidePanel);

            //-Register events
            InputHelpView inputHelpView = (InputHelpView)view;
            inputHelpView.UpdateFieldButtonClicked += UpdateFieldButtonClicked;
            this.CalculationTab.CostItemSelectionChanged += CheckMaterialSelected;
            this.CalculationTab.CostItemSelectionChanged += UpdateShownFieldValue;
            this.CalculationTab.CostItemChanged += UpdateShownFieldValue;
        }

        private void UpdateShownFieldValue(object sender, ICostItemEventArgs e)
        {
            //--Update the field value every time the users changes the selection
            viewModel.FieldValue = this.CalculationTab?.SelectedCostItemInActiveVersion?.Material?.Id;
        }

        private void CheckMaterialSelected(object sender, ICostItemEventArgs e)
        {
           
           if( this.CalculationTab?.SelectedCostItemInActiveVersion?.Material != null)
            {
                viewModel.MaterialSelected = true;
            }
            else
            {
                viewModel.MaterialSelected = false;
            }
        }

        private void UpdateFieldButtonClicked(object sender, EventArgs e)
        {
            //--Check if current item is material
            if ( this.CalculationTab?.SelectedCostItemInActiveVersion?.Material?.Id != null)
            {
                if (this.CalculationTab?.SelectedCostItemInActiveVersion?.Material != null &&
                    viewModel.SelectedInputValue?.Content != null &&
                    this.CalculationTab.SelectedCostItemInActiveVersion.Material.Id != (string)viewModel.SelectedInputValue.Content)
                {
                    var newMAterial = Material.CreateBuilder((string)viewModel.SelectedInputValue.Content).Build();
                    this.CalculationTab.SelectedCostItemInActiveVersion.Material = newMAterial;
                    this.CalculationTab.SelectedCostItemInActiveVersion.UpdateOnBackendAsync();
                }
            }
            else
            {             
                //--No material selected, show warning message
                new Message(
                    Texts.MsgNoMaterialSelected,
                    MessageType.Warning
                ).Show();              
            }
        }
    }
}
