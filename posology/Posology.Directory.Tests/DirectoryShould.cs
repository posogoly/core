using Posology.Core;
using Xunit;
using System.Linq;

namespace Posology.Directory.Tests
{
    public class DirectoryShould
    {

        internal static void VerifyComponentData(IDrugComponent firstActualComponent, IDrugComponent firstExpectedComponent)
        {
            
            Assert.Equal(firstExpectedComponent.ComponentName, firstActualComponent.ComponentName);
            Assert.Equal(firstExpectedComponent.ComponentId, firstActualComponent.ComponentId);
            Assert.Equal(firstExpectedComponent.ComponentAmount, firstActualComponent.ComponentAmount);
            Assert.Equal(firstExpectedComponent.ComponentAmountUnit, firstActualComponent.ComponentAmountUnit);
            Assert.Equal(firstExpectedComponent.ComponentType, firstActualComponent.ComponentType);
            Assert.Equal(firstExpectedComponent.DrugShape, firstActualComponent.DrugShape);
        }

        internal static void VerifyDrugData(IDrug actual, IDrug expectedResult)
        {
            Assert.Equal(expectedResult.Denomination, actual.Denomination);
            Assert.Equal(expectedResult.AdministrationType, actual.AdministrationType);
            Assert.Equal(expectedResult.AutorisationStatus, actual.AutorisationStatus);
            Assert.Equal(expectedResult.DrugType, actual.DrugType);
            Assert.Equal(expectedResult.UnkownNumber, actual.UnkownNumber);
            Assert.Equal(expectedResult.InternalIdentifier, actual.InternalIdentifier);
        }

        internal static void VerifyPackageData(IDrugPackaging actual, IDrugPackaging expectedResult)
        {
            Assert.Equal(expectedResult.Barcode, actual.Barcode);
            Assert.Equal(expectedResult.Description, actual.Description);
            Assert.Equal(expectedResult.InternalDrugIdentifier, actual.InternalDrugIdentifier);
            Assert.Equal(expectedResult.Status, actual.Status);
            Assert.Equal(expectedResult.CommercialisationDate, actual.CommercialisationDate);
            Assert.Equal(expectedResult.CommercialisationStatus, actual.CommercialisationStatus);
            Assert.Equal(expectedResult.Id, actual.Id);
        }
    }
}
