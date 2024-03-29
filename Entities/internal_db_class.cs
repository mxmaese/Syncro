using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.internal_database.context_class;
public class Customer_internal_db
{
    public int CustomerId { get; set; }
    public string? Name { get; set; }
    public string? AccountUser { get; set; }
    public string? AccountPass { get; set; }
    public bool? Active { get; set; }
    public virtual GescomDatabaseCredential_internal_db GescomDatabaseCredentials { get; set; }
    public virtual PrestashopDatabaseCredential_internal_db PrestashopDatabaseCredentials { get; set; }
}

public class GescomDatabaseCredential_internal_db
{
    [Key]
    public int CredentialGescomId { get; set; }
    public int? CustomerId { get; set; }
    public string? Host { get; set; }
    public string? User { get; set; }
    public string? Password { get; set; }
    public string? DatabaseName { get; set; }
    public int? Port { get; set; }

    public virtual Customer_internal_db Customer { get; set; }
}

public class PrestashopDatabaseCredential_internal_db
{
    [Key]
    public int CredentialPrestashopId { get; set; }
    public int? CustomerId { get; set; }
    public string? Host { get; set; }
    public string? User { get; set; }
    public string? Password { get; set; }
    public string? DatabaseName { get; set; }
    public int? Port { get; set; }

    public virtual Customer_internal_db Customer { get; set; }
}
