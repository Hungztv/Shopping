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

2. **Cấu hình Environment Variables**

Copy file `.env.example` thành `.env`:

```bash
cp .env.example .env
```

Mở file `.env` và điền các API keys thực tế:

```env
# Groq AI API Key (Lấy miễn phí tại: https://console.groq.com/keys)
GROQ_API_KEY=your_groq_api_key_here
GROQ_MODEL=llama-3.3-70b-versatile

# Google OAuth (Lấy từ: https://console.cloud.google.com/apis/credentials)
GOOGLE_CLIENT_ID=your_google_client_id_here
GOOGLE_CLIENT_SECRET=your_google_client_secret_here

# Momo Payment Gateway (Lấy từ: https://developers.momo.vn/)
MOMO_PARTNER_CODE=MOMO
MOMO_ACCESS_KEY=your_momo_access_key_here
MOMO_SECRET_KEY=your_momo_secret_key_here

# Database Connection
DB_SERVER=YOUR_SERVER_NAME\\INSTANCE_NAME
DB_NAME=ShoppingCart
DB_INTEGRATED_SECURITY=True
DB_ENCRYPT=True
DB_TRUST_SERVER_CERTIFICATE=True
```

3. **Lấy API Keys Miễn Phí**

#### Groq AI (Bắt buộc - cho AI Chatbot)

- Truy cập: [https://console.groq.com/keys](https://console.groq.com/keys)
- Đăng ký tài khoản miễn phí
- Tạo API key mới
- Copy key vào file `.env`
- **Free tier**: 14,400 requests/ngày

#### Google OAuth (Tùy chọn - đăng nhập Google)

- Vào [Google Cloud Console](https://console.cloud.google.com/apis/credentials)
- Tạo OAuth 2.0 Client ID
- Thêm redirect URI: `https://localhost:5032/signin-google`
- Copy Client ID và Secret vào `.env`

#### Momo Payment (Tùy chọn - thanh toán)

- Đăng ký tại [Momo Developers](https://developers.momo.vn/)
- Lấy test credentials
- Copy vào `.env`

4. **Restore dependencies**

```bash
cd Shopping
dotnet restore
```

5. **Chạy migrations**

```bash
dotnet ef database update
```

6. **Chạy ứng dụng**

```bash
dotnet run
```

Truy cập: `https://localhost:5032`

## 🤖 Sử Dụng AI Chatbot

Chatbot có thể giúp bạn:

- Tìm sản phẩm theo tiêu chí (VD: "laptop dưới 20 triệu")
- So sánh sản phẩm
- Gợi ý dựa trên ngân sách
- Trả lời câu hỏi về sản phẩm

Ví dụ câu hỏi:

- "laptop gaming tốt nhất"
- "smartphone dưới 15 triệu"
- "PC văn phòng giá rẻ"

## 🔒 Bảo Mật

⚠️ **QUAN TRỌNG**:

- **KHÔNG BAO GIỜ** commit file `.env` lên git
- Giữ API keys bí mật
- Thay đổi keys định kỳ nếu bị lộ
- Dùng keys khác nhau cho dev/production
- File `.env` đã được thêm vào `.gitignore`

## 🆓 Giới Hạn Free Tier

**Groq API**:

- 14,400 requests/ngày (miễn phí)
- ~1 request/6 giây
- Hoàn hảo cho development & dự án nhỏ

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
- Email: duongmanhhung1210@gmail.com

## 🙏 Acknowledgments

- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core)
- [Bootstrap](https://getbootstrap.com/)
- [Font Awesome](https://fontawesome.com/)
- [Unsplash](https://unsplash.com/) - Product Images
- [Momo Payment Gateway](https://developers.momo.vn/)

## 📞 Liên Hệ

Nếu có bất kỳ câu hỏi nào, vui lòng liên hệ qua:

- 📧 Email: duongmanhhung1210@gmail.com
- 🐛 Issues: [GitHub Issues](https://github.com/Hungztv/Shopping/issues)

---

⭐ **Nếu dự án hữu ích, hãy cho một star nhé!** ⭐
