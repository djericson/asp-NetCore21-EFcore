using com.mercaderias.DAL.inventario;
using com.mercaderias.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

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
            new Inventario {Id=1, Codigo ="Inv-1", Tipo="TOTAL"},
            new Inventario {Id=2, Codigo ="Inv-2", Tipo="PARCIAL"},
            new Inventario {Id=3, Codigo ="Inv-3", Tipo="TOTAL"}
        };
		
        [HttpPost("inventariosp")]
        public IEnumerable<Inventario> GetInventariosPrueba()
        {
            return listaInvPrueba;
        }
		
        [HttpGet]
        public IActionResult GetInventarios(string codigo = "", string nombreTipo = "")
        {
            var res_listaInv = obj_InvDal.ListarInventarios(codigo, nombreTipo);
            if (res_listaInv == null)
            {
                return NotFound();
            }
            if (res_listaInv.GetType() != typeof(List<Inventario>))
                return BadRequest(res_listaInv);
            else
                return Ok(new { json = res_listaInv });
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

        [HttpGet("tipos-inventario")]
        public IActionResult ListarTiposInventario(string nombreTipo="")
        {
            var res_listaTiposInv = obj_InvDal.ListarTiposInventario(nombreTipo);
            if (nombreTipo == null)
            {
                return NotFound();
            }
            if (res_listaTiposInv.GetType() != typeof(List<TipoInventario>))
                return BadRequest(res_listaTiposInv);
            else return Ok(res_listaTiposInv);
        }
        bool IsAnonymousType(object obj)
        {
            return obj.GetType().GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).Length > 0;
        }
        [HttpPost]
        public IActionResult InsAltaInventario([FromBody] object invNartsEntity)
        {
            try
            {
                var objInv_ArtsInsertados = obj_InvDal.InsInventarioYarts(invNartsEntity);
                if (!IsAnonymousType(objInv_ArtsInsertados))
                    return BadRequest(objInv_ArtsInsertados);
                else
                {
                    dynamic res = objInv_ArtsInsertados;
                    return Ok(new { res.invId, res.numArtsInsertados }
                    );
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
