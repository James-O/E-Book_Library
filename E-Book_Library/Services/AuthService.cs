using E_Book_Library.DTOs;
using E_Book_Library.Models;
using Microsoft.AspNetCore.Identity;

namespace E_Book_Library.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJWTService _jwtService;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IJWTService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        public async Task<LoginResponseDto> Login(LoginDto model)
        {
            LoginResponseDto login = new LoginResponseDto();
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return new LoginResponseDto { Message = "No record found, Kindly signup",IsSuccess=false };

            var userEmailConfirm = await _userManager.IsEmailConfirmedAsync(user);

            if (userEmailConfirm)
            {
                var response = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (response.Succeeded)
                {
                    var token = await _jwtService.GenerateToken(user);

                    login.Token = token;
                    login.IsSuccess = true;
                    login.Message = "Login Succesful";
                    login.StatusCode = 200;
                    return login;
                }

                login.IsSuccess = false;
                login.Message = "Loin not Succesful";
                login.StatusCode = 400;
                return login;
            }
            
            login.IsSuccess = false;
            login.Message = "Email not confirmed";
            login.StatusCode = 400;
            return login;
        }

        public async Task<RegisterResponseDto> Register(RegisterDto model)
        {
            RegisterResponseDto response = new RegisterResponseDto();
            
            var checkUser = await _userManager.FindByEmailAsync(model.Email);
            if(checkUser != null)
            {
                return new RegisterResponseDto() { IsSuccess = false, Message="user already exist",StatusCode=400};
            }

            var user = new User();
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = model.Email;
            user.DateCreated = DateTime.Now;

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                response.IsSuccess = false;
                response.Message = "User wasn't Created";
                response.StatusCode = 400;
                return response;
            }
            response.IsSuccess = true;
            response.Message = "User Created Succesfully";
            response.StatusCode = 200;
            return response;
        }
    }
}
