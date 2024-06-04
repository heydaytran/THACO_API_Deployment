using THACO.Web.Service.Interfaces.Repo;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using THACO.Web.Service.Model;
using THACO.Web.Service.DtoEdit;
using THACO.Web.Service.Exceptions;
using THACO.Web.Service.Properties;
using Dapper;
using System.Collections;

namespace THACO.Web.Repo.Repo
{
    public class TimeSheetRepo : BaseRepo, ITimeSheetRepo
    {
        public TimeSheetRepo(IConfiguration configuration, IServiceProvider serviceProvider) : base(configuration, serviceProvider)
        {
        }

        public override async Task<DAResult> GetDataTable(Type type, FilterTable filterTable)
        {
            var table = this.GetTableName(type);
            var columnSql = this.ParseColumn(string.Join(",", filterTable.fields), "ts");

            var param = new Dictionary<string, object>();
            var where = this.ParseWhere(filterTable.filter, param, "a");

            IDbConnection cnn = null;
            IList result = null;
            int totalRecord = 0;
            try
            {
                cnn = this.Provider.GetOpenConnection();

                var sb = new StringBuilder($"SELECT * FROM (SELECT {columnSql}" +
                    $", c.checker_code, c.checker_name, bt.bookmark_type_code, ts.created_Date" +
                    $", DATE_FORMAT(ts.`start_date`, '%Y-%m-%d') AS start_date_string" +
                    $", DATE_FORMAT(ts.`status_date`, '%Y-%m-%d') AS status_date_string" +
                    $", DATE_FORMAT(ts.`deadline_customer_date`, '%Y-%m-%d') AS deadline_customer_date_string" +
                    $" FROM {table}  ts " +
                    $"LEFT JOIN checker c ON ts.checker_id = c.checker_id " +
                    $"LEFT JOIN bookmark_type bt ON ts.bookmark_type_id = bt.bookmark_type_id) a");
                var sqlSummary = new StringBuilder($"SELECT COUNT(*) FROM ({sb.ToString()}");

                if (!string.IsNullOrWhiteSpace(where))
                {   
                    sb.Append($" WHERE {where}");
                    sqlSummary.Append($" WHERE {where}");
                }
                sqlSummary.Append(" ) AS SA");


                // Sắp xếp
                if (filterTable.sortBy?.Count > 0 && filterTable.sortType?.Count > 0)
                {
                    sb.Append($" ORDER BY ");
                    for (int i = 0; i < filterTable.sortBy.Count; i++)
                    {
                        sb.Append($" {filterTable.sortBy[i]} {filterTable.sortType[i]}");
                        if (i != filterTable.sortBy.Count - 1)
                        {
                            sb.Append(",");
                        }
                    }
                }
                else
                {
                    if (this.GetTableColumns(type).Any(n => n.Equals("created_date")))
                    {
                        sb.Append($" ORDER BY a.created_date DESC");
                    }
                }

                if (filterTable.page > 0 && filterTable.size > 0)
                {
                    sb.Append($" LIMIT {filterTable.size} OFFSET {(filterTable.page - 1) * filterTable.size}");
                }

                result = await this.Provider.QueryAsync(cnn, sb.ToString(), param);
                totalRecord = await cnn.ExecuteScalarAsync<int>(sqlSummary.ToString(), param);
            }
            finally
            {
                this.Provider.CloseConnection(cnn);
            }

            return new DAResult(200, Resources.getDataSuccess, "", result, totalRecord);
        }

        public async Task<DAResult> GetTimeline(Type type, FilterTable filterTable)
        {

            var table = this.GetTableName(type);
            var columnSql = this.ParseColumn(string.Join(",", filterTable.fields), "ts");

            var param = new Dictionary<string, object>();
            var where = this.ParseWhere(filterTable.filter, param, "a");

            IDbConnection cnn = null;
            IList result = null;
            int totalRecord = 0;
            try
            {
                cnn = this.Provider.GetOpenConnection();

                var sb = new StringBuilder($"SELECT * FROM (SELECT tst.time_sheet_target_id" +
                    $", CONCAT(c.checker_code, ' - ', c.checker_name) AS checker" +
                    $", ts.time_sheet_code, ts.task, tst.sort_order, tst.target, DATE_FORMAT(tst.deadline_checker_date, '%Y-%m-%d') AS deadline_checker_date" +
                    $", CASE WHEN DATEDIFF(tst.deadline_checker_date, CURDATE()) > 0 THEN CONCAT(DATEDIFF(tst.deadline_checker_date, CURDATE()), ' ngày') WHEN DATEDIFF(tst.deadline_checker_date, CURDATE()) < 0 THEN CONCAT('- ', DATEDIFF(CURDATE(), tst.deadline_checker_date), ' ngày') ELSE 'Hôm nay' END AS warning" +
                    $" FROM time_sheet_target tst" +
                    $" LEFT JOIN time_sheet ts ON tst.time_sheet_id = ts.time_sheet_id" +
                    $" LEFT JOIN checker c ON ts.checker_id = c.checker_id" +
                    $" WHERE ts.complete = FALSE OR ts.complete IS NULL) a ");
                var sqlSummary = new StringBuilder($"SELECT COUNT(*) FROM ({sb.ToString()}");

                if (!string.IsNullOrWhiteSpace(where))
                {
                    sb.Append($" WHERE {where}");
                    sqlSummary.Append($" WHERE {where}");
                }
                sqlSummary.Append(" ) AS SA");


                // Sắp xếp
                if (filterTable.sortBy?.Count > 0 && filterTable.sortType?.Count > 0)
                {
                    sb.Append($" ORDER BY ");
                    for (int i = 0; i < filterTable.sortBy.Count; i++)
                    {
                        sb.Append($" {filterTable.sortBy[i]} {filterTable.sortType[i]}");
                        if (i != filterTable.sortBy.Count - 1)
                        {
                            sb.Append(",");
                        }
                    }
                    sb.Append($" ,a.deadline_checker_date, a.time_sheet_code, a.sort_order");
                }
                else
                {
                    sb.Append($" ORDER BY a.deadline_checker_date, a.time_sheet_code, a.sort_order");
                }

                if (filterTable.page > 0 && filterTable.size > 0)
                {
                    sb.Append($" LIMIT {filterTable.size} OFFSET {(filterTable.page - 1) * filterTable.size}");
                }

                result = await this.Provider.QueryAsync(cnn, sb.ToString(), param);
                totalRecord = await cnn.ExecuteScalarAsync<int>(sqlSummary.ToString(), param);
            }
            finally
            {
                this.Provider.CloseConnection(cnn);
            }

            return new DAResult(200, Resources.getDataSuccess, "", result, totalRecord);
        }

        public async Task<DAResult> GetCalculator(Type type, FilterTable filterTable)
        {

            var table = this.GetTableName(type);
            var columnSql = this.ParseColumn(string.Join(",", filterTable.fields), "ts");

            var param = new Dictionary<string, object>();
            var where = this.ParseWhere(filterTable.filter, param, "a");

            IDbConnection cnn = null;
            IList result = null;
            int totalRecord = 0;
            try
            {
                cnn = this.Provider.GetOpenConnection();

                var sb = new StringBuilder($"SELECT * FROM (SELECT ts.checker_id, c.checker_code, c.checker_name," +
                    $" SUM(IFNULL(bt.workload, 0)) AS workload, SUM(IF(ts.plan = TRUE, 1, 0)) AS x1," +
                    $" SUM(IF(ts.complete = TRUE, 1, 0)) AS x2," +
                    $" SUM(IF(bt.x3 = TRUE, 1, 0)) AS x3" +
                    $" FROM time_sheet ts" +
                    $" LEFT JOIN bookmark_type bt ON ts.bookmark_type_id = bt.bookmark_type_id" +
                    $" INNER JOIN checker c ON ts.checker_id = c.checker_id" +
                    $" GROUP BY ts.checker_id, c.checker_code, c.checker_name) a ");
                var sqlSummary = new StringBuilder($"SELECT COUNT(*) FROM ({sb.ToString()}");

                if (!string.IsNullOrWhiteSpace(where))
                {
                    sb.Append($" WHERE {where}");
                    sqlSummary.Append($" WHERE {where}");
                }
                sqlSummary.Append(" ) AS SA");


                // Sắp xếp
                if (filterTable.sortBy?.Count > 0 && filterTable.sortType?.Count > 0)
                {
                    sb.Append($" ORDER BY ");
                    for (int i = 0; i < filterTable.sortBy.Count; i++)
                    {
                        sb.Append($" {filterTable.sortBy[i]} {filterTable.sortType[i]}");
                        if (i != filterTable.sortBy.Count - 1)
                        {
                            sb.Append(",");
                        }
                    }
                    sb.Append($" ,a.checker_code");
                }
                else
                {
                    sb.Append($" ORDER BY a.checker_code");
                }

                if (filterTable.page > 0 && filterTable.size > 0)
                {
                    sb.Append($" LIMIT {filterTable.size} OFFSET {(filterTable.page - 1) * filterTable.size}");
                }

                result = await this.Provider.QueryAsync(cnn, sb.ToString(), param);
                totalRecord = await cnn.ExecuteScalarAsync<int>(sqlSummary.ToString(), param);
            }
            finally
            {
                this.Provider.CloseConnection(cnn);
            }

            return new DAResult(200, Resources.getDataSuccess, "", result, totalRecord);
        }
    }
}
