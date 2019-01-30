//
//  IOSSdkService.h
//  Unity-iPhone
//
//  Created by gaea on 2019/1/30.
//

#import <UIKit/UIKit.h>

//全局常量
static const NSString* LoginFaild = @"LoginFaild";

@interface IOSSdkService : NSObject
+(IOSSdkService*) shareInstance;
-(void)send2UnityWithFuncName:(NSString*)funcName andDictionaryParams:(NSDictionary*)parameters;
-(void)sendUnityMessageWithFuncName:(NSString*)functionName andParams:(NSString*)paramerers;
@end
