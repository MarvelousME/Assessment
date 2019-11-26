using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phonebook.Web.Hubs
{
    [HubName("phonebookHub")]
    public class PhonebookHub : Hub
    {
        public static void BroadcastData()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<PhonebookHub>();
            context.Clients.All.refreshPhonebookData();
        }
    }
}