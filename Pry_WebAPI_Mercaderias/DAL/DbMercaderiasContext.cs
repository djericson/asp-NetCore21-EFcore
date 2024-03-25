using com.mercaderias.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace com.mercaderias
{
    public class DbMercaderiasContext: DbContext
    {
        public DbMercaderiasContext(
            DbContextOptions<DbMercaderiasContext> options) :
            base(options)
        { }

        public DbSet<Inventario> inventarioEntity { set; get; }
        public DbSet<TipoInventario> tipoInventarioEntity { set; get; }
        public DbSet<Articulo> articulo_entity { set; get; }

    }
}
