using SeferTasiADO.EntityProject.BLL.Repository;
using ST.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST.BLL.Repository
{
    public class AdresRepo : RepositoryBase<Adres, int> { }
    public class FirmaRepo : RepositoryBase<Firma, int> { }
    public class FirmaUrun : RepositoryBase<FirmaUrun, int> { }
    public class OdemeTipi : RepositoryBase<OdemeTipi, int> { }
    public class SiparisRepo: RepositoryBase<Siparis, int> { }
    public class SiparisDetayRepo : RepositoryBase<SiparisDetay, int> { }
    public class UrunRepo : RepositoryBase<Urun, int> { }
    public class UrunKategoriRepo : RepositoryBase<UrunKategori, int> { }




}
