using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Datos.prestashop_database.context_class;
public class ps_order
{
    [Key]
    public int id_order { get; set; }
    public string reference { get; set; }
    public int id_address_delivery { get; set; }
    public int current_state { get; set; }
    public float total_paid{ get; set; }
    public float total_shiping { get; set; }
    public int id_carrier { get; set; }
    public int id_customer { get; set; }

    public virtual ISet<ps_order_detail> ps_order_details { get; set; }
    public virtual ISet<ps_messege> ps_messeges { get; set; }
}

public class ps_order_detail
{
    [Key]
    public int id_order_detail { get; set; }
    public int? id_order { get; set; }
    public int? product_id { get; set; }
    public int? product_quantity { get; set; }
    public float? product_price { get; set; }
    public float? reduction_percent { get; set; }
    public float? reduction_amount { get; set; }

    public virtual ps_order Ps_Order { get; set; }
}

public class ps_messege
{
    [Key]
    public int id_message { get; set; }
    public int? id_order { get; set; }
    public string? message { get; set; }
    public byte? private_message { get; set; }

    public virtual ps_order Ps_Order { get; set; }
}

public class ps_address
{
    [Key]
    public int id_address { get; set; }
    public int? id_customer { get; set; }
    public string? company { get; set; }
}

public class ps_product
{
    [Key]
    public int id_product { get; set; }
    public string? reference { get; set; }
}

public class ps_customer_group
{
    public int IdCustomer { get; set; }
    public int IdGroup { get; set; }
}
