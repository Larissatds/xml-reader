namespace ReadingXML.Application.Interfaces
{
    public interface IJwtService
    {
        (string token, DateTime expiresAt) GenerateToken();
    }
}
