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
using System.Windows.Forms;


namespace Buttons
{
    public enum Event
    {
        TowerAdded,
        TowerSold,
        VirusCall,
        GameOver,
        LifeChanged
    }

    public enum Status
    {
        Waiting,
        InGame
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
        public bool showDc = true;
        Status gameStatus;
        MouseState oldMs = new MouseState();
        public int life = 0;

        public MultiplayerState3(Game1 game)
        {
            this.game = game;
            NetPeerConfiguration config = new NetPeerConfiguration("YCAV");
            client = new NetClient(config);
            client.RegisterReceivedCallback(new SendOrPostCallback(GotMessage));
            gameState = new GameState(game, this);
            /*if (game.settings.fullScreen)
            {
                var form = (Form)Form.FromHandle(game.Window.Handle);
                form.WindowState = FormWindowState.Minimized;
            }
            */
            IPform = new GetIPForm(this);
            IPform.showForm();
            

            gameStatus = Status.Waiting;
        }


        Status GameStatus
        {
            get { return gameStatus; }
            set
            {
                if (gameStatus == Status.Waiting && value == Status.InGame)
                {
                    gameState = new GameState(game, this);
                    MessageBox.Show(Strings.stringForKey("LanGameStarts"));
                }
                gameStatus = value;
            }
        }


        public void formClosed()
        {
            Console.WriteLine("trying to connect " + IPform.ip.ToString());
            Connect(IPform.ip.ToString(), port);
        }

        public void sendEvent(Event evt, int x, int y)
        {
            NetOutgoingMessage msg = client.CreateMessage();
            string str = "#" + evt.ToString();
            if (evt == Event.LifeChanged || evt == Event.VirusCall)
            {
                str += x;
            }
            msg.Write(str);            

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
        public void Shutdown()
        {
            //sendMessage(GetLocalIP() + " disconnected");
            if(client.ConnectionStatus == NetConnectionStatus.Connected)
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
                        if (evt[0] == '?')
                        {
                            int connections;
                            try
                            {
                                connections = Convert.ToInt32(evt.Substring(1));
                                if (connections == 2)
                                {
                                    GameStatus = Status.InGame;
                                }

                            }
                            catch
                            {
                                Console.WriteLine("Invalid connection report: " + evt);
                            }
                        }
                        else
                        {
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
                                case "VirusCall1":
                                    // virus call to be handled
                                    gameState.addVirus(1);
                                    Console.WriteLine("Virus called");
                                    break;
                                case "VirusCall2":
                                    // virus call to be handled
                                    gameState.addVirus(2);
                                    Console.WriteLine("Virus called");
                                    break;
                                case "VirusCall3":
                                    // virus call to be handled
                                    gameState.addVirus(3);
                                    Console.WriteLine("Virus called");
                                    break;
                                case "GameOver":
                                    gameState.win = true;
                                    showDc = false;
                                    // game over to handle (player wins)
                                    break;
                                case "#dc":
                                    MessageBox.Show(Strings.stringForKey("Dc"));
                                    ChangeState(new MenuState(game));
                                    break;
                                case "Server down":
                                    showDc = false;
                                    ChangeState(new MenuState(game));
                                    MessageBox.Show(Strings.stringForKey("ServerDown"));
                                    break;
                                default:
                                    if (evt.Contains("LifeChanged"))
                                    {
                                        string str = "";
                                        int i;
                                        for (i = 0; str != "LifeChanged"; i++)
                                        {
                                            str += evt[i];
                                        }

                                        life = Convert.ToInt32(evt.Substring(i));
                                    }
                                    else
                                    {
                                        Console.WriteLine("Event type unhandled :" + evt.ToString());
                                    }
                                    break;
                            }
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
            MouseState ms = Mouse.GetState();
            if (IPform.shown)
                return;

            if (connected)
            {
                gameState.Update(gameTime);
                if (gameState.Interface.buttonWithIndexPressed(0) && oldMs.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                    sendEvent(Event.VirusCall, 0, 0);
                
            }
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape) && oldKS.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.Escape))
                ChangeState(new MenuState(game));
            
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
