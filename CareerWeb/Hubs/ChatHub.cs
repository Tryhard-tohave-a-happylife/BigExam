using Microsoft.AspNet.SignalR;
using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

namespace CareerWeb.Hubs
{
    public class ChatHub:Hub
    {
        public static IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
        public override Task OnConnected()
        {
            var accID = int.Parse(HttpContext.Current.User.Identity.Name);
            new AccountDao().AddUserConnection(Guid.Parse(Context.ConnectionId), accID);
            Clients.All.BroadcastOnlineUser(accID);
            return base.OnConnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            int userId = new AccountDao().RemoveUserConnection(Guid.Parse(Context.ConnectionId));
            Clients.All.BroadcastOfflineUser(userId);
            return base.OnDisconnected(stopCalled);
        }
        public void GetUsersToChat(int projecdID)
        {
            var accID = int.Parse(HttpContext.Current.User.Identity.Name);
            var users = new AccountDao().GetUsersChatProject(projecdID, accID);
            Clients.Clients(new AccountDao().GetUserConnection(accID)).BroadcastUsersToChat(users);
        }
        public static void OfflineUsers(int UserId)
        {
            context.Clients.All.BroadcastOfflineUser(UserId);
        }
        public static void RecieveMessage(int toProjectID, string message)
        {
            var accID = int.Parse(HttpContext.Current.User.Identity.Name);
            var listRecieve = new AccountDao().UserRecieveMessage(accID, toProjectID);
            var us = new AccountDao().FindUserChat(accID);
            foreach (var item in listRecieve)
            {
                context.Clients.Clients(new AccountDao().GetUserConnection(item)).BroadcastRecieveMessage(us, message, toProjectID);
            }
        }
    }
}