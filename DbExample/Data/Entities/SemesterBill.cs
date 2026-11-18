using System.ComponentModel.DataAnnotations;

namespace DbExample.Data.Entities
{
    public class SemesterBill
    {
        [Key]
        public int BillId{ get; set; }

        public decimal TotalAmount { get; set; }

        public int RegistrationId { get; set; }

        public string PaymentOption { get; set; }


        public DateOnly Paymentdate { get; set; }



    }
}
