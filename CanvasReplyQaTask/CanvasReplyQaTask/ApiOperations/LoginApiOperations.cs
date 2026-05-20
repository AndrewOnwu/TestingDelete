using CanvasReplyQaTask.Hooks;
using CanvasReplyQaTask.Models;
using CanvasReplyQaTask.Utils.ApiHelper;
using RestSharp;

namespace CanvasReplyQaTask.ApiOperations
{
    public class LoginApiOperations
    {

        public LoginResponseModel GetLoginDetails(RestRequest restRequest)
        {
            var queryParams = new Dictionary<string, string>() { ["action"] = "login" };
            restRequest.AddQueryParams(queryParams);
            restRequest.AddJsonBody(Hook.Config.LoginModel);

            var response = restRequest.SendRequest<LoginResponseModel>(Method.Post);
            //var response = new HttpCalls()
            //    .SendRequest<LoginResponseModel>(Method.Post, queryParams, Hook.Config.LoginModel);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"The login request was unsuccessful with status code {response.StatusCode}");
            if (response.Data!.Result != "ok")
                throw new Exception($"The was an issue logging in due to error : {response.Data.Message}");

            return response.Data;
        }
    }
}
