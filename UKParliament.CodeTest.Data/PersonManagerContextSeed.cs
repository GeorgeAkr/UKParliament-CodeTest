namespace UKParliament.CodeTest.Data
{
    public class PersonManagerContextSeed
    {
        public static void SeedAsync(PersonManagerContext context)
        {
            if(!context.People.Any())
            {
                var people = new List<Person>();
                for(var i = 1; i < 50; i++)
                {
                    people.Add(
                        new Person()
                        {
                            Id = i,
                            Name = $"Name_{i}",
                            Address = $"Address_{i}",
                            Details = $"Details_blah_{i}",
                            DateOfBirth = new DateTime(2000 - i % 20, i % 12 + 1, i % 28 + 1)
                        });
                }
                context.People.AddRange(people);
                context.SaveChanges();
            }
        }
    }
}
