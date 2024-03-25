using com.mercaderias.DAL.inventario;
using com.mercaderias.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace com.mercaderias.inventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        private readonly InventarioDAL obj_InvDal;

        public InventarioController(DbMercaderiasContext dbContext)
        {
            obj_InvDal = new InventarioDAL(dbContext);
        }

        Inventario[] listaInvPrueba = new Inventario[]
        {
            new Inventario {Id=1, Codigo ="Producto 1", Tipo="Categoria 1"},
            new Inventario {Id=2, Codigo ="Producto 2", Tipo="Categoria 2"},
            new Inventario {Id=3, Codigo="Producto 3", Tipo="Categoria 3"}
        };

        [HttpPost("inventariosp")]
        public IEnumerable<Inventario> GetInventariosPrueba()
        {
            return listaInvPrueba;
        }

        [HttpGet]
        public IActionResult GetInventarios(string codigo = "", string NombreTipo = "")
        {
            var res_listaInv = obj_InvDal.ListarInventarios(codigo, NombreTipo);
            if (res_listaInv == null)
            {
                return NotFound();
            }
            if (res_listaInv.GetType() != typeof(List<Inventario>))
                return BadRequest(res_listaInv);
            else return Ok(res_listaInv);
        }

        [HttpGet("{id}")]
        public IActionResult GetInventarioById(int idInventario)
        {
            var res_inventario = obj_InvDal.GetInventarioById(idInventario);
            if (res_inventario == null)
            {
                return NotFound();
            }
            if (res_inventario.GetType() != typeof(List<Inventario>))
                return BadRequest(res_inventario);
            else return Ok(res_inventario);
        }

        [HttpPost]
        public IActionResult ListarTiposInventario([FromBody] TipoInventario tipoInv_entity)
        {
            var res_listaTiposInv = obj_InvDal.ListarTiposInventario(tipoInv_entity);
            if (res_listaTiposInv == null)
            {
                return NotFound();
            }
            if (res_listaTiposInv.GetType() != typeof(List<Inventario>))
                return BadRequest(res_listaTiposInv);
            else return Ok(res_listaTiposInv);
        }

        [HttpPost]
        public IActionResult InsAltaInventario(Inventario invEntity)
        {
            var objArtsInsertados = obj_InvDal.InsInventario(invEntity);
            if (objArtsInsertados.GetType() != typeof(int))
                return BadRequest(objArtsInsertados);
            else
                return Ok(new { Inventario = invEntity.Id , res= objArtsInsertados });
        }
    }
}
