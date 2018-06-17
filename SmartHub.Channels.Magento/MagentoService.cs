using System;
using System.Collections.Generic;
using System.Net;
using RestSharp;
using SmartHub.Channels.Magento.Models;

namespace SmartHub.Channels.Magento
{
    public interface IMagentoService
    {
        bool IsConnected(string siteUrl, string username, string password);

        MagentoServiceResponse<string> GetToken(string siteUrl, string username, string password);

        MagentoServiceResponse<int> CreateTicket(string siteUrl, string token, CreateTicketRequest createTicketRequest);

        MagentoServiceResponse<bool> UpdateTicket(string siteUrl, string token, UpdateTicketRequest updateTicketRequest);

        MagentoServiceResponse<IList<TicketResponse>> GetTickets(string siteUrl, string token, GetTicketsRequest getTicketsRequest);

        MagentoServiceResponse<int> SendMessage(string siteUrl, string token, SendMessageRequest sendMessageRequest);

        MagentoServiceResponse<bool> UpdateMessage(string siteUrl, string token, UpdateMessageRequest updateMessageRequest);

        MagentoServiceResponse<IList<MessageResponse>> GetMessages(string siteUrl, string token, GetMessagesRequest getMessagesRequest);

        MagentoServiceResponse<IList<StoreViewResponse>> GetStoreViews(string siteUrl, string token);
    }

    public class MagentoService : IMagentoService
    {

        public MagentoService()
        {
        }

        public bool IsConnected(string siteUrl, string username, string password)
        {
            var getToken = GetToken(siteUrl, username, password);
            return !getToken.HasError;
        }

        public MagentoServiceResponse<string> GetToken(string siteUrl, string username, string password)
        {
            var client = CreateRestClient(siteUrl);
            var request = CreateRequest("/rest/default/V1/integration/admin/token", Method.POST);
            request.AddBody(new { username = username, password = password });
            var serviceResponse = new MagentoServiceResponse<string>();
            try
            {
                var response = client.Execute(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    serviceResponse.Result = SimpleJson.DeserializeObject<string>(response.Content);
                }
                else
                {
                    serviceResponse.Error = response.Content;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Error = ex.Message;
            }
            return serviceResponse;
        }

        public MagentoServiceResponse<int> CreateTicket(string siteUrl, string token, CreateTicketRequest createTicketRequest)
        {
            var client = CreateRestClient(siteUrl, token);
            var serviceResponse = new MagentoServiceResponse<int>();
            var request = CreateRequest("/rest/aheadworks/helpdesk/createticket", Method.POST);
            request.AddBody(createTicketRequest);
            try
            {
                var response = client.Execute(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var content = SimpleJson.DeserializeObject<JsonArray>(response.Content);
                    if (content == null)
                    {
                        serviceResponse.Error = "Can not deserialize content";
                    }
                    else
                    {
                        if (content[1].ToString() == string.Empty)
                        {
                            serviceResponse.Result = SimpleJson.DeserializeObject<int>(content[0].ToString());
                        }
                        else
                        {
                            serviceResponse.Error = content[1].ToString();
                        }
                    }
                }
                else
                {
                    serviceResponse.Error = response.Content;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Error = ex.Message;
            }

            return serviceResponse;
        }

        public MagentoServiceResponse<bool> UpdateTicket(string siteUrl, string token, UpdateTicketRequest updateTicketRequest)
        {
            var client = CreateRestClient(siteUrl, token);
            var serviceResponse = new MagentoServiceResponse<bool>();
            var request = CreateRequest("/rest/aheadworks/helpdesk/updateticket", Method.POST);
            request.AddBody(updateTicketRequest);

            try
            {
                var response = client.Execute(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var content = SimpleJson.DeserializeObject<JsonArray>(response.Content);
                    if (content == null)
                    {
                        serviceResponse.Error = "Can not deserialize content";
                    }
                    else
                    {
                        serviceResponse.Result = (bool)content[0];
                    }
                }
                else
                {
                    serviceResponse.Error = response.Content;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Error = ex.Message;
            }

            return serviceResponse;
        }

        public MagentoServiceResponse<IList<TicketResponse>> GetTickets(string siteUrl, string token, GetTicketsRequest getTicketsRequest)
        {
            var client = CreateRestClient(siteUrl, token);
            var serviceResponse = new MagentoServiceResponse<IList<TicketResponse>>();
            var request = CreateRequest("/rest/aheadworks/helpdesk/gettickets", Method.POST);
            request.AddBody(getTicketsRequest);

            try
            {
                var response = client.Execute(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var content = SimpleJson.DeserializeObject<JsonArray>(response.Content);
                    if (content == null)
                    {
                        serviceResponse.Error = "Can not deserialize content";
                    }
                    else
                    {
                        if (content[1].ToString() == string.Empty)
                        {
                            serviceResponse.Result = SimpleJson.DeserializeObject<IList<TicketResponse>>(content[0].ToString());
                        }
                        else
                        {
                            serviceResponse.Error = content[1].ToString();
                        }
                    }
                }
                else
                {
                    serviceResponse.Error = response.Content;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Error = ex.Message;
            }

            return serviceResponse;
        }

        public MagentoServiceResponse<int> SendMessage(string siteUrl, string token, SendMessageRequest sendMessageRequest)
        {
            var client = CreateRestClient(siteUrl, token);
            var serviceResponse = new MagentoServiceResponse<int>();
            var request = CreateRequest("/rest/aheadworks/helpdesk/sendmessage", Method.POST);
            request.AddBody(sendMessageRequest);

            try
            {
                var response = client.Execute(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var content = SimpleJson.DeserializeObject<JsonArray>(response.Content);
                    if (content == null)
                    {
                        serviceResponse.Error = "Can not deserialize content";
                    }
                    else
                    {
                        if (content[1].ToString() == string.Empty)
                        {
                            serviceResponse.Result = SimpleJson.DeserializeObject<int>(content[0].ToString());
                        }
                        else
                        {
                            serviceResponse.Error = content[1].ToString();
                        }
                    }
                }
                else
                {
                    serviceResponse.Error = response.Content;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Error = ex.Message;
            }

            return serviceResponse;
        }

        public MagentoServiceResponse<bool> UpdateMessage(string siteUrl, string token, UpdateMessageRequest updateMessageRequest)
        {
            var client = CreateRestClient(siteUrl, token);
            var serviceResponse = new MagentoServiceResponse<bool>();
            var request = CreateRequest("/rest/aheadworks/helpdesk/updatemessage", Method.POST);
            request.AddBody(updateMessageRequest);

            try
            {
                var response = client.Execute(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var content = SimpleJson.DeserializeObject<JsonArray>(response.Content);
                    if (content == null)
                    {
                        serviceResponse.Error = "Can not deserialize content";
                    }
                    else
                    {
                        serviceResponse.Result = (bool)content[0];
                    }
                }
                else
                {
                    serviceResponse.Error = response.Content;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Error = ex.Message;
            }

            return serviceResponse;
        }

        public MagentoServiceResponse<IList<MessageResponse>> GetMessages(string siteUrl, string token, GetMessagesRequest getMessagesRequest)
        {
            var client = CreateRestClient(siteUrl, token);
            var serviceResponse = new MagentoServiceResponse<IList<MessageResponse>>();
            var request = CreateRequest("/rest/aheadworks/helpdesk/getmessages", Method.POST);
            request.AddBody(getMessagesRequest);

            try
            {
                var response = client.Execute(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var content = SimpleJson.DeserializeObject<JsonArray>(response.Content);
                    if (content == null)
                    {
                        serviceResponse.Error = "Can not deserialize content";
                    }
                    else
                    {
                        if (content[1].ToString() == string.Empty)
                        {
                            serviceResponse.Result = SimpleJson.DeserializeObject<IList<MessageResponse>>(content[0].ToString());
                        }
                        else
                        {
                            serviceResponse.Error = content[1].ToString();
                        }
                    }
                }
                else
                {
                    serviceResponse.Error = response.Content;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Error = ex.Message;
            }

            return serviceResponse;
        }

        public MagentoServiceResponse<IList<StoreViewResponse>> GetStoreViews(string siteUrl, string token)
        {
            var client = CreateRestClient(siteUrl, token);
            var serviceResponse = new MagentoServiceResponse<IList<StoreViewResponse>>();
            var request = CreateRequest("/rest/V1/store/storeViews", Method.GET);

            try
            {
                var response = client.Execute(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var content = SimpleJson.DeserializeObject<IList<StoreViewResponse>>(response.Content);
                    if (content == null)
                    {
                        serviceResponse.Error = "Can not deserialize content";
                    }
                    else
                    {
                        if (content[1].ToString() == string.Empty)
                        {
                            serviceResponse.Result = SimpleJson.DeserializeObject<IList<StoreViewResponse>>(content[0].ToString());
                        }
                        else
                        {
                            serviceResponse.Error = content[1].ToString();
                        }
                    }
                }
                else
                {
                    serviceResponse.Error = response.Content;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Error = ex.Message;
            }

            return serviceResponse;
        }

        #region Helper

        private static RestClient CreateRestClient(string siteUrl, string token = null)
        {
            var client = new RestClient(siteUrl);
            client.AddDefaultHeader("Content-Type", "application/json");
            client.ClearHandlers();

            if (!string.IsNullOrEmpty(token))
            {
                client.AddDefaultHeader("Authorization", string.Format("{0} {1}", "Bearer", token));
            }
            return client;
        }

        private static IRestRequest CreateRequest(string url, Method method = Method.GET)
        {
            var request = new RestRequest
            {
                Resource = url,
                Method = method,
                RequestFormat = DataFormat.Json,
            };
            return request;
        }
        #endregion
    }
}