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
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaController : ControllerBase
    {
        private readonly IDespesaRepository _despesaRepository;

        public DespesaController(IDespesaRepository despesa)
        {
            _despesaRepository = despesa;
        }

        //APP   
        [Route("")]
        [HttpGet]
        public ActionResult ObterTodos()
        {
            var item = _despesaRepository.GetAll();
            return Ok(item);
        }


        //web     
        //ex: /api/servicos/1
        [Route("{id}")]
        [HttpGet]
        public ActionResult Obter(int id)
        {
            var obj = _despesaRepository.Find(id);
            if (obj == null)
                return NotFound();

            return Ok(obj);
        }

        //[Route("")]
        //// ex: /api/palavras (post:id.nome,ativo,pontuacao,data)
        //[HttpPost]
        //public ActionResult Cadastrar([FromBody] Despesa depesa)  //Servico Servico como quebrar esse vinculo direto com a model
        //{

        //    if (depesa == null)
        //        return BadRequest();

        //    //Validando dados 
        //    if (!ModelState.IsValid)
        //        return UnprocessableEntity(ModelState);

        //    depesa.DataCadastro = DateTime.Now;
        //    servico.DtAtualizacao = DateTime.Now;
        //    servico.IsDescontinuado = false;

        //    _servicoRepository.Add(servico);

        //    return Created($"/api/servico/{servico.ServicoId}", servico);
        //}

    }
}
