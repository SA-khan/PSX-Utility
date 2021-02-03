using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace PSXDataFetchingApp.Model
{
    public class DatabaseConnectivity
    {

        // Reference: https://www.codeproject.com/Articles/1121822/Using-Async-Await-Task-Methods-With-SQL-Queries-NE

        static string _PrimaryConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        static string _SecondaryConnection = ConfigurationManager.ConnectionStrings["IpamsConnection"].ConnectionString;

        static SqlConnection _connection01 = new SqlConnection(_PrimaryConnection);
        static SqlConnection _connection02 = new SqlConnection(_SecondaryConnection);
        public static bool TestConnection()
        {
            try
            {
                OpenConnection();
                return true;
            }
            catch
            {
                return false; 
            }
        }

        public Task<DataSet> GetDataSetAsync(string sConnectionString, string sSQL, params SqlParameter[] parameters)
        {
            return Task.Run(() =>
            {
                //YOUR CODE HERE
                using (var newConnection = new SqlConnection(sConnectionString))
                using (var mySQLAdapter = new SqlDataAdapter(sSQL, newConnection))
                {
                    mySQLAdapter.SelectCommand.CommandType = CommandType.Text;
                    if (parameters != null) mySQLAdapter.SelectCommand.Parameters.AddRange(parameters);

                    DataSet myDataSet = new DataSet();
                    mySQLAdapter.Fill(myDataSet);
                    return myDataSet;
                }
            });
        }

        public Task<int> ExecuteAsync(string sConnectionString, string sSQL, params SqlParameter[] parameters)
        {
            return Task.Run(() =>
            {
                using (var newConnection = new SqlConnection(sConnectionString))
                using (var newCommand = new SqlCommand(sSQL, newConnection))
                {
                    newCommand.CommandType = CommandType.Text;
                    if (parameters != null) newCommand.Parameters.AddRange(parameters);

                    newConnection.Open();
                    return newCommand.ExecuteNonQuery();
                }
            });
        }

        public async Task<int> ExecuteAsync2(string sConnectionString,string sSQL, params SqlParameter[] parameters)
        {
            using (var newConnection = new SqlConnection(sConnectionString))
            using (var newCommand = new SqlCommand(sSQL, newConnection))
            {
                newCommand.CommandType = CommandType.Text;
                if (parameters != null) newCommand.Parameters.AddRange(parameters);

                await newConnection.OpenAsync().ConfigureAwait(false);
                return await newCommand.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }

        private async Task GetSomeData(string sSQL, params SqlParameter[] sqlParams)
        {
            //Use Async method to get data
            DataSet results = await GetDataSetAsync(_PrimaryConnection, sSQL, sqlParams);

            ////Populate once data received
            //grdResults.DataSource = results.Tables[0];
        }

        private async Task ExecuteSomeData(string sSQL, params SqlParameter[] sqlParams)
        {
            //Use Async method to get data
            await ExecuteAsync(_PrimaryConnection, sSQL, sqlParams);
        }

        public static void OpenConnection()
        {
            _connection01.Open();
            _connection02.Open();
        }

        //Example checking Multiple Records
        public void chechUpdatedRecords()
        {
            ////Setup tasks
            //Task<int> ExecuteTask1 = database.ExecuteAsync(sConnectionString, sExecuteSQL1, sqlParams);
            //Task<int> ExecuteTask2 = database.ExecuteAsync(sConnectionString, sExecuteSQL2, sqlParams);
            //Task<int> ExecuteTask3 = database.ExecuteAsync(sConnectionString, sExecuteSQL3, sqlParams);
            ////Add to array
            //Task<int>[] Tasks = new Task<int>[] { ExecuteTask1, ExecuteTask2, ExecuteTask3 };
            ////Run all
            //await Task.WhenAll(Tasks);
            ////Get results
            //int iRowsAffected = Tasks[0].Result + Tasks[1].Result + Tasks[2].Result;

            //
//            DataSet results = await database.ExecuteAsync(sConnectionString, sExecuteSQL, sqlParams).ContinueWith
//(t => database.GetDataSetAsync(sConnectionString, sGetSQL, sqlParams)).Result;
        }

        private async Task RunSQLQuery(string sSQL, params SqlParameter[] sqlParams)
        {
            //Use await method to get data
            await GetDataSetAsync
                 (_PrimaryConnection, sSQL, sqlParams).ContinueWith(t => PopulateResults(t.Result));
        }
        private void PopulateResults(DataSet results)
        {
            //Do something with results
        }

        public async void GetDataFromAboveTwoMethods(string sSQL, params SqlParameter[] sqlParams)
        {
            await GetDataSetAsync(_PrimaryConnection, sSQL, sqlParams);//.ContinueWith(t => this.Invoke((Action)(() => { PopulateResults(t.Result); })));
        }

    }
}
