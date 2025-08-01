using Application.Utils;
using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class AuthService(IRefreshTokenRepository refreshTokenRepository, ITokenService tokenService, 
    IUserRepository userRepository): IAuthService
{
      public async Task<Result<AuthDto>> LoginAsync(User userDto)
    {
        var userResult = await userRepository.GetByUsernameAsync(userDto.Username);

        if (!userResult.IsSuccess)
        {
            return Result<AuthDto>.Failure(userResult.ErrorMessage!)!;
        }

        var user = userResult.Data;
        var verifyResult = new PasswordHasher<User>().VerifyHashedPassword(user, user.Password, userDto.Password);

        if (verifyResult != PasswordVerificationResult.Success)
        {
            return Result<AuthDto>.Failure("Invalid password")!;
        }
        
        var accessToken = tokenService.GenerateAccessToken(user);
        var refreshToken = RefreshToken.Create(tokenService.GenerateRefreshToken(), user.Id);
        
        var updateResult = await refreshTokenRepository.AddOrUpdateAsync(refreshToken);

        if (!updateResult.IsSuccess)
        {
            return Result<AuthDto>.Failure("Invalid refresh token")!;
        }
        
        return Result<AuthDto>.Success(AuthDto.Create(accessToken, refreshToken.Token));
    }

    public async Task<Result<AuthDto>> RefreshTokenAsync(string accessToken, string refreshToken)
    {
        var principalResult = tokenService.GetPrincipalFromExpiredToken(accessToken);
        
        if (!principalResult.IsSuccess)
        {
            return Result<AuthDto>.Failure(principalResult.ErrorMessage!)!;
        }

        var username = principalResult.Data.Identity!.Name!;
        var userResult = await userRepository.GetByUsernameAsync(username);

        if (!userResult.IsSuccess)
        {
            return Result<AuthDto>.Failure(userResult.ErrorMessage!)!;
        }

        var refreshTokenResult = await refreshTokenRepository.GetByUserIdAsync(userResult.Data.Id);

        if (!refreshTokenResult.IsSuccess || refreshTokenResult.Data.Token != refreshToken)
        {
            return Result<AuthDto>.Failure("Invalid refresh token")!;
        }
        
        if (refreshTokenResult.Data.Expires <= DateTime.UtcNow)
        {
            return Result<AuthDto>.Failure("Refresh token has expired")!;
        }
        
        var newAccessToken = tokenService.GenerateAccessToken(userResult.Data);
        var newRefreshToken = RefreshToken.Create(tokenService.GenerateRefreshToken(), userResult.Data.Id);
        
        var updateResult = await refreshTokenRepository.AddOrUpdateAsync(newRefreshToken);

        if (!updateResult.IsSuccess)
        {
            return Result<AuthDto>.Failure("Invalid refresh token!")!;
        }
        
        return Result<AuthDto>.Success(AuthDto.Create(newAccessToken, newRefreshToken.Token));
    }

    public async Task<Result> RevokeRefreshTokenAsync(string username)
    {
        var userResult = await userRepository.GetByUsernameAsync(username);

        if (!userResult.IsSuccess)
        {
            return Result.Failure(userResult.ErrorMessage!)!;
        }
        
        var deleteResult = await refreshTokenRepository.DeleteAsync(userResult.Data.Id);
        
        return deleteResult.IsSuccess
            ? Result.Success()
            : Result.Failure(deleteResult.ErrorMessage!)!;
    }
}