using AutoMapper;
using DecorHaven.API.DTOs.Auth;
using DecorHaven.API.Models;
using DecorHaven.API.Repositories;
using DecorHaven.API.Utilities;

namespace DecorHaven.API.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(IUnitOfWork unitOfWork, IMapper mapper, IJwtTokenGenerator jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResponseDto?> RegisterAsync(RegisterDto registerDto)
    {
        var userRepository = _unitOfWork.Repository<User>();

        // Check if user already exists
        var existingUser = await userRepository.FirstOrDefaultAsync(u => u.Email == registerDto.Email);
        if (existingUser != null)
        {
            return null;
        }

        // Create new user
        var user = _mapper.Map<User>(registerDto);
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
        user.CreatedAt = DateTime.UtcNow;

        await userRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        // Generate token
        var token = _jwtTokenGenerator.GenerateToken(user);
        var userDto = _mapper.Map<UserDto>(user);

        return new AuthResponseDto
        {
            Token = token,
            User = userDto
        };
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
    {
        var userRepository = _unitOfWork.Repository<User>();

        var user = await userRepository.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
        {
            return null;
        }

        if (!user.IsActive)
        {
            return null;
        }

        var token = _jwtTokenGenerator.GenerateToken(user);
        var userDto = _mapper.Map<UserDto>(user);

        return new AuthResponseDto
        {
            Token = token,
            User = userDto
        };
    }

    public async Task<UserDto?> GetUserByIdAsync(int userId)
    {
        var userRepository = _unitOfWork.Repository<User>();
        var user = await userRepository.GetByIdAsync(userId);

        if (user == null)
            return null;

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto?> UpdateUserAsync(int userId, UserDto userDto)
    {
        var userRepository = _unitOfWork.Repository<User>();
        var user = await userRepository.GetByIdAsync(userId);

        if (user == null)
            return null;

        user.FirstName = userDto.FirstName;
        user.LastName = userDto.LastName;
        user.PhoneNumber = userDto.PhoneNumber;
        user.Address = userDto.Address;
        user.City = userDto.City;
        user.PostalCode = userDto.PostalCode;
        user.Country = userDto.Country;
        user.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<UserDto>(user);
    }
}

