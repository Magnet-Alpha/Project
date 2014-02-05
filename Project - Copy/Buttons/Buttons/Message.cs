using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Buttons
{
    public class Message
    {
        public byte[] data;

        public Message(byte[] data)
        {
            this.data = data;
            Serialize(data);
        }

        public Message() { }

        public void Serialize(object anySerializableObject)
        {
            using (var memoryStream = new MemoryStream())
            {
                (new BinaryFormatter()).Serialize(memoryStream, anySerializableObject);
                data = memoryStream.ToArray();
            }
        }

        public object Deserialize()
        {
            using (var memoryStream = new MemoryStream(data))
                return (new BinaryFormatter()).Deserialize(memoryStream);
        }
    }
}
