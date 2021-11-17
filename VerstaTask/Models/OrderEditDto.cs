namespace VerstaTask.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class OrderEditDto
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Город отправителя")]
        public string SenderCity { get; set; }
        [Required]
        [DisplayName("Адрес отправителя")]
        public string SenderAddress { get; set; }
        [Required]
        [DisplayName("Город получателя")]
        public string RecipientCity { get; set; }
        [Required]
        [DisplayName("Адрес получателя")]
        public string RecipientAddress { get; set; }
        [Required]
        [DisplayName("Вес груза")]
        public float CargoWeight { get; set; }
        [Required]
        [DisplayName("Дата забора груза")]
        [DataType(DataType.DateTime)]
        public DateTime PickupDate { get; set; }
    }
}
