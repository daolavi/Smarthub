using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Requests;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using SmartHub.Channels.Gmail.Models;
using SmartHub.Helper;
using Thread = Google.Apis.Gmail.v1.Data.Thread;
using Message = Google.Apis.Gmail.v1.Data.Message;
using Google.Apis.Gmail.v1.Data;

namespace SmartHub.Channels.Gmail
{
    public interface IGmailService
    {
        bool IsConnected(Token token);

        Task<GmailServiceResponse<Token>> GetToken(string code);

        string GetAuthorizationUrl();

        GmailServiceResponse<Token> RefreshToken(string refreshToken);

        GmailServiceResponse<List<Thread>> GetThreads(Token token, DateTime from, DateTime to);

        GmailServiceResponse<List<Message>> GetMessages(Token token, DateTime from, DateTime to);

        GmailServiceResponse<Message> GetMessage(Token token, string messageId);
    }

    public class GmailService : IGmailService
    {
        private static readonly string ClientId = ConfigHelper.AppSettings("Gmail.ClientId");
        private static readonly string ClientSecret = ConfigHelper.AppSettings("Gmail.ClientSecret");
        private static readonly string RedirectUri = ConfigHelper.AppSettings("Gmail.RedirectUri");

        public GmailService()
        {
            
        }

        public bool IsConnected(Token token)
        {
            if (!string.IsNullOrEmpty(token.AccessToken))
            {
                var timespan = DateTime.UtcNow - token.IssuedUtc;
                if (timespan.TotalSeconds < token.ExpiresInSeconds.GetValueOrDefault(0))
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<GmailServiceResponse<Token>> GetToken(string code)
        {
            var result = new GmailServiceResponse<Token>();
            try
            {
                var credential = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
                {
                    ClientSecrets = new ClientSecrets
                    {
                        ClientId = ClientId,
                        ClientSecret = ClientSecret,
                    },
                    Scopes = new[] {Google.Apis.Gmail.v1.GmailService.Scope.GmailModify}
                });
                var tokenResponse =
                    await credential.ExchangeCodeForTokenAsync("", code, RedirectUri, CancellationToken.None);
                var token = new Token()
                {
                    AccessToken = tokenResponse.AccessToken,
                    ExpiresInSeconds = tokenResponse.ExpiresInSeconds,
                    IdToken = tokenResponse.IdToken,
                    Issued = tokenResponse.Issued,
                    IssuedUtc = tokenResponse.IssuedUtc,
                    RefreshToken = tokenResponse.RefreshToken,
                    Scope = tokenResponse.Scope,
                    TokenType = tokenResponse.TokenType,
                };
                result.Result = token;
            }
            catch (Exception ex)
            {       
                result.Error = ex.Message;
            }

            return result;
        }

        public string GetAuthorizationUrl()
        {
            var credential = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = ClientId,
                    ClientSecret = ClientSecret,
                },
                Scopes = new[] { Google.Apis.Gmail.v1.GmailService.Scope.GmailModify }
            });

            var url = credential.CreateAuthorizationCodeRequest(RedirectUri);
            return url.Build().ToString();
        }

        public GmailServiceResponse<Token> RefreshToken(string refreshToken)
        {
            var result = new GmailServiceResponse<Token>();
            try
            {
                var credential = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
                {
                    ClientSecrets = new ClientSecrets
                    {
                        ClientId = ClientId,
                        ClientSecret = ClientSecret,
                    },
                    Scopes = new[] { Google.Apis.Gmail.v1.GmailService.Scope.GmailModify }
                });
                var task = credential.RefreshTokenAsync("", refreshToken, CancellationToken.None);
                var tokenResponse = task.Result;
                var token = new Token()
                {
                    AccessToken = tokenResponse.AccessToken,
                    ExpiresInSeconds = tokenResponse.ExpiresInSeconds,
                    IdToken = tokenResponse.IdToken,
                    Issued = tokenResponse.Issued,
                    IssuedUtc = tokenResponse.IssuedUtc,
                    RefreshToken = tokenResponse.RefreshToken,
                    Scope = tokenResponse.Scope,
                    TokenType = tokenResponse.TokenType,
                };
                result.Result = token;
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }

            return result;
        }

        public GmailServiceResponse<List<Thread>> GetThreads(Token token, DateTime from, DateTime to)
        {
            var gmailService = CreateGmailService(token);
            var result = new GmailServiceResponse<List<Thread>>();
            var threads = new List<Thread>();
            var request = gmailService.Users.Threads.List("me");
            var query = "after:{0} before:{1}";
            request.Q = string.Format(query, from.ToUnixTime(), to.ToUnixTime());
            do
            {
                try
                {
                    var response = request.Execute();
                    if (response.Threads != null)
                    {
                        threads.AddRange(response.Threads);
                        request.PageToken = response.NextPageToken;
                    }
                }
                catch (Exception ex)
                {
                    result.Error += ex.Message; 
                }
            } while (!string.IsNullOrEmpty(request.PageToken));
            result.Result = threads;

            return result;
        }

        public GmailServiceResponse<List<Message>> GetMessages(Token token, DateTime from, DateTime to)
        {
            var gmailService = CreateGmailService(token);
            var result = new GmailServiceResponse<List<Message>>();
            var messages = new List<Message>();
            var request = gmailService.Users.Messages.List("me");
            var query = "after:{0} before:{1}";
            request.Q = string.Format(query, from.ToUnixTime(), to.ToUnixTime());
            do
            {
                try
                {
                    var response = request.Execute();
                    if (response.Messages != null)
                    {
                        messages.AddRange(response.Messages);
                        request.PageToken = response.NextPageToken;
                    }
                }
                catch (Exception ex)
                {
                    result.Error += ex.Message;
                }
            } while (!string.IsNullOrEmpty(request.PageToken));
            result.Result = messages;

            return result;
        }

        public GmailServiceResponse<Message> GetMessage(Token token, string messageId)
        {
            var gmailService = CreateGmailService(token);
            var result = new GmailServiceResponse<Message>();
            var request = gmailService.Users.Messages.Get("me", messageId);
            try
            {
                var response = request.Execute();
                result.Result = response;
                
                var messagePart = Convert.FromBase64String(response.Payload.Body.Data);
            }
            catch (Exception ex)
            {
                result.Error += ex.Message;
            }
            
            return result;
        }

        #region Helper
        private Google.Apis.Gmail.v1.GmailService CreateGmailService(Token token)
        {
            var tokenResponse = new TokenResponse()
            {
                AccessToken = token.AccessToken,
                ExpiresInSeconds = token.ExpiresInSeconds,
                IdToken = token.IdToken,
                Issued = token.Issued,
                IssuedUtc = token.IssuedUtc,
                RefreshToken = token.RefreshToken,
                Scope = token.Scope,
                TokenType = token.TokenType,
            };

            var credential = new UserCredential(new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
                                                                                    {
                                                                                        ClientSecrets = new ClientSecrets
                                                                                        {
                                                                                            ClientId = ClientId,
                                                                                            ClientSecret = ClientSecret,
                                                                                        },
                                                                                    }), 
                                                "me",
                                                tokenResponse);
            var gmailService = new Google.Apis.Gmail.v1.GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
            });

            return gmailService;
        }
        #endregion
    }
}