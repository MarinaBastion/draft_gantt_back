using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using gantt_backend.Interfaces.UOF;
using gantt_backend.Repositories.UOF;
using A=gantt_backend.Data.Models;
using gantt_backend.Data.Models;
using gantt_backend.Data.Models.Constructor;
using gantt_backend.Data.ViewModels;
using AutoMapper;
using gantt_backend.Data.ModelsDTO;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using gantt_backend.Implementation.Utilities;
using gantt_backend.Data.Models.Constructor;

namespace gantt_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskValueController : ControllerBase
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
   

        public TaskValueController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskValueDto>>> Get()
        {
            try 
            {
                var values = await _unitOfWork.TaskValues.GetAll();
                if (values != null)
                {
                    var value = _mapper.Map<IEnumerable<TaskValueDto>>(values);
                    return Ok(value);
                }
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
            }
            return NotFound();
        }
       
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskValueDto>?> GetValue(Guid id)
        {
            var item = await _unitOfWork.TaskValues.GetById(id);

            // if (item == null)
            //     return NotFound();

            var t = _mapper.Map<TaskValueDto>(item);
            

            return Ok(_mapper.Map<TaskValueDto>(item));
        }

        [HttpGet("byTask/{id}")]
        public async Task<ActionResult<IEnumerable<TaskValueDto>>?> GetValueByTask(string id)
        {
            var items = await _unitOfWork.TaskValues.GetValuesByTask(new Guid(id));

            var t = _mapper.Map<IEnumerable<TaskValueDto>>(items);

            return Ok(t);
        }

        [HttpPost]
        public async Task<ActionResult<TaskValueDto>> CreateValue(TaskValueDto valuedto)
        {
            if (ModelState.IsValid)
            {
                try 
                {
                    var value = _mapper.Map<TaskValue>(valuedto);
                    await _unitOfWork.TaskValues.Add(value);
                    await _unitOfWork.CompleteAsync();
                    var item = await _unitOfWork.TaskValues.GetById(value.Id);
                    if (item == null)
                        return NotFound();
                    return Ok(_mapper.Map<TaskValueDto>(item));
                }
                catch (Exception ex)
                {
                     Console.Write( ex.Message );               
                }
            }
            return NotFound();
        }
        [HttpPost("batch")]
        public async Task<ActionResult> CreateValueBatch(List<TaskValueDto> valuedDto)
        {
            if (ModelState.IsValid)
            {
                var values = _mapper.Map<List<TaskValue>>(valuedDto);

                await _unitOfWork.TaskValues.AddRange(values);
                try
                {
                    await _unitOfWork.CompleteAsync();
                }
                catch(Exception ex)
                {
                    Console.Write(ex.Message);
                }

                return Ok();
            }

            return NoContent();
        }
        [HttpPut]
        public async Task<ActionResult<TaskValueDto>> UpdateValue(TaskValueDto valuedto)
        {   
            var item = await _unitOfWork.TaskValues.GetById((Guid)valuedto.id!);
            if ((ModelState.IsValid))
            {

                if (item == null)
                    return BadRequest();

                    var value = _mapper.Map<TaskValue>(valuedto);
                    try 
                    {
                        await _unitOfWork.TaskValues.Update(value);
                        await _unitOfWork.CompleteAsync();
                        
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    return Ok(_mapper.Map<TaskValueDto>(item));
                }

                return Ok(_mapper.Map<TaskValueDto>(item));
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<TaskValueDto?>> DeleteValue(string id)
        {
            var Id  = new Guid(id);
            var item = await _unitOfWork.TaskValues.GetById(Id);

            // if (item == null)
            //     return BadRequest();
           
            await _unitOfWork.TaskValues.Delete(Id);
            await _unitOfWork.CompleteAsync();

            return Ok(_mapper.Map<TaskValueDto>(item));
        }

        [HttpDelete("batch/{id}")]
        public async Task<ActionResult<bool>> DeleteBatchValue(string id)
        {
            var Id  = new Guid(id);
            var items = await _unitOfWork.TaskValues.Find(c => (c.TaskId ==  new Guid(id)));
            

            // if (item == null)
            //     return BadRequest();
           
            await _unitOfWork.TaskValues.RemoveRange(items);
            await _unitOfWork.CompleteAsync();

            return Ok(true);
        }
    }
}