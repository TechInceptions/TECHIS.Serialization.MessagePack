using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using TECHIS.Serialization.Abstractions;

namespace TECHIS.Serialization.MessagePack
{
    public class SerializerFactory : ISerializerFactory
    {
        public const int MEMORYSIZE_SMALOBJECT = 256; //64*1024

        private ConcurrentDictionary<int,ISerializer> _serializers = new ConcurrentDictionary<int, ISerializer>();

        public ISerializer GetSerializer(int suggestedMemorySize=int.MinValue)
        {
            int key = suggestedMemorySize == int.MinValue ? int.MinValue : (suggestedMemorySize < MEMORYSIZE_SMALOBJECT ? MEMORYSIZE_SMALOBJECT : suggestedMemorySize);
            if (_serializers.TryGetValue(key, out ISerializer serializer))
            {
                return serializer;
            }

            switch (suggestedMemorySize)
            {
                case int.MinValue:
                    serializer = new BinarySerializer();
                    break;
                default:
                    serializer = new BinarySerializer(key);
                    break;
            }
            _serializers[key] = serializer;
            return serializer;

        }

        public ISerializer GetSmallObjectSerializer()
        {
            return GetSerializer(MEMORYSIZE_SMALOBJECT);
        }

        public ISerializer GetStandardSerializer()
        {
            return GetSerializer();
        }
    }
}
