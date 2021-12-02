namespace VerstaTask.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Order
    {
        public long Id { get; set; }

        public string SenderCity { get; set; }

        public string SenderAddress { get; set; }

        public string RecipientCity { get; set; }

        public string RecipientAddress { get; set; }

        public float CargoWeight { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime PickupDate { get; set; }
    }
}
