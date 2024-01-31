using Microsoft.VisualBasic;

namespace AgileRap_Process.Models
{
    public partial class Work
    {
        public int ID { get; set; }
        public int? HeaderID { get; set; }
        public string? Project { get; set; }
        public string? Name { get; set; }
        public DateTime? DueDate { get; set; }
        public int StatusID { get; set; }
        public string? Remark { get; set;}
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }


        public virtual Status Status { get; set; }
        public virtual ICollection<Provider> Provider { get; set;}
        public virtual ICollection<WorkLog> WorkLog { get; set; }
    }
}
