using GeeSuthSoft.KSA.ZATCA.Xml.RootPaths;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GeeSuthSoft.KSA.ZATCA.Helper
{
    internal static class QrCodeGenerator
    {
        internal static string GenerateQRCodeContent(Invoice invoiceObject, SignedUBL signedUBL)
        {
            try
            {
                SortedDictionary<int, byte[]> invoiceDetails = GetInvoiceDetails(invoiceObject, signedUBL);
                var stringCertificate = signedUBL.CertificateContent;

                TryGetPublicKeySByteArray(stringCertificate, out byte[] publicKey, out byte[] certificateSignature);
                invoiceDetails.Add(8, publicKey);

                if (invoiceObject.InvoiceTypeCode.Name.StartsWith("02"))
                {
                    invoiceDetails.Add(9, certificateSignature);
                };

                return GenerateQRCodeFromValues(invoiceDetails);
            }
            catch (Exception exception)
            {
                throw new Exception("Error generating EInvoice QR Code", exception);
            }
        }

        private static string GenerateQRCodeFromValues(SortedDictionary<int, byte[]> invoiceDetails)
        {
            List<byte> data = [];
            foreach (var item in invoiceDetails)
            {
                data.AddRange(WriteTlv((uint)item.Key, item.Value));
            }
            return Convert.ToBase64String(data.ToArray());
        }

        private static SortedDictionary<int, byte[]> GetInvoiceDetails(Invoice invoiceObject, SignedUBL signedUBL)
        {
            var details = new SortedDictionary<int, byte[]>
        {
            { 1, Encoding.UTF8.GetBytes(invoiceObject.AccountingSupplierParty.Party.PartyLegalEntity.RegistrationName)},
            { 2, Encoding.UTF8.GetBytes(invoiceObject.AccountingSupplierParty.Party.PartyTaxScheme.CompanyID)},
            { 3, Encoding.UTF8.GetBytes($"{invoiceObject.IssueDate}T{invoiceObject.IssueTime}")},
            { 4, Encoding.UTF8.GetBytes(invoiceObject.LegalMonetaryTotal.PayableAmount.Value.ToString())},
            { 5, Encoding.UTF8.GetBytes(invoiceObject.TaxTotal[0].TaxAmount.Value.ToString())},
            { 6, Encoding.UTF8.GetBytes(signedUBL.InvoiceHash) },
            { 7, Encoding.UTF8.GetBytes(signedUBL.SignatureValue)}
        };

            return details;
        }
        private static void WriteLength(MemoryStream stream, int? length)
        {
            if (!length.HasValue)
            {
                stream.WriteByte(0x80);
            }
            else
            {
                int? nullable = length;
                int num2 = 0;
                if (!(nullable.GetValueOrDefault() < num2 & nullable.HasValue))
                {
                    nullable = length;
                    long? nullable2 = nullable.HasValue ? new long?(nullable.GetValueOrDefault()) : null;
                    long num3 = 0xffff_ffff;
                    if (!(nullable2.GetValueOrDefault() > num3 & nullable2.HasValue))
                    {
                        nullable = length;
                        num2 = 0x7f;
                        if (nullable.GetValueOrDefault() <= num2 & nullable.HasValue)
                        {
                            stream.WriteByte((byte)length.Value);
                        }
                        else
                        {
                            byte num;
                            nullable = length;
                            num2 = 0xff;
                            if (nullable.GetValueOrDefault() <= num2 & nullable.HasValue)
                            {
                                num = 1;
                            }
                            else
                            {
                                nullable = length;
                                num2 = 0xffff;
                                if (nullable.GetValueOrDefault() <= num2 & nullable.HasValue)
                                {
                                    num = 2;
                                }
                                else
                                {
                                    nullable = length;
                                    num2 = 0xff_ffff;
                                    if (nullable.GetValueOrDefault() <= num2 & nullable.HasValue)
                                    {
                                        num = 3;
                                    }
                                    else
                                    {
                                        nullable = length;
                                        nullable2 = nullable.HasValue ? new long?(nullable.GetValueOrDefault()) : null;
                                        num3 = 0xffff_ffff;
                                        if (!(nullable2.GetValueOrDefault() <= num3 & nullable2.HasValue))
                                        {
                                            throw new Exception($"[Error] Length value too big: {length}");
                                        }
                                        num = 4;
                                    }
                                }
                            }
                            stream.WriteByte((byte)(num | 0x80));
                            for (int i = num - 1; i >= 0; i--)
                            {
                                nullable = length;
                                num2 = 8 * i;
                                int? nullable4 = nullable.HasValue ? new int?(nullable.GetValueOrDefault() >> (num2 & 0x1f)) : null;
                                byte num5 = (byte)nullable4.Value;
                                stream.WriteByte(num5);
                            }
                        }
                        return;
                    }
                }
                throw new Exception($"[Error] Invalid length value: {length}");
            }
        }



        private static void WriteTag(MemoryStream stream, uint tag)
        {
            bool flag = true;
            for (int i = 3; i >= 0; i--)
            {
                byte num2 = (byte)(tag >> 8 * i);
                if (!(num2 == 0 & flag) || i <= 0)
                {
                    if (flag)
                    {
                        if (i != 0)
                        {
                            if ((num2 & 0x1f) != 0x1f)
                            {
                                throw new Exception("[Error] Invalid tag value: first octet indicates no subsequent octets, but subsequent octets found");
                            }
                        }
                        else if ((num2 & 0x1f) == 0x1f)
                        {
                            throw new Exception("[Error] Invalid tag value: first octet indicates subsequent octets, but no subsequent octets found");
                        }
                    }
                    else if (i == 0)
                    {
                        if ((num2 & 0x80) == 0x80)
                        {
                            throw new Exception("[Error] Invalid tag value: last octet indicates subsequent octets");
                        }
                    }
                    else if ((num2 & 0x80) != 0x80)
                    {
                        throw new Exception("[Error] Invalid tag value: non-last octet indicates no subsequent octets");
                    }
                    stream.WriteByte(num2);
                    flag = false;
                }
            }
        }

        private static byte[] WriteTlv(uint tag, byte[] value)
        {
            if (value == null)
            {
                throw new Exception("[Error] Please provide a value!");
            }
            using MemoryStream stream = new();
            WriteTag(stream, tag);
            int num = value != null ? value.Length : 0;
            WriteLength(stream, new int?(num));
            stream.Write(value, 0, num);
            return stream.ToArray();
        }

        private static void TryGetPublicKeySByteArray(string certificate, out byte[] publicKey, out byte[] certificateSignature)
        {
            try
            {
                Org.BouncyCastle.X509.X509Certificate certificate2 = DotNetUtilities.FromX509Certificate(new X509Certificate2(Convert.FromBase64String(certificate)));
                publicKey = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(certificate2.GetPublicKey()).GetEncoded();
                certificateSignature = certificate2.GetSignature();
            }
            catch
            {
                throw new Exception("[Error] Invalid Certificate");
            }

        }
    }
}
