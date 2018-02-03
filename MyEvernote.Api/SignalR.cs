using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;


namespace MyEvernote.Api
{
    [HubName("SignalRHub")]
    public class SignalRHub : Hub
    {
        public void SomeMeth()
        {
            Clients.All.Update();
        }
    }
}