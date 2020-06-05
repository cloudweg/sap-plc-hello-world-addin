using Sap.Plc.AddIn.AddInView.UserInterface;
using Sap.Plc.AddIn.AddInView.UserInterface.Messaging;
using System;

namespace PlcHelloWorld
{
    public partial class AddIn
    {
        /// <summary>
        /// Setup ribbon entries
        /// </summary>
        private void SetupRibbon()
        {
            //--Define ribbon
            RibbonEntry helloWorldRibbon = new RibbonEntry(
                RibbonCategory.Other,        //--Where ore button is displayed
                Texts.ShowHelloWorldMessage  //--Name of our button
            );

            //--Attach ribbon to PLC
            this.CalculationTab.RibbonEntries.Add(helloWorldRibbon);

            //--Register ribbon click event
            helloWorldRibbon.Clicked += OnHelloWorldButtonClicked;
        }
        /// <summary>
        /// Show "hello world" message when clicking the button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnHelloWorldButtonClicked(object sender, EventArgs e)
        {            
            new Message(
                Texts.HelloWorldMessage,    //--Message text
                MessageType.Success         //--Message type: info, success, error, warning
            ).Show();
        }
    }
}
