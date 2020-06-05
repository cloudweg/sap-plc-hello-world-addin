using Sap.Plc.AddIn.AddInView;
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
            //--Setup the ribbons
            this.SetupRibbon();

            //--After successful initialization we return true
            return true;
        }
    }
}