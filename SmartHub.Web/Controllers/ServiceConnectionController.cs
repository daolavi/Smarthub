using SmartHub.Channels.Ebay;
using SmartHub.Channels.Magento;
using SmartHub.Channels.Gmail;
using SmartHub.Services;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartHub.Services.Models;
using SmartHub.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

namespace SmartHub.Web.Controllers
{
    public class ServiceConnectionController : Controller
    {
        private static string eBaySessionId;
        private static ServiceConnectionViewModel viewModel;

        private IEbayService ebayService;
        private IMagentoService magentoService;
        private IGmailService gmailService;
        private IChannelEbayService channelEbayService;
        private IChannelMagentoService channelMagentoService;
        private IChannelEmailService channelEmailService;
        private IUserService userService;
        private IUserChannelService userChannelSerivce;
        private IConnectionService connectionService;
        private readonly IMapper mapper;

        public ServiceConnectionController(IEbayService ebayService, 
                                            IMagentoService magentoService, 
                                            IGmailService gmailService,
                                            IChannelEbayService channelEbayService, 
                                            IChannelMagentoService channelMagentoService, 
                                            IChannelEmailService channelEmailService,
                                            IUserService userService, 
                                            IUserChannelService userChannelSerivce, 
                                            IConnectionService connectionService, 
                                            IMapper mapper)
        {
            this.ebayService = ebayService;
            this.magentoService = magentoService;
            this.gmailService = gmailService;
            this.channelEbayService = channelEbayService;
            this.channelMagentoService = channelMagentoService;
            this.channelEmailService = channelEmailService;
            this.userService = userService;
            this.userChannelSerivce = userChannelSerivce;
            this.connectionService = connectionService;
            this.mapper = mapper;
        }

        public ActionResult GetUserId(string email)
        {
            var userId = userService.UserExists(email);

            if (userId == 0)
            {
                var user = new User
                {
                    Firstname = "default",
                    LastName = "default",
                    Account = email,
                    Password = "default"
                };
                user.Id = userService.Create(user);
                userId = user.Id;
            }

            return RedirectToAction("Index", new { userId = userId });
        }

        // GET: ServiceConnection
        public ActionResult Index(int? userId)
        {
            Session["CurrentUserId"] = userId;
            if (!userId.HasValue)
                return HttpNotFound();

            var userChannels = userChannelSerivce.FindUserChannels(userId.Value).Where(channel => channel.IsActive);

            if (userChannels.Any())
            {
                viewModel = GenerateServiceConnectionVM(userChannels);
            }
            else
            {
                viewModel = new ServiceConnectionViewModel();
            }

            return View(viewModel);
        }

        #region eBay
        public ActionResult EBayLogin()
        {
            var eBaySignInUrl = ebayService.GetSignInUrl().Result;

            if (!string.IsNullOrEmpty(eBaySignInUrl))
            {
                var qscoll = HttpUtility.ParseQueryString(eBaySignInUrl);

                //get sessionID from URL to get token later in the EBayLoginAgree action below
                //don't forget to decode
                eBaySessionId = HttpUtility.UrlDecode(qscoll.Get("SessID")).Replace(' ', '+');
                return Redirect(eBaySignInUrl);
            }

            var userId = int.Parse(Session["CurrentUserId"].ToString());

            return RedirectToAction("Index", new { userId = userId });
        }

        //must set the 'Accept url' on eBay Developer page as https://<domain-name>/ServiceConnection/EBayLoginAgree
        public ActionResult EBayLoginAgree(string ebaytkn, string tknexp, string username)
        {
            var userId = int.Parse(Session["CurrentUserId"].ToString());
            var token = ebayService.GetToken(eBaySessionId);
            if (!token.HasError)
            {
                var tokenStatus = ebayService.GetTokenStatus(token.Result);
                var channeleBay = new ChannelEbay
                {
                    Token = token.Result,
                    ExpiredDate = tokenStatus.Result.ExpirationTime,
                    CreatedDate = DateTime.UtcNow
                };
                channeleBay.Id = channelEbayService.Create(channeleBay);

                var userEbayChannel = new UserChannel()
                {
                    UserId = userId,
                    ChannelId = channeleBay.Id,
                    ChannelType = Shared.Enums.ChannelType.Ebay,
                    IsActive = true,
                };
                userEbayChannel.Id = userChannelSerivce.Create(userEbayChannel);

                var userMagentoChannel = userChannelSerivce.FindUserChannels(userId).SingleOrDefault(channel => channel.ChannelType == Shared.Enums.ChannelType.Magento && channel.IsActive == true);
                if (userMagentoChannel != null)
                {
                    CreateConnection(userEbayChannel.Id, userMagentoChannel.Id);
                }
            }
            return RedirectToAction("Index", new { userId = userId });
        }

        public ActionResult EBayDisconnect(ChannelEbayViewModel channelEbay)
        {
            var userId = int.Parse(Session["CurrentUserId"].ToString());

            var getUserChannelEbay = userChannelSerivce.FindUserChannels(userId).SingleOrDefault(userChannel => userChannel.ChannelId == channelEbay.Id && userChannel.ChannelType == Shared.Enums.ChannelType.Ebay && userChannel.IsActive == true);
            if (getUserChannelEbay != null)
            {
                getUserChannelEbay.IsActive = false;
                userChannelSerivce.Update(getUserChannelEbay);

                var connections = connectionService.GetConnectionsByUserChannelId(getUserChannelEbay.Id);
                foreach (var item in connections)
                {
                    item.IsActive = false;
                    connectionService.Update(item);
                }
            }

            return RedirectToAction("Index", new { userId = userId });
        }
        #endregion eBay

        #region Email
        public ActionResult OpenGmail()
        {
            return new RedirectResult(gmailService.GetAuthorizationUrl());
        }

        public async Task<ActionResult> GoogleOAuth(string code)
        {
            var userId = int.Parse(Session["CurrentUserId"].ToString());
            var token = await gmailService.GetToken(code);
            if (!token.HasError)
            {
                var isConnected = gmailService.IsConnected(token.Result);
                if (isConnected)
                {
                    var emailChannel = mapper.Map<ChannelEmail>(token.Result);
                    emailChannel.CreatedDate = DateTime.UtcNow;
                    emailChannel.Id = channelEmailService.Create(emailChannel);

                    var userEmailChannel = new UserChannel()
                    {
                        UserId = userId,
                        ChannelId = emailChannel.Id,
                        ChannelType = Shared.Enums.ChannelType.Email,
                        IsActive = true,
                    };
                    userEmailChannel.Id = userChannelSerivce.Create(userEmailChannel);

                    var userMagentoChannel = userChannelSerivce.FindUserChannels(userId).SingleOrDefault(channel => channel.ChannelType == Shared.Enums.ChannelType.Magento && channel.IsActive == true);

                    if (userMagentoChannel != null)
                    {
                        CreateConnection(userEmailChannel.Id, userMagentoChannel.Id);
                    }
                }
            }
            return RedirectToAction("Index", new { userId = userId });
        }

        public ActionResult EmailDisconnect(ChannelEmailViewModel channelEmail)
        {
            var userId = int.Parse(Session["CurrentUserId"].ToString());

            var getUserChannelEmail = userChannelSerivce.FindUserChannels(userId).SingleOrDefault(userChannel => userChannel.ChannelId == channelEmail.Id && userChannel.ChannelType == Shared.Enums.ChannelType.Email && userChannel.IsActive == true);
            if (getUserChannelEmail != null)
            {
                getUserChannelEmail.IsActive = false;
                userChannelSerivce.Update(getUserChannelEmail);

                var connections = connectionService.GetConnectionsByUserChannelId(getUserChannelEmail.Id);
                foreach (var item in connections)
                {
                    item.IsActive = false;
                    connectionService.Update(item);
                }
            }

            return RedirectToAction("Index", new { userId = userId });
        }
        #endregion

        #region Magento
        public ActionResult MagentoConnect(ChannelMagentoViewModel model)
        {
            var userId = int.Parse(Session["CurrentUserId"].ToString());
            var isConnected = magentoService.IsConnected(model.Host, model.Username, model.Password);
            if (isConnected)
            {
                var channelMagento = mapper.Map<ChannelMagento>(model);
                channelMagento.CreatedDate = DateTime.UtcNow;
                channelMagento.Id = channelMagentoService.Create(channelMagento);

                var userMagentoChannel = new UserChannel()
                {
                    UserId = userId,
                    ChannelId = channelMagento.Id,
                    ChannelType = Shared.Enums.ChannelType.Magento,
                    IsActive = true,
                };

                userMagentoChannel.Id = userChannelSerivce.Create(userMagentoChannel);

                var userEbayChannel = userChannelSerivce.FindUserChannels(userId).SingleOrDefault(userChannel => userChannel.ChannelType == Shared.Enums.ChannelType.Ebay && userChannel.IsActive);

                if (userEbayChannel != null)
                {
                    CreateConnection(userEbayChannel.Id, userMagentoChannel.Id);
                }

                var userEmailChannel = userChannelSerivce.FindUserChannels(userId).SingleOrDefault(userChannel => userChannel.ChannelType == Shared.Enums.ChannelType.Email && userChannel.IsActive);
                if (userEmailChannel != null)
                {
                    CreateConnection(userEmailChannel.Id, userMagentoChannel.Id);
                }
            }

            return RedirectToAction("Index", new { userId = userId });
        }

        public ActionResult MagentoDisconnect(ChannelMagentoViewModel channelMagento)
        {
            var userId = int.Parse(Session["CurrentUserId"].ToString());

            var getUserChannelMagento = userChannelSerivce.FindUserChannels(userId).SingleOrDefault(userChannel => userChannel.ChannelId == channelMagento.Id && userChannel.ChannelType == Shared.Enums.ChannelType.Magento && userChannel.IsActive == true);
            if (getUserChannelMagento != null)
            {
                getUserChannelMagento.IsActive = false;
                userChannelSerivce.Update(getUserChannelMagento);

                var connections = connectionService.GetConnectionsByUserChannelId(getUserChannelMagento.Id);
                foreach (var item in connections)
                {
                    item.IsActive = false;
                    connectionService.Update(item);
                }
            }

            return RedirectToAction("Index", new { userId = userId });
        }

        #endregion Magento

        #region Helper
        private ServiceConnectionViewModel GenerateServiceConnectionVM(IEnumerable<UserChannel> userChannels)
        {
            //get channels' Ids
            var channelMagento = userChannels.SingleOrDefault(channel => channel.ChannelType == Shared.Enums.ChannelType.Magento);
            var channelEbay = userChannels.SingleOrDefault(channel => channel.ChannelType == Shared.Enums.ChannelType.Ebay);
            var channelEmail = userChannels.SingleOrDefault(channel => channel.ChannelType == Shared.Enums.ChannelType.Email);

            //assign channels into viewmodel
            ServiceConnectionViewModel model = new ServiceConnectionViewModel();
            if (channelMagento != null)
            {
                model.channelMagento = mapper.Map<ChannelMagentoViewModel>(
                    channelMagentoService.GetChannel(channelMagento.ChannelId)
                );
                model.channelMagento.IsConnected = magentoService.IsConnected(model.channelMagento.Host, model.channelMagento.Username, model.channelMagento.Password);
            }
            if (channelEbay != null)
            {
                model.channelEbay = mapper.Map<ChannelEbayViewModel>(
                    channelEbayService.GetChannel(channelEbay.ChannelId)
                );
                var validateToken = ebayService.IsValidToken(model.channelEbay.Token);
                if (!validateToken.HasError && validateToken.Result)
                {
                    model.channelEbay.IsConnected = true;
                }
            }
            if (channelEmail != null)
            {
                var channelEmailModel = channelEmailService.GetChannel(channelEmail.ChannelId);
                model.channelEmail = mapper.Map<ChannelEmailViewModel>(channelEmailModel);
                var token = mapper.Map<SmartHub.Channels.Gmail.Models.Token>(channelEmailModel);
                model.channelEmail.IsConnected = gmailService.IsConnected(token);
            }
            return model;
        }

        //create a connection from eBay to Magento
        private bool CreateConnection(int userChannelId, int userChannelMagentoId)
        {
            var connection = new Connection
            {
                UserChannelSource = userChannelId,
                UserChannelTarget = userChannelMagentoId,
                CreatedDate = DateTime.UtcNow,
                Status = Shared.Enums.ConnectionStatus.Idle,
                Counter = 0,
                IsActive = true
            };
            //if the creation is successful
            if (connectionService.Create(connection) != 0)
            {
                return true;
            }

            return false;
        }
        #endregion Helper
    }
}