using Datos;
using Datos.internal_database.context;
using Microsoft.AspNetCore.Antiforgery.Internal;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Servicios;

public class first_authenticate
{
    public login_internal_context _ctx;
    public Logger<first_authenticate> _logger;
    public first_authenticate(login_internal_context login_Internal_Context, Logger<first_authenticate> logger)
    {
        _ctx = login_Internal_Context;
        _logger = logger;
    }
    public return_vault_login login(string User_by_user, string Pass_by_user)
    {
        var user = _ctx.Customers.Where(cus => cus.AccountUser == User_by_user).FirstOrDefault();

        if (User_by_user == user.AccountUser.DefaultIfEmpty().ToString() &&
            Pass_by_user == user.AccountPass.DefaultIfEmpty().ToString() &&
            true.ToString() == user.Active.ToString())
        {
            return new return_vault_login(true, User_by_user);
        }
        return new return_vault_login(false);
    }
}
public class return_vault_login
{
    public bool result;
    public string? error;
    public string? user_id;
    public return_vault_login(bool result, string user_id = null, string error = null)
    {
        this.result = result;
        if (user_id != null)
        {
            this.user_id = user_id;
        }

        this.error = error;
    }
}