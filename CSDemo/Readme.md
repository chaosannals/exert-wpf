# Client Server Demo

## DbFirst

依赖：Microsoft.EntityFrameworkCore.Tools

工具 -> Nuget包管理器 -> 程序包管理器控制台

出现命令行后要在默认选择默认的项目。

以下库目前都要 5.0.x 版本，6.0.x 版本有 bug 。
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Tools
Microsoft.EntityFrameworkCore.Design
Mysql.EntityFrameworkCore

```powershell
Scaffold-DbContext "server=127.0.0.1;port=3306;database=wpfcsdemo;uid=root;pwd=password;CharSet=utf8mb4;SslMode=none" MySql.EntityFrameworkCore -o Models -f
```

## Code First

依赖：Microsoft.EntityFrameworkCore.Design

工具 -> Nuget包管理器 -> 程序包管理器控制台

出现命令行后要在默认选择默认的项目。

```powershell
Enable-Migrations 

Add-Migration wpfcsdemo
```
