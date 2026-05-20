using CanvasReplyQaTask.Hooks;
using RestSharp;

namespace CanvasReplyQaTask.Utils.ApiHelper
{
    public static class ApiHelper
    {
        public static string baseUrl => Hook.Config.Url;

        //private RestClient RestClient => new(Hook.Config.Url + "/json.php");

        public static void AddQueryParams(this RestRequest restRequest, Dictionary<string, string> values)
        {
            foreach (var item in values)
            {
                restRequest.AddQueryParameter(item.Key, item.Value);
            }
        }

        public static void AddPathParams(this RestRequest restRequest, string path)
        {
            restRequest.Resource = path;
        }

        public static RestResponse SendRequest(this RestRequest restRequest, Method method)
        {
            RestClient client = new(baseUrl);
            return client.Execute(restRequest, method);
        }

        public static RestResponse<T> SendRequest<T>(this RestRequest restRequest, Method method)
        {
            RestClient client = new(baseUrl);
            return client.Execute<T>(restRequest, method);
        }
    }
}
