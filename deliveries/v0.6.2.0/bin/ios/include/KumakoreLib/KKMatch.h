//
//  KKMatch.h
//  KumakoreApplication
//
//  Created by Kumakoe on 9/4/13.
//  Copyright (c) 2013 Kumakore. All rights reserved.
//

#import <Foundation/Foundation.h>

@class KKReturnStatus;
@class KKUser;

/** 
 * Class that represents the data that makes up a move in a match.
 * @param moveId
 * @param moveNumver
 * @param createdAt
 * @param updatedAt
 * @param moveData
 * @param addItems
 * @param attackItems
 * @param addCoins
 * @param close
 * @param removeItems
 * @param userId
 * @param rewardItems
 * @param rewardCoins
 * @param selectedItems
 */
@interface KKMoveData : NSObject

@property NSString *moveId;
@property int moveNumber;
@property NSString *createdAt;
@property NSString *updatedAt;
@property NSString *moveData;
@property NSMutableDictionary *addItems;
@property NSMutableDictionary *attackItems;
@property int addCoins;
@property BOOL close;
@property NSMutableDictionary *removeItems;
@property NSString *userId;
@property NSArray *rewardItems;
@property int rewardCoins;
@property NSString *selectedItems;

@end

/**
 * Class that represents the status of a match.
 * @param matchId
 * @param opponentId
 * @param opponentUserName
 * @param opponentFacebookId
 * @param opponentAvatarId
 * @param turn
 * @param moveCount
 * @param closedBy
 * @param closed
 * @param resigned
 * @param accepted
 * @param rejected
 * @param nudged
 * @param newMessage
 * @param updatedAt
 * @param createdAt
 * @param type
 */
@interface KKMatchStatus : NSObject

@property NSString *matchId;
@property NSString *opponentId;
@property NSString *opponentUserName;
@property NSString *opponentFacebookId;
@property NSString *opponentAvatarId;
@property NSString *turn;
@property NSInteger moveCount;
@property NSArray *closedBy;
@property BOOL closed;
@property BOOL resigned;
@property BOOL accepted;
@property BOOL rejected;
@property BOOL nudged;
@property BOOL newMessage;
@property NSString *updatedAt;
@property NSString *createdAt;
@property NSString *type;

@end

/** 
 * Class that represents a message sent between opponents in a match
 * @param messageId
 * @param createdAt
 * @param data
 * @param read
 * @param userAppDatumId
 */
@interface KKMatchMessage : NSObject

@property NSString *messageId;
@property NSString *createdAt;
@property NSString *data;
@property BOOL read;
@property NSString *userAppDatumId;

@end

/**
 * Class that represents a Match between users 
 */
@interface KKMatch : NSObject

/**
 * Method to accept the match
 * Synchronous
 * @param returnBlock Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 */
- (void) syncAccept: (void (^)(KKReturnStatus* returnStatus)) returnBlock;

/**
 * Method to close the match
 * Synchronous
 * @param returnBlock Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 */
- (void) syncClose: (void (^)(KKReturnStatus* returnStatus)) returnBlock;

/**
 * Method to reject the match
 * Synchronous
 * @param returnBlock Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 */
- (void) syncReject: (void (^)(KKReturnStatus* returnStatus)) returnBlock;

/**
 * Method to resign the match
 * Synchronous
 * @param returnBlock Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 */
- (void) syncResign: (void (^)(KKReturnStatus* returnStatus)) returnBlock;

/**
 * Method to send a nudge to your opponent in the match
 * Synchronous
 * @param returnBlock Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 */
- (void) syncNudge: (void (^)(KKReturnStatus* returnStatus)) returnBlock;


/**
 * Method to get the match's status
 * Synchronous
 * @param returnBlock Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 * @param matchStatus KKMatchStatus that contains data on the match
 */
- (void) syncGetMatchStatus: (void (^)(KKReturnStatus* returnStatus, KKMatchStatus* matchStatus)) returnBlock;

/**
 * Method to submit a move to the match
 * Synchronous
 * @param moveData move data
 * @param returnBlock Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 */
- (void) syncSubmitMove: (KKMoveData*) moveData return: (void (^)(KKReturnStatus* returnStatus)) returnBlock;

/**
 * Method to get all the moves of the match
 * Synchronous
 * @param returnBlock Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 * @param moveList NSArray of KKMoveData of all the moves of the match
 */
- (void) syncGetMoves: (void (^)(KKReturnStatus* returnStatus, NSArray* moveList)) returnBlock;


/**
 * Method to get all the message of the match
 * Synchronous
 * @param returnBlock Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 * @param messages NSArray of KKMatchMessages of all the messages for the match
 */
- (void) syncGetMessages: (void (^)(KKReturnStatus* returnStatus, NSArray* messages)) returnBlock;

/**
 * Method to send a message to your opponent in the match
 * Synchronous
 * @message KKMatchMessage containing the message to be sent
 * @param returnBlock Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 */
- (void) syncSendMessage: (KKMatchMessage*) message return: (void (^)(KKReturnStatus* returnStatus)) returnBlock;

/**
 * Method to accept the match
 * Asynchronous
 * @param callback Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 */
- (void) asyncAccept:(void (^)(KKReturnStatus* returnStatus)) callback;

/**
 * Method to close the match
 * Asynchronous
 * @param callback Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 */
- (void) asyncClose:(void (^)(KKReturnStatus* returnStatus)) callback;

/**
 * Method to reject the match
 * Asynchronous
 * @param callback Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 */
- (void) asyncReject:(void (^)(KKReturnStatus* returnStatus)) callback;

/**
 * Method to resign the match
 * Asynchronous
 * @param callback Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 */
- (void) asyncResign:(void (^)(KKReturnStatus* returnStatus)) callback;

/**
 * Method to send a nudge to your opponent in the match
 * Asynchronous
 * @param callback Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 */ 
- (void) asyncNudge:(void (^)(KKReturnStatus* returnStatus)) callback;

/**
 * Method to get the match's status
 * Asynchronous
 * @param callback Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 * @param matchStatus KKMatchStatus that contains data on the match
 */
- (void) asyncGetMatchStatus:(void (^)(KKReturnStatus* returnStatus, KKMatchStatus*)) callback;

/**
 * Method to submit a move to the match
 * Asynchronous
 * @param callback Block that will be called before the method returns
 * @param returnBlock Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 */
- (void) asyncSubmitMove:(KKMoveData*) moveData callback:(void (^)(KKReturnStatus* returnStatus)) callback;

/**
 * Method to get all the moves of the match
 * Asynchronous
 * @param callback Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 * @param moveList NSArray of KKMoveData of all the moves of the match
 */
- (void) asyncGetMoves:(void (^)(KKReturnStatus* returnStatus, NSArray*)) callback;

/**
 * Method to get all the message of the match
 * Asynchronous
 * @param callback Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 * @param messages NSArray of KKMatchMessages of all the messages for the match
 */
- (void) asyncGetMessages:(void (^)(KKReturnStatus* returnStatus, NSArray*)) callback;

/**
 * Method to send a message to your opponent in the match
 * Asynchronous
 * @message KKMatchMessage containing the message to be sent
 * @param callback Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 */
- (void) asyncSendMessage:(KKMatchMessage*) message callback:(void (^)(KKReturnStatus* returnStatus)) callback;



@end
