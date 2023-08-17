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
    public class ValueController : ControllerBase
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
   

        public ValueController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ValueDto>>> Get()
        {
        try 
        {
           //var tasks = await _unitOfWork.Tasks.GetAll();
        
           var values = _unitOfWork.Values.GetAll();
            if (values != null)
            {
                var value = _mapper.Map<IEnumerable<ValueDto>>(values);
                return Ok(value);
            }
            }
            catch(Exception ex)
            {
             Console.Write(ex.Message);
            }

            
            return NotFound();
        }

       

        [HttpGet("childvalues/{id}")]
        public async Task<ActionResult<IEnumerable<ValueDto>>> GetValuesByParent(string id)
        {
         try 
        {
           //var tasks = await _unitOfWork.Tasks.GetAll();
            var Id  = new Guid(id);
            List<Value> values = await _unitOfWork.Values.GetFirstLevelValue(Id);
            var res = new List<ValueDto>();
            using (FlatMapArray flatMapArr = new FlatMapArray(_mapper))
            {
                foreach(Value t in values){
                var res1 = new List<ValueDto>();    
                res1 = flatMapArr.ReturnFlatMappedArray(t,new List<ValueDto>());
                res.AddRange(res1);
                Console.WriteLine(res);
                }
            }
            return Ok(res);
            }
            catch(Exception ex)
            {
             Console.Write(ex.Message);
             return NotFound();
            }
            
            
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ValueDto>?> GetValue(Guid id)
        {
            var item = await _unitOfWork.Values.GetById(id);

            // if (item == null)
            //     return NotFound();

            var t = _mapper.Map<ValueDto>(item);
            

            return Ok(_mapper.Map<ValueDto>(item));
        }

        [HttpGet("byInstance/{id}")]
        public async Task<ActionResult<IEnumerable<ValueDto>>?> GetValueByInstance(string id)
        {
            var items = await _unitOfWork.Values.GetValuesByInstance(new Guid(id));

            var t = _mapper.Map<IEnumerable<ValueDto>>(items);

            return Ok(t);
        }

        [HttpPost]
        public async Task<ActionResult<ValueDto>> CreateValue(ValueDto valuedto)
        {
            if (ModelState.IsValid)
            {
                try 
                {
                    var value = _mapper.Map<Value>(valuedto);
                    if (valuedto.parent_value_id != null)
                    {
                        var subvalue = await _unitOfWork.Values.GetById(new Guid(valuedto.parent_value_id.ToString()));
                        value.ParentValue = subvalue;
                    }
                    await _unitOfWork.Values.Add(value);
                    await _unitOfWork.CompleteAsync();
                    var item = await _unitOfWork.Values.GetById(value.Id);
                    if (item == null)
                        return NotFound();
                    return Ok(_mapper.Map<ValueDto>(item));
                }
                catch (Exception ex)
                {
                     Console.Write( ex.Message );               
                }
            }
            return NotFound();
        }
        [HttpPost("batch")]
        public async Task<ActionResult> CreateValueBatch(ValueDto[] valuedDto)
        {
            if (ModelState.IsValid)
            {
                var values = _mapper.Map<Value[]>(valuedDto);

                await _unitOfWork.Values.AddRange(values);
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
        public async Task<ActionResult<ValueDto>> UpdateValue(ValueDto valuedto)
        {   
            var item = await _unitOfWork.Values.GetById((Guid)valuedto.id!);
            if ((ModelState.IsValid))
            {

                if (item == null)
                    return BadRequest();

                    var value = _mapper.Map<Value>(valuedto);
                    try 
                    {
                        if (valuedto.parent_value_id != null)
                        {
                            var subvalue = await _unitOfWork.Values.GetById(new Guid(valuedto.parent_value_id.ToString()));
                            value.ParentValue = subvalue;
                        }
                        await _unitOfWork.Values.Update(value);
                        await _unitOfWork.CompleteAsync();
                        
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    return Ok(_mapper.Map<ValueDto>(item));
                }

                return Ok(_mapper.Map<ValueDto>(item));
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ValueDto?>> DeleteValue(string id)
        {
            var Id  = new Guid(id);
            var item = await _unitOfWork.Values.GetById(Id);

            // if (item == null)
            //     return BadRequest();
           
            await _unitOfWork.Values.Delete(Id);
            await _unitOfWork.CompleteAsync();

            return Ok(_mapper.Map<ValueDto>(item));
        }

        [HttpDelete("batch/{id}")]
        public async Task<ActionResult<bool>> DeleteBatchValue(string id)
        {
            var Id  = new Guid(id);
            var items = await _unitOfWork.Values.Find(c => (c.InstanceId ==  new Guid(id)));
            

            // if (item == null)
            //     return BadRequest();
           
            await _unitOfWork.Values.RemoveRange(items);
            await _unitOfWork.CompleteAsync();

            return Ok(true);
        }
    }
}