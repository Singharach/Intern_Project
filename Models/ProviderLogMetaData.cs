using AgileRap_Process.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared;
using System.Diagnostics.Eventing.Reader;

namespace AgileRap_Process.Models
{
    public class ProviderLogMetaData
    {
    }
    [ModelMetadataType(typeof(ProviderLogMetaData))]
    public partial class ProviderLog
    {
        public void Insert(AgileRap_ProcessContext dbContext, ProviderLog i)
        {
            this.UserID = i.UserID;
			this.CreateBy = i.CreateBy;
			this.CreateDate = DateTime.Now;
			this.UpdateDate = DateTime.Now;
			this.IsDelete = false;

		}
        //public void InsertEdit(AgileRap_ProcessContext dbContext, int i)
        //{

        //    this.CreateDate = DateTime.Now;
        //    this.UpdateDate = DateTime.Now;
        //    this.IsDelete = false;
        //    this.UserID = i;
        //    dbContext.Provider.Add(this);
            
        //}
        
    }
}
