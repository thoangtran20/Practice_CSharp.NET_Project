using ExportAppDemo.Models;
using ExportAppDemo.Repository;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Table;

namespace ExportAppDemo.Controllers
{
    public class ReportController : Controller
    {
        readonly IReporting _IReporting;
        public ReportController(IReporting ireporting)
        {
            _IReporting = ireporting;
        }
        [HttpGet]
        public IActionResult DownloadReport()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DownloadReport(IFormCollection obj)
        {
            string reportname = $"User_Wise_{Guid.NewGuid():N}.xlsx";
            var list = _IReporting.GetUserwiseReport();
            if (list.Count > 0)
            {
                var exportbytes = ExportToExcel<UserMasterViewModel>(list, reportname);
                return File(exportbytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", reportname);
            }
            else
            {
                TempData["Message"] = "No Data to Export";
                return View();
            }
        }

        private byte[] ExportToExcel<T>(List<T> table, string filename)
        {
            using ExcelPackage package = new ExcelPackage();
            ExcelWorksheet ws = package.Workbook.Worksheets.Add(filename);
            ws.Cells["A1"].LoadFromCollection(table, true, TableStyles.Light1);
            return package.GetAsByteArray();
        }
    }
}
