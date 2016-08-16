using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Kramatdjati.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Kramatdjati.Infrastructure
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext() : base("IdentityDb")
        {
        }

        static AppIdentityDbContext()
        {
            Database.SetInitializer<AppIdentityDbContext>(new IdentityDbInit());
        }
        public static AppIdentityDbContext Create()
        {
            return new AppIdentityDbContext();
        }
        public class IdentityDbInit : NullDatabaseInitializer<AppIdentityDbContext>
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Contact>()
                    .HasRequired(m => m.NoRekPiutang )
                    .WithMany(t => t.NoRekPiutangs )
                    .HasForeignKey(m => m.NoRekPiutangID )
                    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contact>()
                        .HasRequired(m => m.NoRekHutang )
                        .WithMany(t => t.NoRekHutangs )
                        .HasForeignKey(m => m.NoRekHutangID )
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<BahanBaku>()
                        .HasRequired(m => m.NoRekCOGS)
                        .WithMany (t=>t.NoRekCOGSs )
                        .HasForeignKey(m=>m.NoRekCOGSID )
                        .WillCascadeOnDelete (false);

            modelBuilder.Entity<BahanBaku>()
                        .HasRequired(m => m.NoRekSale)
                        .WithMany (t=>t.NoRekSales )
                        .HasForeignKey(m=>m.NoRekSaleID )
                        .WillCascadeOnDelete (false);

            modelBuilder.Entity<BahanBaku>()
                        .HasRequired(m => m.NoRekInventory)
                        .WithMany(t => t.NoRekInventories )
                        .HasForeignKey(m => m.NoRekInventoryID )
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblDefault >()
                        .HasRequired(m => m.AccHutangBelumFaktur )
                        .WithMany(t => t.AccHutangBelumFakturs )
                        .HasForeignKey(m => m.AccHutangBelumFakturID )
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblDefault>()
                        .HasRequired(m => m.AccPiutangBelumFaktur )
                        .WithMany(t => t.AccPiutangBelumFakturs )
                        .HasForeignKey(m => m.AccPiutangBelumFakturID )
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblDefault>()
                        .HasRequired(m => m.AccPPNKeluaran )
                        .WithMany(t => t.AccPPNKeluarans )
                        .HasForeignKey(m => m.AccPPNKeluaranID )
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblDefault>()
                        .HasRequired(m => m.AccPPNMasukan )
                        .WithMany(t => t.AccPPNMasukans )
                        .HasForeignKey(m => m.AccPPNMasukanID )
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblDefault>()
                        .HasRequired(m => m.AccSelisihKurs )
                        .WithMany(t => t.AccSelisihKurss )
                        .HasForeignKey(m => m.AccSelisihKursID )
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblDefault>()
                        .HasRequired(m => m.AccUangMukaPembelian )
                        .WithMany(t => t.AccUangMukaPembelians )
                        .HasForeignKey(m => m.AccUangMukaPembelianID )
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblDefault>()
                        .HasRequired(m => m.AccUangMukaPenjualan )
                        .WithMany(t => t.AccUangMukaPenjualans )
                        .HasForeignKey(m => m.AccUangMukaPenjualanID)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblDefault>()
                        .HasRequired(m => m.gudangBeli)
                        .WithMany(t => t.gudangBelis)
                        .HasForeignKey(m => m.GudangBeliID)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblDefault>()
                        .HasRequired(m => m.gudangJual)
                        .WithMany(t => t.gudangJuals)
                        .HasForeignKey(m => m.GudangJualID)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblDefault>()
                        .HasRequired(m => m.gudangProduksi )
                        .WithMany(t => t.gudangProduksis)
                        .HasForeignKey(m => m.GudangProduksiID )
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<PindahGudang>()
                        .HasRequired(m => m.gudangAsal )
                        .WithMany(t => t.gudangAsals )
                        .HasForeignKey(m => m.GudangAsalID )
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<PindahGudang>()
                        .HasRequired(m => m.gudangTujuan )
                        .WithMany(t => t.gudangTujuans )
                        .HasForeignKey(m => m.GudangTujuanID )
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblDefault>()
                        .HasRequired(m => m.compound  )
                        .WithMany(t => t.compounds )
                        .HasForeignKey(m => m.JenisPersediaanCompoundID)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblDefault>()
                       .HasRequired(m => m.rajangan)
                       .WithMany(t => t.rajangans )
                       .HasForeignKey(m => m.JenisPersediaanRajanganID )
                       .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblDefault>()
                     .HasRequired(m => m.bahanBaku )
                     .WithMany(t => t.jenisBahanBakus )
                     .HasForeignKey(m => m.JenisPersediaanBahanBakuID )
                     .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblDefault>()
                     .HasRequired(m => m.barangJadi )
                     .WithMany(t => t.jenisBarangJadis )
                     .HasForeignKey(m => m.JenisPersediaanBarangJadiID )
                     .WillCascadeOnDelete(false);

            //Supaya decimal ini memiliki 5 digit di belakang koma
            modelBuilder.Properties<decimal>().Configure(c => c.HasPrecision(20, 5));

        } 

        public System.Data.Entity.DbSet<Kramatdjati.Models.MasterForm> MasterForms { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.MasterRole> MasterRoles { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.MasterRoleDetail> MasterRoleDetails { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.UserRole> UserRoles { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.AppRoleForms> AppRoleForms { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.tblGLAccountType> tblGLAccountTypes { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.Satuan> Satuan { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.tblGLAccount> tblGLAccounts { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.BahanBaku> BahanBakus { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.JenisKemasan> JenisKemasans { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.Bahan> Bahans { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.Jenis> Jenis { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.JenisDetail> JenisDetails { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.Contact > Contacts { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.KasBank> KasBanks { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.PemesananBarang> PemesananBarangs { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.PemesananBarangRincian> PemesananBarangRincians { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.tblGLAccPeriod > tblGLAccPeriods { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.tblGLOpeningBalance > tblGLOpeningBalances { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.tblGLOpeningBalanceDetail > tblGLOpeningBalanceDetails { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.tblDefault> tblDefaults { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.tblGLBatch> tblGLBatches { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.tblGLBatchDetail> tblGLBatchDetails { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.JenisPersediaan> JenisPersediaans { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.Divisi> Divisis { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.Departemen> Departemen { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.PenerimaanBarang> PenerimaanBarangs { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.PenerimaanBarangRincian> PenerimaanBarangRincians { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.KartuStok> KartuStoks { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.StokOpname> StokOpnames { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.StokOpnameRincian> StokOpnameRincians { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.SalesOrder> SalesOrders { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.SalesOrderRincian> SalesOrderRincians { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.SuratJalan> SuratJalans { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.SuratJalanRincian> SuratJalanRincians { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.SuratJalanLog> SuratJalanLogs { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.Gudang> Gudangs { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.GudangBahanBaku> GudangBahanBakus { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.Resep> Reseps { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.ResepRincian> ResepRincians { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.PindahGudang> PindahGudangs { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.PindahGudangRincian> PindahGudangRincians { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.StatusProduksi> StatusProduksis { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.OrderBahanBaku> OrderBahanBakus { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.JPDeptA> JPDeptAs { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.JPDeptARincian> JPDeptARincians { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.JPDeptASO> JPDeptASOes { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.LPDeptA> LPDeptAs { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.LPDeptARincian> LPDeptARincians { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.JPDeptB> JPDeptBs { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.JPDeptBRincian> JPDeptBRincians { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.LPDeptB> LPDeptBs { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.LPDeptBRincian> LPDeptBRincians { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.JPDeptACompound> JPDeptACompounds { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.BPPB> BPPBs { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.BPPBRincian> BPPBRincians { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.LPDeptARajangan> LPDeptARajangans { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.PenimbanganProduksi> PenimbanganProduksis { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.PasswordSerahTerima> PasswordSerahTerimas { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.Packing> Packings { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.PemakaianPengembalianBarang> PemakaianPengembalianBarangs { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.PemakaianPengembalianBarangRincian> PemakaianPengembalianBarangRincians { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.SuratJalanCetakRincian> SuratJalanCetakRincians { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.SuratJalanCetak> SuratJalanCetaks { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.MataUang> MataUangs { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.HargaJual> HargaJuals { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.HargaJualPerCustomer> HargaJualPerCustomers { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.SAPiutang> SAPiutangs { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.FakturJual> FakturJuals { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.FakturJualRincian> FakturJualRincians { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.SAHutang> SAHutangs { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.PembayaranSO> PembayaranSOes { get; set; }

        public System.Data.Entity.DbSet<Kramatdjati.Models.PembayaranSODetail> PembayaranSODetails { get; set; }

        //public System.Data.Entity.DbSet<Kramatdjati.Models.rptLaporanPemakaian> rptLaporanPemakaians { get; set; }
    }

    public class IdentityDbInit : DropCreateDatabaseIfModelChanges<AppIdentityDbContext>
    {
        protected override void Seed(AppIdentityDbContext context)
        {
            PerformInitialSetup(context);
            base.Seed(context);
        }
        public void PerformInitialSetup(AppIdentityDbContext context)
        {
            AppUserManager userMgr = new AppUserManager(new UserStore<AppUser>(context));
            AppRoleManager roleMgr = new AppRoleManager(new RoleStore<AppRole>(context));
            string roleName = "Administrators";
            string userName = "Admin";
            string password = "MySecret";
            string email = "admin@example.com";
            if (!roleMgr.RoleExists(roleName))
            {
                roleMgr.Create(new AppRole(roleName));
            }
            AppUser user = userMgr.FindByName(userName);
            if (user == null)
            {
                userMgr.Create(new AppUser { UserName = userName, Email = email }, password);
                user = userMgr.FindByName(userName);
            }

            if (!userMgr.IsInRole(user.Id, roleName))
            {
                userMgr.AddToRole(user.Id, roleName);
            }
        }
    }
}