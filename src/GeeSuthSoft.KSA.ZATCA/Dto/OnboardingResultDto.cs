using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeeSuthSoft.KSA.ZATCA.Dto
{
    public class OnboardingResultDto
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
