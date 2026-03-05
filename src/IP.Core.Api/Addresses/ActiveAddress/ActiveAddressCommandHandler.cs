namespace IP.Core.Api.Addresses.ActiveAddress;

internal sealed class ActiveAddressCommandHandler(ICoreUoW _unitOfWork) :
    ICommandHandler<ActiveAddressCommand, ActiveAddressResponse>
{
    private readonly IAddressRepository _addressRepository =
        _unitOfWork.GetRepository<IAddressRepository>();

    public async Task<Result<ActiveAddressResponse>> Handle(
        ActiveAddressCommand command,
        CancellationToken cancellationToken)
    {
        AddressId addressId = new(command.Id);

        Address? address = await _addressRepository.Data()
            .AsNoTracking()
            .FirstOrDefaultAsync(
            x => x.Id == addressId,
            cancellationToken);

        if (address is null) return AddressErrors.AddressNotFound;

        bool isActived = command.Request.Active;
        string reason = command.Request.Reason;

        if (isActived && address.ActivableInfo.Active)
            return AddressErrors.AlreadyActiveStatus(isActived);

        if (isActived) address.ActivableInfo.SetAsActive();
        else address.ActivableInfo.SetAsDeactive(reason);

        _addressRepository.Update(address);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var actionText = isActived ? "ativado" : "desativado";

        var response = new ActiveAddressResponse(
            address.Id.Value!,
            address.Name.Value!,
            address.ActivableInfo.Active,
            $"Registro de Endereço {actionText} com sucesso!");

        return Result.Success(response);
    }
}
