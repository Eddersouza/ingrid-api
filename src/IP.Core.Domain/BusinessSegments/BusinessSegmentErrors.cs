namespace IP.Core.Domain.BusinessSegments;

public static class BusinessSegmentErrors
{
    public static readonly Error BusinessSegmentAlreadyExists = Error.Conflict(
        "BusinessSegment.BusinessSegmentAlreadyExists",
        "Já existe um registro de Segmento de Negócio com o nome informado.");

    public static readonly Error BusinessSegmentNotFound = Error.NotFound(
        "BusinessSegment.BusinessSegmentNotFound", 
        "Nenhum registro de Segmento de Negócio foi encontrado.");

    public static Error AlreadyActiveStatus(bool isActive) => Error.Conflict(
        "BusinessSegment.AlreadyActiveStatus",
        $"Registro de Segmento de Negócio já está {(isActive ? "Ativo" : "Desativado")}.");
}
