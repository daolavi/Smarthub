using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eBay.Service.Call;
using eBay.Service.Core.Sdk;
using eBay.Service.Core.Soap;
using SmartHub.Channels.Ebay;

namespace SmartHub.Samples.eBay
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var ebayService = new EbayService();

            var token = ConfigurationManager.AppSettings["UserAccount_SmartHub1.ApiToken"];
            var isConnected = ebayService.IsConnected(token);
            Console.WriteLine("isConnected : " + isConnected);
            GetTokenStatus(ebayService, token);

            GetMemberMessage(ebayService, token, new DateTime(2017,5,1), DateTime.UtcNow, MessageTypeCodeType.All,MessageStatusTypeCodeType.Unanswered);
        }

        static void GetTokenStatus(EbayService service, string token)
        {
            var result = service.GetTokenStatus(token);
            if (result.HasError)
            {
                Console.WriteLine("GetTokenStatus : " + result.Error);
            }
            else
            {
                Console.WriteLine("GetTokenStatus : " + result.Result.ExpirationTime);
            }
        }

        static void GetMemberMessage(EbayService service, string token, DateTime from, DateTime to, MessageTypeCodeType messageTypeCodeType,
            MessageStatusTypeCodeType messageStatusTypeCode)
        {
            var result = service.GetMemberMessage(token, from, to, messageTypeCodeType, messageStatusTypeCode);
            if (result.HasError)
            {
                Console.WriteLine("GetMemberMessage : " + result.Error);
            }
            else
            {
                Console.WriteLine("GetMemberMessage : " + result.Result.Count());
            }
        }
    }
}
