//
//  KKAchievement.h
//  KumakoreApplication
//
//  Created by Kumakoe on 9/5/13.
//  Copyright (c) 2013 Kumakore. All rights reserved.
//

#import <Foundation/Foundation.h>

@class KKReturnStatus;
@class KKUser;

/** 
 * Class that represents an achievement
 * @param achievementId
 * @param achievementDescription
 * @param appId
 * @param createdAt
 * @param updatedAt
 * @param progress
 * @param achievementName
 * @param title
 * @param hidden true if the achievement is hidden
 */
@interface KKAchievement : NSObject

@property NSString *achievementId;
@property NSString *achievementDescription;
@property NSString *appId;
@property NSString *createdAt;
@property NSString *updatedAt;
@property int progress;
@property NSString *achievementName;
@property NSString *title;
@property BOOL hidden;

/**
 * Method to set the progress of an achievement for a User
 * Synchronous
 * @param user the KKUser to set the pro
 * @param progress the achievement's progress
 * @param returnBlock Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 */
- (void) syncSetUserAchievement: (KKUser*) user progress:(int) progress return: (void (^)(KKReturnStatus*)) returnBlock;

/**
 * Method to set the progress of an achievement for a User
 * Asynchronous
 * @param user the KKUser to set the pro
 * @param progress the achievement's progress
 * @param callback Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 */
- (void) asyncSetUserAchievement: (KKUser*) user progress:(int) progress callback:(void (^)(KKReturnStatus*)) callback;

@end