namespace IP.IDI.Persistence.Seerders.Entities;

internal sealed class EditorUser : UserBaseData
{
    private static readonly Guid ID = Guid.Parse("01995a71-3f56-7bd8-ad6a-a85fc1ad9d63");

    private EditorUser()
    {
        User = AppUser.Create(
            "editor",
            "editor@email.com.br",
            "AJ4bbHA1daDY0KvTRsvDiR610nxD1dyX8wyAgO4t38RMyHU8pH95qwqC3O8h4PVEOg==");
        User.Id = ID;
        Role = UserEditorRole.Create().Role;
    }

    public static EditorUser Create() => new();
}

internal sealed class ListUser : UserBaseData
{
    private static readonly Guid ID = Guid.Parse("01997e44-4068-7833-b34b-19e212a81c24");

    private ListUser()
    {
        User = AppUser.Create(
            "userlist",
            "userlist@email.com.br",
            "AJ4bbHA1daDY0KvTRsvDiR610nxD1dyX8wyAgO4t38RMyHU8pH95qwqC3O8h4PVEOg==");
        User.Id = ID;
        Role = UserListRole.Create().Role;
    }

    public static ListUser Create() => new();
}

internal sealed class ManagerUser : UserBaseData
{
    private static readonly Guid ID = Guid.Parse("01995a71-3f56-7a1d-befa-fd8e4c6e0b57");

    private ManagerUser()
    {
        User = AppUser.Create(
            "manager",
            "manager@email.com.br",
            "AJ4bbHA1daDY0KvTRsvDiR610nxD1dyX8wyAgO4t38RMyHU8pH95qwqC3O8h4PVEOg==");
        User.Id = ID;
        Role = UserManagerRole.Create().Role;
    }

    public static ManagerUser Create() => new();
}

internal sealed class NoClaimsUser : UserBaseData
{
    private static readonly Guid ID = Guid.Parse("01995a99-ae41-7ea2-88da-335ad7cfe6a5");

    private NoClaimsUser()
    {
        User = AppUser.Create(
            "noclaim",
            "noclaim@email.com.br",
            "AJ4bbHA1daDY0KvTRsvDiR610nxD1dyX8wyAgO4t38RMyHU8pH95qwqC3O8h4PVEOg==");
        User.Id = ID;
        Role = UserRoleNoClaims.Create().Role;
    }

    public static NoClaimsUser Create() => new();
}

internal sealed class SysAdminUser : UserBaseData
{
    private static readonly Guid ID = Guid.Parse("0198a5ea-cd76-749c-b6bc-58e49e940e25");

    private SysAdminUser()
    {
        User = AppUser.Create(
            "sysadmin",
            "sysadmin@email.com.br",
            "AJ4bbHA1daDY0KvTRsvDiR610nxD1dyX8wyAgO4t38RMyHU8pH95qwqC3O8h4PVEOg==");
        User.Id = ID;
        Role = SysAdminRole.Create().Role;
    }

    public static SysAdminUser Create() => new();
}

internal sealed class ViewerUser : UserBaseData
{
    private static readonly Guid ID = Guid.Parse("01995a71-3f56-7990-9917-52cf9c9bb805");

    private ViewerUser()
    {
        User = AppUser.Create(
            "viewer",
            "viewer@email.com.br",
            "AJ4bbHA1daDY0KvTRsvDiR610nxD1dyX8wyAgO4t38RMyHU8pH95qwqC3O8h4PVEOg==");
        User.Id = ID;
        Role = UserViewerRole.Create().Role;
    }

    public static ViewerUser Create() => new();
}

internal sealed class CreatorUser : UserBaseData
{
    private static readonly Guid ID = Guid.Parse("01997e44-4068-74ce-b926-8e8790b471f1");

    private CreatorUser()
    {
        User = AppUser.Create(
            "creator",
            "creator@email.com.br",
            "AJ4bbHA1daDY0KvTRsvDiR610nxD1dyX8wyAgO4t38RMyHU8pH95qwqC3O8h4PVEOg==");
        User.Id = ID;
        Role = UserCreatorRole.Create().Role;
    }

    public static CreatorUser Create() => new();
}