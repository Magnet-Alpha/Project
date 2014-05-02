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
    public enum Event
    {
        TowerAdded,
        TowerSold,
        VirusCall,
        GameOver
    }

    

    public class MultiplayerState3 : IState
    {
        public Game1 game;
        NetClient client;
        public int port = 14242;
        KeyboardState oldKS = new KeyboardState();
        GameState gameState;
        bool connected = false;
        NetConnectionStatus status = NetConnectionStatus.Disconnected;
        public GetIPForm IPform;
        bool showDc = true;

        public MultiplayerState3(Game1 game)
        {
            this.game = game;
            NetPeerConfiguration config = new NetPeerConfiguration("YCAV");
            client = new NetClient(config);
            client.RegisterReceivedCallback(new SendOrPostCallback(GotMessage));
            gameState = new GameState(game, this);

            IPform = new GetIPForm(this);
            IPform.Show();

            showIPs();

           
        }


        


        public void formClosed()
        {
            Console.WriteLine("trying to connect " + IPform.ip.ToString());
            Connect(IPform.ip.ToString(), port);
        }

        public void sendEvent(Event evt, int x, int y)
        {
            NetOutgoingMessage msg = client.CreateMessage();
            msg.Write("#" + evt.ToString());
            switch (evt)
            {
                case Event.TowerAdded:
                    msg.Write(x);
                    msg.Write(y);
                    break;
                case Event.TowerSold:
                    msg.Write(x);
                    msg.Write(y);
                    break;
                case Event.VirusCall:
                    break;
            }

            client.SendMessage(msg, NetDeliveryMethod.ReliableOrdered);
        }

        void sendMessage(string text)
        {
            NetOutgoingMessage om = client.CreateMessage(text);
            client.SendMessage(om, NetDeliveryMethod.ReliableOrdered);
            Console.WriteLine("Sending '" + text + "'");
            client.FlushSendQueue();
        }

        public void Connect(string host, int port)
        {
            client.Start();
            NetOutgoingMessage hail = client.CreateMessage("This is the hail message");
            client.Connect(host, port, hail);
            
            
        }

        // called by the UI
        void Shutdown()
        {
            //sendMessage(GetLocalIP() + " disconnected");
            client.Disconnect("Requested by user");
            // s_client.Shutdown("Requested by user");
            
        }
        private void showIPs()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if(ip.AddressFamily == AddressFamily.InterNetwork)
                    Console.WriteLine(ip.ToString());
            }
            
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

                        

                        this.status = status;
                        connected = status == NetConnectionStatus.Connected;
                        string reason = im.ReadString();
                        if (status == NetConnectionStatus.Disconnected && showDc)
                        {
                            new DCForm(status.ToString() + ": " + reason, this).Show();
                        }
                        Console.WriteLine(status.ToString() + ": " + reason);

                        break;
                    case NetIncomingMessageType.Data:
                        string evt = im.ReadString();
                        Console.WriteLine("Received : " + evt);
                        int x, y;
                        switch (evt)
                        {
                            case "TowerAdded":

                                x = im.ReadInt32();
                                y = im.ReadInt32();
                                // tower added at (x,y) to be handled
                                Console.WriteLine("Tower added at " + x + "," + y);
                                break;
                            case "TowerSold":
                                x = im.ReadInt32();
                                y = im.ReadInt32();
                                // tower sold at (x,y) to be handled
                                Console.WriteLine("Tower sold at " + x + "," + y);
                                break;
                            case "VirusCall":
                                // virus call to be handled
                                gameState.addVirus();
                                Console.WriteLine("Virus called");
                                break;
                            case "GameOver" :
                                // game over to handle (player wins)
                                break;
                            default:
                                Console.WriteLine("Event type unhandled :" + evt.ToString());
                                break;
                        }


                        break;
                    default:
                        Console.WriteLine("Unhandled type: " + im.MessageType + " " + im.LengthBytes + " bytes");
                        break;
                }
            }
        }


        public void Update(GameTime gameTime)
        {
            if (IPform.shown)
                return;

            if (connected)
            {
                gameState.Update(gameTime);
                if (gameState.Interface.buttonWithIndexPressed(0))
                    sendEvent(Event.VirusCall, 0, 0);
                
            }



            KeyboardState ks = Keyboard.GetState();
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                ChangeState(new MenuState(game));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && oldKS.IsKeyUp(Keys.Enter))
            {
                sendMessage("Hello");
                sendEvent(Event.TowerAdded, 5, 4);
                sendEvent(Event.VirusCall, 0, 0);
            }
            oldKS = ks;

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
        public void Draw(GameTime gameTime)
        {
            if (IPform.shown)
                return;

            if (connected)
                gameState.Draw(gameTime);

        }
        public void Initialize() { }
        public void LoadContent() { }

        public void ChangeState(IState state)
        {
            showDc = false;
            Shutdown();
            game.gameState = state;
        }
        public void Window_ClientSizeChanged() { }

    }
}
