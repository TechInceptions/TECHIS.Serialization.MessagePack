using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using TECHIS.EndApps.AppModel.Business;
using System.Threading.Tasks;

namespace TECHIS.Serialization.MessagePack.Test
{

    public class TestBinarySerializer
    {
        private BinarySerializer _BinarySerializer;


        [Fact]
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
                    Assert.True(ms.Length > 0);
                }
            }
        }

        [Fact]
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
                Assert.True(members[i].ApplicationId == ids[i]);
                Assert.True(members[i].SecurityRoleList.Count == childCount);
            }

        }
        [Fact]
        public async Task DeSerializeAsync()
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
                    await bs.SerializeAsync(sr, ms);
                    dataList2.Add(ms.ToArray());
                }

            }
            for (int i = 0; i < serializationCount; i++)
            {
                using (MemoryStream ms = new MemoryStream(dataList[i]))
                {
                    members.Add(await bs.DeserializeAsync<RoleMember>(ms) );
                }

                using (MemoryStream ms = new MemoryStream(dataList2[i]))
                {
                    roles.Add(await bs.DeserializeAsync<SecurityRole>(ms) );
                }
            }


            for (int i = 0; i < serializationCount; i++)
            {
                Assert.True(members[i].ApplicationId == ids[i]);
                Assert.True(members[i].SecurityRoleList.Count == childCount);
            }

        }

        [Fact]
        public void DeSerializeDictionary()
        {
            var bs = GetSerializer();

            int serializationCount = 3;

            List<Guid> ids = new List<Guid>(serializationCount);
            List<byte[]> dataList = new List<byte[]>(serializationCount);

            List<List<UserSessionInfo>> members = new List<List<UserSessionInfo>>(serializationCount);
            string userName = "sample";
            for (int i = 0; i < serializationCount; i++)
            {
                ids.Add(Guid.NewGuid());
                UserSessionInfo userinfo = new UserSessionInfo {Key = ids[i], RoleMember = BuildRoleMember(ids[i]), UserID= i, UserName=userName, IgnoreThis = "<NIL>" };
                AddDictionaries(userinfo, "key","a string", "intKey", int.MaxValue, "longKey", long.MinValue);
                List<UserSessionInfo> uis = new List<UserSessionInfo> { userinfo };
                using (MemoryStream ms = new MemoryStream())
                {
                    bs.Serialize(uis, ms);
                    dataList.Add(ms.ToArray());
                }

            }
            for (int i = 0; i < serializationCount; i++)
            {
                using (MemoryStream ms = new MemoryStream(dataList[i]))
                {
                    members.Add(bs.Deserialize<List<UserSessionInfo>>(ms));
                }
            }


            for (int i = 0; i < serializationCount; i++)
            {
                Assert.True(members[i][0].NoSetter == nameof(UserSessionInfo));
                Assert.True(members[i][0].IgnoreThis == null);
                Assert.True(members[i][0].RoleMember.UserId == ids[i]);
                Assert.True(members[i][0].Key == ids[i]);
                Assert.True(members[i][0].StringValues["key"] == "a string");
                Assert.True(members[i][0].IntValues["intKey"] == int.MaxValue,       $"{members[i][0].IntValues["intKey"]} is not equal to {int.MaxValue}");
                Assert.True(members[i][0].LongValues["longKey"] == long.MinValue,    $"{members[i][0].LongValues["longKey"]} is not equal to {long.MinValue}");
            }

        }

        private void AddDictionaries(UserSessionInfo userinfo, string key, string strValue, string intKey, int intValue, string longKey, long longValue)
        {
            userinfo.StringValues = new Dictionary<string, string>();
            userinfo.IntValues = new Dictionary<string, int>();
            userinfo.LongValues = new Dictionary<string, long>();

            userinfo.StringValues[key] = strValue;
            userinfo.IntValues[intKey] = intValue;
            userinfo.LongValues[longKey] = longValue;
        }

        private static RoleMember BuildRoleMember(Guid id, int childCount = 2)
        {
            var rm = new RoleMember();
            rm.UserId = id;
            rm.ApplicationId = id;
            rm.IsAnonymous = true;
            rm.LastActivityDate = DateTime.Now;
            for (int i = 0; i < childCount; i++)
            {
                rm.SecurityRoleList.Add(BuildSecurityRole(Guid.NewGuid()));
            }
            return rm;
        }


        private static Dictionary<string, object> CreateDictionary(RoleMember rm, string rmKey)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add(rmKey, rm);
            dictionary.Add(Guid.NewGuid().ToString(), DateTime.Now.Ticks);
            dictionary.Add(Guid.NewGuid().ToString(), nameof(Serialize));
            return dictionary;
        }
        private static Dictionary<string, object> CreateDictionary(string strValue, int intValue, long longValue)
        {
            return new Dictionary<string, object> {[typeof(string).FullName]=strValue, [typeof(int).FullName] = intValue, [typeof(long).FullName]= longValue };
        }

        private static SecurityRole BuildSecurityRole(Guid id)
        {
            var sr = new SecurityRole
            {
                ApplicationId = id,
                ApplicationRegisterId = int.MaxValue,
                RoleName = $"Test: {DateTime.Now.ToLongTimeString()}"
            };

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
