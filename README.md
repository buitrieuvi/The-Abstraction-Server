# The Abstraction Server

Backend API xây dựng bằng ASP.NET Core 8, hiện tập trung vào đăng nhập JWT và quản lý người dùng, vai trò. Giải pháp được tổ chức thành các project API, Application, Domain, Infrastructure và UnitTests.

## Công nghệ

- .NET 8 / ASP.NET Core Web API
- ASP.NET Core Identity
- Entity Framework Core 8 và SQLite
- MediatR (CQRS), AutoMapper
- JWT Bearer Authentication
- Swagger / Swashbuckle
- MongoDB.Driver được tham chiếu trong dự án; các endpoint nghiệp vụ MongoDB chưa được triển khai trong API hiện tại.
- xUnit

## Kiến trúc

```text
TheAbstraction.Api             HTTP controllers, middleware, cấu hình ứng dụng
TheAbstraction.Application     Commands, queries, DTOs, interfaces
TheAbstraction.Domain          Entities và repository interfaces
TheAbstraction.Infrastructure Identity, EF Core, SQLite, repositories
TheAbstraction.UnitTests       Kiểm thử
```

Luồng xử lý chính: HTTP request → Controller → MediatR command/query → dịch vụ Identity/repository → SQLite.

## Chức năng hiện có

- Đăng nhập bằng tên người dùng và mật khẩu, trả JWT.
- Tạo, xem, cập nhật và xóa người dùng.
- Tra cứu thông tin người dùng theo ID hoặc tên đăng nhập.
- Tạo, xem, cập nhật và xóa vai trò.
- Gán hoặc cập nhật vai trò cho người dùng.
- Xử lý lỗi tập trung qua middleware.

README này chỉ liệt kê chức năng có mã nguồn hiện tại. Sản phẩm, đơn hàng, upload ảnh và các API đăng ký được nhắc trong tài liệu cũ chưa có endpoint tương ứng trong dự án.

## API hiện tại

Base route: `/api/v1`. Tên endpoint dùng danh từ số nhiều; ID tài nguyên nằm trên URL.

### Authentication

| Method | Endpoint | Mô tả |
|---|---|---|
| POST | `/api/v1/auth/login` | Đăng nhập, nhận JWT |

Request:

```json
{
  "userName": "example",
  "password": "your-password"
}
```

### Users

| Method | Endpoint | Mô tả |
|---|---|---|
| POST | `/api/v1/users` | Tạo người dùng; trả `201 Created` cùng `Location` |
| GET | `/api/v1/users` | Lấy danh sách kèm vai trò |
| GET | `/api/v1/users?userName={userName}` | Tra cứu theo tên đăng nhập |
| GET | `/api/v1/users/{id}` | Lấy chi tiết theo ID |
| PUT | `/api/v1/users/{id}` | Cập nhật hồ sơ |
| DELETE | `/api/v1/users/{id}` | Xóa người dùng |
| PUT | `/api/v1/users/{id}/roles` | Thay toàn bộ danh sách vai trò |

Ví dụ body tạo tài khoản:

```json
{
  "fullName": "Example User",
  "userName": "example",
  "email": "example@example.com",
  "password": "your-password",
  "confirmationPassword": "your-password",
  "roles": ["user"]
}
```

### Roles

| Method | Endpoint | Mô tả |
|---|---|---|
| POST | `/api/v1/roles` | Tạo vai trò |
| GET | `/api/v1/roles` | Lấy danh sách vai trò |
| GET | `/api/v1/roles/{id}` | Lấy vai trò theo ID |
| PUT | `/api/v1/roles/{id}` | Cập nhật vai trò |
| DELETE | `/api/v1/roles/{id}` | Xóa vai trò |

## Đề xuất chuẩn hóa REST

Route REST hiện được triển khai dưới `/api/v1`. Những route cũ đã bị thay thế:

| Hiện tại | Đề xuất |
|---|---|
| `POST /api/user/create` | `POST /api/v1/users` |
| `GET /api/user/get` | `GET /api/v1/users` |
| `GET /api/user/getUserDetails/{userId}` | `GET /api/v1/users/{userId}` |
| `GET /api/user/getUserDetailsByUserName/{userName}` | `GET /api/v1/users?userName={userName}` |
| `PUT /api/user/editUserProfile/{id}` | `PUT /api/v1/users/{id}` hoặc `PATCH` cho cập nhật một phần |
| `DELETE /api/user/delete` | `DELETE /api/v1/users/{id}` |
| `POST /api/user/assignRoles` | `PUT /api/v1/users/{id}/roles` |
| `PUT /api/user/editUserRoles` | `PUT /api/v1/users/{id}/roles` (gộp với thao tác gán vai trò) |
| `POST /api/role/create` | `POST /api/v1/roles` |
| `GET /api/role/get` | `GET /api/v1/roles` |
| `GET /api/role/get/{id}` | `GET /api/v1/roles/{id}` |
| `PUT /api/role/edit` | `PUT /api/v1/roles/{id}` |
| `DELETE /api/role/delete` | `DELETE /api/v1/roles/{id}` |
| `POST /api/auth/login` | Giữ `POST /api/v1/auth/login`; đăng nhập là thao tác tạo phiên/token |

Nên trả status code theo kết quả: `201 Created` khi tạo, `200 OK` khi đọc/cập nhật có nội dung, `204 No Content` khi xóa thành công, `400 Bad Request` khi dữ liệu không hợp lệ, `401 Unauthorized` khi chưa xác thực, `403 Forbidden` khi không đủ quyền và `404 Not Found` khi không tìm thấy tài nguyên. Với danh sách lớn, bổ sung phân trang và bộ lọc qua query string.

Các thuộc tính `[Authorize]` trên controller vẫn đang bị comment. Cần bật xác thực và xác định quyền truy cập trước khi triển khai môi trường thật.

## Chạy dự án

Yêu cầu .NET 8 SDK. Cơ sở dữ liệu SQLite được lưu trong `TheAbstraction.Api/MyDatabase.db`.

1. Cập nhật `ConnectionStrings:DefaultConnection` trong User Secrets hoặc cấu hình môi trường.
2. Cung cấp cấu hình JWT `Jwt:Key`, `Jwt:Issuer`, `Jwt:Audience` và `Jwt:ExpiryMinutes` bằng User Secrets hoặc biến môi trường (`Jwt__Key`, `Jwt__Issuer`, `Jwt__Audience`, `Jwt__ExpiryMinutes`). Dùng khóa riêng đủ mạnh; không commit bí mật.
3. Restore và chạy:

```bash
dotnet restore The-Abstraction-Server.sln
dotnet run --project TheAbstraction.Api
```

Trong môi trường Development, Swagger UI ở `/swagger`. API tự tạo schema SQLite từ EF Core model khi khởi động nếu database chưa tồn tại.

> `Program.cs` hiện cũng đăng ký `MongoClient` và yêu cầu `MongoDatabaseSettings:ConnectionString` khi khởi động. Hãy cấu hình giá trị này nếu vẫn giữ đăng ký MongoDB; nếu không dùng MongoDB, có thể gỡ đăng ký để tránh phụ thuộc cấu hình không cần thiết.

## Kiểm thử

```bash
dotnet test The-Abstraction-Server.sln
```

Một số test tạo dữ liệu/đăng nhập trực tiếp và có thể cần cấu hình database cũng như tài khoản phù hợp.

## Cấu hình Identity

Identity hiện yêu cầu mật khẩu tối thiểu 6 ký tự, chữ thường; không bắt buộc chữ số, chữ hoa hoặc ký tự đặc biệt. Email phải duy nhất. Khóa tài khoản sau 5 lần đăng nhập thất bại trong 5 phút.

## Giấy phép

README trước đây ghi MIT License. Hãy thêm tệp `LICENSE` nếu muốn phân phối dự án theo giấy phép này.
