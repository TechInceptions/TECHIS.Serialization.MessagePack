
using MessagePack;
using MessagePack.Resolvers;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;
//using MsgPack.Serialization;

namespace TECHIS.Serialization.MessagePack
{
    public class BinarySerializer : TECHIS.Serialization.Abstractions.ISerializer
    {
        //ConcurrentDictionary<string, object> _Serializers = new ConcurrentDictionary<string, object>();

        //private MessagePackSerializer<T> GetSerializer<T>()
        //{
        //    return (MessagePackSerializer<T>)(_Serializers.GetOrAdd(typeof(T).FullName, (key) => MessagePackSerializer.Get<T>()));
        //}
        //public T Deserialize<T>(Stream stream)
        //{
        //    return (GetSerializer<T>()).Unpack(stream);
        //}

        //public Task<T> DeserializeAsync<T>(Stream stream)
        //{
        //    return (GetSerializer<T>()).UnpackAsync(stream);
        //}

        //public void Serialize<T>(T obj, Stream stream)
        //{
        //   (GetSerializer<T>()).Pack(stream, obj);
        //}

        //public Task SerializeAsync<T>(T obj, Stream stream)
        //{
        //   return (GetSerializer<T>()).PackAsync(stream, obj);
        //}

        private readonly MessagePackSerializerOptions _SerializerOptions = ContractlessStandardResolver.Options;

        public int SuggestedMemorySize { get; } = 0;
        public BinarySerializer() { }
        public BinarySerializer(int suggestedMemorySize)
        {
            SuggestedMemorySize = suggestedMemorySize;
            _SerializerOptions = ContractlessStandardResolver.Options.WithSuggestedContiguousMemorySize(suggestedMemorySize);
        }

        public T Deserialize<T>(Stream stream)
        {
            return MessagePackSerializer.Deserialize<T>(stream, _SerializerOptions);
        }

        public Task<T> DeserializeAsync<T>(Stream stream)
        {
            return MessagePackSerializer.DeserializeAsync<T>(stream, _SerializerOptions).AsTask();
        }

        public void Serialize<T>(T obj, Stream stream)
        {
            MessagePackSerializer.Serialize(stream, obj, _SerializerOptions);
        }

        public Task SerializeAsync<T>(T obj, Stream stream)
        {
            return MessagePackSerializer.SerializeAsync(stream, obj, _SerializerOptions);
        }
    }
}
