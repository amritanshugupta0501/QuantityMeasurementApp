using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuantityMeasurementModel;
using QuantityMeasurementRepository;

namespace QuantityMeasurementService
{
    public class AuthService : IAuthService
    {
        private readonly QuantityMeasurementDbContext _context;
        private readonly IConfiguration _config;

        public AuthService(QuantityMeasurementDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public AuthResponseDTO Register(UserRegisterDTO request)
        {
            // 1. Check if user exists
            if (_context.Users.Any(u => u.Username == request.Username))
                throw new Exception("Username already exists.");

            // 2. Hash the password securely using BCrypt
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // 3. Save to database
            var newUser = new UserEntity { Username = request.Username, PasswordHash = passwordHash };
            _context.Users.Add(newUser);
            _context.SaveChanges();

            return new AuthResponseDTO { Message = "User registered successfully! Please login." };
        }

        public AuthResponseDTO Login(UserLoginDTO request)
        {
            // 1. Find the user
            var user = _context.Users.SingleOrDefault(u => u.Username == request.Username);
            if (user == null)
                throw new Exception("User not found.");

            // 2. Verify the password hash
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new Exception("Incorrect password.");

            // 3. Generate the JWT Token
            string token = GenerateJwtToken(user);

            return new AuthResponseDTO { Token = token, Message = "Login successful!" };
        }

        private string GenerateJwtToken(UserEntity user)
        {
            // Grab the secret key from appsettings.json
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Embed the user's ID and Username into the token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Print the token (Expires in 2 hours)
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}