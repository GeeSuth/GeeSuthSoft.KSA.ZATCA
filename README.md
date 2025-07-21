# GeeSuthSoft.KSA.ZATCA



## Overview
GeeSuthSoft.KSA.ZATCA is a .NET library that helps developers integrate with ZATCA (Zakat, Tax and Customs Authority) requirements in Saudi Arabia. This library provides tools and utilities for e-invoicing compliance.

## Features
- Generate CSR (Certificate Signing Request) for ZATCA compliance
- Handle ZATCA onboarding process
- Generate Compliance certificate and secret (CCSID)
- Generate Production certificate and secret (PCSID)
- Share and validate e-invoices with ZATCA
- Support Production and Non-Production , Simulation environments
- Built for .NET 7+

## Installation
Install via NuGet Package Manager:

```bash
dotnet add package GeeSuthSoft.KSA.ZATCA
```

## Configuration

Add the ZATCA service to your dependency injection container in `Program.cs` or `Startup.cs`:


```csharp

using GeeSuthSoft.KSA.ZATCA.Extensions;


services.AddZatca(options =>
            {
                options.Environment = EnvironmentType.NonProduction;
                options.LogRequestAndResponse = true;
            });
```

## Usage

### OnBoarding Service

```csharp
using GeeSuthSoft.KSA.ZATCA.Services;
using GeeSuthSoft.KSA.ZATCA.Dto;

// Example: Generate CSR (Certificate Signing Request)
var onboardingService = serviceProvider.GetRequiredService<IZatcaOnboardingService>();
var csrDto = new CsrGenerationDto {
    CommonName = "TST-886431145-399999999900003",
    SerialNumber = "1-TST|2-TST|3-ed22f1d8-e6a2-1118-9b58-d9a8f11e445f",
    OrganizationIdentifier = "399999999900003",
    OrganizationUnitName = "Riyadh Branch",
    OrganizationName = "Maximum Speed Tech Supply LTD",
    CountryName = "SA",
    InvoiceType = "1100",
    LocationAddress = "RRRD2929",
    IndustryBusinessCategory = "Supply activities"
};
var csrResult = onboardingService.GenerateCsr(csrDto);

// Example: Get CSID (Compliance certificate and secret)
var csidResult = await onboardingService.GetCSIDAsync(csrResult.Csr, "12345");

// Example: Get PCSID (Production certificate and secret)
var pcsidRequest = new PCSIDRequestDto {
    CsidComplianceRequestId = csidResult.RequestID,
    CsidBinarySecurityToken = csidResult.BinarySecurityToken,
    CsidSecret = csidResult.Secret
};
var pcsidResult = await onboardingService.GetPCSIDAsync(pcsidRequest);
```

### Signing Service

```csharp
using GeeSuthSoft.KSA.ZATCA.Services;
using GeeSuthSoft.KSA.ZATCA.Dto;

var signService = serviceProvider.GetRequiredService<IZatcaSignInvoiceService>();
var invoice = /* construct Invoice object (see below) */;
var signRequest = new SignedInvoiceRequestDto {
    Invoice = invoice,
    BinaryToken = pcsidResult.BinarySecurityToken, // from onboarding
    Secret = csrResult.PrivateKey // from CSR generation
};
var signedInvoice = signService.GetSignedInvoice(signRequest);
```

### Compliance Check

```csharp
using GeeSuthSoft.KSA.ZATCA.Services;
using GeeSuthSoft.KSA.ZATCA.Dto;

var invoiceService = serviceProvider.GetRequiredService<IZatcaInvoiceService>();
var complianceResult = await invoiceService.ComplianceCheck(
    ccsidBinaryToken: csidResult.BinarySecurityToken,
    ccsidSecret: csidResult.Secret,
    requestApi: signedInvoice.RequestApi // from signing
);
```

### Sharing Service

```csharp
using GeeSuthSoft.KSA.ZATCA.Services;
using GeeSuthSoft.KSA.ZATCA.Dto;

var shareService = serviceProvider.GetRequiredService<IZatcaShareService>();
var shareRequest = new ShareInvoiceRequestDto {
    invoiceObject = invoice, // your Invoice object
    tokens = new PCSIDInfoDto {
        BinaryToken = pcsidResult.BinarySecurityToken,
        PCSIDSecret = pcsidResult.Secret,
        privateKey = csrResult.PrivateKey
    },
    IsClearance = true // or false for reporting
};
var shareResponse = await shareService.ShareInvoiceWithZatcaAsync(shareRequest);
```

### Constructing an Invoice

You can use the test helper as a template:

```csharp
using GeeSuthSoft.KSA.ZATCA.Xml.RootPaths;

var invoice = InvoicesTemplateTest.GetSimpleInvoice();
// Or construct manually:
// var invoice = new Invoice { ... };
```

---

### Result of ZATCA app :
![image](https://user-images.githubusercontent.com/10328974/159871332-c874d078-9cc2-47a9-9d10-08831702fefe.png)

---

## Test & Web API Example Projects

### 1. Unit & Integration Tests (`GeeSuthSoft.KSA.ZATCA.XunitTest`)

This project contains comprehensive unit and integration tests for the ZATCA library using [xUnit](https://xunit.net/). It demonstrates how to use the library’s services in real scenarios, including onboarding, signing, compliance, and sharing invoices.

**How to Run the Tests**

- **Via Visual Studio**: Right-click the test project and select "Run Tests".
- **Via CLI**:
  ```bash
  dotnet test Test/GeeSuthSoft.KSA.ZATCA.XunitTest/GeeSuthSoft.KSA.ZATCA.XunitTest.csproj
  ```

**What’s Covered**
- Onboarding: Generating CSR, obtaining CSID and PCSID.
- Invoice Signing: Creating signed invoices.
- Compliance: Checking invoice compliance with ZATCA.
- Sharing: Sending invoices to ZATCA.
- Helpers: Test data and service provider setup.

**Example Test (Compliance)**

```csharp
[Fact]
public async Task ComplianceCheckTest()
{
    var csrDto = CompanyTemplateTest.CrsCompanyInfo();
    var csrResult = _zatcaOnboardingService.GenerateCsr(csrDto);
    var csidResult = await _zatcaOnboardingService.GetCSIDAsync(csrResult.Csr);
    var pcsidRequest = new PCSIDRequestDto {
        CsidComplianceRequestId = csidResult.RequestID,
        CsidBinarySecurityToken = csidResult.BinarySecurityToken,
        CsidSecret = csidResult.Secret
    };
    var pcsidResult = await _zatcaOnboardingService.GetPCSIDAsync(pcsidRequest);
    var invoice = InvoicesTemplateTest.GetSimpleInvoice();
    var signRequest = new SignedInvoiceRequestDto {
        Invoice = invoice,
        Secret = csrResult.PrivateKey,
        BinaryToken = pcsidResult.BinarySecurityToken
    };
    var signed = _zatcaSignInvoiceService.GetSignedInvoice(signRequest);
    var result = await _zatcaInvoiceService.ComplianceCheck(
        csidResult.BinarySecurityToken,
        csidResult.Secret,
        signed.RequestApi
    );
    Assert.NotNull(result);
}
```

**How to Add Your Own Tests**
- Add new test classes or methods in the `Test/GeeSuthSoft.KSA.ZATCA.XunitTest` folder.
- Use the provided helpers in `ConstValue` and `Shared` for test data and DI setup.

---

### 2. Web API Test Project (`GeeSuthSoft.KSA.ZATCA.WebApiTest`)

This is a minimal ASP.NET Core Web API project for **manual and integration testing** of the ZATCA library via HTTP endpoints. It demonstrates how to expose the library’s services as RESTful APIs.

**How to Run**

- **Via Visual Studio**: Set as startup project and run.
- **Via CLI**:
  ```bash
  dotnet run --project Test/GeeSuthSoft.KSA.ZATCA.WebApiTest/GeeSuthSoft.KSA.ZATCA.WebApiTest.csproj
  ```
- **Via Docker**:
  ```bash
  docker build -t zatca-webapi -f Test/GeeSuthSoft.KSA.ZATCA.WebApiTest/Dockerfile .
  docker run -p 8080:8080 zatca-webapi
  ```

**Endpoints**
- Onboarding: `/zatca-onboarding/Generate-Csr`, `/zatca-onboarding/Get-CSID/{crs}`, `/zatca-onboarding/Get-PCSID`
- Invoice Sharing: `/zatca-simple/share-json`, `/zatca-simple/share-xml`
- Swagger UI: Available for easy testing and exploration.

**Example: Sharing an Invoice**

```http
POST /zatca-simple/share-json
Content-Type: application/json

{
  "invoiceObject": { /* ...Invoice fields... */ },
  "tokens": {
    "BinaryToken": "...",
    "PCSIDSecret": "...",
    "privateKey": "..."
  },
  "IsClearance": true
}
```

**Configuration**
- App settings: `appsettings.json` and `appsettings.Development.json`
- Logging and allowed hosts are pre-configured for local development.

**Customization**
- Add or modify endpoints in `ApiEndpoint.cs`.
- Use the provided DI setup in `Program.cs` to inject and test your own services.

---

**Tip:** Use the test project as a reference for writing your own tests, and the Web API project as a template for integrating ZATCA features into your own web applications.

