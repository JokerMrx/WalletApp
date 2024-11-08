namespace WalletApp.Core.Models;

public class PageParams
{
    public int Page { get; set; } = 1;
    public int Size { get; set; } = Constants.Page.DefaultSize;
}