using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.Net.Sockets;
using Lidgren.Network;
using System.Net;
using SamplesCommon;

namespace Server
{
    static class Program
    {
        private static Form1 s_form;
        public static NetServer s_server;
        private static NetPeerSettingsWindow s_settingsWindow;
        static int con = 0;
        static DateTime startTime;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            s_form = new Form1();
            // set up network
            NetPeerConfiguration config = new NetPeerConfiguration("YCAV");
            config.MaximumConnections = 2;
            config.Port = 14242;
            s_server = new NetServer(config);

            Application.Idle += new EventHandler(Application_Idle);
            Application.Run(s_form);
        }

        private static void Output(string text)
        {
            DateTime now = DateTime.Now;
            NativeMethods.AppendText(s_form.richTextBox1, now.ToString() + ": " + text);
            Thread.Sleep(10);

        }

        static public void formClosed()
        {
            if (s_server.ConnectionsCount > 0)
            {
                NetOutgoingMessage msg = s_server.CreateMessage();
                msg.Write("Server down");
                s_server.SendMessage(msg, s_server.Connections, NetDeliveryMethod.ReliableOrdered, 0);
            }
            Thread.Sleep(10);
        }

        


        private static void Application_Idle(object sender, EventArgs e)
        {


            while (NativeMethods.AppStillIdle)
            {
                NetIncomingMessage im;
                while ((im = s_server.ReadMessage()) != null)
                {
                    // handle incoming message
                    switch (im.MessageType)
                    {
                        case NetIncomingMessageType.DebugMessage:
                        case NetIncomingMessageType.ErrorMessage:
                        case NetIncomingMessageType.WarningMessage:
                        case NetIncomingMessageType.VerboseDebugMessage:
                        case NetIncomingMessageType.StatusChanged:

                            NetConnectionStatus status = (NetConnectionStatus)im.ReadByte();
                            if (status == NetConnectionStatus.Disconnected && s_server.Connections.Count > 0) //disconnection
                            {
                                con = s_server.ConnectionsCount;
                                NetOutgoingMessage msg = s_server.CreateMessage();
                                msg.Write("#dc");
                                Output(NetUtility.ToHexString(im.SenderConnection.RemoteUniqueIdentifier) + " disconnected");
                                if (s_server.Connections.Count > 0)
                                    s_server.SendMessage(msg, s_server.Connections, NetDeliveryMethod.ReliableOrdered, 0);
                            }

                            if (status == NetConnectionStatus.Connected && s_server.ConnectionsCount > 0)
                            {
                                NetOutgoingMessage msg = s_server.CreateMessage();
                                msg.Write("?" + s_server.ConnectionsCount);
                                s_server.SendMessage(msg, s_server.Connections, NetDeliveryMethod.ReliableOrdered,0);
                                string str = NetUtility.ToHexString(im.SenderConnection.RemoteUniqueIdentifier) + " connected";
                                if (s_server.ConnectionsCount == 2)
                                {
                                    str += ", starting game";
                                    startTime = DateTime.Now;
                                }
                                Output(str);
                            }

                            break;
                        case NetIncomingMessageType.Data:
                            // incoming chat message from a client
                            try
                            {
                                string chat = im.ReadString();

                                if (chat[0] == '#')
                                {
                                    switch (chat)
                                    {
                                        case "#VirusCall":
                                            Output(NetUtility.ToHexString(im.SenderConnection.RemoteUniqueIdentifier) + " sent virus");
                                            break;
                                        case "#GameOver":
                                            TimeSpan time = DateTime.Now.Subtract(startTime);
                                            string str = NetUtility.ToHexString(im.SenderConnection.RemoteUniqueIdentifier) + " lost, game ended after ";
                                            if (time.Hours != 0)
                                                str += time.Hours + " hours ";
                                            if (time.Minutes != 0)
                                                str += time.Minutes + " minutes ";
                                            if (time.Seconds != 0)
                                                str += time.Seconds + " seconds";
                                            Output(str);
                                            break;
                                    }

                                    // broadcast this to all connections, except sender
                                    List<NetConnection> all = s_server.Connections; // get copy
                                    all.Remove(im.SenderConnection);

                                    if (all.Count > 0)
                                    {
                                        NetOutgoingMessage om = s_server.CreateMessage();
                                        om.Write(chat.Substring(1));

                                        s_server.SendMessage(om, all, NetDeliveryMethod.ReliableOrdered, 0);
                                    }
                                }
                                else
                                {
                                    Output(chat);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                            break;
                        default:
                            Output("Unhandled type: " + im.MessageType + " " + im.LengthBytes + " bytes " + im.DeliveryMethod + "|" + im.SequenceChannel);
                            break;
                    }
                }
                Thread.Sleep(1);
            }
        }
        static string GetLocalIP()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            }
            return "127.0.0.1";

        }
        // called by the UI
        public static void StartServer()
        {
            Output("Server running with IP " + GetLocalIP());

            s_server.Start();
        }

        // called by the UI
        public static void Shutdown()
        {
            Output("Server shut down");
            s_server.Shutdown("Requested by user");
        }


    }
}
