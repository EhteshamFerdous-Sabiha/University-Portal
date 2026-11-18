using System.ComponentModel.DataAnnotations;

namespace DbExample.Data.Entities
{
    public class StudentPayment
    {

        [Key]

        public int PaymentId { get; set; }

        public string Bank { get; set; }

        public int BillId { get; set; }

        public int InstallmentNo { get; set; }

        public string PaymentStatus { get; set; }

        public string TransactionRef { get; set; }



    }
}
