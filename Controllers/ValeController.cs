using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestaoFacil.Dados.Modelos;
using GestaoFacil.Dados.Modelos.DTO;
using GestaoFacil.Dados.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace GestaoFacil.WebApi.Controllers
{
    [Route("api/Vale")]
    [ApiController]
    public class ValeController : ControllerBase
    {
        private readonly IValeRepository _vale;
        private readonly IMapper _mapper;

        public ValeController(IValeRepository vale, IMapper mapper)
        {
            _vale = vale;
            _mapper = mapper;
        }

        //APP  
        [Route("")]
        [HttpGet]
        public ActionResult ObterTodos()
        {
            
            var obj = _vale.GetVales();
            return Ok(obj);

            //var item = _vale.GetAll();
            //return Ok(item);
        }


        [Route("")]
        // ex: /api/palavras (post:id.nome,ativo,pontuacao,data)
        [HttpPost]
        public ActionResult Cadastrar([FromBody] Vale vale)  //Servico Servico como quebrar esse vinculo direto com a model
        {

            if (vale == null)
                return BadRequest();

            //Validando dados 
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            vale.DataCadastro = DateTime.Now;
            _vale.Add(vale);

            return Created($"/api/vale/{vale.ValeId}", vale);
        }


        //web     
        //ex: /api/servicos/1
        [Route("{id}")]
        [HttpGet]
        public ActionResult Obter(int id)
        {
            var obj = _vale.Find(id);
            if (obj == null)
                return NotFound();

            return Ok(obj);
        }

        [Route("{id}")]
        [HttpPut]
        public ActionResult Atualizar(int id, [FromBody] Vale vale)//Servico Servico como quebrar esse vinculo direto com a model
        {

            if (vale == null)
                return BadRequest();

            //Validando dados 
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);


            var obj = _vale.Find(id);
            if (obj == null)
                return NotFound();

            vale.ValeId = id;
            vale.DataCadastro = obj.DataCadastro;
    

            _vale.Update(vale);
            return Ok();
        }

        [Route("{id}")]
        [HttpDelete]
        public ActionResult Deletar(int id)
        {
            int ret = _vale.Remove(id);
            if (ret == 0)
                return NotFound();

            return NoContent();
        }



        /*exmplo de uso dos mapeamntos de objetos*/
        private ActionResult ObterObjetoTOValeDto()
        {
            var obj = _vale.GetVales();
            ValeDTO valeDTO = _mapper.Map<ValeDTO>(obj);
            return Ok(obj);
        }


        private ActionResult GetAll()
        {
            var lstVales = _vale.GetAll();
            var _vales = _mapper.Map<List<ValeDTO>>(lstVales);
            return Ok(_vales);
        }

    }
}
