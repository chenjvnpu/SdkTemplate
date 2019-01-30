//添加头文件
#import "IOSSdkService.h"

#define LoginSuccess @"OnLoginSuccess"
#define OnLogoutSuccess @"OnLogoutSuccess"

//添加方法声明,正常情况下应放在 .h 文件进行方法声明，这里为了便于更新操作，就放在一个文件内进行处理
#ifdef __cplusplus
extern "C" {
#endif
    void _Sdk_Login(const char* account,const char* password);
    void _Sdk_Logout();
    
#ifdef __cplusplus
}
#endif


#pragma mark Bridge Implementations
//方法具体实现
void _Sdk_Login(const char* account,const char* password)
{
    NSString* deviceToke = @"91043809750";
    [[IOSSdkService shareInstance] sendUnityMessageWithFuncName:LoginSuccess andParams:deviceToke];
}

void _Sdk_Logout()
{
    NSString* deviceToke = @"91043809750";
    [[IOSSdkService shareInstance] sendUnityMessageWithFuncName:OnLogoutSuccess andParams:deviceToke];
}

