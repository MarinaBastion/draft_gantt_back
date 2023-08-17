using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using gantt_backend.Interfaces.UOF;
using gantt_backend.Repositories.UOF;
using gantt_backend.Data.Models;
using gantt_backend.Data.ViewModels;
using AutoMapper;
using gantt_backend.Data.ModelsDTO;
using gantt_backend.Data.Models.Constructor;

namespace gantt_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EntityFieldController : ControllerBase
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public EntityFieldController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EntityFieldDto>>> Get()
        {
            var entityFields = await _unitOfWork.EntityFields.GetAll();
             if (entityFields != null)
            {
                var items = _mapper.Map<IEnumerable<EntityFieldDto>>(entityFields);
                return Ok(items);
            }
        
            return NotFound();
        }

        [HttpGet("{id}")]
        public async  Task<ActionResult<EntityFieldDto>> GetEntityField(Guid id)
        {
            var item = await _unitOfWork.EntityFields.GetById(id);

            if (item == null)
                return NotFound();

            return Ok(_mapper.Map<EntityFieldDto>(item));
        }

        [HttpPost]
        public async Task<ActionResult<EntityFieldDto>> CreateEntityField(EntityFieldDto entityFieldDto)
        {
            if (ModelState.IsValid)
            {
                var entityField = _mapper.Map<EntityFields>(entityFieldDto);

                await _unitOfWork.EntityFields.Add(entityField);
                await _unitOfWork.CompleteAsync();

                return Ok(entityFieldDto);
            }

            return Ok(entityFieldDto);
        }
        [HttpPost("batch")]
        public async Task<ActionResult<EntityFieldDto>> CreateEntityFieldBatch(EntityFieldDto[] entityFieldDto)
        {
            if (ModelState.IsValid)
            {
                var entityField = _mapper.Map<EntityFields[]>(entityFieldDto);

                await _unitOfWork.EntityFields.AddRange(entityField);
                await _unitOfWork.CompleteAsync();

                return Ok(entityFieldDto);
            }

            return Ok(entityFieldDto);
        }

        [HttpPut]
        public async Task<ActionResult<EntityFieldDto>> UpdateEntityField(EntityFieldDto entityFieldDto)
        {
            if (ModelState.IsValid)
            {
                var item = await _unitOfWork.EntityFields.GetById(entityFieldDto.id);

                if (item == null)
                    return BadRequest();

                var entityField = _mapper.Map<EntityFields>(entityFieldDto);

                await _unitOfWork.EntityFields.Update(entityField);
                await _unitOfWork.CompleteAsync();

                    return Ok(entityFieldDto);
                }

                return Ok(entityFieldDto);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<EntityFieldDto?>> DeleteEntityField(Guid id)
        {
            var item = await _unitOfWork.EntityFields.GetById(id);

            // if (item == null)
            //     return BadRequest();

            await _unitOfWork.EntityFields.Delete(id);
            await _unitOfWork.CompleteAsync();

            return Ok(item);
        }

        [HttpPut("batch")]
        public async Task<ActionResult> DeleteEntityFields(EntityFieldDto[] entityFieldDto)
        {
            if (ModelState.IsValid)
            {
                var entityField = _mapper.Map<EntityFields[]>(entityFieldDto);

                await _unitOfWork.EntityFields.RemoveRange(entityField);
                 await _unitOfWork.CompleteAsync();
                return Ok();
            }
        return Ok();

           
        }
    }
}