using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Web.Http;
using com.mercaderias.WebAPI.Models;
using com.mercaderias.DAL.articulo;

namespace com.mercaderias.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
    public class ArticuloController : ControllerBase
    {
        private readonly ArticuloDAL obj_ArtDal;

        public ArticuloController(DbMercaderiasContext dbContext)
        {
            obj_ArtDal = new ArticuloDAL(dbContext);
        }

        //api/articulo/1
        [HttpGet("{idInv}")]
        //[Route("api/articulo/")]
        public IActionResult GetArticulosByIdInv(int idInv)
        {
            var res_listaArts = obj_ArtDal.ListarArticulosByIdInv(idInv);
            if (res_listaArts == null) {
                return NotFound();
            }
            if (res_listaArts.GetType() != typeof(List<Articulo>))
                return BadRequest(res_listaArts);
            else return Ok(res_listaArts);
        }

        //api/articulo?idInvNew=1
        [HttpPost]
        public IActionResult InsArticulos([FromBody] Articulo[] objArticulos, int idInvNew)
        {
            foreach (var itm_art in objArticulos) { itm_art.IdInventario = idInvNew; }

            var objArtsInsertados = obj_ArtDal.InsArticulos(objArticulos);
            if (objArtsInsertados.GetType() != typeof(int))
                return BadRequest(objArtsInsertados);
            else
            {
                return Ok(new { mensaje = true });
                //return Ok(new { Articulo = objArticulos, IdInv = idInvNew });
                // return Ok(res_listaArts);
            }
        }
    }
}
