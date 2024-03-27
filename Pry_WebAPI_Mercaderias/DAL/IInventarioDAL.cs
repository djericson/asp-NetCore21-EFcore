using com.mercaderias.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace com.mercaderias.DAL.inventario
{
    public interface IInventarioDAL
    {
        object ListarInventarios(string codigo = "", string nombreTipo = "");

        object GetInventarioById(int idInventario);

        object ListarTiposInventario(string objTipoInv);

        object InsInventarioYarts(object invEntity);
    }
}
