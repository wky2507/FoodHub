# 介绍:

本项目是一个前后端分离的餐饮企业管理系统，基于 BlazorWebAssembly  +   ASP.NET Core Web API  + SQLServer构建



## 技术栈：

```
 ASP.NET Core 9
后端：
- ASP.NET Core Web API
- Ardalis.ApiEndpoints
- C# .Net9
ORM: EF Core
前端:
- Blazor WebAssembly(WASM)
数据库:
- SQL Server
身份认证与角色鉴权:
- ASP.NET Core Identity
- JWT
- AspNetCore.Authorization
日志: 
-Serilog 结构化日志
前端存储:
- SessionStorage\LocalStorage
部署:
- Docker
数据库管理:
- EF Core Migration
- Repository
- Ardalis.Specification
```

功能列表：

```
用户注册 / 登录
商户申请
管理员审批申请
菜品管理
订单管理
商户 Dashboard
销售数据统计
图片上传
角色鉴权
登陆状态管理
支持Docker部署

```

通用技术能力:

- 全局统一异常处理，标准化API结果返回
- 前后端分离交互，使用DTO做数据传输
- 分页查询、条件筛选、时间区间查询
- Docker多阶段构建，利用层缓存优化Nuget包下载速度
- Docker Compose 编排，使用Volume持久化SQL数据库与后端上传图片文件夹
- CORS跨域请求配置

### 运行示例:

![](assets\login.png)

![](assets\apply.png)

![](assets\merchant.png)

![](assets\dishes.png)

![](assets\order.png)

![](assets\admin.png)

## 使用Docker运行说明:

从根文件夹（.sln文件所在的文件夹）运行以下的命令来运行该系统

docker compose up -d --build

容器运行后，可以使用 localhost:8081访问Web项目，使用localhost:8080访问api项目，可以通过http://localhost:8080/swagger/index.html访问swagger交互式API文档页面

