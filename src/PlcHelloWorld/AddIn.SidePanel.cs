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
            InputHelpView inputHelpView = (InputHelpView)view;

            //--Create side panel
            var inputSidePanel = new SidePanel(Texts.SidePanelCaption, view);

            //--Add side panel
            this.CalculationTab.SidePanels.Add(inputSidePanel);

            //--Register events if user changes the current line item or changes the selection
            this.CalculationTab.CostItemSelectionChanged += CheckMaterialSelectedOrChanged;
            this.CalculationTab.CostItemChanged += CheckMaterialSelectedOrChanged;

            //--Register main logic if user presses the "Update Field" button
            inputHelpView.UpdateFieldButtonClicked += UpdateFieldButtonClicked;
        }

        private void CheckMaterialSelectedOrChanged(object sender, ICostItemEventArgs e)
        {
           if( e.CostItem.ItemCategory == ItemCategory.Material)
            {
                //--Set material selected property to true
                viewModel.MaterialSelected = true;
                //--Update the field value every time the users changes the selection
                viewModel.FieldValue = this.CalculationTab?.SelectedCostItemInActiveVersion?.Material?.Id;
            }
            else
            {
                //--Set material selected property to false
                viewModel.MaterialSelected = false;
                //--No material so, clear the field
                viewModel.FieldValue = "";
            }
        }

        private void UpdateFieldButtonClicked(object sender, EventArgs e)
        {
            //--Check if new material no. is selected
            if (  viewModel?.SelectedInputValue?.Content != null &&
                  this.CalculationTab?.SelectedCostItemInActiveVersion?.Material?.Id != (string)viewModel?.SelectedInputValue?.Content)
            {
                //--Build new material object
                var newMaterial = Material.CreateBuilder((string)viewModel.SelectedInputValue.Content).Build();
                //--Set new material object locally
                this.CalculationTab.SelectedCostItemInActiveVersion.Material = newMaterial;
                //--Update material on backend
                this.CalculationTab.SelectedCostItemInActiveVersion.UpdateOnBackendAsync();
            }
            else
            {             
                //--No new material selected
                new Message(
                    Texts.MsgNoMaterialSelected,
                    MessageType.Warning
                ).Show();              
            }
        }
    }
}
