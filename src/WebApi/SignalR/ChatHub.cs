﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.BusinessAspect;
using Business.Security.Role;
using Core.CrossCuttingConcerns.Cache;
using Core.Entity.Concretes;
using Core.Extensions;
using Core.Utilities.Cloud.FCM;
using Core.Utilities.Constants;
using Core.Utilities.IoC;
using Core.Utilities.Result.Abstracts;
using Entity.Dtos;
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
        private readonly IChatService _chatService;

        public ChatHub(IUserService userService,ICacheManager cacheManager,INotificationService notificationService,IChatService chatService)
        {
            _userService = userService;
            _cacheManager = cacheManager;
            _notificationService = notificationService;
            _chatService = chatService;
            _context = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        [SecuredOperation($"{Role.User},{Role.Admin},{Role.SuperAdmin}")]
        public override Task OnConnectedAsync()
        {

            Console.WriteLine("--> Connection Opened: " + Context.ConnectionId);

            var groupName = GetGroupName();
   
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
            UpdateGroupMember(groupName,members);
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
            var nameIdentifier = _context.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (nameIdentifier != null)
            {
                var groupName = GetGroupName();
                var id = int.Parse(nameIdentifier);
                var userId = (int) int.Parse(routeOb.UserId.ToString());
                IDataResult<List<ChatDto>> data = _chatService.GetAllByReceiverIdAndSenderId(id,userId);
                await Clients.Group(groupName).SendAsync("GetPreviousMessage", data);
            }

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
            var email = _context.HttpContext?.Request.Cookies[CookieKey.Email];

            if (email is not null && email != "")
            {
                return email;
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
