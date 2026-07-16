# Project Name

> Dự án là một bản demo nhỏ về một hệ thống Backend Server được phát triển bằng ASP.NET API core.

---

## 📖 Giới thiệu

Mô tả chi tiết dự án:

- Cung cấp các API cho cái ứng dụng người dùng trên web, app, unity.
- Người dùng, Người chơi, Khách hàng, Nhân viên, Quản lý,...
- Quản lý một hệ thống bán hàng, cho thuê hay nhân sự

---

## ✨ Tính năng

- Đăng nhập / Đăng ký
- JWT Authentication
- CRUD
- Phân quyền
- Upload ảnh
- Logging
- Validation
- CQRS + MediatR
- Clean Architecture

---

## 🏗️ Kiến trúc

```
Presentation
      │
Application
      │
Domain
      │
Infrastructure
      │
Database
```

---

## 🛠️ Công nghệ

| Công nghệ | Phiên bản |
|-----------|-----------|
| .NET | 9 |
| ASP.NET Core | 9 |
| EF Core | 9 |
| SQL Server | 2022 |
| MediatR | Latest |
| AutoMapper | Latest |
| FluentValidation | Latest |
| Swagger | Latest |
| JWT | Latest |

---

## 📂 Cấu trúc thư mục

```
src
│
├── API
│
├── Application
│   ├── Commands
│   ├── Queries
│   ├── DTOs
│   ├── Interfaces
│   └── Validators
│
├── Domain
│   ├── Entities
│   ├── Enums
│   ├── Common
│   └── Events
│
├── Infrastructure
│   ├── Persistence
│   ├── Identity
│   ├── Repositories
│   └── Services
│
└── Shared
```

---

## 🚀 Cài đặt

### Clone

```bash
git clone https://github.com/username/project.git
```

### Restore

```bash
dotnet restore
```

### Migration

```bash
dotnet ef database update
```

### Run

```bash
dotnet run
```

---

## ⚙️ appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": ""
  },

  "Jwt": {
    "Key": "",
    "Issuer": "",
    "Audience": ""
  }
}
```

---

## 🔑 Authentication

Sử dụng JWT Bearer Authentication.

Header

```
Authorization: Bearer {token}
```

---

## 📌 API

### Authentication

| Method | Endpoint | Description |
|---------|----------|-------------|
| POST | /api/user/register | Đăng ký |
| POST | /api/user/login | Đăng nhập |

---

### Product

| Method | Endpoint | Description |
|---------|----------|-------------|
| GET | /api/product | Danh sách |
| GET | /api/product/{id} | Chi tiết |
| POST | /api/product | Tạo |
| PUT | /api/product/{id} | Cập nhật |
| DELETE | /api/product/{id} | Xóa |

---

## 📄 Request Example

```json
{
  "name": "Laptop",
  "description": "Gaming Laptop",
  "price": 25000000
}
```

---

## 📄 Response Example

```json
{
  "id": "01JKXXXX",
  "name": "Laptop",
  "price": 25000000
}
```

---

## 🧪 Testing

```bash
dotnet test
```

---

## 📈 Logging

- Serilog
- Console
- File

---

## 🔒 Security

- JWT Authentication
- Role Authorization
- Password Hashing
- HTTPS
- Input Validation

---

## 📊 Database

```
Category
    │
    ├──── Product
               │
               ├──── ProductVariant
               │
               └──── ProductImage
```

---

## 📋 Coding Convention

- PascalCase cho Class
- camelCase cho biến
- Async hậu tố cho phương thức bất đồng bộ
- Interface bắt đầu bằng I
- Một lớp một nhiệm vụ (SRP)

---

## 🤝 Đóng góp

1. Fork repository
2. Tạo branch mới
3. Commit
4. Push
5. Tạo Pull Request

---

## 📜 License

MIT License

---

## 👨‍💻 Tác giả

Tên của bạn: Bùi Triều Vĩ

Email: buitrieuvi2019@gmail.com

---

## 📸 Một số hình ảnh về dự án

![](./Screenshot%202026-07-16%20125135.png)
![](./Screenshot%202026-07-16%10130048.png)
![](./Screenshot%202026-07-16%20130323.png)
![](./Screenshot%202026-07-16%20130345.png)
![](./Screenshot%202026-07-16%20130423.png)
---

## ⭐ Nếu dự án hữu ích

Hãy cho repository một ⭐ trên GitHub.
