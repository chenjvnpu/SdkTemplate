

#import "IOSSdkService.h"

#define UnityReceiver @"IOSSdkReceiver"
#define LoginSuccess @"OnLoginSuccess"
#define OnLogoutSuccess @"OnLogoutSuccess"

@implementation IOSSdkService
static IOSSdkService* _shareInstance = nil;



+(IOSSdkService*) shareInstance
{
    @synchronized(self)
    {
        if(_shareInstance == nil){
            _shareInstance = [[self alloc] init];
        }
    }
    return _shareInstance;
}

-(void)send2UnityWithFuncName:(NSString*)funcName andDictionaryParams:(NSDictionary*)parameters
{
    NSError* error = nil;
    NSData* data = nil;
    if([NSJSONSerialization isValidJSONObject:parameters])
    {
        data = [NSJSONSerialization dataWithJSONObject:parameters options:NSJSONWritingPrettyPrinted error:&error];
        NSString* paramsString = [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
        [self sendUnityMessageWithFuncName:funcName andParams:paramsString];
    }
}


-(void)sendUnityMessageWithFuncName:(NSString*)functionName andParams:(NSString*)paramerers
{
    UnitySendMessage([UnityReceiver UTF8String], [functionName UTF8String], [paramerers UTF8String]);
}

@end
