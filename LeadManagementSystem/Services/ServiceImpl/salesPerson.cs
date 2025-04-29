namespace LeadManagementSystem.Services.ServiceImpl
{
    internal class salesPerson
    {
        public salesPerson()
        {
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public decimal FirstPayment { get; internal set; }
        public decimal RecurringPercentage { get; internal set; }
        public string PhoneNumber { get; internal set; }
        public short PaymentType { get; internal set; }
    }
}