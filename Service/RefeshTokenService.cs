
using API.Contract.IRepository;
using API.Contract.IService;
using API.Data.Dto;
using Entities.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace API.Service;

public class RefreshTokenService : IRefreshTokenService
{
    private readonly IRepositoryWrapper _repoWrapper;
    public RefreshTokenService(IRepositoryWrapper repositoryWrapper)
    {
        _repoWrapper = repositoryWrapper;
    }
    public void Create(Guid userId, string token, Guid accessToken)
    {
        RefreshToken refreshToken = new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Token = token,
            AccessToken = accessToken,
            IsRevoked = false,
            ExpireTime = DateTime.UtcNow.AddMinutes(15)
        };

        _repoWrapper.RefreshToken.Create(refreshToken);
        _repoWrapper.Save();
    }

    public AuthResponseDto GenerateAuthResponse()
    {
        throw new NotImplementedException();
    }

    public AuthResponseDto GenereateRefreshToken(RefreshTokenDto authResponseDto)
    {
        return Update(Guid.Parse(authResponseDto.AccessToken));

    }
    public AuthResponseDto Update(Guid accessToken)
    {
        RefreshToken refreshToken = _repoWrapper.RefreshToken.FindFirstByCondition(x => x.AccessToken == accessToken && !x.IsRevoked && x.ExpireTime > DateTime.UtcNow);
        if (refreshToken == null)
        {
            throw new InvalidOperationException("Invalid or expired refresh token.");
        }
        refreshToken.AccessToken = Guid.NewGuid();
        DateTime updatedTime =DateTime.UtcNow.AddMinutes(15);
        if (updatedTime > refreshToken.ExpireTime)
        {
            refreshToken.ExpireTime = updatedTime;
        }

        _repoWrapper.RefreshToken.Update(refreshToken);
        _repoWrapper.Save();

        AuthResponseDto authResponseDto = new AuthResponseDto
        {
            AccessToken = refreshToken.AccessToken.ToString(),
            Token = refreshToken.Token.ToString(),
        };

        return authResponseDto;
    }

    public void ExpireToken(Guid refreshTokenId)
    {

        RefreshToken refreshToken = _repoWrapper.RefreshToken.FindFirstByCondition(rt => rt.AccessToken.Equals(refreshTokenId) && rt.IsRevoked.Equals(false));

        refreshToken.IsRevoked = true;

        _repoWrapper.RefreshToken.Update(refreshToken);
        _repoWrapper.Save();

    }
}