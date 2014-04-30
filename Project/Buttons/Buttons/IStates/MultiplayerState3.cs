using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime;
using System.Threading;
using Lidgren.Network;


namespace Buttons
{
    public class MultiplayerState3 : IState
    {
        Game1 game;
        NetClient client;
        int port = 14242;
        KeyboardState oldKS = new KeyboardState();
        string localIp, remoteIp = "192.168.1.7";


        public MultiplayerState3(Game1 game)
        {
            this.game = game;
            NetPeerConfiguration config = new NetPeerConfiguration("YCAV");
            client = new NetClient(config);
            client.RegisterReceivedCallback(new SendOrPostCallback(GotMessage));

            remoteIp = GetLocalIP();

            Connect(remoteIp, port);
        }

        void sendMessage(string text)
        {
            NetOutgoingMessage om = client.CreateMessage(text);
            client.SendMessage(om, NetDeliveryMethod.ReliableOrdered);
            Console.WriteLine("Sending '" + text + "'");
            client.FlushSendQueue();
        }

        void Connect(string host, int port)
        {
            client.Start();
            NetOutgoingMessage hail = client.CreateMessage("This is the hail message");
            client.Connect(host, port, hail);
        }

        // called by the UI
        void Shutdown()
        {
            client.Disconnect("Requested by user");
            // s_client.Shutdown("Requested by user");
        }

        void GotMessage(object peer)
        {
            NetIncomingMessage im;
            while ((im = client.ReadMessage()) != null)
            {
                // handle incoming message
                switch (im.MessageType)
                {
                    case NetIncomingMessageType.DebugMessage:
                    case NetIncomingMessageType.ErrorMessage:
                    case NetIncomingMessageType.WarningMessage:
                    case NetIncomingMessageType.VerboseDebugMessage:
                        string text = im.ReadString();
                        Console.WriteLine(text);
                        break;
                    case NetIncomingMessageType.StatusChanged:
                        NetConnectionStatus status = (NetConnectionStatus)im.ReadByte();

                        if (status == NetConnectionStatus.Connected)
                        {}
                        else{}


                        if (status == NetConnectionStatus.Disconnected)
                        { }

                        string reason = im.ReadString();
                        Console.WriteLine(status.ToString() + ": " + reason);

                        break;
                    case NetIncomingMessageType.Data:
                        string chat = im.ReadString();
                        Console.WriteLine("Received : " + chat);
                        break;
                    default:
                        Console.WriteLine("Unhandled type: " + im.MessageType + " " + im.LengthBytes + " bytes");
                        break;
                }
            }
        }
        private string GetLocalIP()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            }
            return "127.0.0.1";
        }

        public void Update(GameTime gameTime)
        {

            KeyboardState ks = Keyboard.GetState();
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                ChangeState(new MenuState(game));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && oldKS.IsKeyUp(Keys.Enter))
            {
                sendMessage("Hello");
            }
            oldKS = ks;

        }

        public void Draw(GameTime gameTime) { }
        public void Initialize() { }
        public void LoadContent() { }

        public void ChangeState(IState state)
        {
            Shutdown();
            game.gameState = state;
        }
        public void Window_ClientSizeChanged() { }

    }
}
