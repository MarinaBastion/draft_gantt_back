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
using Microsoft.AspNetCore.Authorization;

namespace gantt_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EntityController : ControllerBase
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public EntityController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet,Authorize]
        public async Task<ActionResult<IEnumerable<EntityDto>?>> Get()
        {
            var entities = await _unitOfWork.Entities.GetAll();
            //  if (entities != null)
            // {
                var link = _mapper.Map<IEnumerable<EntityDto>>(entities);
                return Ok(link);
            // }
        
            // return NotFound();
        }

        [HttpGet("{id}"),Authorize]
        public async  Task<ActionResult<EntityDto?>> GetEntity(Guid id)
        {
            var item = await _unitOfWork.Entities.GetById(id);

            // if (item == null)
            //     return NotFound();

            return Ok(_mapper.Map<EntityDto>(item));
        }

        [HttpPost,Authorize]
        public async Task<ActionResult<EntityDto>> CreateEntity(EntityDto entityDto)
        {
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<Entity>(entityDto);

                await _unitOfWork.Entities.Add(entity);
                await _unitOfWork.CompleteAsync();

                return Ok(entityDto);
            }

            return Ok(entityDto);
        }

        [HttpPut,Authorize]
        public async Task<ActionResult<EntityDto>?> UpdateEntity(EntityDto entityDto)
        {
            if (ModelState.IsValid)
            {
                var item = await _unitOfWork.Entities.GetById(entityDto.id);

                // if (item == null)
                //     return BadRequest();

                var entity = _mapper.Map<Entity>(entityDto);

                await _unitOfWork.Entities.Update(entity);
                await _unitOfWork.CompleteAsync();

                    return Ok(entityDto);
                }

                return Ok(entityDto);
        }
        [HttpDelete("{id}"),Authorize]
        public async Task<ActionResult<EntityDto>?> DeleteEntity(Guid id)
        {
            var item = await _unitOfWork.Entities.GetById(id);

            // if (item == null)
            //     return BadRequest();

            await _unitOfWork.Entities.Delete(id);
            await _unitOfWork.CompleteAsync();

            return Ok(item);
        }
    }
}