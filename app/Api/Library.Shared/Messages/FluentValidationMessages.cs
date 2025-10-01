namespace Library.Shared.Messages;

public static class FluentValidationMessages
{
    public const string NOT_EMPTY = "O campo ({PropertyName}) está vazio. Por favor, forneça um valor.";
    public const string INVALID_VALUE = "O campo ({PropertyName}) está inválido. Por favor, revise.";
    public const string MAX_LENGTH = "O campo ({PropertyName}) está acima do máximo de linhas {0}.";
}