using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            //ilk olarak Ürünlerin bağlı olacağı Müzayede oluşturulur
            IMüzayede satis = new Münadi();
            //Uçak nesneleri oluşturulur.
            Ürünler vazo1 = new Vazo { ürünno = "Vazo1" };
            Ürünler vazo2 = new Vazo { ürünno = "Vazo2" };
            Ürünler vazo3 = new Vazo { ürünno = "Vazo3" };
            //uçak nesneleri kule nesnesine kayıt ettirilir.
            //Uçak nesnesindeki IliskiliKule nesnesi kule nesnesindeki UcakKayit metodunda yapılır.
            satis.ÜrünKayit(vazo1);
            satis.ÜrünKayit(vazo2);
            satis.ÜrünKayit(vazo3);
            
            //sadece ilk izin isteyen uçağa iniş izni true verilir.
            vazo1.Satinal();
            vazo2.Satinal();
            vazo3.alisizni = false;
            //vazo1 nesnesinin satis izni iptal edildiği için vazo2 nesnesine iniş izni verilir.
            vazo2.Satinal();
            Console.ReadKey();
        }
    }

    //mediator yapısı
    interface IMüzayede
    {
        //münadinin gerçekleştirmesi gereken operasyonlar
        void ÜrünKayit(Ürünler _ürün);
        void AlisizniCevap(string ürünno);
    }
    //Colleague yapısı
    abstract class Ürünler
    {
        //Münadinin kim ile irtibata geçmesi gerektiğini tutması gerekir.
        public IMüzayede ilişkilikisi { get; set; }
        public string ürünno { get; set; }
        public bool alisizni { get; set; }
        public void Satinal()
        {
            //uçağın bağlı olduğu kuleden iniş izni istiyor
            ilişkilikisi.AlisizniCevap(ürünno);
        }
        public virtual void Setalisizni(bool Izin)
        {
            //Münadi alış izni isteyen kişiye bu metot ile cevap verir.
            alisizni = Izin;
            if (alisizni)
                Console.WriteLine("Alış izni verildi.");
            else
                Console.WriteLine("Alış izni red edildi.");
        }
    }
    //ConcreteMediator yapısı
    class Münadi : IMüzayede
    {
        //Müzayede kendisine bağlı olan ürünlerin bilgisini tutmak zorunda ki isteklere buna göre cevap verebilsin.
        private List<Ürünler> _UrunListe = new List<Ürünler>();
        public void ÜrünKayit(Ürünler ürün)
        {
            _UrunListe.Add(ürün);
            //Listeye eklenen Ürünler nesnesine yöneticisinin bu sınıf olduğunu bildiriyoruz.
            ürün.ilişkilikisi = this;
        }
        public void AlisizniCevap(string Ürünno)
        {
            bool izin = true;
            // eğer başka bir uçağa iniş izni verilmedi ise ilk izin isteyen uçağa izin ver
            if (_UrunListe.Where(u => u.alisizni == true).Count() > 0)
                izin = false;
            //uçağın cevap alması için barındırdığı metoda cevap verilir.
            _UrunListe.Where(u => u.ürünno == Ürünno).Single().Setalisizni(izin);
        }
    }
    //ConcreteColleague1
    class Vazo : Ürünler
    {
        //InisIzniIste metotu AbsUcak abstract sınıfından gelir.
        //Kule cevabı mu metot ile verir.
        public override void Setalisizni(bool Izin)
        {
            Console.WriteLine("Vazo Ürün No:{0} Aliş izni istiyor...", ürünno);
            base.Setalisizni(Izin);
        }
    }
    //ConcreteColleague2
    class Tablo : Ürünler
    {
        //ConcreteColleague1 yapısı için geçerli olanlar bu yapı için de geçerlidir.
        public override void Setalisizni (bool Izin)
        {
            Console.WriteLine("Tablo Ürün No:{0} Aliş izni istiyor...", ürünno);
            base.Setalisizni(Izin);
        }
    }
}
