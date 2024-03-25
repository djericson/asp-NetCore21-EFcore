using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using com.mercaderias.WebAPI.Models;

namespace com.mercaderias.DAL.articulo
{
    public class ArticuloDAL : IArticuloDAL
    {
        private readonly DbMercaderiasContext _dbContext;

        public ArticuloDAL(DbMercaderiasContext dbContext) {
            _dbContext = dbContext;
        }

        public object ListarArticulosByIdInv(int idInventario)
        {
            try
            {
                List<Articulo> listaArticulos = new List<Articulo>();
                using (var context = _dbContext)
                {
                    listaArticulos = context.articulo_entity
                        .Where(x => x.IdInventario == idInventario)
                        .ToList();
                }
                return listaArticulos;
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

        public object InsArticulos(Articulo[] objArticulos)
        {
            int nroArtsInsertados;
            try
            {
                using (var context = _dbContext)
                {
                    context.articulo_entity.AddRange(objArticulos);
                    nroArtsInsertados = context.SaveChanges();
                }
                return nroArtsInsertados;
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
