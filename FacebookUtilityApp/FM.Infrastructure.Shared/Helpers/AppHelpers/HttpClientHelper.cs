﻿using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace FM.Infrastructure.Shared.Helpers.AppHelpers
{
    public class HttpClientHelper
    {
        static readonly CookieContainer cookieContainer = new CookieContainer();  // Sử dụng CookieContainer riêng, để lưu lại Cookie - hoặc thêm cookie

        public static async Task<string> SendRequestAsync(string url, string cookieStr)
        {
            var arrCookie = cookieStr.Split(';');

            foreach (var cookie in arrCookie)
            {
                var arr = cookie.Split('=');
                if (arr.Length > 1)
                {
                    cookieContainer.Add(new Uri(url), new Cookie(arr[0].Trim(), arr[1].Trim()));
                }
            }

            var htmltask = await GetWebContent(url);

            return htmltask;
        }

        public static async Task<string> GetWebContent(string url)
        {
            using var myHttpClientHandler = new MyHttpClientHandler(cookieContainer);
            using var httpClient = new HttpClient(myHttpClientHandler);

            Console.WriteLine($"Starting connect {url}");

            httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml+json");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.163 Safari/537.36");

            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            byte[] bytes = await response.Content.ReadAsByteArrayAsync();

            Encoding encoding = Encoding.GetEncoding("windows-1251");
            string html = encoding.GetString(bytes, 0, bytes.Length);

            return html;
        }

        public static void ShowHeaders(string lable, HttpHeaders headers)
        {
            Console.WriteLine(lable);

            foreach (var header in headers)
            {
                string value = string.Join(" ", header.Value);
                Console.WriteLine($"{header.Key,20} : {value}");
            }

            Console.WriteLine();
        }


        public class MyHttpClientHandler : HttpClientHandler
        {
            public MyHttpClientHandler(CookieContainer cookie_container)
            {
                CookieContainer = cookie_container;     // Thay thế CookieContainer mặc định
                AllowAutoRedirect = true;                // không cho tự động Redirect
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                UseCookies = true;
            }

            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                ShowHeaders("Request header trước khi qua Handler ", request.Headers);

                var task = base.SendAsync(request, cancellationToken); // bắt buộc gọi
                await task;

                ShowHeaders("Request header sau khi qua Handler ", request.Headers);

                return task.Result;
            }
        }
    }
}