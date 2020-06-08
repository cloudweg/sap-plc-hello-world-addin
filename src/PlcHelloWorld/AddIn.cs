using Sap.Plc.AddIn.AddInView;
using Sap.Plc.AddIn.AddInView.UserInterface.Messaging;
using System.AddIn;
using System.AddIn.Pipeline;

namespace PlcHelloWorld
{
    [AddIn("PlcHelloWorld", Description = "Hello World PLC Add-In", Publisher = "SAP Deutschland SE & Co KG", Version = "0.0.0.1")]
    [QualificationData(AddInConstants.AddInGuid, "4950A53C-DAEC-4E55-A3D3-E7A2757DAB32")]

    public partial class AddIn : AddInBase
    {
        public override bool Setup()
        {
            try
            {
                //--Setup all elements
                this.SetupSidePanel();
            }
            catch(System.Exception e)
            {
                //--Error occured...
                new Message(e.Message, MessageType.Error).Show();
                return false;
            }

            //--After successful initialization we return true
            return true;
        }
    }
}
