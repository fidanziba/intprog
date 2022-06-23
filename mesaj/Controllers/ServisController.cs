using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using mesaj.Models;
using mesaj.ViewModel;



namespace mesaj.Controllers
{
    public class ServisController : ApiController
    {
        Database1Entities db = new Database1Entities();
        SonucModel sonuc = new SonucModel();

        #region Kullanici
        [HttpGet]
        [Route("api/kullanıcıliste")]
        public List<KullaniciModel> KullaniciListe()
        {
            List<KullaniciModel> liste = db.Kullanici.Select(x => new KullaniciModel()
            {

                KullaniciAdi = x.KullaniciAdi,
                Numara = x.Numara,
                Sifre = x.Sifre,
            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/kullanicibyid/{kullaniciId}")]
        public KullaniciModel KullaniciById(int KullaniciId)
        {
            KullaniciModel kayit = db.Kullanici.Where(s => s.KullaniciId == KullaniciId).Select(x => new KullaniciModel()
            {
                KullaniciAdi = x.KullaniciAdi,
                Numara = x.Numara,
                Sifre = x.Sifre,
            }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/kullaniciekle")]
        public SonucModel KullaniciEkle(KullaniciModel model)
        {
            if (db.Kullanici.Count(s => s.KullaniciAdi == model.KullaniciAdi || s.Numara == model.Numara) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Bu kullanıcı kayıtlıdır.";
                return sonuc;
            }

            Kullanici yeni = new Kullanici();
            yeni.KullaniciAdi = model.KullaniciAdi;
            yeni.Numara = model.Numara;
            yeni.Sifre = model.Sifre;

            db.Kullanici.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kullanıcı kaydedildi";

            return sonuc;
        }
        [HttpPut]
        [Route("api/kullaniciduzenle")]
        public SonucModel KullaniciDuzenle(KullaniciModel model)
        {
            Kullanici kayit = db.Kullanici.Where(s => s.KullaniciId == model.KullaniciId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kullanıcı Bulunamadı!";
                return sonuc;
            }

            kayit.KullaniciAdi = model.KullaniciAdi;
            kayit.Numara = model.Numara;
            kayit.Sifre = model.Sifre;

            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kullanıcı Düzenlendi";

            return sonuc;
        }

        [HttpDelete]
        [Route("api/kullanicisil/{kullaniciId}")]
        public SonucModel KullaniciSil(int KullaniciId)
        {
            Kullanici kayit = db.Kullanici.Where(s => s.KullaniciId == KullaniciId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kullanıcı Bulunamadı!";
                return sonuc;
            }


            db.Kullanici.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kullanici Silindi";

            return sonuc;
        }
        #endregion

        #region Resim

        [HttpGet]
        [Route("api/resimliste")]
        public List<ResimModel> ResimListe()
        {
            List<ResimModel> liste = db.Resim.Select(x => new ResimModel()
            {
                ResimId = x.ResimId,
                ResimAdi = x.ResimAdi,
                KullaniciId= x.KullaniciId,
            }).ToList();
            return liste;
        }

        [HttpPost]
        [Route("api/resimekle")]
        public SonucModel ResimEkle(ResimModel model)
        {
            Resim yeni = new Resim();

            yeni.ResimAdi = model.ResimAdi;
            yeni.KullaniciId = model.KullaniciId;
            db.Resim.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Resim Eklendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/resimduzenle")]

        public SonucModel ResinDuzenle(ResimModel model)
        {
            Resim kayit = db.Resim.Where(s => s.ResimId == model.ResimId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayit bulunamadi!";
                return sonuc;
            }

            kayit.ResimAdi = model.ResimAdi;
            kayit.KullaniciId = model.KullaniciId;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Resim Düzenlendi";
            return sonuc;

        }
        [HttpDelete]
        [Route("api/resimsil/{ResimId}")]

        public SonucModel ResimSil(int ResimId)
        {
            Resim kayit = db.Resim.Where(s => s.ResimId == ResimId).SingleOrDefault(
           );
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadal";
                return sonuc;
            }

            db.Resim.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Resim Silindi";
            return sonuc;
        }
        [HttpGet]
        [Route("api/ResimById/{HaberId}")]

        public List<ResimModel> resimByHaberId(int KullaniciId)
        {
            List<ResimModel> kayit = db.Resim.Where(s => s.KullaniciId == KullaniciId).Select(x => new ResimModel()
            {
                ResimId = x.ResimId,
                ResimAdi = x.ResimAdi,
                KullaniciId = x.KullaniciId

            }).ToList();
            return kayit;




        }
        #endregion

        #region Grup

        [HttpGet]
        [Route("api/grupliste")]
        public List<GrupModel> GrupListe()
        {
            List<GrupModel> liste = db.Grup.Select(x => new GrupModel()
            {
                GrupId = x.GrupId,

                GrupAdi = x.GrupAdi,

            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/grupbyid/{GrupId}")]
        public GrupModel GrupById(int GrupId)
        {
            GrupModel kayit = db.Grup.Where(s => s.GrupId == GrupId).Select(x => new GrupModel()
            {
                GrupId = x.GrupId,

                GrupAdi = x.GrupAdi,
            }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/grupekle")]
        public SonucModel GrupEkle(GrupModel model)
        {
            if (db.Grup.Count(c => c.GrupAdi == model.GrupAdi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Aynı Başlığa Sahip Grup Mevcuttur";
                return sonuc;
            }

            Grup yeni = new Grup();
            yeni.GrupAdi = model.GrupAdi;
            yeni.Kurucu = model.Kurucu;


            db.Grup.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Grup Eklendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/grupduzenle")]
        public SonucModel GrupDuzenle(GrupModel model)
        {
            Grup kayit = db.Grup.Where(s => s.GrupAdi == model.GrupAdi).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Grup Bulunmadı!";
                return sonuc;
            }

            kayit.GrupAdi = model.GrupAdi;
            kayit.Kurucu = model.Kurucu;


            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Grup Düzenlendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/Grupsil/{grupId}")]
        public SonucModel GrupSil(int GrupId)
        {
            Grup kayit = db.Grup.Where(s => s.GrupId == GrupId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Grup Bulunmadı!";
                return sonuc;
            }



            db.Grup.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Mesaj Silndi";
            return sonuc;
        }



        #endregion

        #region Mesaj

        [HttpGet]
        [Route("api/mesajliste")]
        public List<MesajModel> MesajListe()
        {
            List<MesajModel> liste = db.Mesaj.Select(x => new MesajModel()
            {
                MesajId = x.MesajId,

                Mesaj1 = x.Mesaj1,
                Gonderen = x.Gonderen,

            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/Mesajbyid/{mesajId}")]
        public MesajModel MesajById(int MesajId)
        {
            MesajModel kayit = db.Mesaj.Where(s => s.MesajId == MesajId).Select(x => new MesajModel()
            {
                MesajId = x.MesajId,

                Mesaj1 = x.Mesaj1,
                Gonderen = x.Gonderen,
            }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/mesajekle")]
        public SonucModel MesajEkle(MesajModel model)
        {

            Mesaj yeni = new Mesaj();
            yeni.MesajId = model.MesajId;

            yeni.Mesaj1 = model.Mesaj1;
            yeni.Gonderen = model.Gonderen;

            db.Mesaj.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Mesaj Eklendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/mesajduzenle")]
        public SonucModel MesajDuzenle(MesajModel model)
        {
            Mesaj kayit = db.Mesaj.Where(s => s.MesajId == model.MesajId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Mesaj Bulunmadı!";
                return sonuc;
            }


            kayit.MesajId = model.MesajId;

            kayit.Mesaj1 = model.Mesaj1;
            kayit.Gonderen = model.Gonderen;

            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Mesaj Düzenlendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/mesajsil/{mesajId}")]
        public SonucModel MesajSil(int MesajId)
        {
            Mesaj kayit = db.Mesaj.Where(s => s.MesajId == MesajId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Mesaj Bulunmadı!";
                return sonuc;
            }



            db.Mesaj.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Mesaj Silndi";
            return sonuc;
        }



        #endregion


    }
}
