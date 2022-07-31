using System;
using System.Collections.Generic;
using System.Text;

namespace TECHIS.Serialization.MessagePack
{
    public interface ISerializerFactory
    {
        TECHIS.Serialization.Abstractions.ISerializer GetSerializer(int suggestedMemorySize);
        TECHIS.Serialization.Abstractions.ISerializer GetSmallObjectSerializer();
        TECHIS.Serialization.Abstractions.ISerializer GetStandardSerializer();
    }
}
