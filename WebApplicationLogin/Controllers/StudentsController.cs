using Microsoft.AspNetCore.Mvc;
using WebApplicationLogin.Models;
using WebApplicationLogin.Data;
using WebApplicationLogin.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationLogin.Controllers
{
    public class StudentsController : Controller
    {
        private readonly AppData dbContext;
        public StudentsController(AppData dbContext) 
        {
            this.dbContext=dbContext;
        } 
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            var student = new Student
            {
                Name = viewModel.Name,
            Email=viewModel.Email,
            Phone=viewModel.Phone,
            Subscribed=viewModel.Subscribed
            };
            await dbContext.Students.AddAsync(student);
            await dbContext.SaveChangesAsync();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> list()
        {
            var students= await dbContext.Students.ToListAsync();
            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var students = await dbContext.Students.FindAsync(id);
            return View(students);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student ViewModel)
        {
            var student = await dbContext.Students.FindAsync(ViewModel.Id);  
            if(student is not null)
            {
                student.Name=ViewModel.Name;
                student.Email=ViewModel.Email;  
                student.Phone=ViewModel.Phone;  
                student.Subscribed=ViewModel.Subscribed;

                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("list", "Students");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Student ViewModel)
        {
            var student = await dbContext.Students.AsNoTracking().FirstOrDefaultAsync(x => x.Id == ViewModel.Id);
            if (student is not null)
            {
                dbContext.Students.Remove(ViewModel);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("list", "Students");
        }
    }
}
