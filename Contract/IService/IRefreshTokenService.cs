using API.Data.Dto;

namespace API.Contract.IService;

public interface IRefreshTokenService
{
    void Create(Guid userId, string token, Guid accessToken);
    AuthResponseDto GenerateAuthResponse();
    AuthResponseDto GenereateRefreshToken(RefreshTokenDto authResponseDto);
    AuthResponseDto Update(Guid accessToken);

    void ExpireToken(Guid refreshTokenId);



}