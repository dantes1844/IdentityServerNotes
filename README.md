# IdentityServerNotes
IdentityServer Notes



项目的搭建过程[参考连接](https://identityserver4.readthedocs.io/en/latest/quickstarts/1_client_credentials.html)



篇末留下的几个问题：

You can now try to provoke errors to learn how the system behaves, e.g.

1. try to connect to IdentityServer when it is not running (unavailable)

   >  报错：Error connecting to https://localhost:5001/.well-known/openid-configuration. 由于目标计算机积极拒绝，无法连接。

2. try to use an invalid client id or secret to request the token

   > 服务端：
   >
   > ​	用户名错误：No client with id 'client1' found. aborting
   >
   > ​	密码错误：Client secret validation failed for client: client.
   >
   > 客户端：
   >
   > ​	用户名错误：invalid_client
   >
   > ​	密码错误：invalid_client

3. try to ask for an invalid scope during the token request

   > 服务端：Scope api2 not found in store.另外会将当前请求的相关信息打印出来
   >
   > 客户端：invalid_scope

4. try to call the API when it is not running (unavailable)

   > 客户端：由于目标计算机积极拒绝，无法连接。

5. don’t send the token to the API

   > 客户端：unauthorized,401

6. configure the API to require a different scope than the one in the token

   > 客户端：unauthorized,401

