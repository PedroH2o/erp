namespace Erp.SharedKernel;

public readonly record struct EmpresaId(Guid Valor)
{
    public static EmpresaId Nova() => new(Guid.CreateVersion7());
    public override string ToString() => Valor.ToString();
}
