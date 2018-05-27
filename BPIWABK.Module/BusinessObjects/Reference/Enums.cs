﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Utils;

namespace BPIWABK.Module.BusinessObjects.Reference
{
    class Enums
    {

    }

    public enum PenilaianKinerja
    {
        [XafDisplayName("1 - Buruk")]
        Buruk,
        [XafDisplayName("2 - Cukup")]
        Cukup,
        [XafDisplayName("3 - Baik")]
        Baik,
        [XafDisplayName("4 - Sangat Baik")]
        SangatBaik
    }

    public enum Kepangkatan
    {
        Kosong,
        Fungsional,
        Staf,
        EselonIV,
        EselonIII,
        EselonII,
        EselonI,
        Menteri
    }
    public enum Eselon
    {
        [XafDisplayName("(Kosong)")]
        Kosong = 0,
        I = 1,
        II = 2,
        III = 3,
        IV = 4,
        V = 5
    }

    public enum StatusKepegawaian
    {
        Honorer,
        Kontraktual,
        PNS
    }

    public enum StatusPernikahan
    {
        TidakKawin,
        Kawin,
        Cerai
    }

    public enum JenisKelamin
    {
        LakiLaki,
        Perempuan
    }

    public enum GolonganDarah
    {
        A,
        B,
        AB,
        O
    }

    public enum JenjangPendidikan
    {
        Kosong=0,
        SD=1,
        SLTP=2,
        SLTA=3,
        D1=4,
        D2=5,
        D3=6,
        D4=7,
        S1=8,
        S2=9,
        S3=10
    }

    public enum Agama
    {
        Buddha,
        Hindu,
        Islam,
        Katolik,
        KongHuChu,
        Kristen
    }

    public enum NamaBulan
    {
        Januari = 1,
        Februari = 2,
        Maret= 3,
        April = 4,
        Mei = 5,
        Juni=6,
        Juli=7,
        Agustus=8,
        September=9,
        Oktober=10,
        November=11,
        Desember=12
    }

    public enum SatuanWaktu
    {
        Menit,
        Jam,
        Hari,
        Minggu,
        Bulan
    }

    public enum JenisAlur
    {
        Mulai,
        Lanjut,
        YaTidak,
        Selesai
    }
}
