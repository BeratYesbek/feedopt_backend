using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstracts;
using Core.CrossCuttingConcerns.Cache;
using Core.Entity.Concretes;
using Core.Utilities.Cloud.FCM;
using Entity.Dtos;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Telegram.Bot.Types;
using Chat = Entity.Concretes.Chat;

namespace WebApi.Hub
{
    public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly INotificationService _notificationService;
        private readonly ICacheManager _cacheManager;
        private readonly IChatService _chatService;
        private readonly IMapper _mapper;

        public ChatHub(ICacheManager cacheManager, INotificationService notificationService, IChatService chatService,IMapper mapper)
        {
            _cacheManager = cacheManager;
            _notificationService = notificationService;
            _chatService = chatService;
            _mapper = mapper;
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
            var email = (string)routeOb?.Email;
            var data = await _chatService.GetAllByReceiverIdAndSenderId(CurrentUser.User.Id, userId);
            if (data != null)
            {
                var list = data.Data.Where(t => t.ReceiverUser.Id == CurrentUser.User.Id && t.Chat.IsSeen != true).ToList();
                var value = await _chatService.UpdateChatList(list);
                if (CheckGroupIsExists(email))
                {
                    await Clients.Group(email).SendAsync("ReceiveUpdatedListMessages", value);
                }
            }
            await Clients.Group(groupName).SendAsync("GetPreviousMessage", data);
        }

        public async Task HandleUpdateIsSeen(string jsonPayload)
        {
            var payload = JsonConvert.DeserializeObject<dynamic>(jsonPayload);
            var chatId = (int) int.Parse(payload?.ChatId.ToString());
            var senderId = (int)int.Parse(payload?.SenderId.ToString());
            var senderEmail = (string)payload?.SenderEmail.ToString();
            if (senderEmail == null) throw new ArgumentNullException(nameof(senderEmail));
            var chat = await _chatService.Get(chatId);
            if (senderId  != CurrentUser.User.Id)
            {
                Console.WriteLine("Message updated command received on: " + CurrentUser.User.Email);
                chat.Data.IsSeen = true;
                var result = await _chatService.UpdateChat(chat.Data);
                await Clients.Group(senderEmail).SendAsync("ReceiveUpdatedMessage", result.Data);
            } 
        }

        public async Task GetLatestMessageBetweenUsers()
        {
            var data = await _chatService.GetAllLastMessages(CurrentUser.User.Id);
            await Clients.Group(GetGroupName()).SendAsync("ReceiveLatestMessageBetweenUsers",data);
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
                Console.WriteLine("Notification was sent");
                //await _notificationService.PushNotification("", "", "", "");
            }
            var data = await _chatService.Add(new Chat
            {
                SenderId = CurrentUser.User.Id,
                ReceiverId = userId,
                Message = routeOb?.Message,
            });
            var list = new List<dynamic> { new { chat = data.Data } };
            var messageObject = new 
            {
                data = list,
                message = data.Message,
                success = data.Success,
            };
            await Clients.Groups(groupName,CurrentUser.User.Email).SendAsync("ReceiveMessage", messageObject);
            list.Clear();
        }

        private  string GetGroupName()
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
            _cacheManager.Add(groupName, numberOfMember, 240);
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
