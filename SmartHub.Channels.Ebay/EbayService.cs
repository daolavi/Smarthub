using System;
using System.Collections.Generic;
using eBay.Service.Call;
using eBay.Service.Core.Sdk;
using eBay.Service.Core.Soap;
using System.Web;
using SmartHub.Channels.Ebay.Models;
using SmartHub.Helper;

namespace SmartHub.Channels.Ebay
{
    public interface IEbayService
    {
        bool IsConnected(string token);

        EbayServiceResponse<TokenStatusType> GetTokenStatus(string token);

        EbayServiceResponse<IEnumerable<MemberMessage>> GetMemberMessage(string token, DateTime from, DateTime to, MessageTypeCodeType messageTypeCodeType, MessageStatusTypeCodeType messageStatusTypeCode);

        EbayServiceResponse<bool> IsValidToken(string token);

        EbayServiceResponse<string> GetSignInUrl();

        EbayServiceResponse<string> GetToken(string sessionId);

        EbayServiceResponse<AddMemberMessageRTQResponseType> SendMessage(string token, string subject, string parentMessageId, MemberMessage memberMessage);
    }

    public class EbayService : IEbayService
    {
        private static readonly string devId = ConfigHelper.AppSettings("Ebay.DevId");
        private static readonly string appId = ConfigHelper.AppSettings("Ebay.AppId");
        private static readonly string certId = ConfigHelper.AppSettings("Ebay.CertId");
        private static readonly string ruName = ConfigHelper.AppSettings("Ebay.RuName");
        private static readonly string apiServerUrl = ConfigHelper.AppSettings("Ebay.ApiServerUrl");
        private static readonly string signinUrl = ConfigHelper.AppSettings("Ebay.SigninUrl");

        public EbayService()
        {
        }

        public bool IsConnected(string token)
        {
            var tokenStatus = GetTokenStatus(token);
            return (!tokenStatus.HasError && tokenStatus.Result.Status == TokenStatusCodeType.Active);
        }
        
        public EbayServiceResponse<TokenStatusType> GetTokenStatus(string token)
        {
            var apiContext = CreateApiContext(apiServerUrl, ruName, appId, devId, certId, token);
            var result = new EbayServiceResponse<TokenStatusType>();
            try
            {
                var apiCall = new GetTokenStatusCall(apiContext);
                result.Result = apiCall.GetTokenStatus();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
            return result;
        }

        public EbayServiceResponse<IEnumerable<MemberMessage>> GetMemberMessage(string token, DateTime from, DateTime to, MessageTypeCodeType messageTypeCodeType,
            MessageStatusTypeCodeType messageStatusTypeCode)
        {
            var apiContext = CreateApiContext(apiServerUrl, ruName, appId, devId, certId, token);
            var response = new EbayServiceResponse<IEnumerable<MemberMessage>>();
            try
            {
                var apiCall = new GetMemberMessagesCall(apiContext);
                var timeFilter = new TimeFilter(from, to);
                var messages = apiCall.GetMemberMessages(timeFilter, messageTypeCodeType, messageStatusTypeCode);
                var result = new List<MemberMessage>();
                foreach (MemberMessageExchangeType message in messages)
                {
                    var memberMessage = new MemberMessage
                    {
                        ParentMessageId = message.Question.ParentMessageID,
                        MessageId = message.Question.MessageID,
                        MessageStatus = message.MessageStatus.ToString(),
                        ItemId = message.Item != null ? message.Item.ItemID : string.Empty,
                        QuestionType = message.Question.QuestionType.ToString(),
                        SenderId = message.Question.SenderID,
                        SenderEmail = message.Question.SenderEmail,
                        RecipientId = message.Question.RecipientID[0],
                        Body = message.Question.Body,
                        CreatedDate = message.CreationDate,
                        LastModifiedDate = message.LastModifiedDate
                    };
                    result.Add(memberMessage);
                }
                response.Result = result;
            }
            catch (Exception ex)
            {
                response.Error = ex.Message;
            }
            return response;
        }

        public EbayServiceResponse<bool> IsValidToken(string token)
        {
            var apiContext = CreateApiContext(apiServerUrl, ruName, appId, devId, certId, token);
            var result = new EbayServiceResponse<bool>();
            try
            {
                apiContext.ApiCredential.eBayToken = token;
                var apiCall = new GetTokenStatusCall(apiContext);
                result.Result = apiCall.GetTokenStatus().ExpirationTime > DateTime.UtcNow;
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
            return result;
        }

        public EbayServiceResponse<string> GetSignInUrl()
        {
            var apiContext = CreateApiContext(apiServerUrl, ruName, appId, devId, certId);
            var result = new EbayServiceResponse<string>();
            try
            {
                GetSessionIDCall sidCall = new GetSessionIDCall(apiContext);
                string sessionId = sidCall.GetSessionID(apiContext.RuName);
                result.Result = signinUrl + "&RuName=" + apiContext.RuName + "&SessID=" + HttpUtility.UrlEncode(sessionId);
            }
            catch(Exception ex)
            {
                result.Error = ex.Message;
            }
            return result;
        }

        public EbayServiceResponse<string> GetToken(string sessionId)
        {
            var apiContext = CreateApiContext(apiServerUrl, ruName, appId, devId, certId);
            var result = new EbayServiceResponse<string>();
            try
            {
                var call = new FetchTokenCall(apiContext);
                result.Result = call.FetchToken(sessionId);
            }
            catch(Exception ex)
            {
                result.Error = ex.Message;
            }
            return result;
        }

        public EbayServiceResponse<AddMemberMessageRTQResponseType> SendMessage(string token,string subject,string parentMessageId,MemberMessage memberMessage)
        {
            var apiContext = CreateApiContext(apiServerUrl, ruName, appId, devId, certId, token);
            var result = new EbayServiceResponse<AddMemberMessageRTQResponseType>();
            try
            {
                var apiCall = new AddMemberMessageRTQCall(apiContext)
                {
                    ItemID = memberMessage.ItemId,
                    MemberMessage = new MemberMessageType()
                    {
                        ParentMessageID = parentMessageId,
                        MessageType = MessageTypeCodeType.ContactEbayMember,
                        Body = memberMessage.Body,
                        Subject = subject,
                        QuestionType = QuestionTypeCodeType.General,
                        SenderID = memberMessage.SenderId,
                        SenderEmail = memberMessage.SenderEmail,
                        RecipientID = new StringCollection {memberMessage.RecipientId},
                    }
                };
                apiCall.Execute();
                result.Result = apiCall.ApiResponse;
            }
            catch(Exception ex)
            {
                result.Error = ex.Message;
            }

            return result;
        }

        public void GetDispute(string token, DateTime from, DateTime to, DisputeFilterTypeCodeType disputeFilterTypeCodeType,
            DisputeSortTypeCodeType disputeSortTypeCodeType)
        {
            var apiContext = CreateApiContext(apiServerUrl, ruName, appId, devId, certId, token);
            var apiCall = new GetUserDisputesCall(apiContext);
            var pagination = new PaginationType();
            var disputes = apiCall.GetUserDisputes(disputeFilterTypeCodeType, disputeSortTypeCodeType, from, to,pagination);
            foreach(DisputeType dispute in disputes)
            {
                dispute/
            }
        }

        #region Helper
        private static ApiContext CreateApiContext(string apiServerUrl, string ruName, string appId, string devId, string certId, string token = null)
        {
            var apiContext = new ApiContext
            {
                Site = SiteCodeType.Australia,
                RuName = ruName,
                SoapApiServerUrl = apiServerUrl,
                ApiCredential = new ApiCredential
                {
                    ApiAccount =
                        {
                            Application = appId,
                            Developer = devId,
                            Certificate = certId,
                        },
                    eBayToken = token,
                }
            };
            return apiContext;
        }
        #endregion
    }
}
