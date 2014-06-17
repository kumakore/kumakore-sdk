//
//  KKLeaderBoard.h
//  KumakoreApplication
//
//  Created by Kumakoe on 9/4/13.
//  Copyright (c) 2013 Kumakore. All rights reserved.
//

#import <Foundation/Foundation.h>

@class KKReturnStatus;
@class KKUser;
@class KKApplication;

/**
 * Class that holds data that represents a placements on the leaderboards
 */
@interface KKLeaderBoardPlacement : NSObject

@property NSString *member;
@property int rank;
@property int score;

@end

/**
 * Class that holds data about a leaderboard
 */
@interface KKLeaderBoardData : NSObject

@property NSString *leaderboardId;
@property NSString *leaderboardName;
@property BOOL reverse;
@property NSString *createdAt;
@property NSString *updatedAt;

@end

/**
 * Class that represents a leaderboard stores the leaderboard id and other data needed
 * @param leaderboardName
 * @param leaderboardId
 * @param description
 * @param reverse
 * @param createdAt
 * @param updatedAt
 */
@interface KKLeaderBoard : NSObject

/**
 * Method that initializes 
 */
//- (id) init:(NSString*) ldbname user:(NSString*) kkuser;


/**
 * Method that requests ranks of a leaderboard from a specific range
 * Synchronous
 * @param start Start of the range
 * @param end End of the range
 * @param returnBlock Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 * @param rankings NSArray of KKLeaderboardPlacement objects
 */
- (void) syncGetLeaderboardData:(int) start end:(int) end return: (void (^)(KKReturnStatus*, NSArray* rankings)) returnBlock;

/**
 * Method that requests ranks of a leaderboard centered on the current user
 * Synchronous
 * @param entries Number of entries to return
 * @param returnBlock Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 * @param rankings NSArray of KKLeaderboardPlacement objects
 */
- (void) syncGetCenteredLeaderboardData:(int) entries return: (void (^)(KKReturnStatus*, NSArray* rankings)) returnBlock;

/**
 * Method that requests ranks leaderboard placements of all opponents
 * Synchronous
 * @param returnBlock Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 * @param rankings NSArray of KKLeaderboardPlacement objects
 */
- (void) syncGetOpponentsLeaderboardData: (void (^)(KKReturnStatus*, NSArray* rankings)) returnBlock;

/**
 * Method that requests leaderboard placements of Facebook friends
 * Synchronous
 * @param fbToken Facebook access token
 * @param returnBlock Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 * @param rankings NSArray of KKLeaderboardPlacement objects
 */
- (void) syncGetFBFriendsLeaderboardData:(NSString*) fbToken return: (void (^)(KKReturnStatus*, NSArray* rankings)) returnBlock;

/**
 * Method that requests leaderboard placements of Facebook friends centered on the user
 * Synchronous
 * @param fbToken Facebook access token
 * @param entries Number of entries to return 
 * @param returnBlock Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 * @param rankings NSArray of KKLeaderboardPlacement objects
 */
- (void) syncGetCenteredFBFriendsLeaderboardData: (NSString*) fbToken entries: (int) entries return: (void (^)(KKReturnStatus*, NSArray* rankings)) returnBlock;

/**
 * Method that requests leaderboard placements of Facebook friends from a specific range
 * Synchronous
 * @param fbToken Facebook access token
 * @param start Start of the range
 * @param end End of the range
 * @param returnBlock Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 * @param rankings NSArray of KKLeaderboardPlacement objects
 */
- (void) syncGetFBFriendsLeaderboardData: (NSString*) fbToken start: (int) start end: (int) end return: (void (^)(KKReturnStatus*, NSArray* rankings)) returnBlock;

/**
 * Method that requests leaderboard placements of friends from a specific range
 * Synchronous
 * @param returnBlock Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 * @param rankings NSArray of KKLeaderboardPlacement objects
 */
- (void) syncGetFriendsLeaderboardData: (void (^)(KKReturnStatus*, NSArray* rankings)) returnBlock;

/**
 * Method that requests leaderboard placements of friends centered on a user
 * Synchronous
 * @param entries Number of entries to return
 * @param returnBlock Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 * @param rankings NSArray of KKLeaderboardPlacement objects
 */
- (void) syncGetCenteredFriendsLeaderboardData:(int) entries return: (void (^)(KKReturnStatus*, NSArray* rankings)) returnBlock;

/**
 * Method that requests leaderboard placements of friends from a specific range
 * Synchronous
 * @param start Start of the range
 * @param end End of the range
 * @param returnBlock Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 * @param rankings NSArray of KKLeaderboardPlacement objects
 */
- (void) syncGetFriendsLeaderboardData: (int) start end:(int) end return: (void (^)(KKReturnStatus*, NSArray* rankings)) returnBlock;

/**
 * Method that requests the user's leaderboard placement
 * Synchronous
 * @param returnBlock Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 * @param ranking KKLeaderboardPlacement of the user's score
 */
- (void) syncGetUserRank: (void (^)(KKReturnStatus*, KKLeaderBoardPlacement* ranking)) returnBlock;

/**
 * Method that sets the user's leaderboard score
 * Synchronous
 * @aram score user's score
 * @param returnBlock Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 */
- (void) syncSetUserScore:(int) score return: (void (^)(KKReturnStatus*)) returnBlock;


/**
 * Method that requests ranks of a leaderboard from a specific range
 * Asynchronous
 * @param start Start of the range
 * @param end End of the range
 * @param callback Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 * @param rankings NSArray of KKLeaderboardPlacement objects
*/
- (void) asyncGetLeaderboardData:(int) start end:(int) end callback:(void (^)(KKReturnStatus*, NSArray*)) callback;
/**
 * Method that requests ranks of a leaderboard centered on the current user
 * Asynchronous
 * @param entries Number of entries to return
 * @param callback Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 * @param rankings NSArray of KKLeaderboardPlacement objects
 */
- (void) asyncGetCenteredLeaderboardData:(int) entries callback:(void (^)(KKReturnStatus*, NSArray*)) callback;

/**
 * Method that requests ranks leaderboard placements of all opponents
 * Asynchronous
 * @param callback Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 * @param rankings NSArray of KKLeaderboardPlacement objects
 */
- (void) asyncGetOpponentsLeaderboardData:(void (^)(KKReturnStatus*, NSArray*)) callback;


/**
 * Method that requests leaderboard placements of Facebook friends
 * Asynchronous
 * @param fbToken Facebook access token
 * @param callback Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 * @param rankings NSArray of KKLeaderboardPlacement objects
 */

- (void) asyncGetFBFriendsLeaderboardData:(NSString*) fbToken callback:(void (^)(KKReturnStatus*, NSArray*)) callback;

/**
 * Method that requests leaderboard placements of Facebook friends centered on the user
 * Asynchronous
 * @param fbToken Facebook access token
 * @param entries Number of entries to return
 * @param callback Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 * @param rankings NSArray of KKLeaderboardPlacement objects
 */

- (void) asyncGetCenteredFBFriendsLeaderboardData:(NSString*) fbToken entries: (int) entries callback:(void (^)(KKReturnStatus*, NSArray*)) callback;

/**
 * Method that requests leaderboard placements of Facebook friends from a specific range
 * Asynchronous
 * @param fbToken Facebook access token
 * @param start Start of the range
 * @param end End of the range
 * @param callback Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 * @param rankings NSArray of KKLeaderboardPlacement objects
 */
- (void) asyncGetFBFriendsLeaderboardData:(NSString*) fbToken start:(int) start end:(int) end callback:(void (^)(KKReturnStatus*, NSArray*)) callback;

/**
 * Method that requests leaderboard placements of friends from a specific range
 * Asynchronous
 * @param callback Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 * @param rankings NSArray of KKLeaderboardPlacement objects
 */

- (void) asyncGetFriendsLeaderboardData:(void (^)(KKReturnStatus*, NSArray*)) callback;

/**
 * Method that requests leaderboard placements of friends centered on a user
 * Asynchronous
 * @param entries Number of entries to return
 * @param callback Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 * @param rankings NSArray of KKLeaderboardPlacement objects
 */

- (void) asyncGetCenteredFriendsLeaderboardData: (int) entries callback:(void (^)(KKReturnStatus*, NSArray*)) callback;
/**
 * Method that requests leaderboard placements of friends from a specific range
 * Asynchronous
 * @param start Start of the range
 * @param end End of the range
 * @param callback Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 * @param rankings NSArray of KKLeaderboardPlacement objects
 */

- (void) asyncGetFriendsLeaderboardData:(int) start end:(int) end callback:(void (^)(KKReturnStatus*, NSArray*)) callback;

/**
 * Method that requests the user's leaderboard placement
 * Asynchronous
 * @param callback Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 * @param ranking KKLeaderboardPlacement of the user's score
 */
- (void) asyncGetUserRank:(void (^)(KKReturnStatus*, KKLeaderBoardPlacement*)) callback;

/**
 * Method that sets the user's leaderboard score
 * Asynchronous
 * @aram score user's score
 * @param callback Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 */- (void) asyncSetUserScore:(int) score callback:(void (^)(KKReturnStatus*)) callback;

@property NSString *leaderboardName;
@property NSString *leaderboardId;
@property NSString *description;
@property bool reverse;
@property NSString *createdAt;
@property NSString *updatedAt;


@end

