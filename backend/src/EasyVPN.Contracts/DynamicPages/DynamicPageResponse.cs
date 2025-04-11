namespace EasyVPN.Contracts.DynamicPages;

public record DynamicPageResponse(
        string Route,
        string Title,
        DateTime LastModified,
        DateTime Created,
        string Base64Content
    ) : DynamicPageInfo(Route, Title, LastModified, Created);