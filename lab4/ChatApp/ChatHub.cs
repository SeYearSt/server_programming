using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AjaxControlToolkit;
using ChatApp.Models;
using Microsoft.AspNet.SignalR;

namespace ChatApp
{
    public class ChatHub : Hub
    {
        static List<User> Users = new List<User>();
        static List<Message> Messages = new List<Message>();

        public void Connect(string userName)
        {
            if (!Users.Exists(x => x.Name == "GroupChat"))
            {
                Users.Add(new User { ConnectionId = "GroupChatConnection", Name = "GroupChat" });
            }
            
            var id = Context.ConnectionId;

            if (!Users.Any(x => x.ConnectionId == id))
            {
                bool isUniqueName = true; 
                // check if no already registered with the same name
                foreach (User activeUser in Users)
                {
                    if (userName == activeUser.Name)
                    {
                        isUniqueName = false;
                        Clients.Caller.showAlert("There is user with the same name. Please choose another.");
                    }
                }
                if (isUniqueName)
                {
                    Users.Add(new User { ConnectionId = id, Name = userName });

                    Clients.Caller.onConnected(userName, Users);

                    Clients.AllExcept(id).onNewUserConnected(userName);
                }
                
            }
        }

        // User disconnection
        public override Task OnDisconnected(bool stopCalled)
        {
            var item = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                Users.Remove(item);
                Clients.All.onUserDisconnected(item.Name);
            }
            return base.OnDisconnected(stopCalled);
        }

        // Send conversation history with chosen user
        public void GetAllMessages(string me, string user)
        {
            if (user == "GroupChat")
            {
                List<Message> ChatMessages = Messages.FindAll(x => x.ToUser == user);
                Clients.Caller.loadMessages(ChatMessages);
            }
            else
            {
                List<Message> SelectedMessages = Messages.FindAll(x => (x.FromUser == me && x.ToUser == user) || (x.FromUser == user && x.ToUser == me));
                Clients.Caller.loadMessages(SelectedMessages);
            }
        }

        //Send message to users
        public void Send(string from, string to, string message, string time, string photo)
        {
            Messages.Add(new Message { FromUser = from, ToUser = to, MessageText = message, Time = time, Photo = photo });

            if (to == "GroupChat")
            {
                Clients.All.getNewMessage(from, to, message, time, photo);
            }
            else
            {
                if (from != to)
                {
                    //sender
                    Clients.Caller.getNewMessage(from, to, message, time, photo);

                    // get id by name
                    var toUserId = Users.Find(x => x.Name == to).ConnectionId;
                    //recipient
                    Clients.Client(toUserId).getNewMessage(from, to, message, time, photo);
                }
                else
                {
                    Clients.Caller.getNewMessage(from, to, message, time, photo);
                }
            }
        }

        /*
        protected void FileUploadComplete(object sender, EventArgs e)
        {
            string filename = System.IO.Path.GetFileName(AsyncFileUpload.FileName);
            AsyncFileUpload1.SaveAs(Server.MapPath(this.UploadFolderPath) + filename);
        }
        */

        //public override Task OnConnected()
        //{
        //    //Clients.Caller.notifyWrongVersion();
        //    Clients.All.addNewMessageToPage("Ivan", "Hi");
        //    return base.OnConnected();
        //}
    }
}