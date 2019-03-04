using Microsoft.AspNet.SignalR;
using System;

namespace SignalRApp
{
    public class ChatHub : Hub
    {
        public void HerkeseGonder(string gonderen, string mesaj)
        {
            Clients.All.herkeseGonder(gonderen, mesaj, $"{DateTime.Now:g}");
        }

        public void OzelMesaj(string gonderenId, string aliciId, string mesaj)
        {
            Clients.User(aliciId).mesajGeldi(gonderenId, mesaj);
        }

        public void GetconnectionID()
        {
            var id = Context.ConnectionId;
            Clients.All.getir(id);
            Clients.User(id).getId();
        }

    }
}