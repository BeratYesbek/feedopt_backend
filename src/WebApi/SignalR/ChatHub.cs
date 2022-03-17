using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Business.Abstracts;
using Castle.DynamicProxy;
using Core.Utilities.IoC;
using Core.Utilities.Result.Abstracts;
using Entity.Concretes;
using Microsoft.AspNetCore.Http;
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
        private static int count;
        public ChatHub(IUserService userService)
        {
            _userService = userService;
            _context = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        public override Task OnConnectedAsync()
        {

            Console.WriteLine("--> Connection Opened: " + Context.ConnectionId);

            var groupName = GetGroupName();
            Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            Clients.Client(Context.ConnectionId).SendAsync("ReceiveConnId", Context.ConnectionId);

            return base.OnConnectedAsync();

        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine("--> Connection Closed: " + Context.ConnectionId);
            var groupName = GetGroupName();
            Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
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
    }
}
