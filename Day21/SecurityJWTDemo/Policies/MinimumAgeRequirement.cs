using Microsoft.AspNetCore.Authorization;

public class MinimumAgeRequirement : IAuthorizationRequirement
{
    public int Age { get; }

    public MinimumAgeRequirement(int age)
    {
        Age = age;
    }
}