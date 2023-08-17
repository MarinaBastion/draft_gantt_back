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
    public class InstanceController : ControllerBase
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public InstanceController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstanceDto>?>> Get()
        {
            var instances = await _unitOfWork.Instances.GetAll();
            //  if (instances != null)
            // {
                var instance = _mapper.Map<IEnumerable<InstanceDto>>(instances);
                return Ok(instance);
            // }
        
            // return NotFound();
        }

        [HttpGet("{id}")]
        public async  Task<ActionResult<InstanceDto?>> GetInstance(Guid id)
        {
            var item = await _unitOfWork.Instances.GetById(id);

            // if (item == null)
            //     return NotFound();

            return Ok(_mapper.Map<InstanceDto>(item));
        }

        [HttpGet("GetInstancesByEntity{id}")]
        public async  Task<ActionResult<IEnumerable<InstanceDto>?>> GetInstancesByEntity(Guid id)
        {
            try
            {
                List<InstanceDto> items = new List<InstanceDto>();
                items =  _mapper.Map<List<InstanceDto>>(_unitOfWork.Instances.GetAllInstancesByEntityId(id).ToList());
                return Ok(items);
            }
            catch(Exception ex ) {
                Console.Write(ex.Message);
             }
            // if (item == null)
            //     return NotFound();

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<InstanceDto>> CreateInstance(InstanceDto instanceDto)
        {
            if (ModelState.IsValid)
            {
                var instance = _mapper.Map<Instance>(instanceDto);
                if (instanceDto.parent_instance_id != null)
                {
                     var item = await _unitOfWork.Instances.GetById((Guid)instanceDto.parent_instance_id);
                     instance.ParentInstance = item;
                }

                await _unitOfWork.Instances.Add(instance);
                await _unitOfWork.CompleteAsync();

                return Ok(instanceDto);
            }

            return Ok(instanceDto);
        }

        [HttpPut]
        public async Task<ActionResult<InstanceDto>> UpdateInstance(InstanceDto instanceDto)
        {
            if (ModelState.IsValid)
            {
                var item = await _unitOfWork.Instances.GetById(instanceDto.id);

                if (item == null)
                    return BadRequest();

                var instance = _mapper.Map<Instance>(instanceDto);
                if (instanceDto.parent_instance_id != null)
                {
                     var parentItem = await _unitOfWork.Instances.GetById((Guid)instanceDto.parent_instance_id);
                     instance.ParentInstance = parentItem;
                }

                await _unitOfWork.Instances.Update(instance);
                await _unitOfWork.CompleteAsync();

                    return Ok(instanceDto);
                }

                return Ok(instanceDto);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<InstanceDto?>> DeleteInstance(Guid id)
        {
            var item = await _unitOfWork.Instances.GetById(id);

            // if (item == null)
            //     return BadRequest();

            await _unitOfWork.Instances.Delete(id);
            await _unitOfWork.CompleteAsync();

            return Ok(item);
        }
    }
}