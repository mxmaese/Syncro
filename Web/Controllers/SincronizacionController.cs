using Microsoft.AspNetCore.Mvc;
using Servicies.Syncro_function;
using Servicios;

namespace server_api.Controllers;
[ApiController]
[Route("[controller]")]
public class SincronizacionController : ControllerBase
{
    public readonly SincronizadorService _Syncro;
    public readonly ILogger<SincronizacionController> _logger;
    public readonly first_authenticate _auth;

    public SincronizacionController(SincronizadorService syncro, ILoggerFactory loggerFactory, first_authenticate authenticate)
    {
        _Syncro = syncro;
        _logger = loggerFactory.CreateLogger<SincronizacionController>();
        _auth = authenticate;
    }


    [HttpPost]
    public async Task<IActionResult> Sincronizar([FromBody] SolicitudSincronizacion solicitud)
    {
        _logger.LogCritical("algo");
        var autenticate = false;
        var user_id = "";
        try
        {
            return_vault_login login = _auth.login(solicitud.Usuario, solicitud.Contrasena);
            autenticate = login.result;
            user_id = login.user_id;
        }
        catch (Exception ex)
        {
            if (ex.InnerException is MySqlConnector.MySqlException)
            {
                _logger.LogCritical("can't connect with the database");// ex.InnerException.ToString());
                return BadRequest("se a producido un error, intentelo devuelta mas tarde");
            }
            else
            {
                // Manejo específico de la excepción MySqlException
                // Por ejemplo, puedes registrar este error o lanzar una excepción personalizada
                throw new ApplicationException("Error al conectar con la base de datos MySQL: " + ex.Message, ex);
            }
        }
        // Aquí deberías verificar las credenciales del usuario
        if (!autenticate)
        {
            return Unauthorized("Credenciales inválidas");
        }
        else
        {
            _logger.LogTrace($"connection started from {user_id}");
            await _Syncro.RealizarSincronizacion(solicitud.TipoSincronizacion, int.Parse(user_id));
            _logger.LogTrace("fin de la sincronizacion");
            return Ok("Sincronización completada para " + solicitud.TipoSincronizacion);
        }
    }
}
public class SolicitudSincronizacion
{
    public string TipoSincronizacion { get; set; }
    public string Usuario { get; set; }
    public string Contrasena { get; set; }
    // Otros campos necesarios...
    public SolicitudSincronizacion(System.String TipoSincronizacion, System.String Usuario, System.String Contrasena)
    {
        this.TipoSincronizacion = TipoSincronizacion;
        this.Usuario = Usuario;
        this.Contrasena = Contrasena;
    }
}
