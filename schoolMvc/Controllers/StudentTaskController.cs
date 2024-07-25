using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using schoolMvc.BLL.Interfaces;
using schoolMvc.DAL.Models;
using schoolMvc.PL.ViewModel.Task;
using System.Threading.Tasks;

namespace schoolMvc.PL.Controllers
{
    public class StudentTaskController : Controller
    {


        private readonly ITaskRepository _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentTaskController(ITaskRepository repository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
        }

        #region Student Task

        #region Get Task

        // Get Task => Student

        [Authorize(Roles = "Student")]
        [HttpGet]

        public async Task<IActionResult> ShowTaskStudent()
        {
            var studentId = _userManager.GetUserId(User);
            var tasks = await _repository.GetAllTaskStudent(studentId);

            var Result = _mapper.Map<IEnumerable<StudentTaskVM>>(tasks);
            return View(Result);

        }



        #endregion

        #region Update Status

        // Update Status Task 
        [HttpGet]
        public async Task<IActionResult> EditStudent(int? id)
        {

            if (id is null)
            {
                return BadRequest();
            }
            var data = await _repository.GetByID(id.Value);
            var Taskviewmodel = _mapper.Map<StudentTaskVM>(data);
            if (data == null) return NotFound();
            return View(Taskviewmodel);
        }
        [HttpPost]
        public async Task<IActionResult> EditStudent(int id, StudentTaskVM task)
        {

            if (id != task.Id)
                return BadRequest();

            var data = _mapper.Map<SchoolTask>(task);
            if (!ModelState.IsValid)
            {


                await _repository.UpdateStudentTask(data);
                var count = await _repository.Complete();
                if (count > 0)
                {

                    return RedirectToAction(nameof(ShowTaskStudent));
                }
            }
            return View(task);
        }
        #endregion

        #endregion
    }
}
