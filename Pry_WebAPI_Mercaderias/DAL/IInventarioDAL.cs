using com.mercaderias.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace com.mercaderias.DAL.inventario
{
    public interface IInventarioDAL
    {
        object ListarInventarios(string codigo = "", string NombreTipo = "");

        object GetInventarioById(int idInventario);

        object ListarTiposInventario(TipoInventario tipoInv_entity);

        object InsInventario(Inventario invEntity);
    }
}
