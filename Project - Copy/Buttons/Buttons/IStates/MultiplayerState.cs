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




namespace Buttons
{
    public class MultiplayerState : IState
    {
        Game1 game;
        Socket sck;
        EndPoint epLocal, epRemote;
        SpriteFont font;
        string localIp, remoteIp = "192.168.1.8";
        int localPort = 1580, remotePort = 1581;
        KeyboardState olKS = new KeyboardState();
        byte[] buffer;
        bool dc = false;

        public MultiplayerState(Game1 game)
        {
            this.game = game;
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            localIp = GetLocalIP();
            remoteIp = localIp;
            remotePort = localPort;
            //Console.WriteLine(localIp);
            connect();
            //sendMessage("Hello");
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
            if (dc)
                return;

            epLocal = new IPEndPoint(IPAddress.Any, localPort);
            epRemote = new IPEndPoint(IPAddress.Parse(remoteIp), remotePort);
            sck.Bind(epLocal);

            sck.Connect(epRemote);
            

            buffer = new byte[1500];
            sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(MessageCallBack), buffer);

            //Console.WriteLine("Connected to " + remoteIp);
        }


        void sendMessage(string str)
        {
            if (dc)
                return;

            System.Text.ASCIIEncoding enc = new ASCIIEncoding();
            byte[] msg = new byte[8000];
            msg = enc.GetBytes(str);
            sck.Send(msg);
        }

        void MessageCallBack(IAsyncResult aResult)
        {
            if (dc)
                return;

            Console.WriteLine("Message received");

            int size = sck.EndReceiveFrom(aResult, ref epRemote);


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

            KeyboardState ks = Keyboard.GetState();
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                ChangeState(new MenuState(game));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && olKS.IsKeyUp(Keys.Enter))
            {
                sendMessage("Hello");
            }
            olKS = ks;

        }
        public void Draw(GameTime gameTime) { }
        public void Initialize() { }
        public void LoadContent() { }
        public void ChangeState(IState state)
        {
            dc = true;
            sck.Close();
            game.gameState = state;
        }
        public void Window_ClientSizeChanged() { }

    }
}
