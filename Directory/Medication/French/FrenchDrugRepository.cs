﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directory.Medication.French
{
    public class FrenchDrugRepository:IDrugRepository
    {
        private const string DrugHeaderDetails = "CIS.txt";
        private const string DrugInfoWithBarcodes = "CIS_CIP.txt";
        private const string DrugCompositions = "COMPO.txt";
        private readonly string _rootDirectory;
        private readonly string _path;

        public FrenchDrugRepository(string rootDirectory, string path)
        {
            _rootDirectory = rootDirectory;
            _path = path;
        }

        public async Task<IDrugPackaging> GetBy(string barCode)
        {
            //todo move files into blobs in azure?

            var drugPackage = await GetDataFromPackageInfoFile(DrugInfoWithBarcodes, barCode);
            await AddDataFromDrugFile(DrugHeaderDetails, drugPackage);
            await AddDataFromDrugCompositionFile(DrugCompositions, drugPackage);

            //todo add found drug-package to cache

            return drugPackage;

        }
        
        private async Task<IDrugPackaging> GetDataFromPackageInfoFile(string filePath, string barCode)
        {
            var fileContent = await FileHelper.ReadAllLinesAsync(_rootDirectory, _path, filePath, Encoding.UTF8);
            var row = fileContent.FirstOrDefault(line => line.Contains(barCode));
            if (row == null) throw new KeyNotFoundException();
            var drugDetails = row.Split('\t');

            var package = new FrenchDrugPackaging(drugDetails[0])
            {
                Id = drugDetails[1],
                Description = drugDetails[2],
                Status = drugDetails[3],
                CommercialisationStatus = drugDetails[4],
                CommercialisationDate = drugDetails[5],
                Barcode = drugDetails[6]
            };
            return package;
        }

        private async Task AddDataFromDrugFile(string filePath, IDrugPackaging drugPackage)
        {
            var fileContent = await FileHelper.ReadAllLinesAsync(_rootDirectory, _path, filePath, Encoding.UTF8);
            var row = fileContent.FirstOrDefault(line => line.Contains(drugPackage.InternalDrugIdentifier));
            if (row != null)
            {
                var drugDetails = row.Split('\t');

                var drug = new FrenchDrug
                {
                    InternalIdentifier = drugDetails[0],
                    Denomination = drugDetails[1],
                    DrugType = drugDetails[2],
                    AutorisationStatus = drugDetails[4],
                    AdministrationType = drugDetails[3],
                    NoticeDocumentId = drugDetails[7]
                };
                drugPackage.Drug = drug;
            }
        }

        private async Task AddDataFromDrugCompositionFile(string filePath, IDrugPackaging package)
        {
            var fileContent = await FileHelper.ReadAllLinesAsync(_rootDirectory, _path, filePath, Encoding.UTF8);

            foreach (string row in fileContent.Where(line => line.Contains(package.InternalDrugIdentifier)))
            {
                var drugDetails = row.Split('\t');

                var component = new FrenchDrugComponent(drugDetails[0])
                {
                    DrugShape = drugDetails[1],
                    ComponentId = drugDetails[2],
                    ComponentName = drugDetails[3],
                    ComponentAmount = drugDetails[4],
                    ComponentAmountUnit = drugDetails[5],
                    ComponentType = drugDetails[6],
                };
                //todo remove line 112 once refactoring complete
                package.AddComponent(component);
                package.Drug.AddComponent(component);
            }
        }
    }
}