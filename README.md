# BiliPassport

用于通过用户名密码登录bilibili，获取鉴权用access_token以及cookies。

包含使用样例`Program.cs`，输出如下（已做匿名化处理）：

```
-------- Info --------
username: <hide>
password: <hide>

-------- Normal login --------
LoginToken:
  Mid: <hide>
  AccessToken: <hide>
  RefreshToken: <hide>
  Expires: 9/8/2021 2:33:28 AM
LoginCookies:
  bili_jct=<hide>; .bilibili.com/; 9/8/2021 1:33:31 AM
  bili_jct=<hide>; .biligame.com/; 9/8/2021 1:33:31 AM
  bili_jct=<hide>; .bigfunapp.cn/; 9/8/2021 1:33:31 AM
  DedeUserID=<hide>; .bilibili.com/; 9/8/2021 1:33:31 AM
  DedeUserID=<hide>; .biligame.com/; 9/8/2021 1:33:31 AM
  DedeUserID=<hide>; .bigfunapp.cn/; 9/8/2021 1:33:31 AM
  DedeUserID__ckMd5=<hide>; .bilibili.com/; 9/8/2021 1:33:31 AM
  DedeUserID__ckMd5=<hide>; .biligame.com/; 9/8/2021 1:33:31 AM
  DedeUserID__ckMd5=<hide>; .bigfunapp.cn/; 9/8/2021 1:33:31 AM
  sid=<hide>; .bilibili.com/; 9/8/2021 1:33:31 AM
  sid=<hide>; .biligame.com/; 9/8/2021 1:33:31 AM
  sid=<hide>; .bigfunapp.cn/; 9/8/2021 1:33:31 AM
  SESSDATA=<hide>; .bilibili.com/; 9/8/2021 1:33:31 AM
  SESSDATA=<hide>; .biligame.com/; 9/8/2021 1:33:31 AM
  SESSDATA=<hide>; .bigfunapp.cn/; 9/8/2021 1:33:31 AM
SSO:
  https://passport.bilibili.com/api/v2/sso
  https://passport.biligame.com/api/v2/sso
  https://passport.bigfunapp.cn/api/v2/sso

-------- Login with captcha --------
Please type in the captcha: M5BC2
LoginToken:
  Mid: <hide>
  AccessToken: <hide>
  RefreshToken: <hide>
  Expires: 9/8/2021 2:33:40 AM
LoginCookies:
  bili_jct=<hide>; .bilibili.com/; 9/8/2021 1:33:43 AM
  bili_jct=<hide>; .biligame.com/; 9/8/2021 1:33:43 AM
  bili_jct=<hide>; .bigfunapp.cn/; 9/8/2021 1:33:43 AM
  DedeUserID=<hide>; .bilibili.com/; 9/8/2021 1:33:43 AM
  DedeUserID=<hide>; .biligame.com/; 9/8/2021 1:33:43 AM
  DedeUserID=<hide>; .bigfunapp.cn/; 9/8/2021 1:33:43 AM
  DedeUserID__ckMd5=<hide>; .bilibili.com/; 9/8/2021 1:33:43 AM
  DedeUserID__ckMd5=<hide>; .biligame.com/; 9/8/2021 1:33:43 AM
  DedeUserID__ckMd5=<hide>; .bigfunapp.cn/; 9/8/2021 1:33:43 AM
  sid=<hide>; .bilibili.com/; 9/8/2021 1:33:43 AM
  sid=<hide>; .biligame.com/; 9/8/2021 1:33:43 AM
  sid=<hide>; .bigfunapp.cn/; 9/8/2021 1:33:43 AM
  SESSDATA=<hide>; .bilibili.com/; 9/8/2021 1:33:43 AM
  SESSDATA=<hide>; .biligame.com/; 9/8/2021 1:33:43 AM
  SESSDATA=<hide>; .bigfunapp.cn/; 9/8/2021 1:33:43 AM
SSO:
  https://passport.bilibili.com/api/v2/sso
  https://passport.biligame.com/api/v2/sso
  https://passport.bigfunapp.cn/api/v2/sso

-------- Refresh token --------
Mid: <hide>
AccessToken: <hide>
RefreshToken: <hide>
Expires: 10/8/2021 2:33:40 AM

-------- Finished --------
```