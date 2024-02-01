using AgileRap_Process.Data;
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
        public void InsertCreate(AgileRap_ProcessContext dbContext, Work work)
        {
            this.No = 1;
            this.Project = work.Project;
            this.Name = work.Name;
            this.DueDate = work.DueDate;
            this.StatusID = work.StatusID;
            this.Remark = work.Remark;
            this.CreateBy = work.CreateBy;
            this.UpdateBy = work.UpdateBy;
            this.CreateDate = DateTime.Now;
            this.UpdateDate = DateTime.Now;
            this.IsDelete = false;
        }
        public void InsertEdit(AgileRap_ProcessContext dbContext, Work work,List<WorkLog> workLogDBList)
        {
            this.WorkID = work.ID;
            this.Project = work.Project;
            this.Name = work.Name;
            this.No = workLogDBList.Last().No + 1;
            this.DueDate = work.DueDate;
            this.StatusID = work.StatusID;
            this.Remark = work.Remark;
            this.CreateBy = work.CreateBy;
            this.UpdateBy = work.UpdateBy;
            this.CreateDate = DateTime.Now;
            this.UpdateDate = DateTime.Now;
            this.IsDelete = work.IsDelete;
        }
        [NotMapped]
        public WorkLog? WorkLogChangeNext { get; set; }
    }
}
