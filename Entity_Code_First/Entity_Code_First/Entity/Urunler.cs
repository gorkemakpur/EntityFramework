using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Code_First.Entity
{
    public class Urunler
    {
        [Key]
        public int UrunID { get; set; }
        public string UrunAd { get; set; }
        public string UrunMarka { get; set; }
        public string UrunKategori { get; set; }
        public int UrunStok { get; set; }
        public string UrunAciklama { get; set; }

        public Kategori Kategori { get; set; }//her bir ürün için 1 kategori olacak yani her ürünün 1 kategorisi olucak
    }
}
