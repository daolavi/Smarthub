using SmartHub.Channels.Magento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using SmartHub.Channels.Magento.Models;

namespace SmartHub.Samples.Magento
{
    class Program
    {
        static void Main(string[] args)
        {
            var siteUrl = "http://test.rainstormstudio.com.au/maghelpdesk";
            var username = "admin";
            var password = "admin123";

            var service = new MagentoService();

            var isconnected = service.IsConnected(siteUrl,username,password);

            Console.WriteLine("MagentoService Connected : " + isconnected.ToString());

            var token = service.GetToken(siteUrl, username, password).Result;

            Console.WriteLine("GetToken : " + isconnected.ToString());

            var ticketId = CreateTicket(service, siteUrl,token);

            UpdateTicket(service, siteUrl, token, ticketId);

            GetTickets(service, siteUrl, token);

            var messageId = SendMessage(service, siteUrl, token, ticketId);

            UpdateMessange(service, siteUrl, token, messageId);

            GetMessages(service, siteUrl, token);
        }

        static int CreateTicket(MagentoService service,string siteUrl,string token)
        {
            var createTicketRequest = new CreateTicketRequest()
            {
                connection_id = "1",
                store_id =  "1",
                department_id = "1",
                agent_id = "1",
                status = "unread",
                type = "messsage",
                item_id = "listing123",
                subject = "listing",
                creator_id = "",
                creator_name = "TESTUSER_smarthub",
                creator_email = "TESTUSER_smarthub@gmail.com",
                recipient_id = "daolavi",
                created_at = DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"),
                last_modified_at = DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"),
                note = "",
            };
            var result = service.CreateTicket(siteUrl, token,createTicketRequest);
            if (result.HasError)
            {
                Console.WriteLine("CreateTicket " + result.Error);
            }
            else
            {
                Console.WriteLine("CreateTicket " + result.Result.ToString());
            }
            return result.Result;
        }

        static void UpdateTicket(MagentoService service, string siteUrl, string token, int ticketId)
        {
            var updateTicketRequest = new UpdateTicketRequest()
            {
                ticket_id = ticketId.ToString(),
                status = "read",
                type = "dispute",
                item_id = "listing124",
                subject = "listing",
                updated_by_id = "daolam",
                last_modified_at = DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"),
                note = "updated note",
            };
            var result = service.UpdateTicket(siteUrl, token, updateTicketRequest);
            if (result.HasError)
            {
                Console.WriteLine("UpdateTicket " + result.Error);
            }
            else
            {
                Console.WriteLine("UpdateTicket " + result.Result.ToString());
            }
        }

        static void GetTickets(MagentoService service, string siteUrl, string token)
        {
            var getTicketsRequest = new GetTicketsRequest()
            {
                connection_id = "1",
                from = new DateTime(2017, 7, 1).ToString("yyyy-MM-dd hh:mm:ss"),
                to = DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"),
            };
            var result = service.GetTickets(siteUrl, token, getTicketsRequest);
            if (result.HasError)
            {
                Console.WriteLine("GetTickets " + result.Error);
            }
            else
            {
                Console.WriteLine("GetTickets " + result.Result.Count);
            }
        }

        static int SendMessage(MagentoService service, string siteUrl, string token, int ticketId)
        {
            var sendMessageRequest = new SendMessageRequest()
            {
                message = "message",
                type = "cutomer message",
                sender_id = "",
                sender_name = "daolavi",
                sender_email = "daolavi@gmail.com",
                ticket_id = ticketId.ToString(),
                created_at = DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"),
                last_modified_at = DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"),
                note = "sendMessageRequest",
            };

            var result = service.SendMessage(siteUrl, token, sendMessageRequest);
            if (result.HasError)
            {
                Console.WriteLine("SendMessage " + result.Error);
            }
            else
            {
                Console.WriteLine("SendMessage " + result.Result.ToString());
            }
            return result.Result;
        }

        static void UpdateMessange(MagentoService service, string siteUrl, string token, int messageId)
        {
            var updateMessageRequest = new UpdateMessageRequest()
            {
                message_id = messageId.ToString(),
                message = "updating message",
                type = "customer message",
                modified_date = DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"),
                note = "updateMessageRequest",
            };

            var result = service.UpdateMessage(siteUrl, token, updateMessageRequest);
            if (result.HasError)
            {
                Console.WriteLine("UpdateMessange " + result.Error);
            }
            else
            {
                Console.WriteLine("UpdateMessange " + result.Result.ToString());
            }
        }

        static void GetMessages(MagentoService service, string siteUrl, string token)
        {
            var getMesssagesRequest = new GetMessagesRequest()
            {
                connection_id = "1",
                from = new DateTime(2017, 7, 1).ToString("yyyy-MM-dd hh:mm:ss"),
                to = DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"),
            };
            var result = service.GetMessages(siteUrl, token, getMesssagesRequest);
            if (result.HasError)
            {
                Console.WriteLine("GetMessages " + result.Error);
            }
            else
            {
                Console.WriteLine("GetMessages " + result.Result.Count);
            }
        }
    }
}
