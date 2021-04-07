using ConsultaPreco.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;


namespace ConsultaPreco.API.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class PrecoController : ControllerBase
    {
        Queries queries = new Queries();
        

        public List<PrecoModel> ListaPreco = new List<PrecoModel>();
        public void AdicionarLista()
        {
            ListaPreco.Clear();
            ListaPreco.Add(new PrecoModel("", "", "", "", "")
            {
                centro = queries.centro,
                material = queries.material,
                descricao = queries.descricao,
                precoRegular = queries.precoRegular,
                precoMinhaLe = queries.precoMinhaLe
            }) ;
        }

        [HttpGet("porloja")]
        public PrecoModel PrecoPorCentro(string centro, string codigo)
        {
            queries.ConsultaPreco(centro, codigo);
            AdicionarLista();
            PrecoModel precoMercadoria = ListaPreco.Where(n => n.centro == centro)
                                                .Select(n => n)
                                                .FirstOrDefault();


            //if (centro == null)
            //{
            //    return NotFound();
            //}

            return precoMercadoria;
        }
        // POST api/<PrecoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PrecoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PrecoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
