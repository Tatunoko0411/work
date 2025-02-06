using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MySqlConnector;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Function2DAction
{
    public static class Function1
    {
        [FunctionName("GetRanking")]
        public static async Task<IActionResult> GetRanking(
     [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "ranking/get")] HttpRequest req,
     ILogger log)
        {
            string responseMessage;
            using var conn = new MySqlConnection(connectionBilder.ConnectionString);
            await conn.OpenAsync();


            MySqlCommand command = conn.CreateCommand();


            if (string.IsNullOrEmpty(req.Query["stage"]))
            {
                return new BadRequestResult();
            }
            else
            {

                bool isSuccess = int.TryParse(req.Query["stage"], out int stage);

                command.CommandText = "select player_id,time from 2d_action  where stage = @stage order by time limit 5;";
                command.Parameters.AddWithValue("@Stage", stage);
                using MySqlDataReader reader = await command.ExecuteReaderAsync();


                List<Ranking> rankingList = new List<Ranking>();
                while (await reader.ReadAsync())
                {
                    Ranking user = new Ranking();
                    user.UserId = (int)reader[0];
                    user.Time = (float)reader[1];
                    rankingList.Add(user);
                }

                responseMessage = JsonConvert.SerializeObject(rankingList);

            }

            return new ContentResult()
            {
                StatusCode = 200,
                ContentType = "application/json",
                Content = responseMessage
            };
        }


        [FunctionName("AddRanking")]
        public static async Task<IActionResult> AddRanking(
                [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "ranking/add")] HttpRequest req,
                ILogger log)
        {
            using var conn = new MySqlConnection(connectionBilder.ConnectionString);
            await conn.OpenAsync();

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            if (string.IsNullOrEmpty(requestBody))
            {
                return new BadRequestResult();
            }

            User user;
            try
            {
                user = JsonConvert.DeserializeObject<User>(requestBody);
                bool isSuccess = Validator.TryValidateObject(user, new ValidationContext(user, null, null), null, true);
                if (!isSuccess)
                {
                    return new BadRequestObjectResult("無効な値です");
                }
            }
            catch (Newtonsoft.Json.JsonReaderException e)
            {
                return new BadRequestObjectResult("正しいデータを送信してください");
            }

            catch (Exception ex)
            {
                return new BadRequestObjectResult("ようわからん");
            }


            MySqlCommand command = conn.CreateCommand();
            command.CommandText =
                "insert  into 2d_action(player_id, stage,time)  VALUES (@UserId,@Stage,@time) on duplicate key update time = if(time<values(time),time,values(time) );";
            command.Parameters.AddWithValue("@UserId", user.UserId);
            command.Parameters.AddWithValue("@Stage", user.StageId);
            command.Parameters.AddWithValue("@time", user.Time);

            await command.ExecuteNonQueryAsync();


            return new ContentResult()
            {
                StatusCode = 200,
                ContentType = "application/json",
                Content = ""
            };
        }

        [FunctionName("GetMember")]
        public static async Task<IActionResult> GetMember(
           [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "member/get")] HttpRequest req,
           ILogger log)
        {
            string responseMessage;
            using var conn = new MySqlConnection(connectionBilder.ConnectionString);
            await conn.OpenAsync();


            MySqlCommand command = conn.CreateCommand();

            command.CommandText = "select count(distinct player_id) as member from 2d_action;";
            using MySqlDataReader reader = await command.ExecuteReaderAsync();
            await reader.ReadAsync();

            long member = (long)reader[0];

            responseMessage = JsonConvert.SerializeObject(member);



            return new ContentResult()
            {
                StatusCode = 200,
                ContentType = "application/json",
                Content = responseMessage
            };
        }

        static MySqlConnectionStringBuilder connectionBilder =
                                           new MySqlConnectionStringBuilder()
                                           {
#if DEBUG
                                               Server = "localhost",
                                               Database = "azure_db",
                                               UserID = "root",
                                               Password = "",
                                               //SslMode = MySqlSslMode.Required,
#else
                                               Server = "db-ge-202402.mysql.database.azure.com",//  AzureのMySQLサーバーのサーバー名
                                               Database = "azure_db",//Azure上データベース名
                                               UserID = "student",
                                               Password = "Yoshidajobi2024",
                                               SslMode = MySqlSslMode.Required
#endif
                                           };
    }
}
