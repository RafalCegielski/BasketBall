using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace BasketBallMVC.Hubs
{
    public class NotificationHub : Hub
    {
        public static Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();

        public NotificationHub()
        {

        }

        public override Task OnConnected()
        {
            string name = Context.User.Identity.Name;

            if (dictionary.ContainsKey(name))
            {
                dictionary[name].Add(Context.ConnectionId);
            }
            else
            {
                dictionary.Add(name, new List<string>());
                dictionary[name].Add(Context.ConnectionId);
            }
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string name = Context.User.Identity.Name;

            if (dictionary.ContainsKey(name))
            {
                if (dictionary[name].Count > 1)
                {
                    dictionary[name].Remove(Context.ConnectionId);
                }
                else
                {
                    dictionary.Remove(name);
                }
            }

            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            string name = Context.User.Identity.Name;


            if (dictionary.ContainsKey(name))
            {
                dictionary[name].Add(Context.ConnectionId);
            }
            else
            {
                dictionary.Add(name, new List<string>());
                dictionary[name].Add(Context.ConnectionId);
            }

            return base.OnReconnected();
        }

        internal static void AddNotification(string email)
        {
            if (dictionary.ContainsKey(email))
            {
                var clientsToNotify = dictionary[email];
                var notificationHub = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                notificationHub.Clients.Clients(clientsToNotify).notify();
            }
        }
        internal static void SendMessage(string email)
        {
            if (dictionary.ContainsKey(email))
            {
                var clientsToNotify = dictionary[email];
                var notificationHub = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                notificationHub.Clients.Clients(clientsToNotify).message();
            }
        }

        internal static void LogoutUser(string email)
        {
            if (dictionary.ContainsKey(email))
            {
                var clientsToNotify = dictionary[email];
                var notificationHub = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                notificationHub.Clients.Clients(clientsToNotify).logout();
            }
        }
    }
}