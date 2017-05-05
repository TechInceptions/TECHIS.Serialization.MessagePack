
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;
using MsgPack.Serialization;

namespace TECHIS.Serialization.MessagePack
{
    public class BinarySerializer : TECHIS.Serialization.Abstractions.ISerializer
    {
        ConcurrentDictionary<string, object> _Serializers = new ConcurrentDictionary<string, object>();

        private MessagePackSerializer<T> GetSerializer<T>()
        {
            return (MessagePackSerializer<T>)(_Serializers.GetOrAdd(typeof(T).FullName, (key) => MessagePackSerializer.Get<T>()));
        }
        public T Deserialize<T>(Stream stream)
        {
            return (GetSerializer<T>()).Unpack(stream);
        }

        public Task<T> DeserializeAsync<T>(Stream stream)
        {
            return (GetSerializer<T>()).UnpackAsync(stream);
        }

        public void Serialize<T>(T obj, Stream stream)
        {
           (GetSerializer<T>()).Pack(stream, obj);
        }

        public Task SerializeAsync<T>(T obj, Stream stream)
        {
           return (GetSerializer<T>()).PackAsync(stream, obj);
        }
    }
}
