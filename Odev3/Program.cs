using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ödev3
{

    class Program
    {
        static void Main(string[] args)
        {
            KanalConcreteAggregate kca = new KanalConcreteAggregate();
            kca.Ekle(new TVKnali { KanalAdi = "TRT 1" });
            kca.Ekle(new TVKnali { KanalAdi = "Kanal D" });
            kca.Ekle(new TVKnali { KanalAdi = "TRT Spor" });
            kca.Ekle(new TVKnali { KanalAdi = "NTV Spor" });
            kca.Ekle(new TVKnali { KanalAdi = "TV8" });
            IKanalIterator kanalI = kca.getIterator();
            string kanallar = "";
            while (kanalI.bittiMi())
            {
                kanallar += kanalI.GecerliKanal().KanalAdi + Environment.NewLine;
                kanalI.SonrakiKanal();
            }
            Console.WriteLine(kanallar);
            Console.ReadKey();
        }
    }
    //Tvkanal sınıfı
    public class TVKnali
    {
        public string KanalAdi { get; set; }
    }
    //İnterface Kanal İterator
    public interface IKanalIterator
    {
        TVKnali SonrakiKanal();
        TVKnali GecerliKanal();
        bool bittiMi();
    } 
    //Interface Kanal Aggregate
    public interface IKanalAggregate
    {
        IKanalIterator getIterator();
    }
    //Kanal Concreate Aggregate Sınıfı
    public class KanalConcreteAggregate : IKanalAggregate
    {
        private List<TVKnali> _kanalListesi = new List<TVKnali>();
        public int kanalSayisi {
            get { return _kanalListesi.Count; }
        }
        public void Ekle(TVKnali t)
        {
            _kanalListesi.Add(t);
        }
        public TVKnali GetItem(int deger)
        {
            return _kanalListesi[deger];
        }
        public IKanalIterator getIterator()
        {
            return new KanalConcreteIterator(this);
        }
    }
    //Kanal Concrete Iterator 
    public class KanalConcreteIterator : IKanalIterator
    {
        private KanalConcreteAggregate kanallar;
        private int deger = 0;
        public KanalConcreteIterator(KanalConcreteAggregate kanal)
        {
            kanallar = kanal;
        }
        public bool bittiMi()
        {
            return deger < kanallar.kanalSayisi;
        }
        public TVKnali GecerliKanal()
        {
            return kanallar.GetItem(deger);
        }
        public TVKnali SonrakiKanal()
        {
            deger++;
            if (bittiMi())
            {
                return kanallar.GetItem(deger);
            }
            else
                return null;
        }

        //public TVKnali SonrakiKanal()
        //{
        //    throw new NotImplementedException();
        //}

        //public TVKnali GecerliKanal()
        //{
        //    throw new NotImplementedException();
        //}

        //public bool bittimi()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
