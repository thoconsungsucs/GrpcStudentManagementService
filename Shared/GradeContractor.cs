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
    public interface IGradeService
    {
        [OperationContract]
        public Task<Result<List<GradeShared>>> GetGradesByLevelNameAsync(string LevelName);
        [OperationContract]
        public Task<Result<List<SelectionItem>>> GetGradeSelectionByLevelNameAsync(string levelName);
    }
    [DataContract]
    public class GradeShared
    {
        [DataMember(Order = 1)]
        public int GradeId { get; set; }
        [DataMember(Order = 2)]
        public string GradeName { get; set; }
    }
}
