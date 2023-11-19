using BanHangThoiTrang.Models;
namespace BanHangThoiTrang.Repository
{
    public interface ILoaiSpRepository
    {
        TLoaiSp Add(TLoaiSp loaiSp);
        TLoaiSp Update(TLoaiSp loaiSp);
        TLoaiSp Delete(TLoaiSp loaiSp);
        TLoaiSp GetLoaiSp(String maloaiSp);
        IEnumerable<TLoaiSp> GetAllLoaiSp();
    }
}
