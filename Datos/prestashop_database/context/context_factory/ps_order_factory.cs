using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Datos.prestashop_database.context;

namespace Datos.prestashop_database.context.context_factory;
public class PrestashopOrderContextFactory
{
    public prestashop_order_context CreateDbContext(string connectionString)
    {
        var optionsBuilder = new DbContextOptionsBuilder<prestashop_order_context>();
        optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(7, 0, 0)));

        return new prestashop_order_context(optionsBuilder.Options);
    }
}
