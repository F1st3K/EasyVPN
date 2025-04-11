namespace EasyVPN.Contracts.DynamicPages;

public record DynamicPageRequest(
        string Route,
        string Title,
        string Base64Content
    );