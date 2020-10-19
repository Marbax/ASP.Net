using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using SignalRSample.Models;

namespace SignalRSample.Hubs
{
    public class ChatHub : Hub
    {
        static List<User> users = new List<User>();
        public void Send(string name, string message)
        {
            Clients.All.addMessage(name, message);
        }
        public void Connect(string userName)
        {
            var id = Context.ConnectionId;
            if (!users.Any(x => x.ConnectionId == id))
            {
                users.Add(new User { ConnectionId = id, Name = userName });
                Clients.Caller.onConnected(id, userName, users);

                Clients.AllExcept(id).onNewUserConnected(id, userName);
            }
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var item = users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                users.Remove(item);
                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.Name);
            }
            return base.OnDisconnected(stopCalled);
        }
    }
}