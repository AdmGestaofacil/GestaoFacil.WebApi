using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestaoFacil.Dados.Modelos;
using GestaoFacil.Dados.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoFacil.WebApi.Controllers
{
    [Route("api/TipoDespesa")]
    [ApiController]
    public class TipoDespesaController : ControllerBase
    {

        private readonly ITipoDespesaRepository _tipoDespesaRepository;
        public TipoDespesaController(ITipoDespesaRepository tipoDespesaRepository)
        {
            _tipoDespesaRepository = tipoDespesaRepository;
        }

        //APP  
        [Route("")]
        [HttpGet]
        public ActionResult ObterTodos()
        {
            var item = _tipoDespesaRepository.GetAll();
            return Ok(item);
        }

        //web     
        //ex: /api/servicos/1
        [Route("{id}")]
        [HttpGet]
        public ActionResult Obter(int id)
        {
            var obj = _tipoDespesaRepository.Find(id);
            if (obj == null)
                return NotFound();

            return Ok(obj);
        }


        [Route("")]
        // ex: /api/palavras (post:id.nome,ativo,pontuacao,data)
        [HttpPost]
        public ActionResult Cadastrar([FromBody] TipoDespesa tipodepesa)
        {

            if (tipodepesa == null)
                return BadRequest();

            //Validando dados 
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            _tipoDespesaRepository.Add(tipodepesa);

            return Created($"/api/tipodespesa/{tipodepesa.TipoDespesaId}", tipodepesa);
        }


        [Route("{id}")]
        [HttpPut]
        public ActionResult Atualizar(int id, [FromBody] TipoDespesa tipodespesa)
        {

            if (tipodespesa == null)
                return BadRequest();

            //Validando dados 
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);


            var obj = _tipoDespesaRepository.Find(id);
            if (obj == null)
                return NotFound();


            _tipoDespesaRepository.Update(tipodespesa);
            return Ok();
        }


        [Route("{id}")]
        [HttpDelete]
        public ActionResult Deletar(int id)
        {
          _tipoDespesaRepository.Remove(id);
            return NoContent();
        }


    }

}
