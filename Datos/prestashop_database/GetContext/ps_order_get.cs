using Datos.internal_database.context;
using Datos.prestashop_database.context;
using Datos.prestashop_database.context.context_factory;
using Datos.prestashop_database.context_class;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Datos.prestashop_database.GetContext;
public class prestashop_database_get_data
{
    public readonly PrestashopOrderContextFactory _contextFactory;
    public readonly ILogger<prestashop_database_get_data> _logger;

    public prestashop_database_get_data(PrestashopOrderContextFactory contextFactory, ILoggerFactory loggerFactory)
    {
        _contextFactory = contextFactory;
        _logger = loggerFactory.CreateLogger<prestashop_database_get_data>();
    }

    public prestashop_order_context Get_Context(string connectionString, string id_order_filter = "", string current_state_filter = "")
    {
        using (var context = _contextFactory.CreateDbContext(connectionString))
        {
            return context;
        }
    }
}
public class output_ps_order
{
    public ps_order? ps_orders { get; set; }
    public List<ps_order_detail>? ps_orders_detail { get; set; }
    public List<ps_messege>? ps_Messeges { get; set; }
    public List<ps_product>? ps_products { get; set; }
    public List<ps_address>? ps_address { get; set; }
    public List<ps_customer_group>? ps_customer_group { get; set; }
}