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
    public class TeacherTaskController : Controller
    {

        private readonly ITaskRepository _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public TeacherTaskController(ITaskRepository repository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
        }


        #region Teacher Task 


        #region Get Task


        //  [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> ShowTaskTeacher()
        {
            var teacherId = _userManager.GetUserId(User);

            var tasks = await _repository.GetAllTaskTeacher(teacherId);

            var Result = _mapper.Map<IEnumerable<TeacherTaskVM>>(tasks);
            return View(Result);
        }
        #endregion


        #region CreateTask

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskVM task)
        {
            if (!ModelState.IsValid)
            {
                var teacherId = _userManager.GetUserId(User);
                var model = _mapper.Map<SchoolTask>(task);
                _repository.AddTask(teacherId,model);
                var count = await _repository.Complete();
                if (count > 0)
                {
                    TempData["Message"] = "ADD Successfully";

                }
                else
                {
                    TempData["Message"] = "Not ADD Successfully";

                }
                return RedirectToAction(nameof(Index));
            }

            return View(task);
        }


        #endregion

       

        #region Update Task 

        [HttpGet]
        public async Task<IActionResult> EditTeacher(int? id)
        {

            if (id is null)
            {
                return BadRequest();
            }
            var data = await _repository.GetByID(id.Value);
            var Taskviewmodel = _mapper.Map<TeacherTaskVM>(data);
            if (data == null) return NotFound();
            return View(Taskviewmodel);
        }
        [HttpPost]
        public async Task<IActionResult> EditTeacher(int id, TeacherTaskVM task)
        {

            if (id != task.Id)
                return BadRequest();

            var data = _mapper.Map<SchoolTask>(task);
            if (!ModelState.IsValid)
            {


                await _repository.UpdateTeacherTask(data);
                var count = await _repository.Complete();
                if (count > 0)
                {

                    return RedirectToAction(nameof(ShowTaskTeacher));
                }
            }
            return View(task);
        }


        #endregion

        #region Delete Task

        [HttpGet]
        public async Task<IActionResult> DeleteTask(int? id)
        {

            if (id is null)
            {
                return BadRequest();
            }
            var data = await _repository.GetByID(id.Value);
            var Taskviewmodel = _mapper.Map<TeacherTaskVM>(data);
            if (data == null) return NotFound();
            return View(Taskviewmodel);
        }

        [HttpPost]
        public IActionResult DeleteTask(int id, TeacherTaskVM task)
        {

            if (id != task.Id)
                return BadRequest();

            var data = _mapper.Map<SchoolTask>(task);
            if (!ModelState.IsValid)
            {


                _repository.DeleteTask(data);

                return RedirectToAction(nameof(ShowTaskTeacher));
            }
            return View(task);
        }

        #endregion


        #endregion
    }
}
