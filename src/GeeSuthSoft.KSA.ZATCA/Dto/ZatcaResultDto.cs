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


    public class PCSIDRequestDto
    {
       public string CsidComplianceRequestId { get; set; }
       public string CsidBinarySecurityToken { get; set; } 
       public string CsidSecret { get; set; }
    }
    
}
