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



namespace Buttons
{
    public class MultiplayerState2 : IState
    {
        Game1 game;
        TcpListener server;
        TcpClient client;
        EndPoint epLocal, epRemote;
        SpriteFont font;
        string localIp, remoteIp = "192.168.1.7";
        int localPort = 1580, remotePort = 1581;
        KeyboardState olKS = new KeyboardState();
        bool dc = false;
        Byte[] bytes = new Byte[256];
        String data = null;
        Thread networkingThread, clientThread;

        public MultiplayerState2(Game1 game)
        {
            this.game = game;
            localIp = GetLocalIP();

            server = new TcpListener(IPAddress.Parse(localIp), localPort);
            server.Start();

            networkingThread = new Thread(getData);

            
            server.BeginAcceptTcpClient(new AsyncCallback(acceptClient), null);
            client = new TcpClient(remoteIp, remotePort);
            //networkingThread.Start();
        }

        void acceptClient(IAsyncResult aResult)
        {
            client = ((TcpListener)aResult.AsyncState).EndAcceptTcpClient(aResult);
            Console.WriteLine("Connection accepted");
            networkingThread.Start();
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



        void getData()
        {
            // Enter the listening loop. 
            while (true)
            {
                Console.Write("Waiting for a connection... ");

                // Perform a blocking call to accept requests. 
                // You could also user server.AcceptSocket() here.
                client = server.AcceptTcpClient();
                Console.WriteLine("Connected!");

                data = null;

                // Get a stream object for reading and writing
                NetworkStream stream = client.GetStream();

                int i;

                // Loop to receive all the data sent by the client. 
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    // Translate data bytes to a ASCII string.
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    Console.WriteLine("Received: {0}", data);

                    // Process the data sent by the client.

                }

            }
        }


        void sendMessage(string message)
        {
            if (client == null)
                return;

            // Create a TcpClient. 
            // Note, for this client to work you need to have a TcpServer  
            // connected to the same address as specified by the server, port 
            // combination.



            // Translate the passed message into ASCII and store it as a Byte array.
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

            // Get a client stream for reading and writing. 
            //  Stream stream = client.GetStream();

            NetworkStream stream = client.GetStream();

            // Send the message to the connected TcpServer. 
            stream.Write(data, 0, data.Length);

            Console.WriteLine("Sent: {0}", message);
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
            networkingThread.Abort();
            client.Close();
            server.Stop();

            game.gameState = state;
        }
        public void Window_ClientSizeChanged() { }

    }
}
