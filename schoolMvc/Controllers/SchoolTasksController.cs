using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using schoolMvc.BLL.Interfaces;
using schoolMvc.DAL.Models;
using schoolMvc.PL.ViewModel.Task;

namespace schoolMvc.PL.Controllers
{
    public class SchoolTasksController : Controller
    {

        private readonly ITaskRepository _repository;
        private readonly IMapper _mapper;
        public SchoolTasksController(ITaskRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        #region Index_AllTask

        //[Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            var tasks = await _repository.GetAllTasks();
            var Result = _mapper.Map<IEnumerable<TaskShow>>(tasks);
            return View(Result);
        }
        #endregion
    }
}
