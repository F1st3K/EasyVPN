namespace EasyZsV.Application.Common.Interfaces.Authentication;

public interface IHashGenerator
{
    public string Hash(string value);
}