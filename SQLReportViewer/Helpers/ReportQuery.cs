using Microsoft.Data.SqlClient;
using PagedList.Core;
using SQLReportViewer.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace SQLReportViewer
{
    public class ReportQuery
    {
        private string _query;
        private string _connectionString;
        private int _page;
        private int _count;
        private Dictionary<string, string> _filter;

        public ReportQuery(string connectionString, string query, int page, int count)
        {
            _connectionString = connectionString;
            _query = query;
            _count = count;
            _page = page;
        }

        public ReportQuery(string connectionString, string query, int page, int count, Dictionary<string, string> filter)
        {
            _connectionString = connectionString;
            _query = query;
            _count = count;
            _page = page;
            _filter = filter;
        }

        public ReportQuery(string connectionString, string query)
        {
            _connectionString = connectionString;
            _query = query;
            _count = 10;
            _page = 1;
        }

        public bool QueryControl()
        {
            var r = _query.StartsWith("SELECT") && !_query.Contains("ORDER BY");

            if (!r)
                return false;

            try
            {
                GetResult();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public string GetQuery()
        {
            return $@"
                SELECT T.* FROM
                ({_query}) T
                {whereClause()}
                ORDER BY (SELECT NULL)
                OFFSET ({(_page - 1) * _count}) ROWS
                FETCH NEXT ({_count}) ROWS ONLY;";
        }

        private string whereClause()
        {
            if (_filter != null && _filter.Count > 0)
            {
                string result = "WHERE ";
                int count = 0;
                foreach (var filter in _filter)
                {
                    result += $" {(count > 0 ? ("AND") : (""))} T.[{filter.Key}] = @p{count}";
                    count++;
                }
                return result;
            }
            return string.Empty;
            
        }

        public string GetCountQuery()
        {
            return $@"
                SELECT COUNT(*) FROM
                ({_query}) T {whereClause()}";
        }

        public ReportViewModel GetResult()
        {
            ReportViewModel result = new ReportViewModel();

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                using (SqlCommand myCommand = new SqlCommand(GetCountQuery(), sqlConnection))
                {
                    if (_filter != null && _filter.Count > 0)
                    {
                        int count = 0;
                        foreach (var f in _filter)
                        {
                            myCommand.Parameters.Add(new SqlParameter($"@p{count}", f.Value));
                            count++;
                        }
                    }
                    result.TotalRowCount = Convert.ToInt32(myCommand.ExecuteScalar());
                }

                using (SqlCommand myCommand = new SqlCommand(GetQuery(), sqlConnection))
                {
                    if (_filter != null && _filter.Count > 0)
                    {
                        int count = 0;
                        foreach (var f in _filter)
                        {
                            myCommand.Parameters.Add(new SqlParameter($"@p{count}", f.Value));
                            count++;
                        }
                    }
                    var myReader = myCommand.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(myReader);
                    result.Columns = dt.Columns;
                    var rows = new List<DataRow>();
                    foreach (DataRow row in dt.Rows)
                        rows.Add(row);
                    result.DataRows = new StaticPagedList<DataRow>(rows, _page, _count, result.TotalRowCount);
                    myReader.Close();
                }

                sqlConnection.Close();
            }

            return result;
        }

        public DataTable ExecuteQuery(string query)
        {
            var dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand myCommand = new SqlCommand(query, sqlConnection))
                {
                    var myReader = myCommand.ExecuteReader();
                    dataTable.Load(myReader);
                    myReader.Close();
                }

                sqlConnection.Close();
            }
            return dataTable;
        }
    }
}
