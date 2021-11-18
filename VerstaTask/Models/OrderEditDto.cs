namespace VerstaTask.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class OrderEditDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле обязательно")]
        [DisplayName("Город отправителя")]
        [DataType(DataType.Text,ErrorMessage = "Неправильные данные!")]
        public string SenderCity { get; set; }

        [Required(ErrorMessage = "Поле обязательно")]
        [DisplayName("Адрес отправителя")]
        [DataType(DataType.Text,ErrorMessage = "Неправильные данные!")]
        public string SenderAddress { get; set; }

        [Required(ErrorMessage = "Поле обязательно")]
        [DisplayName("Город получателя")]
        [DataType(DataType.Text,ErrorMessage = "Неправильные данные!")]
        public string RecipientCity { get; set; }

        [Required(ErrorMessage = "Поле обязательно")]
        [DisplayName("Адрес получателя")]
        [DataType(DataType.Text,ErrorMessage = "Неправильные данные!")]
        public string RecipientAddress { get; set; }

        [Required(ErrorMessage = "Поле обязательно")]
        [DisplayName("Вес груза")]
        public float CargoWeight { get; set; }

        [Required(ErrorMessage = "Поле обязательно")]
        [DisplayName("Дата забора груза")]
        [DataType(DataType.DateTime,ErrorMessage = "Неправильные данные!")]
        public DateTime PickupDate { get; set; }
    }
}
