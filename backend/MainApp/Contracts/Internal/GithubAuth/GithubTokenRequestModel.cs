using Refit;

namespace Contracts.Internal.GithubAuth;

public sealed class GithubTokenRequestModel
{
    [AliasAs("client_id")] public string ClientId { get; init; } = null!;

    [AliasAs("client_secret")] public string ClientSecret { get; init; } = null!;

    [AliasAs("code")] public string Code { get; init; } = null!;

    [AliasAs("redirect_uri")] public string RedirectUri { get; init; } = null!;
}
