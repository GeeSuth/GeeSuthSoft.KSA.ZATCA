# GeeSuthSoft.KSA.ZATCA
this Repo will be have a tools to help programmers to new requirements from ZATCA In Saudi Arabia 


### Qr Code 

#### Use :
Install `Install-Package GeeSuthSoft.KSA.ZATCA -Version 1.0.0`

after add `GeeSuthSoft.KSA.ZATCA.dll` to your project

call it `using GeeSuthSoft.KSA.ZATCA.Qr;`

`var qr= QrCodeGenerate.GetBase64InUrl("salem", "1111111111100555", DateTime.Now, 15, 230, new QrCodeOption()
            {
                Language = GeeSuthSoft.KSA.ZATCA.Enums.Options.Language.Ar,
                CenterImage = (Bitmap)Bitmap.FromFile("C:\\vat.jpg")
            }
            )`
            
 
 the result will be like 
 
 ![image](https://user-images.githubusercontent.com/10328974/140614818-8bf54479-ecb1-45ba-a501-a3aa5620f844.png)


##### Methods:
`GetBase64()` => 'Get QrCode  in Base64 string to use'

`GetBase64InUrl()` => 'Get QrCode in Base64 string can be opened in browser just past result in url '

`GetImage()` => 'Get QrCode In Image object'


