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
- Built for .NET 7

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
                options.ZatcaBaseUrl = "https://gw-fatoora.zatca.gov.sa";
                options.Environment = EnvironmentType.NonProduction;
                options.LogRequestAndResponse = true;
            });
```

## Usage

### OnBoarding Service

```csharp
IZatcaOnboardingService
```


### Sharing Service

```csharp
IZatcaShareService
```







### Result of ZATCA app :
![image](https://user-images.githubusercontent.com/10328974/159871332-c874d078-9cc2-47a9-9d10-08831702fefe.png)

