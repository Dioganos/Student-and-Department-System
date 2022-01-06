using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolum_Ogrenci_Veri_Yapilari_Final_Porjesi
{
    public class Data
    {
        public static BolumList<string> ogrenciler = new BolumList<string>();
    }

    public class BolumList<T>
    {
        int boyut = 0;
        public Node ilk = null, son = null;
        public bool bolumvar(T bolum)
        {
            Node pointer = ilk;
            bool bolvarmi = false;

            for (; pointer != null; pointer = pointer.next)
            {
                if (pointer.bolum.Equals(bolum))
                {
                    bolvarmi = true;
                    break;
                }
            }

            return bolvarmi;
        }

        public bool ogrencivar(T ogrenci)
        {
            Node pointer = ilk;
            bool ogvarmi = false;

            for (; pointer != null; pointer = pointer.next)
            {
                if (pointer.ogrenci.Equals(ogrenci))
                {
                    ogvarmi = true;
                    break;
                }
            }

            return ogvarmi;
        }

        public Node OgrenciBul(T ogrenci)
        {
            Node pointer = ilk;
            bool ogvarmi = false;

            for (; pointer != null; pointer = pointer.next)
                if (pointer.ogrenci.Equals(ogrenci))
                {
                    ogvarmi = true;
                    break;
                }

            if (ogvarmi == true)
            {
                return pointer;
            }
            else return null;
        }

        private void KonumBul(int konum, out Node pointer)
        {
            pointer = ilk;

            for (int i = 0; i < konum; i++)
            {
                pointer = pointer.next;
            }

        }
        
        public void Ekle(T sinif, T numara, T ogrenci, T bolum)
        {
            Node node = new Node(sinif,numara, ogrenci, bolum);
            KonumBul(boyut, out Node pointer);

            if (pointer != ilk)
            {
                son.next = node;
                node.pre = son;
                son = node;
            }

            else
            {
                ilk = son = node;
            }

            boyut++;
        }

        public void Cikar(T ogrenci) 
        {
            Node pointer = ilk;
            int konum = 0;

            for (; pointer != null; pointer = pointer.next)
            {
                if (pointer.ogrenci.Equals(ogrenci))
                {
                    break;
                }

                konum++;
            }

            if (konum is 0)  
            {
                if (ilk == son)
                {
                    ilk = son = null;
                }

                else
                {
                    ilk = pointer.next;
                    ilk.pre = null;
                }
            }

            else if (konum == boyut - 1)
            {
                son = pointer.pre;
                son.next = null;
                pointer.pre = null;
            }

            else  
            {
                pointer.pre.next = pointer.next;
                pointer.next.pre = pointer.pre;
                pointer.pre = null;
                pointer.next = null;
            }

            boyut--;
        }

        public IEnumerable<T> BolumGonder()
        {
            Node pointer = ilk;
            List<T> gecici = new List<T>();

            for (; pointer != null; pointer = pointer.next)
            {
                if (!gecici.Contains(pointer.bolum))
                {
                    gecici.Add(pointer.bolum);
                    yield return pointer.bolum;
                }
            }

        }
        public IEnumerable<T> OgrenciGonder(T bolum)
        {
            for (Node pointer = ilk; pointer != null; pointer = pointer.next)
            {
                if (pointer.bolum.Equals(bolum))
                {
                    yield return pointer.ogrenci;
                }
            }
        }
        public IEnumerable<Node> BolumNodeGonder(T bolum)
        {
            for (Node pointer = ilk; pointer != null; pointer = pointer.next)
            {
                if (pointer.bolum.Equals(bolum))
                {
                    yield return pointer;
                }
            }
        }

        public IEnumerable<string> SendBack()
        {
            for (Node pointer = ilk; pointer != null; pointer = pointer.next)
            {
                yield return $"{pointer.sinif},{pointer.numara},{pointer.ogrenci},{pointer.bolum}";
            }
        }
        public class Node
        {
            
            public T bolum;
            public T ogrenci;
            public T numara;
            public T sinif;
            public Node next = null;
            public Node pre = null;
            public Node(T sinif,T numara, T ogrenci, T bolum)
            {
                this.sinif = sinif; 
                this.numara = numara;
                this.ogrenci = ogrenci;
                this.bolum = bolum; 
            }
        }
    }
}
