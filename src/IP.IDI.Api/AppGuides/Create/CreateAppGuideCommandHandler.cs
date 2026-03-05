using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Text.RegularExpressions;

namespace IP.IDI.Api.AppGuides.Create;

internal sealed class CreateAppGuideCommandHandlerr(IIDIUnitOfWork _unitOfWork) :
    ICommandHandler<CreateAppGuideCommand, CreateAppGuideResponse>
{
    private readonly IAppGuidesRepository _appGuideRepository =
        _unitOfWork.GetRepository<IAppGuidesRepository>();

    private readonly IAppUserRepository _userRepository =
        _unitOfWork.GetRepository<IAppUserRepository>();

    public async Task<Result<CreateAppGuideResponse>> Handle(
        CreateAppGuideCommand command,
        CancellationToken cancellationToken)
    {
        string name = command.Request.Name!;
        string linkId = command.Request.LinkId!;
     
        AppGuide? guide = await _appGuideRepository.Data()
            .AsNoTracking()
            .FirstOrDefaultAsync(role => role.Name == name || role.LinkId == linkId,
            cancellationToken);

        if (guide is not null) return AppGuideErrors.AlreadyExists;

        string regexOnlyAphanumericAndUnderscoreAndDash = "^[A-Za-z0-9_-]+$";
        bool isMatch = Regex.IsMatch(linkId, regexOnlyAphanumericAndUnderscoreAndDash);

        if (!isMatch) return AppGuideErrors.InvalidLinkIdPattern;

        guide = AppGuide.Create(name, linkId);

        await _appGuideRepository.Create(guide);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        int totalUsers = await _userRepository.Data()
            .CountAsync(x => x.ActivableInfo.Active &&
                !x.DeletableInfo.Deleted, cancellationToken);

        CreateAppGuideResponseData responseData = new(
            guide.Id.Value,
            guide.Name!,
            guide.LinkId,
            0,
            totalUsers,
            guide.ActivableInfo.Active);

        CreateAppGuideResponse response = new(responseData,
            $"Registro de Guia do Sistema [{guide}] criado com sucesso!");

        return await Task.FromResult(Result.Success(response));
    }
}