namespace EasyZsV.Contracts.DynamicPages;

public record DynamicPageRequest(
        string Route,
        string Title,
        string Base64Content
    );