using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using gantt_backend.Interfaces.UOF;
using gantt_backend.Repositories.UOF;
using gantt_backend.Data.Models;
using gantt_backend.Data.ViewModels;
using AutoMapper;
using gantt_backend.Data.ModelsDTO;

namespace gantt_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AssignmentController : ControllerBase
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public AssignmentController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssignViewModel>>> Get()
        {
            var assignments = await _unitOfWork.Assignments.GetAll();
            if (assignments != null)
            {
                var assign = _mapper.Map<IEnumerable<AssignViewModel>>(assignments);
                return Ok(assign);
            }
        
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AssignViewModel>> GetAssignment(Guid id)
        {
            var item = await _unitOfWork.Assignments.GetById(id);

            if (item == null)
                return NotFound();

            return Ok(_mapper.Map<AssignViewModel>(item));
        }

        [HttpPost]
        public async Task<ActionResult<AssignViewModel>> CreateAssignment(AssignmentDto assignmentdto)
        {
            if (ModelState.IsValid)
            {
                var assignment = _mapper.Map<Assignment>(assignmentdto);
                assignment.Id = Guid.NewGuid();
                await _unitOfWork.Assignments.Add(assignment);
                await _unitOfWork.CompleteAsync();
            var item = await _unitOfWork.Assignments.GetById(assignment.Id);

            if (item == null)
                return NotFound();

            return Ok(_mapper.Map<AssignViewModel>(item));
                
            }

             return NotFound();
        }
        [HttpPut]
          public async Task<ActionResult<AssignViewModel>> UpdateAssignment(AssignmentDto assignmentdto)
        {
            var item = await _unitOfWork.Assignments.GetById(Guid.Parse(assignmentdto.id));
            if ((ModelState.IsValid))
            {

                if (item == null)
                    return BadRequest();

                    var assign = _mapper.Map<Assignment>(assignmentdto);
                    try 
                    {
                        await _unitOfWork.Assignments.Update(assign);
                        await _unitOfWork.CompleteAsync();
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    return Ok(_mapper.Map<AssignViewModel>(item));
                }

                return Ok(_mapper.Map<AssignViewModel>(item));
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAssignment(string id)
        {
            var Id  = Guid.Parse(id);
            var item = await _unitOfWork.Assignments.GetById(Id);

            if (item == null)
                return BadRequest();

            await _unitOfWork.Assignments.Delete(Id);
            await _unitOfWork.CompleteAsync();

            return Ok(true);
        }
    }
}