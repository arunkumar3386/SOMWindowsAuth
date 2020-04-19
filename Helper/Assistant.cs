//using ExpertPdf.HtmlToPdf;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
//using System.Runtime.Serialization.Json;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;

namespace Helper
{
    public static class Assistant
    {
        public static string LoadDatabaseByBankName()
        {
            return "";
        }

        public static string GetFileNameFromFilePath(string filename)
        {
            if (filename.Contains("\\"))
            {
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);
            }

            return filename;
        }

        public static string DateFormater(string date)
        {
            if (!string.IsNullOrEmpty(date))
            {
                string text = DateTime.ParseExact(date, "yyyyMMdd",
                            CultureInfo.InvariantCulture).ToString("yyyy-MMM-dd");

                return String.Join("-", text.Split('-').Reverse().ToArray());
            }
            return date;
        }

        public static string TimeFormater(string time)
        {
            if (!string.IsNullOrEmpty(time))
            {
                return time.Substring(0, 2) + ":" + time.Substring(2, 2) + ":" + time.Substring(4, 2);
            }
            return time;
        }

        public static string DateDifferenceByDays(string SourceDate)
        {
            DateTime sDt = DateTime.Now;
            DateTime eDt = DateTime.ParseExact(SourceDate, "yyyyMMdd", CultureInfo.InvariantCulture);
            return (eDt.Date - sDt.Date).Days + " Days";
        }

        public static DataTable GetDataTableFromExcel(string path, bool hasHeader = true)
        {
            DataTable tbl = new DataTable();
            try
            {
                using (var pck = new OfficeOpenXml.ExcelPackage())
                {
                    using (var stream = File.OpenRead(path))
                    {
                        pck.Load(stream);
                    }
                    var ws = pck.Workbook.Worksheets.First();

                    foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                    {
                        tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column{0}", firstRowCell.Start.Column));
                    }
                    var startRow = hasHeader ? 2 : 1;
                    for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                    {
                        var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                        DataRow row = tbl.Rows.Add();
                        foreach (var cell in wsRow)
                        {
                            row[cell.Start.Column - 1] = cell.Text;
                        }
                    }
                    return tbl;
                }
            }
            catch (Exception ex)
            {
                return tbl;
            }

        }

        public static IEnumerable<SelectListItem> convertListToIEnumerable(List<string> data, bool addSelect)
        {
            var selectList = new List<SelectListItem>();

            if (addSelect)
            {
                selectList.Add(new SelectListItem
                {
                    Value = "-1",
                    Text = "-- Select --"
                });
            }
            foreach (var element in data)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element,
                    Text = element.Replace("[", "").Replace("]", "")
                });
            }

            return selectList;
        }

        public static bool CheckEmaillstIsValid(string emailAddress)
        {
            bool result = false;
            if (string.IsNullOrEmpty(emailAddress))
            {
                result = true;
            }
            else
            {
                if (emailAddress.Contains(";"))
                {
                    var data = emailAddress.Split(';').ToList();
                    foreach (var item in data)
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(item))
                            {
                                var mail = new MailAddress(item);
                                result = true;
                            }
                        }
                        catch
                        {
                            result = false;
                        }
                    }
                }
                else
                {
                    try
                    {
                        var mail = new MailAddress(emailAddress);
                        result = true;
                    }
                    catch (Exception)
                    {

                        result = false;
                    }
                }
            }
            return result;
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static DataSet ToDataSet<T>(this IList<T> list)
        {
            Type elementType = typeof(T);
            DataSet ds = new DataSet();
            DataTable t = new DataTable();
            ds.Tables.Add(t);

            //add a column to table for each public property on T
            foreach (var propInfo in elementType.GetProperties())
            {
                Type ColType = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;

                t.Columns.Add(propInfo.Name, ColType);
            }

            //go through each property on T and add each value to the table
            foreach (T item in list)
            {
                DataRow row = t.NewRow();

                foreach (var propInfo in elementType.GetProperties())
                {
                    row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value;
                }

                t.Rows.Add(row);
            }

            return ds;
        }

        public static string GetCellEndName(int wscolCount)
        {
            string[] arr1 = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            string value = string.Empty;

            if (wscolCount > 26)
            {
                wscolCount = 26;
            }


            //Find Total Column Width
            for (int m = 0; m <= (wscolCount - 1); m++)
            {
                value = arr1[m] + "1";
            }
            return value;
        }

        public static bool DatasetToExcelExport(DataSet ds, string fPath, string fName, int Count, string orgin)
        {
            bool result = false;
            try
            {
                if (!Directory.Exists(fPath))
                {
                    Directory.CreateDirectory(fPath);
                }
                using (ExcelPackage pck = new ExcelPackage())
                {
                    foreach (DataTable dataTable in ds.Tables)
                    {
                        ExcelWorksheet workSheet = pck.Workbook.Worksheets.Add(dataTable.TableName);
                        workSheet.Cells["A1"].LoadFromDataTable(dataTable, true);
                        workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();

                        if (orgin == "AccountStautsCode")
                        {
                            workSheet.Column(2).Width = 100;
                            workSheet.Column(2).Style.WrapText = true;
                            workSheet.Column(4).Width = 24;
                        }
                        else if (orgin == "BIC")
                        {
                            workSheet.Column(1).Width = 20;
                            workSheet.Column(1).Style.WrapText = true;
                            workSheet.Column(2).Width = 20;
                            workSheet.Column(2).Style.WrapText = true;
                            workSheet.Column(3).Width = 20;
                            workSheet.Column(3).Style.WrapText = true;
                            workSheet.Column(4).Width = 20;
                            workSheet.Column(4).Style.WrapText = true;
                        }
                        else if (orgin == "CorpAccount")
                        {
                            workSheet.Column(1).Width = 22;
                            workSheet.Column(1).Style.WrapText = true;
                        }
                        else if (orgin == "POAAccount")
                        {
                            workSheet.Column(1).Width = 22;
                            workSheet.Column(1).Style.WrapText = true;
                        }
                        else if (orgin == "Product")
                        {
                            workSheet.Column(2).Width = 27;
                            workSheet.Column(2).Style.WrapText = true;
                            workSheet.Column(2).Width = 18;
                            workSheet.Column(2).Style.WrapText = true;
                            workSheet.Column(3).Width = 30;
                            workSheet.Column(3).Style.WrapText = true;
                        }
                        else if (orgin == "SortCode")
                        {
                            workSheet.Column(1).Width = 15;
                            workSheet.Column(1).Style.WrapText = true;
                        }
                        else if (orgin == "ExcRptExcludeSts")
                        {
                            workSheet.Column(1).Width = 50;
                            workSheet.Column(1).Style.WrapText = true;
                            workSheet.Column(2).Width = 150;
                            workSheet.Column(2).Style.WrapText = true;
                        }
                        else if (orgin == "ExceptionReport")
                        {
                            workSheet.Column(1).Width = 50;
                            workSheet.Column(1).Style.WrapText = true;
                            workSheet.Column(2).Width = 150;
                            workSheet.Column(2).Style.WrapText = true;
                            workSheet.Column(3).Width = 11;
                            workSheet.Column(3).Style.WrapText = true;
                        }
                        else if (orgin == "ExcpSummary")
                        {
                            workSheet.Column(3).Width = 12;
                            workSheet.Column(3).Style.WrapText = true;
                            workSheet.Column(4).Width = 41;
                            workSheet.Column(4).Style.WrapText = true;
                            workSheet.Column(5).Width = 41;
                            workSheet.Column(5).Style.WrapText = true;
                        }


                        string val = GetCellEndName(Count);
                        string cellValue = "A1:" + val;
                        using (ExcelRange Rng = workSheet.Cells[cellValue])
                        {
                            Rng.Style.Font.Size = 13;
                            Rng.Style.Font.Bold = true;
                            Rng.Style.Font.Color.SetColor(Color.White);
                            //Rng.Style.Font.UnderLine = true;
                            Rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            Rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(76, 181, 245));
                            //Rng.Style.WrapText = true;
                            //Rng.Style.ShrinkToFit = true;
                        }

                    }

                    pck.SaveAs(new FileInfo(fPath + "" + fName));
                }
                result = true;
            }
            catch (Exception ex)
            {
                return result;
            }
            return result;
        }

        public static string CreateDirectoryIfNotExists(string fPath)
        {
            if (!Directory.Exists(fPath))
            {
                Directory.CreateDirectory(fPath);
            }
            return fPath;
        }

        public static string FormatedDateTime()
        {
            DateTime now = DateTime.Now;
            return now.ToString("ddMMMyyyyHHmmss");
        }

        public static string GetMonthFromCurrentDate()
        {
            DateTime now = DateTime.Now;
            return now.ToString("MMM");
        }

        public static string GetYearFromCurrentDate()
        {
            DateTime now = DateTime.Now;
            return now.ToString("yyyy");
        }

        public static string ReplaceSnglQote(string strTxt)
        {
            string retTxt = string.Empty;
            if (strTxt != null && strTxt.Length > 0)
            {
                retTxt = strTxt.Replace("'", " ");
            }
            else
            {
                retTxt = string.Empty;
            }
            return retTxt;
        }

        public static string Encrypt(string sA)
        {
            string sEncrypt = "";

            for (int nIntI = 0; nIntI <= sA.Length - 1; nIntI++)
            {
                int i = Convert.ToInt32(System.Convert.ToChar(sA.Substring(nIntI, 1)));
                char c = Convert.ToChar(i + 5);
                sEncrypt = sEncrypt + c;
            }

            return sEncrypt;
        }

        public static string Decrypt(string sA)
        {
            string sDecrypt = "";

            for (int nIntI = 0; nIntI <= sA.Length - 1; nIntI++)
            {
                int i = Convert.ToInt32(System.Convert.ToChar(sA.Substring(nIntI, 1)));
                char c = Convert.ToChar(i - 5);
                sDecrypt = sDecrypt + c;
            }

            return sDecrypt;
        }

        public static string ReplaceText(string Source, string Find, string Replace)
        {
            int Place = Source.IndexOf(Find);
            string result = Source.Remove(Place, Find.Length).Insert(Place, Replace);
            return result;
        }

        public static string ToXML(Object oObject)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlSerializer xmlSerializer = new XmlSerializer(oObject.GetType());
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, oObject);
                xmlStream.Position = 0;
                xmlDoc.Load(xmlStream);
                return xmlDoc.InnerXml;
            }
        }

        public static Object XMLToObject(string XMLString, Object oObject)
        {
            XmlSerializer oXmlSerializer = new XmlSerializer(oObject.GetType());
            oObject = oXmlSerializer.Deserialize(new StringReader(XMLString));
            return oObject;
        }

        public static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        //public static void ConvertHTML2PDF(string content, string path, string fileName)
        //{
        //    string PDFBody = "";
        //    PDFBody = File.ReadAllText(@"C:\Users\Arunkumar.S\source\repos\arunkumar3386\SOM\StarOfTheMonth\StarOfTheMonth\HTMLFiles\SOM_New.mht");
        //    //var htmlContent = String.Format("<body>Hello world: {0}</body>", DateTime.Now);
        //    var htmlContent = PDFBody;
        //    var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
        //    //htmlToPdf.GeneratePdf(htmlContent, "", "export.pdf");
        //    string _path = @"C:\Users\Arunkumar.S\source\repos\arunkumar3386\SOM\StarOfTheMonth";
        //    GeneratePDF(PDFBody, ref _path, "SOM");

        //    //htmlToPdf.GeneratePdf((pdfBytes, null, "export.pdf");
        //}

        //public static bool GeneratePDF(string PDFBody, ref string strOutPdfFilePath, string strOutFileNamePrefix)
        //{
        //    //Create PDF with Password
        //    PdfConverter objPDFConverter = new PdfConverter();
        //    objPDFConverter.LicenseKey = "ACsyIDggNTMgMTEuMCAzMS4xMi45OTk5";
        //    objPDFConverter.PdfSecurityOptions.KeySize = EncryptionKeySize.EncryptKey128Bit;
        //    objPDFConverter.PdfSecurityOptions.UserPassword = "";
        //    objPDFConverter.PdfSecurityOptions.OwnerPassword = "";

        //    // set the converter options - optional
        //    //objPDFConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4;
        //    //objPDFConverter.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait;
        //    //objPDFConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal;

        //    strOutPdfFilePath = strOutPdfFilePath + strOutFileNamePrefix + "_" + DateTime.Today.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("HHmmss") + ".pdf"; ;

        //    MemoryStream ms = new MemoryStream();

        //    objPDFConverter.SavePdfFromHtmlStringToStream(PDFBody, ms);


        //    MemoryStream pdfstream = new MemoryStream(ms.ToArray());

        //    //objPDFConverter.SavePdfFromHtmlStringToFile(PDFBody, strOutPdfFilePath);

        //    //write to file
        //    FileStream objPDFFile = new FileStream(strOutPdfFilePath, FileMode.Create, FileAccess.Write);
        //    pdfstream.WriteTo(objPDFFile);
        //    objPDFFile.Close();


        //    ms.Close();
        //    pdfstream.Close();

        //    //msg.Attachments.Add(New Attachment(pdfstream, PDFFileName & ".pdf"))

        //    return true;
        //}

        public static string DateConversion(string date)
        {
            var dt = DateTime.ParseExact(date, "ddMMMyy", CultureInfo.InvariantCulture);
            return dt.ToString("dd/MM/yyyy");
        }

        public static string SOMDbToUIDateConversion (string date) //ddMMYYYY to dd/MM/yyyy
        {
            //sText = sText.Substring(6, 2) + "-" + sText.Substring(4, 2) + "-" + sText.Substring(0, 4);
            date = date.Substring(0, 2) + "/" + date.Substring(2, 2) + "/" + date.Substring(4, 4);
            return date;
        }

        public static string SOMDbToUIDateConversion_New(string date) //ddMMYYYY HHmmss to dd/MM/yyyy
        {
            if (string.IsNullOrEmpty(date))
            {
                return date;
            }
            string[] v1 = date.Split(' ');
            date = v1[0];
            //sText = sText.Substring(6, 2) + "-" + sText.Substring(4, 2) + "-" + sText.Substring(0, 4);
            date = date.Substring(0, 2) + "/" + date.Substring(2, 2) + "/" + date.Substring(4, 4);
            return date;
        }

        public static string SOMDbToUIDateConversionForPopup(string date) //ddMMYYYY to dd/MM/yyyy
        {
            //sText = sText.Substring(6, 2) + "-" + sText.Substring(4, 2) + "-" + sText.Substring(0, 4);
            date = date.Substring(4, 4) + "-" + date.Substring(2, 2) + "-" + date.Substring(0, 2);
            return date;
        }

        public static string SOMDateConversionFrom_UIToDb(DateTime dt)
        {
            DateTime _dtsDate = (DateTime)dt;
            return _dtsDate.ToString("dd/MM/yyyy").Replace("-", "");
        }

        public static DateTime stringToDatePickerUI(string sDate)
        {
            sDate = sDate.Substring(0, 2) + "/" + sDate.Substring(2, 2) + "/" + sDate.Substring(4, 4);
            return DateTime.Parse(sDate);
        }

        public static DateTime SOMDbToDateTimePicker(string sDate)
        {
            sDate = sDate.Substring(2, 2) + "/" + sDate.Substring(0, 2) + "/" + sDate.Substring(4, 4);
            return DateTime.Parse(sDate);
        }

        public static string SOMDbToDateTimePickerAsString(string sDate)
        {
            if (!string.IsNullOrEmpty(sDate))
            {
                sDate = sDate.Substring(0, 2) + "/" + sDate.Substring(2, 2) + "/" + sDate.Substring(4, 4);
            }
            return sDate;
        }

        public static string GetCurrentDateTime()
        {
            DateTime dt = DateTime.Now;
            return dt.ToString("ddMMyyyyTHHmmss");
        }

        public static DateTime SOMDbToDateTimeForFilter(string sDate, bool start)
        {
            if (!string.IsNullOrEmpty(sDate))
            {
                string val = sDate.Substring(2, 3);
                string month = string.Empty;
                string date = string.Empty;
                if (val == "Jan")
                {
                    month = "01";
                    if (start)
                    {
                        date = "01";
                    }
                    else
                    {
                        date = "31";
                    }
                }
                if (val == "Feb" )
                {
                    month = "02";
                    if (start)
                    {
                        date = "01";
                    }
                    else
                    {
                        date = "28";
                    }
                }
                if (val == "Mar" )
                {
                    month = "03";
                    if (start)
                    {
                        date = "01";
                    }
                    else
                    {
                        date = "31";
                    }
                }
                if (val == "Apr" )
                {
                    month = "04";
                    if (start)
                    {
                        date = "01";
                    }
                    else
                    {
                        date = "30";
                    }
                }
                if (val == "May" )
                {
                    month = "05";
                    if (start)
                    {
                        date = "01";
                    }
                    else
                    {
                        date = "31";
                    }
                }
                if (val == "Jun" )
                {
                    month = "06";
                    if (start)
                    {
                        date = "01";
                    }
                    else
                    {
                        date = "30";
                    }
                }
                if (val == "Jul" )
                {
                    month = "07";
                    if (start)
                    {
                        date = "01";
                    }
                    else
                    {
                        date = "31";
                    }
                }
                if (val == "Aug" )
                {
                    month = "08";
                    if (start)
                    {
                        date = "01";
                    }
                    else
                    {
                        date = "31";
                    }
                }
                if (val == "Sep" )
                {
                    month = "09";
                    if (start)
                    {
                        date = "01";
                    }
                    else
                    {
                        date = "30";
                    }
                }
                if (val == "Oct" )
                {
                    month = "10";
                    if (start)
                    {
                        date = "01";
                    }
                    else
                    {
                        date = "31";
                    }
                }
                if (val == "Nov" )
                {
                    month = "11";
                    if (start)
                    {
                        date = "01";
                    }
                    else
                    {
                        date = "30";
                    }
                }
                if (val == "Dec" )
                {
                    month = "12";
                    if (start)
                    {
                        date = "01";
                    }
                    else
                    {
                        date = "31";
                    }
                }
                sDate = month + "/" + date + "/" + sDate.Substring(5, 4);
            }
            return DateTime.Parse(sDate);
        }

        public static IEnumerable<SelectListItem> LoadListofMonth(string selectedMonth)
        {
            List<SelectListItem> lstSelect = new List<SelectListItem>();
            var months = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
            foreach (var item in months)
            {
                SelectListItem objSelectListItem = new SelectListItem();
                string _month = item.Substring(0, 3);
                objSelectListItem.Text = _month;//its displayed in UI
                objSelectListItem.Value = _month;
                if (!string.IsNullOrEmpty(selectedMonth) && selectedMonth == _month)
                {
                    objSelectListItem.Selected = true;
                }
                lstSelect.Add(objSelectListItem);
            }
            return lstSelect;
        }

        public static IEnumerable<SelectListItem> LoadListofFutureYears(int count, string SelectedYear)
        {
            List<SelectListItem> lstSelect = new List<SelectListItem>();
            var currentYear = DateTime.Today.Year;
            for (int i = count; i >= 0; i--)
            {
                SelectListItem objSelectListItem = new SelectListItem();
                string _year = (currentYear - i).ToString();
                objSelectListItem.Text = _year;//its displayed in UI
                objSelectListItem.Value = _year;
                if (!string.IsNullOrEmpty(SelectedYear) && SelectedYear == _year)
                {
                    objSelectListItem.Selected = true;
                }
                lstSelect.Add(objSelectListItem);
            }
            return lstSelect;
        }




    }



}
