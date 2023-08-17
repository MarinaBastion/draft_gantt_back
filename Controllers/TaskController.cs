using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using gantt_backend.Interfaces.UOF;
using gantt_backend.Repositories.UOF;
using A=gantt_backend.Data.Models;
using gantt_backend.Data.Models;
using gantt_backend.Data.ViewModels;
using AutoMapper;
using gantt_backend.Data.ModelsDTO;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using gantt_backend.Implementation.Utilities;
using Microsoft.AspNetCore.Authorization;
using System.Dynamic;
using gantt_backend.Data.Models.Constructor;
namespace gantt_backend.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
   

        public TaskController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskViewModel>>> Get()
        {
        try 
        {         
            var tasks1 = _unitOfWork.Tasks.GetAllTasks().Include(o => o.Resources).ToList();
            if (tasks1 != null)
            {
                var task = _mapper.Map<IEnumerable<TaskViewModel>>(tasks1);
                return Ok(task);
            }
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
            }
            return NotFound();
        }

        [HttpGet("projects")]
        public async Task<ActionResult<IEnumerable<TaskViewModel>>> GetProjects()
        {
        try 
        {   
            var tasks = _unitOfWork.Tasks.GetOnlyProjects().Include(o => o.Resources).ToList();
            if (tasks != null)
            {
                var task = _mapper.Map<IEnumerable<TaskViewModel>>(tasks);
                return Ok(task);
            }
        }
        catch(Exception ex)
            {
             Console.Write(ex.Message);
            }            
        return NotFound();
        }

        [AllowAnonymous]
        [HttpGet("projects/{id}")]
        public async Task<ActionResult<IEnumerable<TaskViewModel>>> GetTasksByProject(string id)
        {
        try 
        {
            var Id  = new Guid(id);
            List<A.Task> tasks = await _unitOfWork.Tasks.GetTasksByProjectId(Id);
            var res = new List<TaskViewModel>();
            using (FlatMapArray flatMapArr = new FlatMapArray(_mapper))
            {
                foreach(A.Task t in tasks){
                var res1 = new List<TaskViewModel>();    
                res1 = flatMapArr.ReturnFlatMappedArray(t,new List<TaskViewModel>());
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

        [AllowAnonymous]
        [HttpGet("projectswithFields/{id}")]
        public async Task<ActionResult<IEnumerable<TaskViewModel>>> GetTasksWithFieldsByProject(string id)
        {
        try 
        {
            var Id  = new Guid(id);
            List<A.Task> tasks = await _unitOfWork.Tasks.GetTasksByProjectId(Id);
            var res = new List<TaskViewModel>();
            using (FlatMapArray flatMapArr = new FlatMapArray(_mapper))
            {
                foreach(A.Task t in tasks){
                var res1 = new List<TaskViewModel>();    
                res1 = flatMapArr.ReturnFlatMappedArray(t,new List<TaskViewModel>());
                var res2 = new List<TaskViewModel>();
                foreach(TaskViewModel tv in res1)
                {
                    List<TaskValueFieldDto>? values = await _unitOfWork.TaskValues.GetValuesByTask(new Guid(tv.id));
                    List<TaskValueView> tvList = new List<TaskValueView>();
                    Instance instance = new Instance();
                    if (values != null)  
                        foreach( var item in values )
                        {
                            
                            string value = "";
                            if (item.text_data != null && item.text_data != "")
                                value = item.text_data;
                            else if (item.numeric_data != null )
                                value = item.numeric_data.ToString()!;
                            else if (item.bool_data != null)
                            {
                                if (item.bool_data == true)
                                    value = "true";
                                else
                                    value = "false"; 
                            }
                            else if (item.type.directory_id != null || item.type.directory_id != "")
                            {
                                if (item.instance_id != null)
                                {
                                    Guid inst_id = (Guid)item.instance_id;
                                    instance = await _unitOfWork.Instances.GetById(inst_id);
                                    value = item.instance_id.ToString(); 
                                }
                            }
                            TaskValueView taskView = new TaskValueView{ field = item.field,value = value, type = item.type ,field_id = item.field_id.ToString()} ;
                            tvList.Add(taskView);   
                        }
                    tv.values = tvList;
                    res2.Add(tv);
                }                
                res.AddRange(res2);
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
        public async Task<ActionResult<TaskViewModel>> GetTask(Guid id)
        {
            var item = await _unitOfWork.Tasks.GetById(id);
            if (item == null)
                return NotFound();
            IEnumerable<A.Assignment>? exist = await _unitOfWork.Assignments.Find(c => c.TaskId == id);
            var t = _mapper.Map<TaskViewModel>(item);
            var a = _mapper.Map<List<AssignViewModel>>(exist);
            t.user = a;
            return Ok(_mapper.Map<TaskViewModel>(item));
        }

        [HttpPost]
        public async Task<ActionResult<TaskViewModel>> CreateTask(TaskDto taskdto)
        {
            if (ModelState.IsValid)
            {
                try 
                {
                    var task = _mapper.Map<A.Task>(taskdto);
                    if (taskdto.parent != null)
                    {
                        var subtask = await _unitOfWork.Tasks.GetById(new Guid(taskdto.parent.ToString()));
                        task.ParentTask = subtask;
                    }
                    if (taskdto.project_type_id != null)
                    {
                        var projectType = await _unitOfWork.ProjectTypes.GetById(new Guid(taskdto.project_type_id));
                        task.ProjectType = projectType;
                    }
                    await _unitOfWork.Tasks.Add(task);
                    await _unitOfWork.CompleteAsync();
                    if  (!(taskdto.user == null))
                    {
                        var users = _mapper.Map<List<Assignment>>(taskdto.user);
                        foreach(Assignment user in  users )
                        {
                            user.Id =  Guid.NewGuid();
                            user.TaskId =  task.Id;
                            await _unitOfWork.Assignments.Add(user);
                        }
                    }
                    await _unitOfWork.CompleteAsync();
                    var item = await _unitOfWork.Tasks.GetById(task.Id);

                    if (item == null)
                        return NotFound();
                    return Ok(_mapper.Map<TaskViewModel>(item));
                }
                catch (Exception ex)
                {
                     Console.Write( ex.Message );               
                }
            }
            return NotFound();
        }
        [HttpPut]
        public async Task<ActionResult<TaskViewModel>> UpdateTask(TaskDto assigndto)
        {
            var item = await _unitOfWork.Tasks.GetById(assigndto.id);
            if ((ModelState.IsValid))
            {
                if (item == null)
                    return BadRequest();
                    var task = _mapper.Map<A.Task>(assigndto);
                    try 
                    {
                        if (assigndto.parent != null)
                        {
                            var subtask = await _unitOfWork.Tasks.GetById(new Guid(assigndto.parent.ToString()));
                            task.ParentTask = subtask;
                        }
                        if (assigndto.project_type_id != null)
                        {
                            Guid pr_id = new Guid(assigndto.project_type_id!);
                            var pts = await _unitOfWork.ProjectTypes.GetAll();
                            var projectType = pts.Where(c => c.Id == pr_id ).FirstOrDefault();
                            // = await _unitOfWork.ProjectTypes.GetById(pr_id);
                            task.ProjectType = projectType;
                            
                        }
                        else if  (assigndto.type == "project")
                        {
                            task.ProjectTypeId = item.ProjectTypeId;
                            
                        }
                        await _unitOfWork.Tasks.Update(task);
                        await _unitOfWork.CompleteAsync();
                        //  adding new assignments
                        if  (!(assigndto.user == null))
                        {
                            var users = _mapper.Map<List<Assignment>>(assigndto.user);
                            foreach(Assignment user in  users )
                            { 
                                var assign = await _unitOfWork.Assignments.GetById(user.Id);
                                if (assign == null)
                                {
                                    user.Id =  Guid.NewGuid();
                                    user.TaskId =  task.Id;
                                    await _unitOfWork.Assignments.Add(user);
                                }
                            }
                            //updating existing assignments or deleting old
                            var assigns =  await _unitOfWork.Assignments.GetByTaskId(task.Id);
                            if (assigns != null){
                                foreach(Assignment assign in  assigns )
                                {
                                    var assgn = users.Where(c => c.Id == assign.Id).FirstOrDefault();
                                    if (assgn != null)
                                    {
                                            await _unitOfWork.Assignments.Update(assgn);
                                    }
                                    else{
                                        await _unitOfWork.Assignments.Delete(assign.Id);
                                    }
                                }
                            }
                            await _unitOfWork.CompleteAsync();
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    return Ok(_mapper.Map<TaskViewModel>(item));
                }
                return Ok(_mapper.Map<TaskViewModel>(item));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TaskViewModel>> DeleteTask(string id)
        {
            var Id  = new Guid(id);
            var item = await _unitOfWork.Tasks.GetById(Id);
            if (item == null)
                return BadRequest();
            var assigns =  await _unitOfWork.Assignments.GetByTaskId(Id);
            if (assigns != null)
                await _unitOfWork.Assignments.RemoveRange(assigns);
            await _unitOfWork.Tasks.Delete(Id);
            await _unitOfWork.CompleteAsync();
            return Ok(_mapper.Map<TaskViewModel>(item));
        }
    }
}