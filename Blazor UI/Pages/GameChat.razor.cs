using Blazor_UI.Data;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;

namespace Blazor_UI.Pages
{
    public partial class GameChat
    {
        // flag to indicate chat status
        private bool _isChatting = false;

        // name of the user who will be chatting
        private string _username;

        // on-screen message
        private string _message;

        // new message input
        private string _newMessage;

        // list of messages in chat
        private List<Message> _messages = new List<Message>();
        private string _hubUrl;
        private HubConnection _hubConnection;

        private async Task UpdateMood(string mood)
        {
            _gameRoom.UpdateGamer(new Buffert.GamersRecord(_username, mood));
            await _hubConnection.SendAsync("Refresh");
        }
        public async Task Chat()
        {
            try
            {
                //Ny metod för lättare läst kod.
                // Start chatting and force refresh UI.
                if (!_login.IsLoggedIn()) throw new Exception("User not logged in");
                _username = _login.GetUserName();
                _gameRoom.AddGamer(new Buffert.GamersRecord(_username, "Happy"));
                _isChatting = true;
                await Task.Delay(1);

                // remove old messages if any
                _messages.Clear();

                //-- Ny metod för lättare läst kod.
                // Create the chat client
                string baseUrl = navigationManager.BaseUri;

                _hubUrl = baseUrl.TrimEnd('/') + GameHub.HubUrl;

                _hubConnection = new HubConnectionBuilder()
                    .WithUrl(_hubUrl)
                    .Build();

                _hubConnection.On<string, string>("Broadcast", BroadcastMessage);
                _hubConnection.On<bool>("Refresh", UpdateScreen);

                await _hubConnection.StartAsync();

                await SendAsync($"[Notice] {_username} joined chat room.");
            }
            catch (Exception e)
            {
                _message = $"ERROR: Failed to start chat client: {e.Message}";
                _isChatting = false;
            }
        }

        private void BroadcastMessage(string name, string message)
        {
            bool isMine = name.Equals(_username, StringComparison.OrdinalIgnoreCase);

            _messages.Add(new Message(name, message, isMine));
            // Inform blazor the UI needs updating
            StateHasChanged();
            ScrollIt();
        }
        private void UpdateScreen(bool x)
        {
            StateHasChanged();
        }
        private async Task DisconnectAsync()
        {
            if (_isChatting)
            {
                await SendAsync($"[Notice] {_username} left chat room.");
                _gameRoom.DeleteGamer(_username);
                await _hubConnection.StopAsync();
                await _hubConnection.DisposeAsync();
                _hubConnection = null;
                _isChatting = false;
                StateHasChanged();
            }
        }

        private async Task ScrollIt()
        {
            await JS.InvokeAsync<object>("autoScroll");
        }
        private async Task SendAsync(string message)
        {
            if (_isChatting && !string.IsNullOrWhiteSpace(message))
            {
                await _hubConnection.SendAsync("Broadcast", _username, message);

                _newMessage = string.Empty;
            }

        }

        private class Message
        {
            public Message(string username, string body, bool mine)
            {
                Username = username;
                Body = body;
                Mine = mine;
            }

            public string Username { get; set; }
            public string Body { get; set; }
            public bool Mine { get; set; }

            public bool IsNotice => Body.StartsWith("[Notice]");

            public string CSS => Mine ? "sent" : "received";
        }
    }
}
