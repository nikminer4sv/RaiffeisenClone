namespace RaiffeisenClone.Application.ViewModels;

public class DepositViewModel
{
    public DateTime Term { get; set; }
    public float Bid { get; set; }
    public string Currency { get; set; }
    public bool IsReplenished { get; set; }
    public bool IsWithdrawed { get; set; }
}