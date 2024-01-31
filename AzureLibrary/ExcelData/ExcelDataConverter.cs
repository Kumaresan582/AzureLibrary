using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace AzureLibrary.ExcelData
{
    public class ExcelDataConverter
    {
        public Task<ExcelPackage> ConvertJsonFilesToExcel(string filePath)
        {
            return ConvertJsonDataToExcel(filePath);
        }

        public Task<ExcelPackage> ConvertJsonFilesToExcel(List<IFormFile> formFiles)
        {
            return ConvertJsonDataToExcel(null, formFiles);
        }

        public static async Task<ExcelPackage> ConvertJsonDataToExcel(string filePath = null, List<IFormFile> formFiles = null)
        {
            #region ConvertJsonDataToExcel

            var data = new List<Dictionary<string, object>>();

            try
            {
                if (!string.IsNullOrEmpty(filePath))
                {
                    string jsonContent = System.IO.File.ReadAllText(filePath);
                    data = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonContent);
                }

                if (formFiles != null && formFiles.Any())
                {
                    foreach (var formFile in formFiles)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await formFile.CopyToAsync(memoryStream);
                            memoryStream.Position = 0;

                            string content = new StreamReader(memoryStream).ReadToEnd();
                            data.AddRange(JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(content));
                        }
                    }
                }

                if (data == null || data.Count == 0)
                {
                    return null;
                }

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                ExcelPackage excelPackage = new ExcelPackage();
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");

                int headerRowIndex = 1;
                int columnIndex = 1;

                var allKeys = data.SelectMany(dict => dict.Keys).Distinct();

                foreach (var key in allKeys)
                {
                    worksheet.Cells[headerRowIndex, columnIndex].Value = key.ToUpper();
                    columnIndex++;
                }

                int dataRowIndex = headerRowIndex + 1;

                foreach (var dict in data)
                {
                    columnIndex = 1;
                    foreach (var key in allKeys)
                    {
                        var value = dict.ContainsKey(key) ? dict[key] : null;

                        worksheet.Cells[dataRowIndex, columnIndex].Value = value;
                        columnIndex++;
                    }
                    dataRowIndex++;
                }
                worksheet.Cells.AutoFitColumns();

                return excelPackage;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the data.", ex);
            }

            #endregion ConvertJsonDataToExcel
        }
    }
}