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
        IPAddress localIP, remoteIP;
        int localPort = 80;
        int friendPort = 80;
        TextButton backButton;
        SpriteFont font
            ;

        public MultiplayerState(Game1 game)
        {
            this.game = game;
            /*localIP = GetLocalIP();
            try
            {
                remoteIP = IPAddress.Parse(GetFriendIP());
                //Console.WriteLine("Connected to " + remoteIP.ToString());
            }
            catch
            {
                Console.WriteLine("No connection found. Waiting for connection.");
            }


            epLocal = new IPEndPoint(localIP, localPort);
            epRemote = new IPEndPoint(remoteIP, friendPort);



            sck = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);


            sck.Bind(epRemote);



            sck.Connect(epLocal);

            byte[] buffer = new byte[1500];
            sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(MessageCallBack), buffer);
            SendMessage("Connection received");*/
            localIP = GetLocalIP();
            font = game.Content.Load<SpriteFont>("font");
            LoadContent();
        }


        void ShowConnectedPCs()
        {
            string str = GetFriendIP();
            game.spriteBatch.DrawString(font, str, new Vector2(game.width / 2 - font.MeasureString(str).X / 2, 20), Color.White); 
        }

        public void Update(GameTime gameTime)
        {
            backButton.Update();
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                SendMessage("lol");
                Console.WriteLine("Message sent");
            }
            if (backButton.clicked)
                ChangeState(new MenuState(game));

        }
        public void Draw(GameTime gameTime) {
            game.spriteBatch.Draw(Textures.background, new Rectangle(0,0,game.width, game.height), Color.White);
            backButton.Draw();
            ShowConnectedPCs();

        }
        public void Initialize() { }
        public void LoadContent()
        {
            backButton = new TextButton(font, game, "Back", new Vector2(20, game.height - 80));
        }
        public void ChangeState(IState state) {

            game.gameState = state;
        }
        public void Window_ClientSizeChanged() { }


        private IPAddress GetLocalIP()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip;
            }
            return IPAddress.Parse("127.0.0.1");
        }

        private string GetFriendIP()
        {
            //Console.WriteLine(GetLocalIP());

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip != localIP)
                {
                    Console.WriteLine(GetMachineNameFromIPAddress(ip.ToString()));
                    Process[] allProcs = Process.GetProcesses(GetMachineNameFromIPAddress(ip.ToString()));
                    Console.WriteLine(GetMachineNameFromIPAddress(ip.ToString()));

                    foreach (Process p in allProcs)
                    {
                        if (p.ProcessName == "You'll Catch A virus")
                            return GetMachineNameFromIPAddress(ip.ToString()) + ": " + ip.ToString();
                    }
                }


            }
            return "No PCs connected on network";
        }


        void MessageCallBack(IAsyncResult aResult)
        {

            try
            {
                Console.WriteLine("Message received");
                int size = sck.EndReceiveFrom(aResult, ref epRemote);
                if (size > 0)
                {
                    byte[] receivedData = (byte[])aResult.AsyncState;

                    Message message = new Message(receivedData);
                    object data = message.Deserialize();
                    if (data is TestClass)
                    {
                        Console.WriteLine("From " + remoteIP.Address.ToString() + " : " + ((TestClass)data).text);
                    }
                }

                byte[] buffer = new byte[1500];
                sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(MessageCallBack), buffer);



            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        void SendMessage(object obj)
        {
            try
            {
                byte[] msg = new byte[1500];
                Message m = new Message();
                m.Serialize(obj);

                sck.Send(m.data);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static string GetMachineNameFromIPAddress(string ipAdress)
        {
            string machineName = string.Empty;
            try
            {
                IPHostEntry hostEntry = Dns.GetHostEntry(ipAdress);

                machineName = hostEntry.HostName;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return machineName;
        }

        // Convert an object to a byte array
        private byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }
        // Convert a byte array to an Object
        private Object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object obj = (Object)binForm.Deserialize(memStream);
            return obj;
        }

    }
}
