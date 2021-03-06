﻿using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Hubs
{

    public class NotificationHub : Hub
    {
        public void Send(string message, string userName, string postId)
        {
            {
                Clients.All.SendAsync("Send", postId, message, userName, DateTime.Now);
            }
        }
    }
}
