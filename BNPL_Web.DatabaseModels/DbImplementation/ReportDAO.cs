using System;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace Project.DatabaseModel.DbImplementation
{
    public class ReportDAO
    {
        private readonly IConfiguration _IConfiguration;
        public ReportDAO(IConfiguration _IConfiguration)
        {
            this._IConfiguration = _IConfiguration;
        }

        public DataTable ExecuteReport(String reportQuery)
        {
            try
            {
                SqlConnection myReadOnlyConnection = getReadOnlyConnection();
                myReadOnlyConnection.Open();

                SqlCommand myCommand = new SqlCommand(reportQuery, myReadOnlyConnection);
                 myCommand.CommandTimeout = 0;
                DataTable reportDataTable = new DataTable();
                reportDataTable.Load(myCommand.ExecuteReader());
                myReadOnlyConnection.Close();

                return reportDataTable;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public SqlConnection getReadOnlyConnection()
        {
            if (ConfigurationManager.ConnectionStrings != null)
            {
                if (ConfigurationManager.ConnectionStrings["ReadOnlyConnection"] != null)
                {
                    String myConStr = ConfigurationManager.ConnectionStrings["ReadOnlyConnection"].ConnectionString;
                    SqlConnection myConnection = new SqlConnection(myConStr);
                    return myConnection;
                }
                else
                {
                    return null;
                }

            }
            return null;
        }
        public DataTable ExecuteNonQueryReport(String reportQuery)
        {
            try
            {
                SqlConnection myReadOnlyConnection =getReadOnlyConnection();
                myReadOnlyConnection.Open();

                SqlCommand myCommand = new SqlCommand(reportQuery, myReadOnlyConnection);

                DataTable reportDataTable = new DataTable();
                myCommand.ExecuteNonQuery();
                //reportDataTable.Load();
                myReadOnlyConnection.Close();

                return reportDataTable;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
