using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Business.Abstracts;
using Core.Utilities.IoC;
using Core.Utilities.Result.Abstracts;
using Entity.Concretes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace WebApi.SignalR
{
    public class ChatHub : Hub
    {
        private readonly IHttpContextAccessor _context;
        private readonly IWebSocketConnectionService _socketService;
        private readonly IUserService _userService;

        public ChatHub(IUserService userService, IWebSocketConnectionService socketService)
        {
            _userService = userService;
            _socketService = socketService;
            _context = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        public override Task OnConnectedAsync()
        {

            var email = _context.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;


            Console.WriteLine("--> Connection Opened: " + Context.ConnectionId);
            var user = _userService.GetByMail(email);
            if (user.Success)
            {
                Clients.Group(user.Data.Email).SendAsync("ReceiveConnId", Context.ConnectionId);
                _socketService.Add(new WebSocketConnection
                {
                    UserId = user.Data.Id,
                    ConnectionId = Context.ConnectionId
                });
            }
            return base.OnConnectedAsync();

        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine("--> Connection Closed: " + Context.ConnectionId);
            var conn = _socketService.GetByConnection(Context.ConnectionId);
            _socketService.Delete(conn.Data);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessageAsync(string message)
        {
            var routeOb = JsonConvert.DeserializeObject<dynamic>(message);

            Console.WriteLine("Message received on: " + Context.ConnectionId);

            var userId = routeOb?.To;

            if (userId != null && typeof(int) == userId)
            {
                Console.WriteLine("--> UserId could not detected");
                return;

            }
            var connections = (IDataResult<List<WebSocketConnection>>)_socketService.GetByUser(userId);
            if (routeOb?.To.ToString() != string.Empty)
            {
                foreach (var conn in connections.Data)
                {
                    string toClient = conn.ConnectionId;
                    Console.WriteLine("Target on: " + toClient);
                    await Clients.Client(toClient).SendAsync("ReceiveMessage", message);
                }
            }

        }
    }
}
