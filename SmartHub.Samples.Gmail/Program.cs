using System;
using System.Threading.Tasks;
using SmartHub.Channels.Gmail;
using SmartHub.Channels.Gmail.Models;

namespace SmartHub.Samples.Gmail
{
    class Program
    {
        static void Main(string[] args)
        {
            const string accessToken = "ya29.GlutBHjogOFSUOy-xOzbQZQ1FLOuULM3taKkZ9uj3RDM0FbtlQNV1U3g9W-S54-XadA9cTb_Mb1bqJ-gt-VU-35qJtG9Vc0SAY1gfepBPV9kaBzzwO2RQbX_OXs9";
            const string refreshToken = "1/VJSrTtjP8JGZCnEW0DmgII-ONTUtMdUHs7jmHkp5coE";

            var gmailService = new GmailService();
            
            var tokenReponse = RefreshToken(gmailService, refreshToken);
            var token = tokenReponse.Result;

            GetThreads(gmailService,token,new DateTime(2017,08,19,17,0,0,DateTimeKind.Utc), DateTime.UtcNow);

            GetMessages(gmailService, token, new DateTime(2017,08,19,17,0,0, DateTimeKind.Utc), DateTime.UtcNow);

            GetMessage(gmailService,token, "15dfbb63fec83d91");
        }

        static GmailServiceResponse<Token> RefreshToken(GmailService gmailService, string refreshToken)
        {
            var result = gmailService.RefreshToken(refreshToken);
            return result;
        }

        static void GetThreads(GmailService gmailService, Token token, DateTime from, DateTime to)
        {
            var result = gmailService.GetThreads(token, from, to);
        }

        static void GetMessages(GmailService gmailService, Token token, DateTime from, DateTime to)
        {
            var result = gmailService.GetMessages(token, from, to);
        }

        static void GetMessage(GmailService gmailService, Token token, string messageId)
        {
            var result = gmailService.GetMessage(token, messageId);
        }
    }
}
