namespace APITaskManagement.Logic.Api.Data
{
    public class DutchNedCustomer
    {
        public virtual string Name { get; set; }
        public virtual string Street { get; set; }
        public virtual string HouseNumber { get; set; }
        public virtual string HouseNumberAddition { get; set; }
        public virtual string ZipCode { get; set; }
        public virtual string City { get; set; }
        public virtual string CountryCode { get; set; }
        public virtual string Email { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string Instructions { get; set; }
     }
}
