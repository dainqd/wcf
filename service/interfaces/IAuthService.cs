using baseNetApi.models.Auth;

namespace baseNetApi.service.interfaces;

public interface IAuthService
{
    AuthenticateResponse Authenticate(AuthenticateRequest model);
    void Register(RegisterRequest model);
}