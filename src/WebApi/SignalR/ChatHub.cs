using System;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstracts;
using Core.CrossCuttingConcerns.Cache;
using Core.Extensions;
using Core.Utilities.Cloud.FCM;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace WebApi.SignalR
{
    public class ChatHub : Hub
    {
        private readonly IHttpContextAccessor _context;
        private readonly IUserService _userService;
        private readonly INotificationService _notificationService;
        private readonly ICacheManager _cacheManager;

        public ChatHub(IUserService userService,ICacheManager cacheManager,INotificationService notificationService)
        {
            _userService = userService;
            _cacheManager = cacheManager;
            _notificationService = notificationService;
            _context = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        public override Task OnConnectedAsync()
        {

            Console.WriteLine("--> Connection Opened: " + Context.ConnectionId);
            var dummyCookie = _context.HttpContext.Request.Cookies["dummyCookie"];

            var groupName = "";// GetGroupName();
            var s = _context.HttpContext.Request.Headers["Foo"];
            var roleClaims = _context.HttpContext?.User.ClaimRoles();
            var exp = _context.HttpContext?.User.Claims.FirstOrDefault(t => t.Type == "exp");
            var cultureName = _context.HttpContext?.Request.Cookies[CookieRequestCultureProvider.DefaultCookieName];
            var tokenType = _context.HttpContext?.User.Claims.FirstOrDefault(t => t.Type == "TokenType")?.Value; Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            if (CheckGroupIsExists(groupName))
            {
                int members = GetGroupMember(groupName);
                members++;
                UpdateGroupMember(groupName,members);
            }
            else
            {
                AddGroup(groupName,1);
            }
            Clients.Client(Context.ConnectionId).SendAsync("ReceiveConnId", Context.ConnectionId);

            return base.OnConnectedAsync();

        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("--> Connection Closed: " + Context.ConnectionId);
            var groupName = GetGroupName();
            Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            var members = GetGroupMember(groupName);
            members--;
            if (members == 0)
            {
                RemoveGroup(groupName);
                return base.OnDisconnectedAsync(exception);
            }
            UpdateGroupMember(groupName,members);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessageAsync(string message)
        {
            var routeOb = JsonConvert.DeserializeObject<dynamic>(message);
            Console.WriteLine("Message received on: " + Context.ConnectionId);

            if (routeOb?.To == null)
            {
                Console.WriteLine("--> UserId could not detected");
                return;

            }
            var groupName = (string)routeOb?.To.ToString();

            if (!CheckGroupIsExists(groupName))
            {
                await _notificationService.PushNotification("", "", "", "");
            }

            await Clients.Group(groupName).SendAsync("ReceiveMessage", message);

        }

        private string GetGroupName()
        {
            if (_context.HttpContext!.Request.Query.TryGetValue("email", out StringValues _email))
            {
                return _email;
            }
            throw new ArgumentNullException("--> User email is null");
        }

        private void AddGroup(string groupName,int numberOfMember)
        {
            _cacheManager.Add(groupName, numberOfMember,150);

        }

        private void UpdateGroupMember(string groupName,int numberOfMember)
        {
            AddGroup(groupName,numberOfMember);
        }

        private void RemoveGroup(string groupName)
        {
            _cacheManager.Remove(groupName);
        }

        private int GetGroupMember(string groupName)
        {
            return _cacheManager.Get<int>(groupName);
        }

        private bool CheckGroupIsExists(string groupName)
        {
            return _cacheManager.IsAdd(groupName);
        }


    }
}
