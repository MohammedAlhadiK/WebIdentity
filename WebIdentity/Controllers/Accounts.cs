using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebIdentity.ViewModels;

namespace WebIdentity.Controllers
{
    public class Accounts : Controller
    {
        private readonly UserManager<IdentityUser> _usermanager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public Accounts(UserManager<IdentityUser> usermanager, SignInManager<IdentityUser> signInManager)
        {
            _usermanager = usermanager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Registers()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistersPost(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //valid
                var user = new IdentityUser()
                {
                    Email = model.Email,
                    UserName = model.Email
                };

                var result = await _usermanager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //sign in if register succeeded
                    await _signInManager.SignInAsync(user, isPersistent: false); //isPersistent  دي عشان يعمل كوكي لليوزر اللى عمل تسجيل
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }

            //return RedirectToAction("Index", "Home");
            return View("Registers", model);
        }





        [HttpGet]
        public IActionResult List()
        {
            var users = new RegisterListViewModel()
            {
                Users = _usermanager.Users.ToList()
            };
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string ID)
        {
            var user = await _usermanager.FindByIdAsync(ID);
            var UserEdit = new RegisterEditViewModel()
            {
                Email = user.Email,
                ID = user.Id
            };

            return View("EditPost", UserEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(RegisterEditViewModel model)
        {

            var user = await _usermanager.FindByIdAsync(model.ID);
            if (user == null)
            {
                return NotFound();
            }
            user.Email = model.Email;
           
            var result =await _usermanager.UpdateAsync(user);
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string ID)
        {
            var user = await _usermanager.FindByIdAsync(ID);
            var UserEdit = new RegisterEditViewModel()
            {
                Email = user.Email,
                ID = user.Id
            };

            return View("DeletePost", UserEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(RegisterEditViewModel model)
        {

            var user = await _usermanager.FindByIdAsync(model.ID);
            if (user == null)
            {
                return NotFound();
            }
            user.Email = model.Email;

            var result = await _usermanager.DeleteAsync(user);
            return RedirectToAction("List");
        }
    }
}
