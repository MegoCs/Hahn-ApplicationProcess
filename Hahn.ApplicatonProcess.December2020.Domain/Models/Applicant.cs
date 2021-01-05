namespace Hahn.ApplicatonProcess.December2020.Domain.Models
{
    public class Applicant
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string Address { get; set; }
        public string CountryOfOrigin { get; set; }
        public string EmailAdress { get; set; }
        public int Age { get; set; }
        public bool Hired { get; set; }

        public Applicant(int iD, string name, string familyName, string address, string countryOfOrigin, string emailAdress, int age, bool hired = false)
        {
            ID = iD;
            Name = name;
            FamilyName = familyName;
            Address = address;
            CountryOfOrigin = countryOfOrigin;
            EmailAdress = emailAdress;
            Age = age;
            Hired = hired;
        }
    }
}