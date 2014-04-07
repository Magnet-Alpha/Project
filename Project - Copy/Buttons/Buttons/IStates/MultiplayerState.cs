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


namespace Buttons
{
    public class MultiplayerState : IState
    {
        Game1 game;
        Socket sck;
        EndPoint epLocal, epRemote;
        SpriteFont font;
        string localIp, remoteIp = "192.168.137.2";
        int localPort = 80, remotePort = 81;

        public MultiplayerState(Game1 game)
        {
            this.game = game;
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            localIp = GetLocalIP();
            remoteIp = localIp;
            Console.WriteLine(localIp);
            connect();
            sendMessage("Hello");
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

        void connect()
        {

            epLocal = new IPEndPoint(IPAddress.Parse(localIp), localPort);
            sck.Bind(epLocal);

            epRemote = new IPEndPoint(IPAddress.Parse(remoteIp), remotePort);
            sck.Connect(epRemote);

            byte[] buffer = new byte[1500];
            sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(MessageCallBack), buffer);

            Console.WriteLine("Connected to " + remoteIp);
        }


        void sendMessage(string str)
        {
            System.Text.ASCIIEncoding enc = new ASCIIEncoding();
            byte[] msg = new byte[8000];
            msg = enc.GetBytes(str);
            
            sck.Send(msg);

        }

        void MessageCallBack(IAsyncResult aResult)
        {
            Console.WriteLine("Message received");
            byte[] buffer = new byte[1500];
            int size = 0;

            size = sck.EndReceiveFrom(aResult, ref epRemote);


            if (size > 0)
            {
                byte[] receivedData = new Byte[1464];
                receivedData = (byte[])aResult.AsyncState;

                ASCIIEncoding eEncpding = new ASCIIEncoding();
                string receivedMessage = eEncpding.GetString(receivedData);
                Console.WriteLine("received message:" + receivedMessage);
            }


            sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(MessageCallBack), buffer);
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                game.gameState = new MenuState(game);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                sendMessage("Hello");
            /*byte[] buffer = new byte[100];

            int size = sck.Receive(buffer);

            if(size > 0)
            {
                ASCIIEncoding eEncpding = new ASCIIEncoding();
                string receivedMessage = eEncpding.GetString(buffer);
                Console.WriteLine("received message:" + receivedMessage);
            }
            */

        }
        public void Draw(GameTime gameTime) { }
        public void Initialize() { }
        public void LoadContent() { }
        public void ChangeState(IState state) { }
        public void Window_ClientSizeChanged() { }

    }
}
