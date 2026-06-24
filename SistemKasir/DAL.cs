using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SistemKasir
{
    public class DAL
    {
        SqlConnection conn = Koneksi.GetConnection();
        SqlDataAdapter da;
        DataTable dtBarang;

        // =====================
        // BARANG
        // =====================
        public DataTable GetBarang()
        {
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlCommand cmd = new SqlCommand(
                "SELECT KodeBarang, NamaBarang, HargaBeli, HargaJual, Stok, Satuan, Foto FROM Barang ORDER BY ID", conn);
            da = new SqlDataAdapter(cmd);
            dtBarang = new DataTable();
            da.Fill(dtBarang);
            conn.Close();
            return dtBarang;
        }

        public int CountBarang()
        {
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Barang", conn);
            int jumlah = (int)cmd.ExecuteScalar();
            conn.Close();
            return jumlah;
        }

        public void TambahBarang(string kode, string nama, decimal beli,
                          decimal jual, int stok, string satuan, byte[] foto)
        {
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlCommand cmd = new SqlCommand("sp_tambah_barang", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@kode", kode);
            cmd.Parameters.AddWithValue("@nama", nama);
            cmd.Parameters.AddWithValue("@beli", beli);
            cmd.Parameters.AddWithValue("@jual", jual);
            cmd.Parameters.AddWithValue("@stok", stok);
            cmd.Parameters.AddWithValue("@satuan", satuan);
            SqlParameter fotoParam = new SqlParameter("@foto", SqlDbType.VarBinary);
            fotoParam.Value = (foto != null) ? (object)foto : DBNull.Value;
            cmd.Parameters.Add(fotoParam);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void UpdateBarang(string kode, string nama, decimal beli,
                                  decimal jual, int stok, string satuan, byte[] foto)
        {
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlCommand cmd = new SqlCommand("sp_update_barang", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@kode", kode);
            cmd.Parameters.AddWithValue("@nama", nama);
            cmd.Parameters.AddWithValue("@beli", beli);
            cmd.Parameters.AddWithValue("@jual", jual);
            cmd.Parameters.AddWithValue("@stok", stok);
            cmd.Parameters.AddWithValue("@satuan", satuan);
            cmd.Parameters.AddWithValue("@foto", (object)foto ?? DBNull.Value);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void HapusBarang(string kode)
        {
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlCommand cmd = new SqlCommand("sp_hapus_barang", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@kode", kode);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public DataTable SearchBarang(string keyword)
        {
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlCommand cmd = new SqlCommand("sp_search_barang", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@keyword", keyword);
            da = new SqlDataAdapter(cmd);
            dtBarang = new DataTable();
            da.Fill(dtBarang);
            conn.Close();
            return dtBarang;
        }
        public DataTable GetBarangByKode(string kode)
        {
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlCommand cmd = new SqlCommand("sp_search_barang_kode", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@kode", kode);
            da = new SqlDataAdapter(cmd);
            dtBarang = new DataTable();
            da.Fill(dtBarang);
            conn.Close();
            return dtBarang;
        }

        // =====================
        // CHART / DASHBOARD
        // =====================
        public DataTable GetAllDataChart()
        {
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlCommand cmd = new SqlCommand("sp_DashBoard", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt;
        }

        public DataTable GetDataChartByTahun(DateTime thMasuk)
        {
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlCommand cmd = new SqlCommand("sp_DashBoardByTahun", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@inTglMsuk", thMasuk.Year.ToString());
            da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt;
        }

        public DataTable GetLog(DateTime? tanggal = null)
        {
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlCommand cmd = new SqlCommand("sp_GetLog", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@tanggal",
                tanggal.HasValue ? (object)tanggal.Value.Date : DBNull.Value);
            da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt;
        }

        // =====================
        // REPORT
        // =====================
        public List<DataBarang> GetReportBarang()
        {
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlCommand cmd = new SqlCommand("sp_ReportBarang", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();
            List<DataBarang> list = new List<DataBarang>();
            while (reader.Read())
            {
                list.Add(new DataBarang
                {
                    KodeBarang = reader["KodeBarang"].ToString(),
                    NamaBarang = reader["NamaBarang"].ToString(),
                    HargaBeli = Convert.ToDecimal(reader["HargaBeli"]),
                    HargaJual = Convert.ToDecimal(reader["HargaJual"]),
                    Stok = Convert.ToInt32(reader["Stok"]),
                    Satuan = reader["Satuan"].ToString()
                });
            }
            reader.Close();
            conn.Close();
            return list;
        }
        public List<DataTransaksi> GetReportTransaksi(string tanggal)
        {
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlCommand cmd = new SqlCommand("sp_ReportTransaksi", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@inTanggal", tanggal);
            SqlDataReader reader = cmd.ExecuteReader();
            List<DataTransaksi> list = new List<DataTransaksi>();
            while (reader.Read())
            {
                list.Add(new DataTransaksi
                {
                    NoTransaksi = Convert.ToInt32(reader["NoTransaksi"]),
                    Tanggal = reader["Tanggal"].ToString(),
                    NamaBarang = reader["NamaBarang"].ToString(),
                    Qty = Convert.ToInt32(reader["Qty"]),
                    HargaJual = Convert.ToDecimal(reader["HargaJual"]),
                    Subtotal = Convert.ToDecimal(reader["Subtotal"]),
                    TotalPenjualan = Convert.ToDecimal(reader["TotalPenjualan"]),
                    Bayar = Convert.ToDecimal(reader["Bayar"]),
                    Kembali = Convert.ToDecimal(reader["Kembali"])
                });
            }
            reader.Close();
            conn.Close();
            return list;
        }
    }
}
