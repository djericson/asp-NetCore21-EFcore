using com.mercaderias.WebAPI.Models;
using Newtonsoft.Json.Linq;
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
                    || (x.Tipo == NombreTipo || NombreTipo == "")
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

        public object ListarTiposInventario(string objTipoInv)
        {
            List<TipoInventario> listaTiposInventario = new List<TipoInventario>();
            try
            {
                using (var context = _dbContext)
                {
                    listaTiposInventario = context.tipoInventarioEntity.Where(x => (
                    x.NombreTipo == objTipoInv || objTipoInv == "")
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

        public object InsInventarioYarts(object invYarts)
        {
            int res_ArtsInsertados;
            try
            {
                using (var context = _dbContext)
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            JObject data = JObject.FromObject(invYarts);
                            var inv = data.GetValue("inventario").ToObject<Inventario>();
                            var articulos = data.GetValue("articulos").ToObject<List<Articulo>>();

                            context.inventarioEntity.Add(inv);
                            context.SaveChanges();

                            int res_invId = inv.Id;

                            foreach (var artItm in articulos)
                            {
                                artItm.IdInventario = res_invId;
                            }

                            context.articulo_entity.AddRange(articulos);
                            res_ArtsInsertados = context.SaveChanges();

                            transaction.Commit();

                            return  new { invId = res_invId
                                , numArtsInsertados = res_ArtsInsertados };
                        }
                        catch (Exception ex)
                        {
                            // Revertir la transacción en caso de error
                            transaction.Rollback();
                            return ex.Message;
                        }
                    }
                }
                
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
