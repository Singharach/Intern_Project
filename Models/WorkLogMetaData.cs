using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgileRap_Process.Models
{
    public class WorkLogMetaData
    {
    }
    [ModelMetadataType(typeof(WorkLogMetaData))]
    public partial class WorkLog
    {
        [NotMapped]
        public WorkLog? WorkLogChangeNext { get; set; }
    }
}
