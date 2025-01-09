namespace WebApplication1.Models
{
	public class Address
	{
		public string Street { get; set; }
		public string City { get; set; }
		public string PostalCode { get; set; }
	}

	public class Contact
	{
		public string Name { get; set; }
		public string Phone { get; set; }
		public Address Address { get; set; }
	}

	public class Company
	{
		public string Name { get; set; }
		public string Industry { get; set; }
		public List<Contact> Contacts { get; set; }
	}

	public class LargeData
	{
		public List<Company> Companies { get; set; }
	}
}
