using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstracts;
using Core.CrossCuttingConcerns.Cache;
using Core.Entity.Concretes;
using Core.Utilities.Cloud.FCM;
using Core.Utilities.Result.Abstracts;
using Entity.Concretes;
using Entity.Dtos;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace WebApi.Hub
{
    public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly INotificationService _notificationService;
        private readonly ICacheManager _cacheManager;
        private readonly IChatService _chatService;

        public ChatHub(ICacheManager cacheManager, INotificationService notificationService, IChatService chatService)
        {
            _cacheManager = cacheManager;
            _notificationService = notificationService;
            _chatService = chatService;
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine("--> Connection Opened: " + Context.ConnectionId);
            var groupName = GetGroupName();

            if (CheckGroupIsExists(groupName))
            {
                int members = GetGroupMember(groupName);
                members++;
                UpdateGroupMember(groupName, members);
            }
            else
            {
                AddGroup(groupName, 1);
            }
            Groups.AddToGroupAsync(Context.ConnectionId, groupName);
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
            UpdateGroupMember(groupName, members);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task GetPreviousMessageAsync(string jsonPayload)
        {
            var routeOb = JsonConvert.DeserializeObject<dynamic>(jsonPayload);
            if (routeOb?.UserId == null)
            {
                Console.WriteLine("--> UserId could not detected");
                return;
            }
            var groupName = GetGroupName();
            var userId = (int)int.Parse(routeOb.UserId.ToString());
            IDataResult<List<ChatDto>> data = _chatService.GetAllByReceiverIdAndSenderId(CurrentUser.User.Id, userId);
            await Clients.Group(groupName).SendAsync("GetPreviousMessage", data);
        }

        public async Task SendMessageAsync(string message)
        {
            var routeOb = JsonConvert.DeserializeObject<dynamic>(message);
            Console.WriteLine("Message received on: " + Context.ConnectionId);
            if (routeOb?.Email == null || routeOb?.UserId == null)
            {
                Console.WriteLine("--> UserId could not detected");
                return;
            }
            var groupName = (string)routeOb?.Email.ToString();
            var userId = (int)int.Parse(routeOb?.UserId.ToString());
            if (!CheckGroupIsExists(groupName))
            {
                await _notificationService.PushNotification("", "", "", "");
            }
            var data = _chatService.Add(new Chat
            {
                SenderId = CurrentUser.User.Id,
                ReceiverId = userId,
                Message = routeOb?.Message,
            });
            await Clients.Group(groupName).SendAsync("ReceiveMessage", data);

        }

        private static string GetGroupName()
        {
            var email = CurrentUser.User.Email;

            if (email is not null && email != "")
            {
                return email;
            }
            throw new ArgumentNullException("--> User email is null");
        }

        private void AddGroup(string groupName, int numberOfMember)
        {
            _cacheManager.Add(groupName, numberOfMember, 150);

        }

        private void UpdateGroupMember(string groupName, int numberOfMember)
        {
            AddGroup(groupName, numberOfMember);
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
