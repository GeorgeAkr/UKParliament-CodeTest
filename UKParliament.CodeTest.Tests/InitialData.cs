using UKParliament.CodeTest.Data;

namespace UKParliament.CodeTest.Tests
{
    public static class InitialData
    {
        public static void PopulateTestData(PersonManagerContext context)
        {
            context.People.Add(new Person() { Name = "Name_a", Address = "Address_a", Details = "details_a", DateOfBirth = new DateTime(2000, 12, 20) });
            context.People.Add(new Person() { Name = "Name_b", Address = "Address_b", Details = "details_b", DateOfBirth = new DateTime(2000, 12, 21) });
            context.People.Add(new Person() { Name = "Name_c", Address = "Address_c", Details = "details_c", DateOfBirth = new DateTime(2000, 12, 22) });
            context.People.Add(new Person() { Name = "Name_d", Address = "Address_d", Details = "details_d", DateOfBirth = new DateTime(2000, 12, 23) });

            context.SaveChanges();
        }
    }
}