namespace Erp.SharedKernel;

public abstract class Entidade
{
    public Guid Id { get; protected set; } = Guid.CreateVersion7();
}

public abstract class EntidadeDaEmpresa : Entidade
{
    public EmpresaId EmpresaId { get; protected set; }
    public DateTime CriadoEm { get; protected set; }
    public Guid? CriadoPor { get; protected set; }
}
