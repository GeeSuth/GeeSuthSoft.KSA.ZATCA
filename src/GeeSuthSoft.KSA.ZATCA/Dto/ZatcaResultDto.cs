namespace GeeSuthSoft.KSA.ZATCA.Dto
{
    public class ZatcaResultDto
    {
        public string RequestID { get; set; }
        public string TokenType { get; set; }
        public string DispositionMessage { get; set; }
        public string BinarySecurityToken { get; set; }
        public string Secret { get; set; }
        public List<string> Errors { get; set; }
    }
}
