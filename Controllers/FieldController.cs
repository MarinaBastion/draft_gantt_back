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
    public class FieldController : ControllerBase
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public FieldController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FieldDto>>> Get()
        {
            var fields = await _unitOfWork.Fields.GetAll();
            if (fields != null)
            {
                var link = _mapper.Map<IEnumerable<FieldDto>>(fields);
                return Ok(link);
            }
            return Ok();
        }

        [HttpGet("{id}")]
        public async  Task<ActionResult<FieldDto?>> GetField(Guid id)
        {
            var item = await _unitOfWork.Fields.GetById(id);
            // if (item == null)
            //     return NotFound();
            return Ok(_mapper.Map<FieldDto>(item));
        }

        [HttpGet("GetByEntityId{id}")]
        public async Task<ActionResult<IEnumerable<FieldDto>>> GetFieldByEntityId(Guid id)
        {
            List<FieldDto> items = new List<FieldDto>();
            items =  _mapper.Map<List<FieldDto>>(_unitOfWork.Fields.GetAllFieldsByEntityId(id).ToList());
            return Ok(items);
        }

        [HttpGet("GetByProjectTypeId{id}")]
        public async Task<ActionResult<IEnumerable<FieldDto>>> GetFieldByProjectTypeId(Guid id)
        {
            List<FieldDto> items = new List<FieldDto>();
            items =  _mapper.Map<List<FieldDto>>(_unitOfWork.Fields.GetAllFieldsByProjectTypeId(id).ToList());
            return Ok(items);
        }

        [HttpGet("GetByProjectId{id}")]
        public async Task<ActionResult<IEnumerable<FieldDto>>> GetFieldByProjectId(Guid id)
        {
            List<FieldDto> items = new List<FieldDto>();
            gantt_backend.Data.Models.Task task = await _unitOfWork.Tasks.GetById(id);
            Console.Write(task);
            var projectTypeId = task.ProjectTypeId;
            if (projectTypeId != null)
            {
                items =  _mapper.Map<List<FieldDto>>(_unitOfWork.Fields.GetAllFieldsByProjectTypeId((Guid)projectTypeId).ToList());
            }
            
            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult<FieldDto>> CreateField(FieldDto fieldDto)
        {
            if (ModelState.IsValid)
            {
                var field = _mapper.Map<Field>(fieldDto);
                await _unitOfWork.Fields.Add(field);
                await _unitOfWork.CompleteAsync();
                return Ok(fieldDto);
            }
            return Ok(fieldDto);
        }

        [HttpPut]
        public async Task<ActionResult<FieldDto>> UpdateField(FieldDto fieldDto)
        {
            if (ModelState.IsValid)
            {
                var item = await _unitOfWork.Fields.GetById(fieldDto.id);
                if (item == null)
                    return BadRequest();
                var field = _mapper.Map<Field>(fieldDto);
                await _unitOfWork.Fields.Update(field);
                await _unitOfWork.CompleteAsync();
                return Ok(fieldDto);
            }
            return Ok(fieldDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<FieldDto?>> DeleteField(Guid id)
        {
            var item = await _unitOfWork.Fields.GetById(id);

            // if (item == null)
            //     return BadRequest();
            await _unitOfWork.Fields.Delete(id);
            await _unitOfWork.CompleteAsync();
            return Ok(item);
        }
    }
}