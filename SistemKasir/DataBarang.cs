using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemKasir
{
    public class DataBarang
    {
        public string KodeBarang { get; set; }
        public string NamaBarang { get; set; }
        public decimal HargaBeli { get; set; }
        public decimal HargaJual { get; set; }
        public int Stok { get; set; }
        public string Satuan { get; set; }
    }

    public class DataTransaksi
    {
        public int NoTransaksi { get; set; }
        public string Tanggal { get; set; }
        public string NamaBarang { get; set; }
        public int Qty { get; set; }
        public decimal HargaJual { get; set; }
        public decimal Subtotal { get; set; }
        public decimal TotalPenjualan { get; set; }
        public decimal Bayar { get; set; }
        public decimal Kembali { get; set; }
    }
}