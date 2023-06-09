namespace WebClient.Utils;

public static class PolicyName
{
    public const string ADMIN = nameof(Role.Admin);
    public const string USER = nameof(Role.User);
}

public enum Role
{
    Admin,
    User
}
