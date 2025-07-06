namespace GeeSuthSoft.KSA.ZATCA.Models;

internal class PCSIDInfo
{
    public string BinaryToken { get; set; } = null!;
    public string PCSIDSecret { get; set; } = null!;
    public string privateKey { get; set;} = null!;
    
    public bool isValid => !string.IsNullOrEmpty(privateKey) && 
                           !string.IsNullOrEmpty(BinaryToken) && 
                           !string.IsNullOrEmpty(PCSIDSecret);
}