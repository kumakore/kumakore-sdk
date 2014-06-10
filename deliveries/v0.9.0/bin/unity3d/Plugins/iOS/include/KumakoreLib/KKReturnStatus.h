//
//  KKReturnCode.h
//  KumakoreApplication
//
//  Created by Kumakoe on 8/16/13.
//  Copyright (c) 2013 Kumakore. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface KKReturnStatus : NSObject{
}

@property int responseCode;
@property int errorCode;
@property NSString *message;
@property int value;

- (id)init: (int) value;
- (bool) success;
- (void) setMessage:(NSString *)message;


@end

extern const int KKErrorCode_FAIL;
extern const int KKErrorCode_FAIL_AlreadySignedIn;
extern const int KKErrorCode_FAIL_UserNotSignedIn;
extern const int KKErrorCode_FAIL_InvalidParameters;
extern const int KKErrorCode_FAIL_InvalidDashboardVersion;
extern const int KKErrorCode_FAIL_InvalidAppVersion;
extern const int KKErrorCode_FAIL_DeviceTokenNotSet;
extern const int KKErrorCode_FAIL_DeviceNotRegistered;
extern const int KKErrorCode_FAIL_UnknownNetworkError;
extern const int KKErrorCode_FAIL_InvalidCredentials;