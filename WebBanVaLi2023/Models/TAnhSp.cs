using System;
using System.Collections.Generic;

namespace WebBanVaLi2023.Models;

public partial class TAnhSp
{
    public string MaSp { get; set; } = null!;

    public string TenFileAnh { get; set; } = null!;

    public short? ViTri { get; set; }

    public virtual TDanhMucSp MaSpNavigation { get; set; } = null!;
}
