using Datos.internal_database.context_class;
using Datos.prestashop_database.context.context_factory;
using Datos.prestashop_database.GetContext;
using Microsoft.Extensions.Logging;

namespace Entidades;
public class Syncro_Pedidos
{
    public readonly ILogger<Syncro_Pedidos> _logger;
    private readonly prestashop_database_get_data _ps_order_get;

    public Syncro_Pedidos(ILoggerFactory loggerFactory, prestashop_database_get_data ps_order_get)
    {
        _logger = loggerFactory.CreateLogger<Syncro_Pedidos>();
        _ps_order_get = ps_order_get;
    }

    public void Synchronize_Pedidos(PrestashopDatabaseCredential_internal_db prestashop_credentials, GescomDatabaseCredential_internal_db gescom_credentials, int user_id)
    {
        var connection_string_prestashop = $"server={prestashop_credentials.Host};port={prestashop_credentials.Port};database={prestashop_credentials.DatabaseName};user={prestashop_credentials.User};password={prestashop_credentials.Password};";
        var context = _ps_order_get.Get_Context(connection_string_prestashop );
        var result = context.ps_order.Where(ord => ord.id_customer == user_id);
        _logger.LogCritical("llegamos");
    }
}
