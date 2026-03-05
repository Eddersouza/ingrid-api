namespace IP.IDI.Api.AppGuides.ViewLink;

public sealed record ViewLinkAppGuideQuery(string LinkId) :
    IQuery<ViewLinkAppGuideResponse>;

public sealed class ViewLinkAppGuideResponse(
    bool viewed) :
    ResolvedData<ViewLinkAppGuideResponseData>(
        new ViewLinkAppGuideResponseData(viewed), string.Empty);

public sealed record ViewLinkAppGuideResponseData(bool Viewed);