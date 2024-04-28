using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HMS.Library.Models
{
	public class AppointmentPrescribe
	{
		[Key]
		[ForeignKey(nameof(Appointment))]
		public int AppointmentID { get; set; }
		[Required]
		public string Symptoms { get; set; }

		[Required]
		[StringLength(250)]
		public string Medication { get; set; }
		public string? Dosgae { get; set; }
		public string? Instructions { get; set; }
		[Required]
		[DataType(DataType.DateTime)]
		public DateTime FollowupDate { get; set; }

		public string? FollowupNotes { get; set; }


		//Navigation Property
		public Appointment? Appointment { get; set; }


	}
}
