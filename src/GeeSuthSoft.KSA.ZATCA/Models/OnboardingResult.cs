namespace GeeSuthSoft.KSA.ZATCA.Models
{
    internal class OnboardingResult
    {
        public string GeneratedCsr { get; set; }
        public string PrivateKey { get; set; }
        public string CCSIDComplianceRequestId { get; set; }
        public string CCSIDBinaryToken { get; set; }
        public string CCSIDSecret { get; set; }
        public string PCSIDBinaryToken { get; set; }
        public string PCSIDSecret { get; set; }
    }
}
