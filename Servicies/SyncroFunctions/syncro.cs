using Datos.internal_database.context;
using Entidades;
using Microsoft.Extensions.Logging;

namespace Servicies.Syncro_function;
public class SincronizadorService
{
    private readonly ILogger<SincronizadorService> _logger;
    private readonly Syncro_Pedidos _syncro_Pedidos;
    private readonly prestashop_database_context _ctxPrestashop;
    private readonly gescom_database_context _ctxGescom;

    public SincronizadorService(ILogger<SincronizadorService> logger, Syncro_Pedidos syncro_Pedidos, prestashop_database_context ctxPrestashop, gescom_database_context ctxGescom)
    {
        _logger = logger;
        _syncro_Pedidos = syncro_Pedidos;
        _ctxPrestashop = ctxPrestashop;
        _ctxGescom = ctxGescom;
    }

    public async Task<string> RealizarSincronizacion(string TipoSincronizacion, int user_id)
    {
        var get_prestashop_credential = _ctxPrestashop.Customers.Where(cus => cus.CustomerId == user_id).FirstOrDefault();
        var get_gescom_credential = _ctxGescom.Customers.Where(cus => cus.CustomerId== user_id).FirstOrDefault();
        _logger.LogCritical("casi");
        switch (TipoSincronizacion)
        {
            case "Pedidos":
                // Lógica para Tipo1
                _syncro_Pedidos.Synchronize_Pedidos(get_prestashop_credential.PrestashopDatabaseCredentials, get_gescom_credential.GescomDatabaseCredentials, user_id);
                break;
            case "Tipo2":
                // Lógica para Tipo2
                await Console.Out.WriteLineAsync("Tipo 2");
                break;
            // Agrega casos para otros tipos de sincronización
            default:
                await Console.Out.WriteLineAsync($"mala respuesta: {TipoSincronizacion}");
                return "Tipo de sincronización desconocido";
        }
        return "ok";
    }
}
