using Shared;
namespace BlazorClient.Models
{
    public static class GenderList
    {
        public static List<GenderSelection> Genders = new List<GenderSelection>
        {
            new GenderSelection { Value = Gender.Male, Text = "Male" },
            new GenderSelection { Value = Gender.Female, Text = "Female" },
        };
    }
}
