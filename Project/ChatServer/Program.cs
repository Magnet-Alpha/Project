using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

using Lidgren.Network;

using SamplesCommon;

namespace ChatServer
{
	static class Program
	{
		private static Form1 s_form;
		private static NetServer s_server;
		private static NetPeerSettingsWindow s_settingsWindow;
		
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
			NativeMethods.AppendText(s_form.richTextBox1, text);
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
                                NetOutgoingMessage msg = s_server.CreateMessage();
                                msg.Write("dc");
                                Output("Broadcasting 'dc'");
                                s_server.SendMessage(msg, s_server.Connections, NetDeliveryMethod.ReliableOrdered, 0);
                            }
							
							break;
						case NetIncomingMessageType.Data:
							// incoming chat message from a client
							string chat = im.ReadString();

							Output("Broadcasting '" + chat + "'");

							// broadcast this to all connections, except sender
							List<NetConnection> all = s_server.Connections; // get copy
							all.Remove(im.SenderConnection);

							if (all.Count > 0)
							{
								NetOutgoingMessage om = s_server.CreateMessage();
								om.Write(NetUtility.ToHexString(im.SenderConnection.RemoteUniqueIdentifier) + " said: " + chat);
								s_server.SendMessage(om, all, NetDeliveryMethod.ReliableOrdered, 0);
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

		// called by the UI
		public static void StartServer()
		{
			s_server.Start();
		}

		// called by the UI
		public static void Shutdown()
		{
			s_server.Shutdown("Requested by user");
		}

		
	}
}
