# GeeSuthSoft.KSA.ZATCA
This Repository will be have a tools to help programmers to new requirements from ZATCA In Saudi Arabia 
support .NET 7



### Qr Code 

#### Use :

~~Install `Install-Package GeeSuthSoft.KSA.ZATCA -Version 1.0.0`~~

after add `GeeSuthSoft.KSA.ZATCA.dll` to your project

call it `using GeeSuthSoft.KSA.ZATCA.Qr;`

the result of checking Qrcode(Generated from this Library) by [ZATCA](https://zatca.gov.sa/en/E-Invoicing/SystemsDevelopers/ComplianceEnablementToolbox/Pages/DownloadSDK.aspx)

![image](https://user-images.githubusercontent.com/10328974/143316390-370ed783-7ec1-4d0e-b4ae-fbb1abb0fcbf.png)

As like what you see, we can say this working very well


##### Methods:
`GetBase64()` => 'Get QrCode  in Base64 string to use'

`GetBase64InUrl()` => 'Get QrCode in Base64 string can be opened in browser just past result in url '

`GetImage()` => 'Get QrCode In Image object'


### Result of ZATCA app :
![image](https://user-images.githubusercontent.com/10328974/159871332-c874d078-9cc2-47a9-9d10-08831702fefe.png)

