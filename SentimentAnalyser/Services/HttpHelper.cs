using Newtonsoft.Json;
using SentimentAnalyser.Exceptions;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SentimentAnalyser.Services
{
    public static class HttpHelper
    {
        private static HttpClient client;

        public static async Task<T> Get<T>(string resourceUrl)
        {
            InitClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, resourceUrl))
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                HttpResponseMessage result = await client.SendAsync(requestMessage);
                if (result.IsSuccessStatusCode)
                {
                    return await result.Content.ReadAsAsync<T>();
                }

                if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new NotFoundException();
                }
                else if (result.StatusCode == HttpStatusCode.BadRequest)
                {
                    try
                    {
                        throw new BadRequestException(await result.Content.ReadAsAsync<ExceptionModel>());
                    }
                    catch
                    {
                        throw new BadRequestException(await result.Content.ReadAsStringAsync());
                    }
                }
                else
                {
                    throw new ServerException(await result.Content.ReadAsStringAsync(), result.StatusCode);
                }
            }
        }

        public static async Task<T> Post<T>(string resourceUrl, object obj)
        {
            InitClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, resourceUrl))
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                StringContent content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                HttpResponseMessage result = await client.PostAsync(requestMessage.RequestUri.ToString(), content);

                if (result.IsSuccessStatusCode)
                {
                    return await result.Content.ReadAsAsync<T>();
                }
                if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new NotFoundException();
                }
                else if (result.StatusCode == HttpStatusCode.BadRequest)
                {
                    try
                    {
                        throw new BadRequestException(await result.Content.ReadAsAsync<ExceptionModel>());
                    }
                    catch
                    {
                        throw new BadRequestException(await result.Content.ReadAsStringAsync());
                    }
                }
                else
                {
                    throw new ServerException(await result.Content.ReadAsStringAsync(), result.StatusCode);
                }
            }
        }

        public static async Task Delete(string resourceUrl)
        {
            InitClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Delete, resourceUrl))
            {
                HttpResponseMessage result = await client.DeleteAsync(requestMessage.RequestUri.ToString());

                if (result.IsSuccessStatusCode)
                {
                    return;
                }
                if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new NotFoundException();
                }
                else if (result.StatusCode == HttpStatusCode.BadRequest)
                {
                    try
                    {
                        throw new BadRequestException(await result.Content.ReadAsAsync<ExceptionModel>());
                    }
                    catch
                    {
                        throw new BadRequestException(await result.Content.ReadAsStringAsync());
                    }
                }
                else
                {
                    throw new ServerException(await result.Content.ReadAsStringAsync(), result.StatusCode);
                }
            }
        }

        private static void InitClient()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44389");
            client.Timeout = TimeSpan.FromSeconds(30);
        }
    }
}
