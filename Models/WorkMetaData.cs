using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AgileRap_Process.Models
{
	public class WorkMetaData
	{
		[Display(Name = "Assign By/ Requester")]
		public int? Createby { get; set; }
		[Display(Name = "Assign By/ Provider")]
		public int? Updateby { get; set; }
        [Display(Name = "Created Date")]
        public DateTime? CreateDate { get; set; }
        [Display(Name = "Due Date")]
        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }
	}
	[ModelMetadataType(typeof(WorkMetaData))]
	public partial class Work
	{
		[NotMapped]
		public string? ProviderValue { get; set; }
		[NotMapped]
		public bool IsSelectAll { get; set; }
		[NotMapped]
		public string CreateDates
		{
			get
			{
				var CreateDates = string.Format("{0:dd/MM/yyyy}", CreateDate);
				return CreateDates;
			}
		}
		[NotMapped]
		public string DueDates
		{
			get
			{
				var DueDates = string.Format("{0:dd/MM/yyyy}", DueDate);
				return DueDates;
			}
		}

        public bool IsEqual(Work workCompare, bool skipProvider = false)
        {
            //! Start Check Provider is Equal
            bool ProviderResult = skipProvider;

            if (this.Provider != null && workCompare != null && ProviderResult == false)
            {
                List<Provider> originalProvider = this.Provider.ToList();
                List<Provider> compareProvider = workCompare.Provider.ToList();
                if (originalProvider.Count == compareProvider.Count)
                {
                    for (int i = 0; i < originalProvider.Count; i++)
                    {
                        //! Loop all Provider if not Equal will be false
                        ProviderResult = originalProvider.ToList()[i].IsEqual(compareProvider.ToList()[i]);
                        if (!ProviderResult) break;
                    }
                }
            }
            //! End Check Provider
            if (!ProviderResult) return false;

            var originalProperties = this.GetType().GetProperties();
            var compareProperties = workCompare.GetType().GetProperties();

            //! Iterate over each property in the original model
            foreach (var originalProperty in originalProperties)
            {
                //! Skip Property
                if (originalProperty.Name == "Provider") continue;
                if (originalProperty.Name == "UpdateBy") continue;
                if (originalProperty.Name == "UpdateDate") continue;
                if (originalProperty.Name == "WorkLog") continue;

                //! Get the corresponding property in the copied model
                var compareProperty = compareProperties.FirstOrDefault(p => p.Name == originalProperty.Name);

                //! If the property doesn't exist in the copied model, the models are not the same
                if (compareProperty == null)
                {
                    return false;
                }


                //! DateTime Inequality Check if Equal
                if (originalProperty.Name == "CreateDate")
                {
                    var original = originalProperty.GetValue(this);
                    var compare = compareProperty.GetValue(workCompare);
                    DateTime? originalDateTime = original as DateTime?;
                    DateTime? compareDateTime = compare as DateTime?;
                    if (originalDateTime != null && compareDateTime != null)
                    {
                        if (originalDateTime.Value.Hour != compareDateTime.Value.Hour)
                        {
                            return false;
                        }
                        if (originalDateTime.Value.Minute != compareDateTime.Value.Minute)
                        {
                            return false;
                        }
                        if (originalDateTime.Value.Second != compareDateTime.Value.Second)
                        {
                            return false;
                        }
                    }
                    continue;
                }

                //! Compare the values of the properties
                if (!object.Equals(originalProperty.GetValue(this), compareProperty.GetValue(workCompare)))
                {
                    return false;
                }
            }

            //! If all properties are equal, the models are the same
            return true;
        }

    }
}
