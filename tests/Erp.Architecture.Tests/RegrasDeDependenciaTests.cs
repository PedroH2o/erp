using System.Reflection;
using NetArchTest.Rules;
using Xunit;

namespace Erp.Architecture.Tests;

public class RegrasDeDependenciaTests
{
    private static readonly string[] Web = ["Microsoft.EntityFrameworkCore", "Npgsql", "Microsoft.AspNetCore"];
    private static readonly string[] Modulos = ["Erp.Comercio", "Erp.Agenda"];

    private static void Verificar(Assembly assembly, params string[] proibidos)
    {
        var resultado = Types.InAssembly(assembly)
            .ShouldNot().HaveDependencyOnAny(proibidos)
            .GetResult();

        Assert.True(resultado.IsSuccessful,
            "Dependência proibida em: " + string.Join(", ", resultado.FailingTypeNames ?? []));
    }

    [Fact]
    public void SharedKernel_nao_depende_de_nada_do_sistema() =>
        Verificar(typeof(Erp.SharedKernel.AssemblyMarker).Assembly,
            [.. Web, "Erp.Nucleo", "Erp.Api", .. Modulos]);

    [Fact]
    public void Contracts_so_dependem_do_SharedKernel() =>
        Verificar(typeof(Erp.Nucleo.Contracts.AssemblyMarker).Assembly,
            [.. Web, "Erp.Nucleo.Domain", "Erp.Nucleo.Application", "Erp.Nucleo.Infrastructure", "Erp.Api", .. Modulos]);

    [Fact]
    public void Dominio_nao_depende_de_banco_web_nem_de_camadas_externas() =>
        Verificar(typeof(Erp.Nucleo.Domain.AssemblyMarker).Assembly,
            [.. Web, "Erp.Nucleo.Application", "Erp.Nucleo.Infrastructure", "Erp.Api", .. Modulos]);

    [Fact]
    public void Aplicacao_nao_depende_de_banco_web_nem_de_infraestrutura() =>
        Verificar(typeof(Erp.Nucleo.Application.AssemblyMarker).Assembly,
            [.. Web, "Erp.Nucleo.Infrastructure", "Erp.Api", .. Modulos]);

    [Fact]
    public void Infraestrutura_nao_conhece_a_API_nem_os_modulos() =>
        Verificar(typeof(Erp.Nucleo.Infrastructure.AssemblyMarker).Assembly,
            ["Erp.Api", .. Modulos]);
}
