# 🛒 Shopping - Website Thương Mại Điện Tử

[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-10.0-blue)](https://dotnet.microsoft.com/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-2019-red)](https://www.microsoft.com/sql-server)
[![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3.3-purple)](https://getbootstrap.com/)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

Website thương mại điện tử hiện đại được xây dựng bằng ASP.NET Core MVC với giao diện gradient design và tích hợp thanh toán trực tuyến Momo.

## 📋 Mục Lục

- [Tính Năng](#-tính-năng)
- [Công Nghệ](#-công-nghệ-sử-dụng)
- [Cài Đặt](#-cài-đặt)
- [Cấu Hình](#-cấu-hình)
- [Sử Dụng](#-sử-dụng)
- [Screenshots](#-screenshots)
- [Đóng Góp](#-đóng-góp)

## ✨ Tính Năng

### 👥 Người Dùng

- 🔐 Đăng ký/Đăng nhập với ASP.NET Identity
- 🛍️ Duyệt sản phẩm theo danh mục và thương hiệu
- 🔍 Tìm kiếm và lọc sản phẩm nâng cao
- 🛒 Giỏ hàng với cập nhật real-time
- 💳 Thanh toán trực tuyến qua Momo
- 🎟️ Áp dụng mã giảm giá
- 📦 Theo dõi đơn hàng
- ⭐ Đánh giá và nhận xét sản phẩm
- 📧 Trang liên hệ với Google Maps

### 👨‍💼 Quản Trị Viên

- 📊 Dashboard thống kê tổng quan
- 📦 Quản lý sản phẩm (CRUD)
- 🏷️ Quản lý danh mục và thương hiệu
- 📋 Quản lý đơn hàng và trạng thái
- 👤 Quản lý người dùng và phân quyền
- 🎨 Quản lý slider trang chủ
- 🎫 Quản lý mã giảm giá
- 📞 Quản lý thông tin liên hệ

### 🎨 Giao Diện

- Modern gradient design system
- Responsive cho mọi thiết bị
- Smooth animations và transitions
- Interactive hover effects
- Live search với empty states
- Active state highlighting

## 🚀 Công Nghệ Sử Dụng

### Backend

- **Framework:** ASP.NET Core MVC (.NET 10.0)
- **ORM:** Entity Framework Core
- **Database:** SQL Server
- **Authentication:** ASP.NET Identity
- **Payment Gateway:** Momo API

### Frontend

- **Template Engine:** Razor Pages
- **CSS Framework:** Bootstrap 5.3.3
- **JavaScript:** jQuery 3.7.1
- **Icons:** Font Awesome 6.5.1
- **Notifications:** SweetAlert2
- **Animations:** Custom CSS Keyframes

### Database

- **143+ Products** với hình ảnh từ Unsplash CDN
- **16 Categories** đa dạng
- **20 Brands** nổi tiếng
- **Entity Relations:** Products, Orders, Users, Reviews

## 📦 Cài Đặt

### Yêu Cầu

- .NET 10.0 SDK hoặc cao hơn
- SQL Server 2019 trở lên
- Visual Studio 2022 hoặc VS Code
- Git

### Các Bước Cài Đặt

1. **Clone repository**

```bash
git clone https://github.com/Hungztv/Shopping.git
cd Shopping
```

2. **Restore dependencies**

```bash
dotnet restore
```

3. **Cập nhật database connection string**

Mở `appsettings.json` và cập nhật connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=ShoppingCart;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

4. **Chạy migrations**

```bash
dotnet ef database update
```

5. **Seed dữ liệu mẫu (Optional)**

Chạy các file SQL trong thư mục `Shopping/`:

- `SeedData.sql` - 36 sản phẩm ban đầu
- `UpdateProductImages.sql` - Cập nhật hình ảnh
- `SeedData100Products.sql` - Thêm 105 sản phẩm

6. **Chạy ứng dụng**

```bash
dotnet run
```

Truy cập: `https://localhost:5032`

## ⚙️ Cấu Hình

### Momo Payment Gateway

Cập nhật thông tin Momo trong `appsettings.json`:

```json
{
  "Momo": {
    "PartnerCode": "YOUR_PARTNER_CODE",
    "AccessKey": "YOUR_ACCESS_KEY",
    "SecretKey": "YOUR_SECRET_KEY",
    "ReturnUrl": "https://localhost:5032/Payment/PaymentCallback",
    "NotifyUrl": "https://localhost:5032/Payment/PaymentCallback"
  }
}
```

### Email Configuration (Optional)

```json
{
  "EmailSettings": {
    "SmtpServer": "smtp.gmail.com",
    "SmtpPort": 587,
    "SenderEmail": "your-email@gmail.com",
    "SenderPassword": "your-password"
  }
}
```

## 💻 Sử Dụng

### Tài Khoản Mặc Định

**Admin:**

- Email: `admin@shopping.com`
- Password: `Admin@123`

**User:**

- Email: `user@shopping.com`
- Password: `User@123`

### Endpoints Chính

- `/` - Trang chủ
- `/Product` - Danh sách sản phẩm
- `/Category/{slug}` - Sản phẩm theo danh mục
- `/Brand/{slug}` - Sản phẩm theo thương hiệu
- `/Cart` - Giỏ hàng
- `/Checkout` - Thanh toán
- `/Contact` - Liên hệ
- `/Admin` - Trang quản trị

## 📸 Screenshots

### Trang Chủ

![Home Page](screenshots/home.png)

### Danh Mục Sản Phẩm

![Category Page](screenshots/category.png)

### Giỏ Hàng

![Cart Page](screenshots/cart.png)

### Admin Dashboard

![Admin Dashboard](screenshots/admin.png)

## 🗂️ Cấu Trúc Thư Mục

```
Shopping/
├── Areas/
│   └── Admin/
│       ├── Controllers/
│       └── Views/
├── Controllers/
│   ├── HomeController.cs
│   ├── ProductController.cs
│   ├── CartController.cs
│   ├── CheckoutController.cs
│   └── PaymentController.cs
├── Models/
│   ├── ProductModel.cs
│   ├── CategoryModel.cs
│   ├── OrderModel.cs
│   └── ...
├── Views/
│   ├── Home/
│   ├── Product/
│   ├── Cart/
│   └── Shared/
├── wwwroot/
│   ├── css/
│   ├── js/
│   └── media/
├── Migrations/
├── Services/
│   └── Momo/
├── appsettings.json
└── Program.cs
```

## 🔧 Development

### Build Project

```bash
dotnet build
```

### Run Tests

```bash
dotnet test
```

### Create Migration

```bash
dotnet ef migrations add MigrationName
```

### Update Database

```bash
dotnet ef database update
```

## 🤝 Đóng Góp

Mọi đóng góp đều được hoan nghênh! Vui lòng:

1. Fork repository
2. Tạo branch mới (`git checkout -b feature/AmazingFeature`)
3. Commit changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to branch (`git push origin feature/AmazingFeature`)
5. Mở Pull Request

## 📝 License

Dự án này được phân phối dưới giấy phép MIT. Xem file `LICENSE` để biết thêm chi tiết.

## 👨‍💻 Tác Giả

**Hungztv**

- GitHub: [@Hungztv](https://github.com/Hungztv)
- Email: your-email@example.com

## 🙏 Acknowledgments

- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core)
- [Bootstrap](https://getbootstrap.com/)
- [Font Awesome](https://fontawesome.com/)
- [Unsplash](https://unsplash.com/) - Product Images
- [Momo Payment Gateway](https://developers.momo.vn/)

## 📞 Liên Hệ

Nếu có bất kỳ câu hỏi nào, vui lòng liên hệ qua:

- 📧 Email: your-email@example.com
- 🐛 Issues: [GitHub Issues](https://github.com/Hungztv/Shopping/issues)

---

⭐ **Nếu dự án hữu ích, hãy cho một star nhé!** ⭐
