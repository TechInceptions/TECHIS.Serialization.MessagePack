using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TECHIS.EndApps.AppModel.Business;

namespace TECHIS.Serialization.MessagePack.Test
{
    [TestClass]
    public class TestBinarySerializer
    {
        private BinarySerializer _BinarySerializer;

        [TestMethod]
        public void Serialize()
        {
            var bs = GetSerializer();

            int serializationCount = 3;

            for (int i = 0; i < serializationCount; i++)
            {
                var id = Guid.NewGuid();
                RoleMember rm = BuildRoleMember(id);

                using (MemoryStream ms = new MemoryStream())
                {
                    bs.Serialize(rm, ms);
                    Assert.IsTrue(ms.Length > 0);
                }
            }
        }

        [TestMethod]
        public void DeSerialize()
        {
            var bs = GetSerializer();

            int serializationCount = 3;
            int childCount = 3;

            List<Guid> ids = new List<Guid>(serializationCount);
            List<byte[]> dataList = new List<byte[]>(serializationCount);
            List<byte[]> dataList2 = new List<byte[]>(serializationCount);
            List<RoleMember> members = new List<RoleMember>(serializationCount);
            List<SecurityRole> roles = new List<SecurityRole>(serializationCount);
            for (int i = 0; i < serializationCount; i++)
            {
                ids.Add( Guid.NewGuid());
                RoleMember rm = BuildRoleMember(ids[i], childCount);

                using (MemoryStream ms = new MemoryStream())
                {
                    bs.Serialize(rm, ms);
                    dataList.Add(ms.ToArray());
                }

                SecurityRole sr = BuildSecurityRole(Guid.NewGuid());
                using (MemoryStream ms = new MemoryStream())
                {
                    bs.Serialize(sr, ms);
                    dataList2.Add(ms.ToArray());
                }

            }
            for (int i = 0; i < serializationCount; i++)
            {
                using (MemoryStream ms = new MemoryStream(dataList[i]))
                {
                    members.Add(bs.Deserialize<RoleMember>(ms));
                }

                using (MemoryStream ms = new MemoryStream(dataList2[i]))
                {
                    roles.Add(bs.Deserialize<SecurityRole>(ms));
                }
            }


            for (int i = 0; i < serializationCount; i++)
            {
                Assert.IsTrue(members[i].ApplicationId == ids[i]);
                Assert.IsTrue(members[i].SecurityRoleList.Count == childCount);
            }

        }
        [TestMethod]
        public void DeSerializeAsync()
        {
            var bs = GetSerializer();

            int serializationCount = 3;
            int childCount = 3;

            List<Guid> ids = new List<Guid>(serializationCount);
            List<byte[]> dataList = new List<byte[]>(serializationCount);
            List<byte[]> dataList2 = new List<byte[]>(serializationCount);
            List<RoleMember> members = new List<RoleMember>(serializationCount);
            List<SecurityRole> roles = new List<SecurityRole>(serializationCount);
            for (int i = 0; i < serializationCount; i++)
            {
                ids.Add(Guid.NewGuid());
                RoleMember rm = BuildRoleMember(ids[i], childCount);

                using (MemoryStream ms = new MemoryStream())
                {
                    bs.SerializeAsync(rm, ms).Wait();
                    dataList.Add(ms.ToArray());
                }

                SecurityRole sr = BuildSecurityRole(Guid.NewGuid());
                using (MemoryStream ms = new MemoryStream())
                {
                    bs.SerializeAsync(sr, ms).Wait();
                    dataList2.Add(ms.ToArray());
                }

            }
            for (int i = 0; i < serializationCount; i++)
            {
                using (MemoryStream ms = new MemoryStream(dataList[i]))
                {
                    members.Add(bs.DeserializeAsync<RoleMember>(ms).Result);
                }

                using (MemoryStream ms = new MemoryStream(dataList2[i]))
                {
                    roles.Add(bs.DeserializeAsync<SecurityRole>(ms).Result);
                }
            }


            for (int i = 0; i < serializationCount; i++)
            {
                Assert.IsTrue(members[i].ApplicationId == ids[i]);
                Assert.IsTrue(members[i].SecurityRoleList.Count == childCount);
            }

        }

        private static RoleMember BuildRoleMember(Guid id, int childCount = 2)
        {
            var rm = new RoleMember();
            rm.ApplicationId = id;
            rm.IsAnonymous = true;
            rm.LastActivityDate = DateTime.Now;
            for (int i = 0; i < childCount; i++)
            {
                rm.SecurityRoleList.Add(BuildSecurityRole(Guid.NewGuid()));
            }
            return rm;
        }



        private static SecurityRole BuildSecurityRole(Guid id)
        {
            var sr = new SecurityRole();
            sr.ApplicationId = id;
            sr.ApplicationRegisterId = int.MaxValue;
            sr.RoleName = $"Test: {DateTime.Now.ToLongTimeString()}";

            return sr;
        }
        private BinarySerializer GetSerializer()
        {
            if (_BinarySerializer==null)
            {
                _BinarySerializer = new BinarySerializer();
            }

            return _BinarySerializer;
        }
    }
}
