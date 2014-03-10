//
//  KKDevice.h
//  KumakoreApplication
//
//  Created by Kumakoe on 9/3/13.
//  Copyright (c) 2013 Kumakore. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "KKUser_private.h"
#import "KKReturnStatus.h"

/**
 * Class that represents the device that is currently running the program. 
 * Used to register/unregister for push. Mute/Unmute pushes.  Set Badge Numbers.
 */
@interface KKDevice : NSObject{
}

-(id) init:(KKUser*) user;

- (void) syncRegisterForPush: (void (^)(KKReturnStatus*)) returnBlock;
- (void) syncUnregisterForPush: (void (^)(KKReturnStatus*)) returnBlock;
- (void) syncMute: (void (^)(KKReturnStatus*)) returnBlock;
- (void) syncUnmute: (void (^)(KKReturnStatus*)) returnBlock;
- (void) syncSetDeviceBadge: (int) badgeNumber return: (void (^)(KKReturnStatus*)) returnBlock;

- (void) asyncRegisterForPush: (void (^)(KKReturnStatus*)) callback;
- (void) asyncUnregisterForPush: (void (^)(KKReturnStatus*)) callback;
- (void) asyncMute: (void (^)(KKReturnStatus*)) callback;
- (void) asyncUnmute: (void (^)(KKReturnStatus*)) callback;

- (void) asyncSetDeviceBadge: (int) badgeNumber callback: (void (^)(KKReturnStatus*)) callback;

//static function for ios to set the device token
+(void) setDeviceToken:(NSString*) token;


@end
