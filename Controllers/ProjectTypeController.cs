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
    public class ProjectTypeController : ControllerBase
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public ProjectTypeController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet,Authorize]
        public async Task<ActionResult<IEnumerable<ProjectTypeDto>?>> Get()
        {
            var projectTypes = await _unitOfWork.ProjectTypes.GetAll();
            var pts = _mapper.Map<IEnumerable<ProjectTypeDto>>(projectTypes);
            return Ok(pts);
        }

        [HttpGet("{id}"),Authorize]
        public async  Task<ActionResult<ProjectTypeDto?>> GetProjectType(Guid id)
        {
            var item = await _unitOfWork.ProjectTypes.GetById(id);
            return Ok(_mapper.Map<ProjectTypeDto>(item));
        }

        [HttpPost,Authorize]
        public async Task<ActionResult<ProjectTypeDto>> CreateProjectType(ProjectTypeDto ptDto)
        {
            if (ModelState.IsValid)
            {
                var prType = _mapper.Map<ProjectType>(ptDto);
                await _unitOfWork.ProjectTypes.Add(prType);
                await _unitOfWork.CompleteAsync();
                return Ok(ptDto);
            }
            return Ok(ptDto);
        }

        [HttpPut,Authorize]
        public async Task<ActionResult<ProjectTypeDto>?> UpdateProjectType(ProjectTypeDto ptDto)
        {
            if (ModelState.IsValid)
            {
                var item = await _unitOfWork.ProjectTypes.GetById(ptDto.id);
                var prType = _mapper.Map<ProjectType>(ptDto);
                await _unitOfWork.ProjectTypes.Update(prType);
                await _unitOfWork.CompleteAsync();
                return Ok(ptDto);
            }
            return Ok(ptDto);
        }
        [HttpDelete("{id}"),Authorize]
        public async Task<ActionResult<ProjectTypeDto>?> DeleteProjectType(Guid id)
        {
            var item = await _unitOfWork.ProjectTypes.GetById(id);
            await _unitOfWork.ProjectTypes.Delete(id);
            await _unitOfWork.CompleteAsync();
            return Ok(item);
        }
    }
}