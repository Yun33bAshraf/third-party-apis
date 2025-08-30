using System.Data.Common;

namespace OpenProfileAPI.Application.FunctionalTests;

public interface ITestDatabase
{
    Task InitialiseAsync();

    DbConnection GetConnection();

    Task ResetAsync();

    Task DisposeAsync();
}
