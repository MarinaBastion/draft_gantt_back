using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using gantt_backend.Interfaces.UOF;
using gantt_backend.Repositories.UOF;
using gantt_backend.Data.Models;
using gantt_backend.Data.ViewModels;
using AutoMapper;
using gantt_backend.Data.ModelsDTO;

namespace gantt_backend.Controllers

{   [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public DataController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        // GET api/
        [HttpGet]
        public async Task<ActionResult<GanttViewModel>> Get()
        {
            var tasks = await _unitOfWork.Tasks.GetAll();
            var links = await _unitOfWork.Links.GetAll();
            // if (tasks != null)
            // {
            //     var task = _mapper.Map<IEnumerable<TaskViewModel>>(tasks);
            // }
            var link = new List<LinkViewModel>().AsEnumerable();
            var task = new List<TaskViewModel>().AsEnumerable();
            
            if ( tasks != null)
            { 
                if (links != null) 
                {
                    link = _mapper.Map<IEnumerable<LinkViewModel>>(links);
                }
                task = _mapper.Map<IEnumerable<TaskViewModel>>(tasks);
            }
            return new GanttViewModel
                {
                     tasks =task,
                     links = link
                };
            
        }
    }
}