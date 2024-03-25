using com.mercaderias.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mercaderias.DAL.inventario
{
    public class InventarioDAL : IInventarioDAL
    {
        private readonly DbMercaderiasContext _dbContext;

        public InventarioDAL(DbMercaderiasContext dbContext)
        {
            _dbContext = dbContext;
        }

        public object ListarInventarios(string codigo="", string NombreTipo="")
        {
            List<Inventario> listaInventarios = new List<Inventario>();
            try {
                using (var context = _dbContext)
                {
                    listaInventarios = context.inventarioEntity.Where(x => (x.Codigo == codigo
                    || codigo == "")
                    && (x.Tipo == NombreTipo || NombreTipo == "")
                    ).ToList();
                }
                return listaInventarios;
            }
            catch (SqlException ex) {
                return ex.Message;
            }
            catch (Exception ex) {
                return ex.Message;
            }
        }

        public object GetInventarioById(int idInventario)
        {
            Inventario inv_entity = new Inventario();
            try
            {
                using (var context = _dbContext)
                {
                    inv_entity = context.inventarioEntity.Where(x => x.Id == idInventario).FirstOrDefault();
                }
                return inv_entity;
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public object ListarTiposInventario(TipoInventario tipoInv_entity)
        {
            List<TipoInventario> listaTiposInventario = new List<TipoInventario>();
            try
            {
                using (var context = _dbContext)
                {
                    listaTiposInventario = context.tipoInventarioEntity.Where(x => (
                    x.NombreTipoInventario == tipoInv_entity.NombreTipoInventario || tipoInv_entity.NombreTipoInventario == "")
                    ).ToList();
                }
                return listaTiposInventario;
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public object InsInventario(Inventario invEntity)
        {
            int res_InvInsertado;
            try
            {
                using (var context = _dbContext)
                {
                    context.inventarioEntity.Add(invEntity);
                    res_InvInsertado = context.SaveChanges();
                }
                return res_InvInsertado;
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
