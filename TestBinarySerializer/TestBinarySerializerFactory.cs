using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TECHIS.Serialization.MessagePack.Test
{
    public class TestBinarySerializerFactory
    {
        [Fact]
        public void TestGetSerializerMemSizeValueCalculation()
        {
            var factory = new SerializerFactory();
            var serializer = factory.GetSerializer();
            Assert.NotNull(serializer);

            //check that the standard serializer is created
            Assert.Equal(((BinarySerializer)serializer).SuggestedMemorySize, (new BinarySerializer()).SuggestedMemorySize);

            //check that small mem sizes default to 'small object mem size'
            serializer = factory.GetSerializer(1);
            Assert.NotNull(serializer);
            Assert.Equal(((BinarySerializer)serializer).SuggestedMemorySize,SerializerFactory.MEMORYSIZE_SMALOBJECT);


            //check that small mem sizes default to 'small object mem size'
            int largeMem = 1128;
            serializer = factory.GetSerializer(largeMem);
            Assert.NotNull(serializer);
            Assert.Equal(((BinarySerializer)serializer).SuggestedMemorySize, largeMem);
        }
        [Fact]
        public void TestGetSerializerInstanceReuse()
        {
            var factory = new SerializerFactory();
            var serializer = factory.GetSerializer();
            Assert.NotNull(serializer);

            //Same serializer for 2 default calls
            Assert.Same(serializer, factory.GetSerializer());

            serializer = factory.GetSerializer(1);
            Assert.NotNull(serializer);

            //Same serializer for 3 'small object' calls
            Assert.Same(serializer, factory.GetSerializer(1));
            Assert.Same(serializer, factory.GetSerializer(SerializerFactory.MEMORYSIZE_SMALOBJECT));


            int largeMem = 1128;

            serializer = factory.GetSerializer(largeMem);
            Assert.NotNull(serializer);

            //Same serializer for 2 calls with the same mem spec
            Assert.Same(serializer, factory.GetSerializer(largeMem));


        }
        [Fact]
        public void TestGetSerializerByName()
        {
            var factory = new SerializerFactory();
            var serializer = factory.GetStandardSerializer();
            Assert.NotNull(serializer);

            //Same serializer for 2 default calls
            Assert.Same(serializer, factory.GetSerializer());

            serializer = factory.GetSmallObjectSerializer();
            Assert.NotNull(serializer);

            //Same serializer for 2 'small object' calls
            Assert.Same(serializer, factory.GetSerializer(SerializerFactory.MEMORYSIZE_SMALOBJECT));


        }
    }
}
