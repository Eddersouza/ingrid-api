using IP.Core.Domain.VO;

namespace IP.Core.Domain.Employees;

public class Employee : EntityAuditableDeletableActivable<EmployeeId>
{
    public const int EMAIL_MAX_LENGTH = 256;
    public const int NAME_MAX_LENGTH = 256;
    public const int NAME_MIN_LENGTH = 5;
    public const int USERNAME_MAX_LENGTH = 256;
    public const int USERNAME_MIN_LENGTH = 5;

    public Employee() =>
        Id = EmployeeId.Create();

    public Employee(string name, string cpf) : this()
    {
        Name = new EmployeName(name);
        CPF = new CPF(cpf);
        ActivableInfo.SetAsActive();
    }

    public CPF CPF { get; private set; } = null!;
    public EmployeeManager Manager { get; private set; } = null!;
    public EmployeName Name { get; private set; } = null!;

    public EmployeeUser User { get; private set; } = null!;

    public static Employee Create(string name, string cpf) => new(name, cpf);

    public void AddManager(Guid id, string name) =>
        Manager = new EmployeeManager(id, name);

    public void AddUser(Guid id, string name) =>
        User = new EmployeeUser(id, name);

    public void RemoveManager() => Manager = EmployeeManager.CreateEmpty();

    public void RemoveUser() => User = EmployeeUser.CreateEmpty();

    public void SetCPF(string cpf) => CPF = new CPF(cpf);

    public void SetName(string name) => Name = new EmployeName(name);

    public override string ToEntityName() => "Colaborador";

    public override string ToString() => Name.Value;
}

public class EmployeeManager : ValueObject
{
    public EmployeeManager()
    {
    }

    public EmployeeManager(Guid id, string name)
    {
        Id = id;
        Name = name;
        NameNormalized = name.NormalizeCustom();
    }

    public Guid? Id { get; init; } = null!;
    public string Name { get; init; } = null!;
    public string NameNormalized { get; init; } = null!;

    public static EmployeeManager CreateEmpty() => new() { Id = null, Name = null!, NameNormalized = null! };

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Id!;
        yield return Name;
    }
}

public class EmployeName : ValueObject
{
    public EmployeName()
    {
    }

    public EmployeName(string name)
    {
        Value = name;
        ValueNormalized = name.NormalizeCustom();
    }

    public string Value { get; init; } = null!;
    public string ValueNormalized { get; init; } = null!;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return ValueNormalized;
        yield return Value;
    }
}

public class EmployeeUser : ValueObject
{
    public EmployeeUser()
    {
    }

    public EmployeeUser(Guid id, string name)
    {
        Id = id;
        Name = name;
        NameNormalized = name.NormalizeCustom();
    }

    public Guid? Id { get; init; } = null!;

    public string Name { get; init; } = null!;

    public string NameNormalized { get; init; } = null!;

    public static EmployeeUser CreateEmpty() => new() { Id = null, Name = null!, NameNormalized = null! };

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Id!;
        yield return Name;
    }
}

public class EmployeeId :
    TypedValue<Guid>,
    IEntityId<Guid>,
    ICreateGuid<EmployeeId, Guid>
{
    public EmployeeId() : base(Guid.CreateVersion7())
    {
    }

    public EmployeeId(Guid value) : base(value)
    {
    }

    public static EmployeeId Create() => new();

    public static EmployeeId Create(Guid id) => new(id);

    public override string ToString() => Value.ToString();
}