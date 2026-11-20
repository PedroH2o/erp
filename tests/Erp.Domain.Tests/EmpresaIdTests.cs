using Erp.SharedKernel;
using Xunit;

namespace Erp.Domain.Tests;

public class EmpresaIdTests
{
    [Fact]
    public void Nova_gera_identificadores_diferentes()
    {
        Assert.NotEqual(EmpresaId.Nova(), EmpresaId.Nova());
    }
}
