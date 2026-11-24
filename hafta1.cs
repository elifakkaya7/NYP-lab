using System;
using System.Collections.Generic;

enum CihazTuru
{
    Lamba,
    Klima,
    GuvenlikSistemi
}

class Cihaz
{
    public int Kimlik { get; private set; }
    public string Ad { get; private set; }
    public CihazTuru Tur { get; private set; }
    public bool AcikMi { get; private set; }

    public Cihaz(int kimlik, string ad, CihazTuru tur)
    {
        Kimlik = kimlik;
        Ad = ad;
        Tur = tur;
        AcikMi = false;
    }

    public void Ac()
    {
        if (AcikMi)
        {
            Console.WriteLine($"{Ad} zaten açık.");
            return;
        }
        AcikMi = true;
        Console.WriteLine($"{Ad} açıldı.");
    }

    public void Kapat()
    {
        if (!AcikMi)
        {
            Console.WriteLine($"{Ad} zaten kapalı.");
            return;
        }
        AcikMi = false;
        Console.WriteLine($"{Ad} kapatıldı.");
    }

    public void BilgileriGoster()
    {
        string durum = AcikMi ? "Açık" : "Kapalı";
        Console.WriteLine($"Kimlik: {Kimlik,-4} Ad: {Ad,-20} Tür: {Tur,-16} Durum: {durum}");
    }
}

class Program
{
    static void Main()
    {
        List<Cihaz> cihazlar = new List<Cihaz>
        {
            new Cihaz(1, "Salon Lambası", CihazTuru.Lamba),
            new Cihaz(2, "Yatak Odası Lambası", CihazTuru.Lamba),
            new Cihaz(3, "Salon Kliması", CihazTuru.Klima),
            new Cihaz(4, "Güvenlik Sistemi", CihazTuru.GuvenlikSistemi)
        };

        bool devam = true;
        while (devam)
        {
            Console.WriteLine();
            Console.WriteLine("===== AKILLI EV MENÜSÜ =====");
            Console.WriteLine("1 - Cihazları listele");
            Console.WriteLine("2 - Yeni cihaz ekle");
            Console.WriteLine("3 - Cihaz aç");
            Console.WriteLine("4 - Cihaz kapat");
            Console.WriteLine("0 - Çıkış");
            Console.Write("Seçiminiz: ");

            string secim = Console.ReadLine();
            Console.WriteLine();

            switch (secim)
            {
                case "1":
                    CihazlariListele(cihazlar);
                    break;
                case "2":
                    CihazEkle(cihazlar);
                    break;
                case "3":
                    {
                        Cihaz cihaz = CihazSec(cihazlar);
                        if (cihaz != null) cihaz.Ac();
                        break;
                    }
                case "4":
                    {
                        Cihaz cihaz = CihazSec(cihazlar);
                        if (cihaz != null) cihaz.Kapat();
                        break;
                    }
                case "0":
                case null:
                    devam = false;
                    Console.WriteLine("Programdan çıkılıyor...");
                    break;
                default:
                    Console.WriteLine("Hata: Geçersiz seçim! Lütfen menüdeki numaralardan birini girin.");
                    break;
            }
        }
    }

    static void CihazlariListele(List<Cihaz> cihazlar)
    {
        if (cihazlar.Count == 0)
        {
            Console.WriteLine("Sistemde kayıtlı cihaz yok.");
            return;
        }

        Console.WriteLine("--- Cihaz Listesi ---");
        foreach (Cihaz c in cihazlar)
        {
            c.BilgileriGoster();
        }
    }

    static void CihazEkle(List<Cihaz> cihazlar)
    {
        Console.Write("Cihaz kimliği (sayı): ");
        if (!int.TryParse(Console.ReadLine(), out int kimlik))
        {
            Console.WriteLine("Hata: Kimlik bir sayı olmalıdır.");
            return;
        }

        if (KimlikIleBul(cihazlar, kimlik) != null)
        {
            Console.WriteLine($"Hata: {kimlik} kimliğine sahip bir cihaz zaten var.");
            return;
        }

        Console.Write("Cihaz adı: ");
        string ad = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(ad))
        {
            Console.WriteLine("Hata: Cihaz adı boş olamaz.");
            return;
        }

        Console.WriteLine("Cihaz türü:");
        CihazTuru[] turler = (CihazTuru[])Enum.GetValues(typeof(CihazTuru));
        for (int i = 0; i < turler.Length; i++)
        {
            Console.WriteLine($"  {i + 1} - {turler[i]}");
        }
        Console.Write("Tür seçiniz: ");
        if (!int.TryParse(Console.ReadLine(), out int turSecim) || turSecim < 1 || turSecim > turler.Length)
        {
            Console.WriteLine("Hata: Geçersiz tür seçimi.");
            return;
        }

        Cihaz yeni = new Cihaz(kimlik, ad.Trim(), turler[turSecim - 1]);
        cihazlar.Add(yeni);
        Console.WriteLine($"{yeni.Ad} sisteme eklendi (başlangıç durumu: kapalı).");
    }

    static Cihaz CihazSec(List<Cihaz> cihazlar)
    {
        Console.Write("Cihaz kimliği: ");
        if (!int.TryParse(Console.ReadLine(), out int kimlik))
        {
            Console.WriteLine("Hata: Kimlik bir sayı olmalıdır.");
            return null;
        }

        Cihaz cihaz = KimlikIleBul(cihazlar, kimlik);

        if (cihaz == null)
        {
            Console.WriteLine($"Hata: {kimlik} kimliğine sahip bir cihaz bulunamadı.");
        }
        return cihaz;
    }

    static Cihaz KimlikIleBul(List<Cihaz> cihazlar, int kimlik)
    {
        foreach (Cihaz c in cihazlar)
        {
            if (c.Kimlik == kimlik)
                return c;
        }
        return null;
    }
}
