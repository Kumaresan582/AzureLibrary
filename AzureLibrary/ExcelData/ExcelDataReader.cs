using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureLibrary.ExcelData
{
    public class ExcelDataReader
    {
        public Task<List<Dictionary<string, object>>> ReadExcelData(string filePath)
        {
            return ExcelData(filePath);
        }

        public Task<List<Dictionary<string, object>>> ReadExcelData(List<IFormFile> formFiles)
        {
            return ExcelData(null, formFiles);
        }

        public static async Task<List<Dictionary<string, object>>> ExcelData(string filepath = null, List<IFormFile> formFiles = null)
        {
            var jsonData = new List<Dictionary<string, object>>();

            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var excelPackage = new ExcelPackage(new FileInfo(filepath)))
                {
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();

                    if (worksheet == null)
                    {
                        throw new Exception("No worksheet found in the Excel file.");
                    }

                    int rowCount = worksheet.Dimension.Rows;
                    int columnCount = worksheet.Dimension.Columns;

                    if (rowCount < 2)
                    {
                        throw new Exception("Excel file Only a column name.");
                    }

                    var columnNames = new List<string>();

                    for (int column = 1; column <= columnCount; column++)
                    {
                        var cellValue = worksheet.Cells[2, column].Value;
                        string columnName = cellValue != null ? cellValue.ToString() : string.Empty;
                        columnNames.Add(columnName);
                    }

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var rowData = new Dictionary<string, object>();

                        for (int column = 1; column <= columnCount; column++)
                        {
                            string columnName = columnNames[column - 1];
                            object cellValue = worksheet.Cells[row, column].Value;
                            rowData[columnName] = cellValue;
                        }

                        jsonData.Add(rowData);
                    }
                }

                return jsonData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error reading Excel data: " + ex.Message);
            }
        }
    }
}
