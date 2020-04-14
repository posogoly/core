using Posology.Core;
using Xunit;
using System.Linq;

namespace Posology.Directory.Tests
{
    public class DirectoryShould
    {

        internal static void VerifyComponentData(IDrugPackaging actual, IDrugPackaging expectedResult)
        {
            Assert.Equal(expectedResult.Components.Count(), actual.Components.Count());
            var firstExpectedComponent = expectedResult.GetMainComponent();
            var firstActualComponent = actual.GetMainComponent();
            Assert.Equal(firstExpectedComponent.ComponentName, firstActualComponent.ComponentName);
            Assert.Equal(firstExpectedComponent.ComponentId, firstActualComponent.ComponentId);
            Assert.Equal(firstExpectedComponent.ComponentAmount, firstActualComponent.ComponentAmount);
            Assert.Equal(firstExpectedComponent.ComponentAmountUnit, firstActualComponent.ComponentAmountUnit);
            Assert.Equal(firstExpectedComponent.ComponentType, firstActualComponent.ComponentType);
            Assert.Equal(firstExpectedComponent.DrugShape, firstActualComponent.DrugShape);
        }

        internal static void VerifyDrugData(IDrugPackaging actual, IDrugPackaging expectedResult)
        {
            Assert.Equal(expectedResult.Drug.Denomination, actual.Drug.Denomination);
            Assert.Equal(expectedResult.Drug.AdministrationType, actual.Drug.AdministrationType);
            Assert.Equal(expectedResult.Drug.AutorisationStatus, actual.Drug.AutorisationStatus);
            Assert.Equal(expectedResult.Drug.DrugType, actual.Drug.DrugType);
            Assert.Equal(expectedResult.Drug.UnkownNumber, actual.Drug.UnkownNumber);
            Assert.Equal(expectedResult.Drug.InternalIdentifier, actual.Drug.InternalIdentifier);
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
