namespace IP.Core.Domain.VO;

public sealed class CPF : IdentityDocumentNumber
{
    public const int LENGTH = 11;

    public CPF()
    { }

    public CPF(string value) :
        base(value.ToOnlyNumericCharacter())
    { }

    public static Error Invalid => Error.Validation("CPF.Invalid", "CPF inválido");
    public override string ValueFormated => $"{Value[..3]}.{Value.Substring(3, 3)}.{Value.Substring(6, 3)}-{Value.Substring(9, 2)}";

    public override bool IsValid() => Value.IsCpf();
}

public sealed class CPFOrCNPJ : IdentityDocumentNumber
{
    public CPFOrCNPJ()
    { }

    public CPFOrCNPJ(string value) :
        base(value.ToOnlyNumericCharacter())
    { }

    public static Error Invalid => Error.Validation("CPFOrCNPJ.Invalid", "CPF/CNPJ inválido");

    public override string ValueFormated =>
            Value.Length == 11 ?
        new CPF(Value).ValueFormated :
        new CNPJ(Value).ValueFormated;

    public override bool IsValid() =>
        Value.Length == 11 ?
        new CPF(Value).IsValid() :
        new CNPJ(Value).IsValid();
}

public abstract class IdentityDocumentNumber : ValueObject
{
    public IdentityDocumentNumber()
    {
    }

    public IdentityDocumentNumber(string value)
    {
        Value = value;
    }

    public string Value { get; init; } = null!;
    public abstract string ValueFormated { get; }

    public abstract bool IsValid();

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

public sealed class CNPJ : IdentityDocumentNumber
{
    public const int LENGTH = 14;

    public CNPJ()
    { }

    public CNPJ(string value) :
        base(value.ToOnlyNumericCharacter())
    { }

    public static Error Invalid => Error.Validation("CNPJ.Invalid", "CNPJ inválido");
    public override string ValueFormated => $"{Value[..2]}.{Value.Substring(2, 3)}.{Value.Substring(5, 3)}/{Value.Substring(8, 4)}-{Value.Substring(12, 2)}";

    public override bool IsValid() => Value.IsCnpj();
}