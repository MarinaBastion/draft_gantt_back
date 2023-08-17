using Microsoft.AspNetCore.Mvc;
using gantt_backend.Interfaces.UOF;
using AutoMapper;
using gantt_backend.Data.ModelsDTO;
using gantt_backend.Data.Models.Constructor;

namespace gantt_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectTypeFieldController : ControllerBase
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public ProjectTypeFieldController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectTypeFieldsDto>>> Get()
        {
            var prTypeFields = await _unitOfWork.ProjectTypeFields.GetAll();
             if (prTypeFields != null)
            {
                var items = _mapper.Map<IEnumerable<ProjectTypeFieldsDto>>(prTypeFields);
                return Ok(items);
            }
        
            return NotFound();
        }

        [HttpGet("{id}")]
        public async  Task<ActionResult<ProjectTypeFieldsDto>> GetEntityField(Guid id)
        {
            var item = await _unitOfWork.ProjectTypeFields.GetById(id);

            if (item == null)
                return NotFound();

            return Ok(_mapper.Map<ProjectTypeFieldsDto>(item));
        }

        [HttpPost]
        public async Task<ActionResult<EntityFieldDto>> CreateEntityField(ProjectTypeFieldsDto prTypeFieldDto)
        {
            if (ModelState.IsValid)
            {
                var prTypeField = _mapper.Map<ProjectTypeFields>(prTypeFieldDto);

                await _unitOfWork.ProjectTypeFields.Add(prTypeField);
                await _unitOfWork.CompleteAsync();

                return Ok(prTypeFieldDto);
            }

            return Ok(prTypeFieldDto);
        }
        [HttpPost("batch")]
        public async Task<ActionResult<ProjectTypeFieldsDto>> CreateProjectTypeFieldBatch(ProjectTypeFieldsDto[] prTypeFieldDto)
        {
            if (ModelState.IsValid)
            {
                var prTypeField= _mapper.Map<ProjectTypeFields[]>(prTypeFieldDto);

                await _unitOfWork.ProjectTypeFields.AddRange(prTypeField);
                await _unitOfWork.CompleteAsync();

                return Ok(prTypeFieldDto);
            }

            return Ok(prTypeFieldDto);
        }

        [HttpPut]
        public async Task<ActionResult<ProjectTypeFieldsDto>> UpdateProjectTypeField(ProjectTypeFieldsDto prTypeFieldDto)
        {
            if (ModelState.IsValid)
            {
                var item = await _unitOfWork.ProjectTypeFields.GetById(prTypeFieldDto.id);

                if (item == null)
                    return BadRequest();

                var prTypeField = _mapper.Map<ProjectTypeFields>(prTypeFieldDto);

                await _unitOfWork.ProjectTypeFields.Update(prTypeField);
                await _unitOfWork.CompleteAsync();

                    return Ok(prTypeFieldDto);
                }

                return Ok(prTypeFieldDto);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProjectTypeFieldsDto?>> DeleteProjectTypeField(Guid id)
        {
            var item = await _unitOfWork.ProjectTypeFields.GetById(id);

            // if (item == null)
            //     return BadRequest();

            await _unitOfWork.ProjectTypeFields.Delete(id);
            await _unitOfWork.CompleteAsync();

            return Ok(item);
        }

        [HttpPut("batch")]
        public async Task<ActionResult> DeleteProjectTypeFields(ProjectTypeFieldsDto[] prTypeFieldDto)
        {
            if (ModelState.IsValid)
            {
                var prTypeField = _mapper.Map<ProjectTypeFields[]>(prTypeFieldDto);

                await _unitOfWork.ProjectTypeFields.RemoveRange(prTypeField);
                 await _unitOfWork.CompleteAsync();
                return Ok();
            }
        return Ok();

           
        }
    }
}