using ProtoBuf.Grpc.Configuration;
using Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    [ServiceContract]
    public interface ILevelService
    {
        [OperationContract]
        public Task<Result<List<LevelShared>>> GetAllLevelsAsync();
    }
    [DataContract]
    public class LevelShared
    {
        [DataMember(Order = 1)]
        public string LevelName { get; set; }
    }
}
