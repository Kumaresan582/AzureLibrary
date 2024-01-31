using AzureLibrary.ExcelData;
using Microsoft.AspNetCore.Http;

namespace Excel
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            var excelDataconvert = new ExcelDataConverter();

            string filePath = @"E:\Kumaresan\GaneshAnnaTask\JSON_File\batch-account-update_us.json";
            string outputFilePath = "E:\\Kumaresan\\GaneshAnnaTask\\AzureLibrary\\ExcelFile\\output.xlsx";

            try
            {
                var excelPackageFromFile = await excelDataconvert.ConvertJsonFilesToExcel(filePath);

                if (excelPackageFromFile != null)
                    excelPackageFromFile.SaveAs(new FileInfo(outputFilePath));
                else
                    Console.WriteLine("Error");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred with the file path: {ex.Message}");
            }

            // ReadExcelData
            ExcelDataReader dataReader = new ExcelDataReader();

            try
            {
                var excelData = await dataReader.ReadExcelData(outputFilePath);

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}