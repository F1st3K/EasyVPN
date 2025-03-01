namespace EasyVPN.Contracts.DynamicPages;

public record DynamicPageInfo(
        string Route,
        string Title,
        DateTime LastModified,
        DateTime Created
    );