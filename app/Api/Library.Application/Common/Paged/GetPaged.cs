namespace Library.Application.Common.Paged;

public class GetPaged
{
    public int? Page        { get; set; } = 1;
    public int? PageSaze    { get; set; } = 50;
    public string OrderBy   { get; set; } = "desc";
}