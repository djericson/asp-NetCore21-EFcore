using com.mercaderias.WebAPI.Models;
using System;
using System.Collections.Generic;

namespace com.mercaderias.DAL.articulo
{
    public interface IArticuloDAL
    {
        object ListarArticulosByIdInv(int idInventario);

        object InsArticulos(Articulo[] objArticulos);
    }
}
