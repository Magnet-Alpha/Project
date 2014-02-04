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
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

namespace Buttons
{
    public class MultiplayerState : IState
    {
        Game1 game;
        Socket sck;
        EndPoint epLocal, epRemote;
        IPAddress localAddress, friendAddress;
        int localPort = 80;
        int friendPort =81;

        public MultiplayerState(Game1 game)
        {
            this.game = game;
            localAddress = GetLocalIP();
            try
            {
                friendAddress = GetFriendIP();
                Console.WriteLine("Connected to " + friendAddress.ToString());
            }
            catch
            {
                Console.WriteLine("No connection found. Waiting for connection.");
            }
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

                epLocal = new IPEndPoint(localAddress, localPort);
                sck.Bind(epLocal);

                epRemote = new IPEndPoint(friendAddress, friendPort);
                sck.Connect(epRemote);
                
                byte[] buffer = new byte[1500];
                sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(MessageCallBack), buffer);


        }

        public void Update(GameTime gameTime) { }
        public void Draw(GameTime gameTime) { }
        public void Initialize() { }
        public void LoadContent() { }
        public void ChangeState(IState state) { }
        public void Window_ClientSizeChanged(object sender, EventArgs e) { }


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

        private IPAddress GetFriendIP()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                
                Process[] allProcs = Process.GetProcesses(GetMachineNameFromIPAddress(ip.ToString()));
                Console.WriteLine(GetMachineNameFromIPAddress(ip.ToString()));
                
                    foreach (Process p in allProcs)
                    {
                        if (p.ProcessName == "You'll Catch A virus")
                            return ip;
                    }

                
            }
            throw (new Exception("No connections found"));
        }


        void MessageCallBack(IAsyncResult aResult)
        {
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
                // Machine not found...
            }
            return machineName;
        }

    }
}
