using System.Reflection;

namespace OutsourceTracker.Authorization;

public readonly struct ApplicationRole
{
    public string Name { get; }

    public string Permission { get; }

    public ApplicationRole(string name, string permission)
    {
        Name = name;
        Permission = permission;
    }

    public static ApplicationRole TrailerViewer { get; } = new ApplicationRole("Trailer Viewer", "Trailers.Read");
    public static ApplicationRole TrailerAdmin { get; } = new ApplicationRole("Trailer Admin", "Trailers.Write");
    public static ApplicationRole TrailerSpotter { get; } = new ApplicationRole("Trailer Spotter", "Trailers.UpdateLocation");
    public static ApplicationRole ZoneViewer { get; } = new ApplicationRole("Zone Viewer", "Zones.Read");
    public static ApplicationRole ZoneAdmin { get; } = new ApplicationRole("Zone Admin", "Zones.Write");
    public static IEnumerable<ApplicationRole> Roles() => RoleCollection.Values;
    private static IReadOnlyDictionary<string, ApplicationRole> RoleCollection { get; }

    static ApplicationRole()
    {
        var properties = typeof(ApplicationRole)
            .GetProperties(BindingFlags.Public | BindingFlags.Static)
            .Where(p => p.PropertyType == typeof(ApplicationRole));

        Dictionary<string, ApplicationRole> roles = new Dictionary<string, ApplicationRole>();

        foreach (var item in properties)
        {
            ApplicationRole role = (ApplicationRole)item.GetValue(null)!;
            roles.Add(role.Permission, role);
        }

        RoleCollection = roles.AsReadOnly();
    }
}
