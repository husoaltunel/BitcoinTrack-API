using Business.Abstract;
using Business.Services.Hosted;
using Entities.Dtos;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Services.BitcoinWebDetail
{
    public class BitcoinWebDetailService :HostedService, IBitcoinWebDetailService
    {
        
        private readonly IBitcoinDetailService _bitcoinDetailService;
        private IConfiguration _configuration;
        string Url;
        int BackgroundTaskDelay;
        public BitcoinWebDetailService(IBitcoinDetailService bitcoinDetailService,IConfiguration configuration)
        {
            _bitcoinDetailService = bitcoinDetailService;
            _configuration = configuration;
            Url = _configuration.GetSection("BitcoinSiteUrl").Value;
            BackgroundTaskDelay = int.Parse(_configuration.GetSection("BackgroundTaskDelay").Value);
        }

        protected async override Task ExecuteAsync(CancellationToken cToken)
        {
            while (!cToken.IsCancellationRequested)
            {          
                GetAsync();
                await Task.Delay(TimeSpan.FromSeconds(BackgroundTaskDelay), cToken );
            }
        }

        public async void GetAsync()
        {
            WebClient client = new WebClient();
            string html = await client.DownloadStringTaskAsync(Url);
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);

            var bitcoinPriceSpan = document.DocumentNode.SelectNodes("/html/body/div[4]/div[4]/div[1]/div/div[1]/div[4]/div/div[1]/span[1]/span")[0].InnerText;
            string bitcoinPrice = bitcoinPriceSpan.Replace("$", "").Replace(",", "").Replace(".", "");


            InsertAsync(new BitcoinDetailDto(){ Price = decimal.Parse(bitcoinPrice), Date = DateTime.Now });
            

        }

        public void InsertAsync(BitcoinDetailDto bitcoinDetailDto)
        {
            _bitcoinDetailService.InsertAsync(bitcoinDetailDto);
        }
    }
}
