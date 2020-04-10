using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Helper
{
    public class Methods
    {
        public string connString = string.Empty;
        private SqlConnection sqlConn;
        private DataSet _ds;
        private SqlDataAdapter _da;
        private DataTable _dt = new DataTable();
        private DataTable _dt1 = new DataTable();
        private DataTable _dt2 = new DataTable();
        private DataRow _dr;
        private string _fSelect, _fSelect1;

        public string StrUser = string.Empty;
        public string StrProjName = "";

        private void OpenConnection()
        {
            //connString=System.Configuration.ConfigurationManager.ConnectionStrings["Constr"].ToString();
            connString = Assistant.LoadDatabaseByBankName();

            sqlConn = new SqlConnection(connString);
            if (sqlConn.State == ConnectionState.Closed)
            {
                sqlConn.Open();
            }
        }

        private void CloseConnection()
        {
            if (sqlConn.State == ConnectionState.Open)
            {
                sqlConn.Close();
                sqlConn.Dispose();
            }
        }

        public bool BulkInsertDataTable(string tableName, DataTable dataTable)
        {
            bool isSuccuss;
            tableName = "[Adhoc_" + tableName + "]";
            try
            {
                ArrayList dt_index = new ArrayList();
                List<DataRow> rowsToDelete = new List<DataRow>();
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    string valuesarr = string.Empty;
                    List<Object> lst = new List<Object>(dataTable.Rows[i].ItemArray);

                    foreach (Object s in lst)
                    {
                        valuesarr += s.ToString();
                    }

                    if (string.IsNullOrEmpty(valuesarr))
                    {
                        rowsToDelete.Add(dataTable.Rows[i]);
                    }
                }

                foreach (DataRow row in rowsToDelete)
                {
                    dataTable.Rows.Remove(row);
                }


                OpenConnection();
                SqlConnection SqlConnectionObj = sqlConn;

                using (var bulkCopy = new SqlBulkCopy(connString, SqlBulkCopyOptions.KeepIdentity))
                {
                    bulkCopy.BulkCopyTimeout = 0;
                    string createTable = "IF OBJECT_ID('" + tableName + "', 'U') IS NULL " + "CREATE TABLE " + tableName + "( ID INT IDENTITY(1,1) ,ADHOC_REMARKS NVARCHAR(MAX), ";
                    string dropTable = "IF OBJECT_ID('" + tableName + "', 'U') IS NOT NULL " + " DROP TABLE " + tableName;

                    ExecuteCommand(dropTable);

                    foreach (DataColumn col in dataTable.Columns)
                    {
                        bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                        createTable += "[" + col.ColumnName.ToString() + "]" + " NVARCHAR(MAX),";
                    }
                    createTable = createTable.TrimEnd(',');
                    createTable = createTable + ")";

                    ExecuteCommand(createTable);

                    //bulkCopy.BulkCopyTimeout = 600;
                    bulkCopy.DestinationTableName = tableName;
                    bulkCopy.WriteToServer(dataTable);
                }
                isSuccuss = true;
            }
            catch (Exception ex)
            {
                isSuccuss = false;
            }
            finally
            {
                CloseConnection();
            }
            return isSuccuss;
        }

        public string CTODate(string sText)
        {
            if (!string.IsNullOrEmpty(sText) && sText.Length > 7)
            {
                sText = sText.Substring(6, 2) + "-" + sText.Substring(4, 2) + "-" + sText.Substring(0, 4);
                return sText;
            }
            else
            {
                //string sToday = DateTime.Now.ToString("dd/MM/yyyy");
                //return sToday;
                return string.Empty;
            }
        }

        public string CTOTime(string sText)
        {
            if (!string.IsNullOrEmpty(sText) && sText.Length > 5)
            {
                sText = sText.Substring(0, 2) + ":" + sText.Substring(2, 2) + ":" + sText.Substring(4, 2);
                return sText;
            }
            else
            {
                //string sToday = DateTime.Now.ToString("dd/MM/yyyy");
                //return sToday;
                return string.Empty;
            }
        }

        public DataTable GetData(string strCmd)
        {
            try
            {

                OpenConnection();

                SqlCommand sqlComm;
                sqlComm = new System.Data.SqlClient.SqlCommand(strCmd, sqlConn);
                sqlComm.CommandTimeout = 0;
                _da = new SqlDataAdapter(sqlComm);
                _ds = new DataSet();
                _da.Fill(_ds);
                _dt = _ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return _dt;
        }

        public void ExecuteCommand(string command)
        {
            try
            {
                OpenConnection();

                SqlCommand strcmd;
                strcmd = new SqlCommand(command, sqlConn);
                strcmd.CommandType = CommandType.Text;
                strcmd.CommandTimeout = 0;
                strcmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        public string RetPolValue(string pmod, string ptype)
        {
            string strVal = string.Empty;
            string sqlCmd = "select POLDET from POLMTPF where pmodule='" + pmod.ToString().Trim() + "'" + " and ptype ='" + ptype.ToString().Trim() + "'";
            _dt = GetData(sqlCmd);

            if (_dt.Rows.Count > 0)
            {
                _dr = _dt.Rows[0];
                strVal = _dr["POLDET"].ToString().Trim();
            }

            return strVal;
        }

        public void UpdatePolValue(String pmod, String ptype, String PDet)
        {
            string strVal = string.Empty;

            try
            {
                string sqlCmd = "Update POLMTPF set POLDET='" + PDet + "' where pmodule='" + pmod.ToString().Trim() + "' and ptype ='" + ptype.ToString().Trim() + "'";
                ExecuteCommand(sqlCmd);
            }

            catch (Exception ex)
            { throw ex; }
        }

        public string NullToSpace(object txt)
        {
            string functionReturnValue = null;

            if (txt == null)
            {
                functionReturnValue = "";

            }
            else if (txt == null)
            {
                functionReturnValue = "";

            }
            else if (string.IsNullOrEmpty(txt.ToString()) | txt.ToString() == " ")
            {
                functionReturnValue = "";
            }
            else
            {
                functionReturnValue = txt.ToString().Trim();
            }
            return functionReturnValue;
        }

        public void StoreEvent(string strCode, string strFind, string strRplc, string UserName)
        {
            if (string.IsNullOrEmpty(UserName))
            {
                StrUser = "System";
            }
            else
            {
                StrUser = UserName;
            }

            StoreEvent(strCode, strFind, strRplc);
        }

        public void StoreEvent(string strCode, string strFind, string strRplc)
        {
            string Desc = string.Empty;
            if (strCode == "99999")
            {
                Desc = strRplc;
            }
            else if (strCode == "99998")
            {
                Desc = strRplc;
            }
            else if (strCode == "77777") // DP Error Code - Sucess Error Code
            {
                Desc = strRplc;
            }
            else if (strCode == "77778") // DP Error Code - Failure Error Code
            {
                Desc = strRplc;
            }
            else
            {
                Desc = GetAuditDesc(strCode).Replace(strFind, strRplc.Trim());
            }

            if (string.IsNullOrEmpty(Desc) == false)
            {
                Desc = Assistant.ReplaceSnglQote(Desc);
            }

            _fSelect = "Insert into AUDITREPF([Date],[Time],[Category],[Description],[User])Values('" + DateTime.Today.ToString("yyyyMMdd")
                + "','" + DateTime.Now.ToString("HHmmss") + "','" + strCode + "', '" + Desc + "','" + StrUser + "')";
            ExecuteCommand(_fSelect);
        }

        private string GetAuditDesc(string strCode)
        {
            string auditDesc = string.Empty;
            _fSelect1 = "SELECT DESCRIPTION FROM  AUDITMTPF WHERE CODE='" + strCode + "'";
            _dt1 = GetData(_fSelect1);

            if (_dt1.Rows.Count > 0)
            {
                auditDesc = _dt1.Rows[0]["DESCRIPTION"].ToString().Trim();
            }
            return auditDesc;
        }

        public void SendExceptionEmailMsg(string pMessage, string pSubject, string pAttachment)
        {
            try
            {
                //if (pSubject == null || pSubject == string.Empty || pSubject == "")
                //{
                //    pSubject = ConfigurationManager.AppSettings["ApplnNm"].ToString() + " - Exception";
                //}
                //ExceptionEmail MailObj = new ExceptionEmail();
                //MailObj.SendEmail(ConfigurationManager.AppSettings["Provider"], ConfigurationManager.AppSettings["constr"], ConfigurationManager.AppSettings["ApplnNm"], ConfigurationManager.AppSettings["EmailTo"], ConfigurationManager.AppSettings["SMTP"], ConfigurationManager.AppSettings["UserId"], ConfigurationManager.AppSettings["Password"], pMessage, pSubject, pAttachment, ConfigurationManager.AppSettings["HdrBgClr"], ConfigurationManager.AppSettings["HdrFntClr"]);

            }
            catch (Exception ex)
            {
                // Exception email thrown exception
                StoreEvent("99999", "@@@", "SCV automation - Exception email thrown an exception as " + ex.ToString().Replace("'", ""));
            }
        }

        public void WriteExceptionLog(string _SCVOutputFolder, Exception ex)
        {
            // Write Error details  ***************************************************************************************************
            string errorLogFileName = "SCV_FSCS_Validation_ErrorLog_" + DateTime.Today.ToString("yyyyMMdd") + ".txt";
            string errorLogFilePath = _SCVOutputFolder + "Log\\" + errorLogFileName;

            if (!System.IO.Directory.Exists(_SCVOutputFolder + "Log\\"))
            {
                System.IO.Directory.CreateDirectory(_SCVOutputFolder + "Log\\");
            }

            if (!System.IO.File.Exists(errorLogFilePath))
            {
                System.IO.File.Create(errorLogFilePath).Dispose();
            }

            System.IO.StreamWriter objWriter = new System.IO.StreamWriter(errorLogFilePath, true);
            objWriter.WriteLine(DateTime.Today.ToString("dd/MM/yyyy") + "      " + DateTime.Now.ToString("HH:mm:ss") + "      " + " Exception Caught");
            objWriter.WriteLine(ex.Message);
            objWriter.WriteLine();
            objWriter.WriteLine(ex.ToString());
            objWriter.WriteLine();
            objWriter.WriteLine();
            objWriter.Close();
            // Write Error details  ***************************************************************************************************

        }

        public void WriteEventLog(string _SCVOutputFolder, string strEvent)
        {
            // Write Event Log ***************************************************************************************************
            // string logpath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().FullName);



            string errorLogFileName = "SCV_FSCS_Validation_ErrorLog_" + DateTime.Today.ToString("yyyyMMdd") + ".txt";
            string errorLogFilePath = _SCVOutputFolder + "Log\\" + errorLogFileName;


            if (!System.IO.Directory.Exists(_SCVOutputFolder + "Log\\"))
            {
                string path = _SCVOutputFolder + "Log\\";
                System.IO.Directory.CreateDirectory(path);
            }

            if (!System.IO.File.Exists(errorLogFilePath))
            {
                System.IO.File.Create(errorLogFilePath).Dispose();
            }

            System.IO.StreamWriter objWriter = new System.IO.StreamWriter(errorLogFilePath, true);
            objWriter.WriteLine(DateTime.Today.ToString("dd/MM/yyyy") + "      " + DateTime.Now.ToString("HH:mm:ss") + "      " + strEvent);
            objWriter.Close();
            // Write Event Log ***************************************************************************************************
        }

        public string replaceFSCSSplChars(string strTxt)
        {
            string[] strSplChars = { "!", "$", "%", "*", "+", ";", "<", "=", ">", "?", "@", "^", "_", "|", "~", "\"", "'" };

            string retTxt = strTxt;
            if (retTxt != null && retTxt.Length > 0)
            {
                foreach (string x in strSplChars)
                {
                    retTxt = retTxt.Replace(x, " ");
                }
            }
            else
            {
                retTxt = string.Empty;
            }
            retTxt = retTxt.Trim();

            return retTxt;
        }

        public static List<string> SplitCustomerName(string strCustomerFullName, int totalPartitions)
        {
            List<string> list = new List<string>(strCustomerFullName.Split(' '));

            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            if (totalPartitions < 1)
            {
                throw new ArgumentOutOfRangeException("totalPartitions");
            }

            List<string>[] partitions = new List<string>[totalPartitions];

            List<string> partitionsFinal = new List<string>();
            string strTemp = null;

            int maxSize = (int)Math.Ceiling(list.Count / (double)totalPartitions);
            int k = 0;

            for (int i = 0; i < partitions.Length; i++)
            {
                partitions[i] = new List<string>();
                for (int j = k; j < k + maxSize; j++)
                {
                    if (j >= list.Count)
                    {
                        break;
                    }

                    partitions[i].Add(list[j]);
                    strTemp += list[j] + " ";
                }
                k += maxSize;

                partitionsFinal.Add(strTemp.Trim());
                strTemp = "";
            }

            // Re-order the splitter names - Forename and surname should be present if there are more name parts available.
            if (partitionsFinal[0].ToString().Trim() != "")
            {
                if (partitionsFinal[3].ToString().Trim() == "")
                {
                    if (partitionsFinal[2].ToString().Trim() != "")
                    {
                        partitionsFinal[3] = partitionsFinal[2];
                        partitionsFinal[2] = "";
                    }
                    else
                    {
                        partitionsFinal[3] = partitionsFinal[1];
                        partitionsFinal[1] = "";
                    }
                }
            }
            //return partitions;
            return partitionsFinal;
        }

        public int CheckNum(int KeyVal)
        {
            int _CheckNum = 0;
            try
            {
                if ((KeyVal == 8) || ((KeyVal >= 48) && (KeyVal <= 57)))
                {
                    _CheckNum = KeyVal;
                }
                else
                {
                    _CheckNum = 0;
                }
                return (_CheckNum);
            }
            catch (Exception ex) { throw ex; }
        }

        public int CheckSpecial(int KeyVal)
        {
            int _CheckNum = 0;
            try
            {
                if ((KeyVal == 8) || ((KeyVal >= 33) && (KeyVal <= 47)) || ((KeyVal >= 58) && (KeyVal <= 64)) || ((KeyVal >= 91) && (KeyVal <= 96)) || ((KeyVal >= 123) && (KeyVal <= 126)))
                {
                    _CheckNum = KeyVal;
                }
                else
                {
                    _CheckNum = 0;
                }
                return (_CheckNum);
            }
            catch (Exception ex) { throw ex; }
        }

        public int Checkalphanum(int KeyVal)
        {
            int _CheckNum = 0;
            try
            {
                if ((KeyVal == 8) || ((KeyVal >= 48) && (KeyVal <= 57)) || ((KeyVal >= 65) && (KeyVal <= 90)) || ((KeyVal >= 97) && (KeyVal <= 122)))
                {
                    _CheckNum = KeyVal;
                }
                else
                {
                    _CheckNum = 0;
                }
                return (_CheckNum);
            }
            catch (Exception ex) { throw ex; }
        }

        public int Checkalphanum_keys(int KeyVal)
        {
            int _CheckNum = 0;
            try
            {
                if ((KeyVal == 8) || (KeyVal == 32) || (KeyVal == 44) || ((KeyVal >= 48) && (KeyVal <= 57)) || ((KeyVal >= 65) && (KeyVal <= 90)) || ((KeyVal >= 97) && (KeyVal <= 122)))
                {
                    _CheckNum = KeyVal;
                }
                else
                {
                    _CheckNum = 0;
                }
                return (_CheckNum);
            }
            catch (Exception ex) { throw ex; }
        }

    }
}
