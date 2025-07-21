using GeeSuthSoft.KSA.ZATCA.Enums;
using GeeSuthSoft.KSA.ZATCA.Helper;
using GeeSuthSoft.KSA.ZATCA.Xml.RootPaths;

namespace GeeSuthSoft.KSA.ZATCA.XunitTest.ConstValue
{
    public static class InvoicesTemplateTest
    {
        public static Invoice GetSimpleInvoice()
        {
            return new Invoice
            {
                ProfileID = "reporting:1.0",
                ID = new ID("SME00010"),
                UUID = "8e6000cf-1a98-4174-b3e7-b5d5954bc10d",
                IssueDate = DateTime.Now.ToString("yyyy-MM-dd"),
                IssueTime = "01:41:08",
                InvoiceTypeCode = new InvoiceTypeCode(InvoiceType.TaxInvoice, "0200000"),
                DocumentCurrencyCode = "SAR",
                TaxCurrencyCode = "SAR",
                Note = new Note
                {
                    LanguageID = "ar",
                    Value = "ABC"
                },

                AdditionalDocumentReference = [
                  new()
                  {
                      ID = new ID("ICV"),
                      UUID = "10"
                  },
                    new()
                    {
                        ID = new ID("PIH"),
                        Attachment = new Attachment
                        {
                            EmbeddedDocumentBinaryObject = new EmbeddedDocumentBinaryObject
                            {
                                Value = "NWZlY2ViNjZmZmM4NmYzOGQ5NTI3ODZjNmQ2OTZjNzljMmRiYzIzOWRkNGU5MWI0NjcyOWQ3M2EyN2ZiNTdlOQ==",
                                MimeCode = "text/plain"
                            }
                        }
                    },
                ],

                AccountingSupplierParty = new AccountingSupplierParty
                {
                    Party = new()
                    {
                        PartyIdentification = new PartyIdentification
                        {
                            ID = new ID("CRN", null, "1010010000")
                        },
                        PostalAddress = new PostalAddress
                        {
                            StreetName = "الامير سلطان | Prince Sultan",
                            BuildingNumber = "2322",
                            CitySubdivisionName = "المربع | Al-Murabba",
                            CityName = "الرياض | Riyadh",
                            PostalZone = "23333",
                            Country = new Country("SA")
                        },
                        PartyTaxScheme = new PartyTaxScheme
                        {
                            CompanyID = "399999999900003",
                            TaxScheme = new TaxScheme
                            {
                                ID = new ID("VAT")
                            }
                        },
                        PartyLegalEntity = new PartyLegalEntity("شركة توريد التكنولوجيا بأقصى سرعة المحدودة | Maximum Speed Tech Supply LTD")
                    }
                },

                AccountingCustomerParty = new AccountingCustomerParty
                {
                    Party = new()
                    {
                        PostalAddress = new PostalAddress
                        {
                            StreetName = "صلاح الدين | Salah Al-Din",
                            BuildingNumber = "1111",
                            CitySubdivisionName = "المروج | Al-Murooj",
                            CityName = "الرياض | Riyadh",
                            PostalZone = "12222",
                            Country = new Country("SA")
                        },
                        PartyTaxScheme = new PartyTaxScheme
                        {
                            CompanyID = "399999999800003",
                            TaxScheme = new TaxScheme
                            {
                                ID = new ID("VAT")
                            }
                        },
                        PartyLegalEntity = new PartyLegalEntity("شركة نماذج فاتورة المحدودة | Fatoora Samples LTD")
                    }
                },

                PaymentMeans = new PaymentMeans
                {
                    PaymentMeansCode = "10"
                },

                AllowanceCharge = new AllowanceCharge
                {
                    ChargeIndicator = false,
                    AllowanceChargeReason = "discount",
                    Amount = new Amount("SAR", 0.00),
                    TaxCategory = [
                      new()
                      {
                          ID = new ID("UN/ECE 5305", "6", "S"),
                          Percent = 15,
                          TaxScheme = new TaxScheme
                          {
                              ID = new ID("UN/ECE 5153", "6", "VAT")
                          }
                      },
                        new()
                        {
                            ID = new ID("UN/ECE 5305", "6", "S"),
                            Percent = 15,
                            TaxScheme = new TaxScheme
                            {
                                ID = new ID("UN/ECE 5153", "6", "VAT")
                            }
                        }
                    ]
                },

                TaxTotal = [
                  new()
                  {
                      TaxAmount = new Amount("SAR", 30.15),
                  },
                    new()
                    {
                        TaxAmount = new Amount("SAR", 30.15),
                        TaxSubtotal = [
                        new()
                        {
                            TaxableAmount = new Amount("SAR", 201.00),
                            TaxAmount = new Amount("SAR", 30.15),
                            TaxCategory = new TaxCategory
                            {
                                ID = new ID("UN/ECE 5305", "6", "S"),
                                Percent = 15.00,
                                TaxScheme = new TaxScheme
                                {
                                    ID = new ID("UN/ECE 5153", "6", "VAT")
                                }
                            }
                        }
                      ]
                    }
                ],
                LegalMonetaryTotal = new LegalMonetaryTotal
                {
                    LineExtensionAmount = new Amount("SAR", 201.00),
                    TaxExclusiveAmount = new Amount("SAR", 201.00),
                    TaxInclusiveAmount = new Amount("SAR", 231.15),
                    AllowanceTotalAmount = new Amount("SAR", 0.00),
                    PrepaidAmount = new Amount("SAR", 0.00),
                    PayableAmount = new Amount("SAR", 231.15),
                },

                InvoiceLine = [
                  new()
                  {
                      ID = new ID("1"),
                      InvoicedQuantity = new InvoicedQuantity("PCE", 33.000000),
                      LineExtensionAmount = new Amount("SAR", 99.00),
                      TaxTotal = new TaxTotal
                      {
                          TaxAmount = new Amount("SAR", 14.85),
                          RoundingAmount = new Amount("SAR", 113.85)
                      },
                      Item = new Item
                      {
                          Name = "كتاب",
                          ClassifiedTaxCategory = new ClassifiedTaxCategory
                          {
                              ID = new ID("S"),
                              Percent = 15.00,
                              TaxScheme = new TaxScheme
                              {
                                  ID = new ID("VAT")
                              }
                          }
                      },
                      Price = new Price
                      {
                          PriceAmount = new Amount("SAR", 3.00),
                          AllowanceCharge = new AllowanceCharge
                          {
                              ChargeIndicator = false, // it's not allowed to be true anymore due to Zatca : BR-KSA-EN16931-06 = Charge on price level (BG-29) is not allowed. The value of Indicator can only be 'false'. in (v3.4.2)
                              AllowanceChargeReason = "discount",
                              Amount = new Amount("SAR", 0.00)
                          }
                      }
                  },
                    new()
                    {
                        ID = new ID("2"),
                        InvoicedQuantity = new InvoicedQuantity("PCE", 3.000000),
                        LineExtensionAmount = new Amount("SAR", 102.00),
                        TaxTotal = new TaxTotal
                        {
                            TaxAmount = new Amount("SAR", 15.30),
                            RoundingAmount = new Amount("SAR", 117.30)
                        },
                        Item = new Item
                        {
                            Name = "قلم",
                            ClassifiedTaxCategory = new ClassifiedTaxCategory
                            {
                                ID = new ID("S"),
                                Percent = 15.00,
                                TaxScheme = new TaxScheme
                                {
                                    ID = new ID("VAT")
                                }
                            }
                        },
                        Price = new Price
                        {
                            PriceAmount = new Amount("SAR", 34.00),
                            AllowanceCharge = new AllowanceCharge
                            {
                                ChargeIndicator = false, // it's not allowed to be true anymore due to Zatca : BR-KSA-EN16931-06 = Charge on price level (BG-29) is not allowed. The value of Indicator can only be 'false'. in (v3.4.2)
                                AllowanceChargeReason = "discount",
                                Amount = new Amount("SAR", 0.00)
                            }
                        }
                    }
                ]
            };
        }
    }
}