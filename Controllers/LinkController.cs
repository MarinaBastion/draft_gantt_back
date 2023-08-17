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
    public class LinkController : ControllerBase
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public LinkController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LinkViewModel>>> Get()
        {
            var links = await _unitOfWork.Links.GetAll();
             if (links != null)
            {
                var link = _mapper.Map<IEnumerable<LinkViewModel>>(links);
                return Ok(link);
            }
        
            return NotFound();
        }

        [HttpGet("{id}")]
        public async  Task<ActionResult<LinkViewModel>> GetLink(Guid id)
        {
            var item = await _unitOfWork.Links.GetById(id);

            if (item == null)
                return NotFound();

            return Ok(_mapper.Map<LinkViewModel>(item));
        }

        [HttpPost]
        public async Task<ActionResult<LinkViewModel>> CreateLink(LinkViewModel linkVm)
        {
            if (ModelState.IsValid)
            {
                var link = _mapper.Map<Link>(linkVm);
                //link.Id =new Guid(linkVm.id);

                await _unitOfWork.Links.Add(link);
                await _unitOfWork.CompleteAsync();

                return Ok(linkVm);
            }

            return Ok(linkVm);
        }

        [HttpPut]
        public async Task<ActionResult<LinkViewModel>> UpdateLink(LinkViewModel linkVm)
        {
            if (ModelState.IsValid)
            {
                var item = await _unitOfWork.Links.GetById(linkVm.id);

                if (item == null)
                    return BadRequest();

                var link = _mapper.Map<Link>(linkVm);

                await _unitOfWork.Links.Update(link);
                await _unitOfWork.CompleteAsync();

                    return Ok(linkVm);
                }

                return Ok(linkVm);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<LinkViewModel>> DeleteLink(Guid id)
        {
            var item = await _unitOfWork.Links.GetById(id);

            if (item == null)
                return BadRequest();

            await _unitOfWork.Links.Delete(id);
            await _unitOfWork.CompleteAsync();

            return Ok(item);
        }
    }
}