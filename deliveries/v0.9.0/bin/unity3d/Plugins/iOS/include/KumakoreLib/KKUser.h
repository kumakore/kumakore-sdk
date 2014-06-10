//
//  KKUser.h
//  KumakoreApplication
//
//  Created by Kumakoe on 8/16/13.
//  Copyright (c) 2013 Kumakore. All rights reserved.
//

#import <Foundation/Foundation.h>
@class KKApplication;
@class KKReturnStatus;
@class KKHttpClient;
@class KKDevice;
@class KKMatch;

/**
 Class that represents user data, each instance is not guaranteed nor is it necessary to fill out all the fields depending on how it is used.
 @property userName User's name
 @property email User's email
 @property userId User's Id
 @property password User's password
 @property avatar_id User's avatar id
 @property push_enabled Whether or not push is enabled for the user, true = yes
 */
@interface KKUserData : NSObject {
}

@property NSString *userName;
@property NSString *email;
@property NSString *userId;
@property NSString *password;
@property NSString *avatar_id;
@property BOOL push_enabled;

@end

/**
 Class that represents the data returned when obtaining data about Facebook friends
 @property firstName Facebook friend's first name
 @property lastName Facebook friend's last name
 @property pictureUrl Url where you can find the Facebook friend's profile picture
 @property pictureIsSilhoutette Whether or not the facebook friend's profile picture is a silhouette, true = yes
 @property facebookId Facebook friend's facebook id
 */
@interface KKFacebookFriendData :NSObject {
    
}


@property NSString *firstName;
@property NSString *lastName;
@property NSString *pictureUrl;
@property bool pictureIsSilhouette;
@property NSString *facebookId;

@end

/**
 Class that represents the data returned when obtaining data about Kumakore friends
 @property friendId Friend's id
 @property friendName Friend's user name
 @property avatarId Friend's avatar id
 */

@interface KKFriend : NSObject {
    
}

@property NSString *friendId;
@property NSString *friendName;
@property NSString *avatarId;

@end


/** 
 * Class that represents a KumakoreUser, stores the Session Id and other variables needed
 */
@interface KKUser : NSObject{
}

/** 
 * Initializes the KKUser class.  Requires an instance of KKApp which ties the User to that Application.
 * @param kkAppVar KKApplication instance
 */
- (id) init:(KKApplication*) kkAppVar;

/**
 * Method that signs the user up with the parameters given.  Requires either email or username to be filled out.
 * If the instance of the user is already signed in, it will return an error.
 * Synchronous
 * @param userData KKUserData instance that contains user information you want to sign up with
 * @param returnBlock Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 */
- (void) syncSignUp:(KKUserData*) userData return: (void (^)(KKReturnStatus* returnStatus)) returnBlock;

/**
 * Method that signs the user up with the parameters given.  Requires either email or username to be filled out.
 * If the instance of the user is already signed in, it will return an error.
 * Asynchronous
 * @param userData KKUserData instance that contains user information you want to sign up with
 * @param callBack Block that will be called when the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 */
- (void) asyncSignUp:(KKUserData*) userData callback:(void (^)(KKReturnStatus* returnStatus)) callback;

/**
 Method that signs the user in with the parameters given.  Requires either email or username to be filled out along with a password.
 If the instance of the user is already signed in, it will return an error.
 Synchronous
 @param userData KKUserData instance that contains user information to sign in with
 @param returnBlock Block that will be called before the method returns
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 */
- (void) syncSignIn:(KKUserData*) userData return: (void (^)(KKReturnStatus* returnStatus)) returnBlock;

/**
 Method that signs the user in with the parameters given.  Requires either email or username to be filled out along with a password.
 If the instance of the user is already signed in, it will return an error.
 Asynchronous
 @param userData KKUserData instance that contains user information to sign in with
 @param callBack Block that will be called when the method returns
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 */
- (void) asyncSignIn:(KKUserData*) userData callback:(void (^)(KKReturnStatus* returnStatus)) callback;

/**
 Method that gets the profile info of the user currently signed in.
 If the instance of the user is not signed in, it will return an error.
 Synchronous
 @param returnBlock Return block that will be called before the method returns
 @param profileInfo KKUserData where the profile info will be returned
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 */
- (void) syncGetProfileInfo:(void (^)(KKReturnStatus* returnStatus, KKUserData* profileInfo)) returnBlock;

/**
 Method that gets the profile info of the user currently signed in.
 If the instance of the user is not signed in, it will return an error.
 Asynchronous
 @param callBack Block that will be called when the method returns
 @param profileInfo KKUserData where the profile info will be returned
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 */
- (void) asyncGetProfileInfo:(void (^)(KKReturnStatus* returnStatus, KKUserData* profileInfo)) callback;


/**
 Method that gets the updates the profile info of the user currently signed in.
 If the instance of the user is not signed in, it will return an error.
 Synchronous
 @param userData KKUserData instance that contains the userdata to be sent to the
 @param returnBlock Return block that will be called before the method returns
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 */
- (void) syncUpdateProfileInfo:(KKUserData*) userData return: (void (^)(KKReturnStatus* returnStatus)) returnBlock;


/**
 Method that gets the updates the profile info of the user currently signed in.
 If the instance of the user is not signed in, it will return an error.
 Asynchronous
 @param userData KKUserData instance that contains the userdata to be updated
 @param callBack Block that will be called when the method returns
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 */
- (void) asyncUpdateProfileInfo:(KKUserData*) userData callback:(void (^)(KKReturnStatus* returnStatus)) callback;


/**
 Method that requests a password reset for the user currently signed in.
 If the instance of the user is not signed in, it will return an error.
 Synchronous
 @param returnBlock Return block that will be called before the method returns
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 */
- (void) syncRequestPasswordReset: (void (^)(KKReturnStatus* returnStatus)) returnBlock;


/**
 Method that requests a password reset for the user currently signed in.
 If the instance of the user is not signed in, it will return an error.
 Asnchronous
 @param callBack Block that will be called when the method returns
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 */
- (void) asyncRequestPasswordReset:(void (^)(KKReturnStatus* returnStatus)) callback;


/**
 Method that requests the list of friends of the user currently signed in.
 If the instance of the user is not signed in, it will return an error.
 Synchronous
 @param returnBlock Return block that will be called before the method returns
 @param limit Max number of friends to return
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 @param friendsList NSArray of KKFriend objects
 */
- (void) syncGetFriends: (int) limit return: (void (^)(KKReturnStatus* returnStatus,NSArray* friendsList)) returnBlock;

/**
 Method that requests the list of friends of the user currently signed in.
 If the instance of the user is not signed in, it will return an error.
 Asynchronous
 @param callBack Block that will be called when the method returns
 @param limit Max number of friends to return
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 @param friendsList NSArray of KKFriend objects
 */
- (void) asyncGetFriends: (int) limit callback: (void (^)(KKReturnStatus* returnStatus, NSArray* friendsList)) callback;


//- (void) getDevice: (void (^)(KKReturnStatus*, KKDevice*)) returnBlock;

/**
 Method that requests the list of matches the user is currently participating in
 If the instance of the user is not signed in, it will return an error.
 Synchronous
 @param returnBlock Return block that will be called before the method returns
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 @param matches NSArray of KKMatch objects representing the user's current matches
 */
- (void) syncGetCurrentMatches:(void (^)(KKReturnStatus* returnStatus,NSArray* matches)) returnBlock;

/**
 Method that requests the list of matches the user has completed
 If the instance of the user is not signed in, it will return an error.
 Synchronous
 @param returnBlock Return block that will be called before the method returns
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 @param matches NSArray of KKMatch objects representing the user's completed matches
 */
- (void) syncGetCompletedMatches: (void (^)(KKReturnStatus* returnStatus,NSArray* matches)) returnBlock;

/**
 Method that creates a new match between the user and a specific opponent
 If the instance of the user is not signed in, it will return an error.
 Synchronous
 @param userData KKUserData instance that contains the userdata to be updated
 @param returnBlock Return block that will be called before the method returns
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 @param match KKMatch object representing the newly created match
 */
- (void) syncCreateMatch:(KKUserData*) userData returnBlock: (void (^)(KKReturnStatus* returnStatus, KKMatch* match)) returnBlock;

/**
 Method that creates a new match between the user and a random opponent
 If the instance of the user is not signed in, it will return an error.
 Synchronous
 @param returnBlock Return block that will be called before the method returns
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 @param match KKMatch object representing the newly created match
 */
- (void) syncCreateRandomMatch:(void (^)(KKReturnStatus* returnStatus ,KKMatch* match)) returnBlock;

/**
 Method that requests the list of achievements for the current application
 If the instance of the user is not signed in, it will return an error.
 Synchronous
 @param returnBlock Return block that will be called before the method returns
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 @param achievements NSArray of KKAchievement objects representing the applicaiton's achievements
 */
- (void) syncGetAppAchievements:(void (^)(KKReturnStatus* retunStatus, NSArray* achievements)) returnBlock;

/**
 Method that requests the list of achievements achieved by the user
 If the instance of the user is not signed in, it will return an error.
 Synchronous
 @param returnBlock Return block that will be called before the method returns
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 @param achievements NSArray of KKAchievement objects representing the user's achievements
 */
- (void) syncGetAchievements:(void (^)(KKReturnStatus* returnStatus,NSArray* achievements)) returnBlock;

/**
 Method that requests the list of items available for the current applicaiton
 If the instance of the user is not signed in, it will return an error.
 Synchronous
 @param returnBlock Return block that will be called before the method returns
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 @param items NSArray of KKItems representing the available itmes
 @param bundles NSArray of KKBundles representing the available bundles
 */
- (void) syncGetAppItems:(void (^)(KKReturnStatus* returnStatus, NSDictionary* items, NSDictionary* bundles)) returnBlock;


/**
 Method that requests the list of items available for the current applicaiton
 If the instance of the user is not signed in, it will return an error.
 Synchronous
 @param returnBlock Return block that will be called before the method returns
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 @param items NSDictionary with the Key being the KKitem and the value being the quantity
 */
- (void) syncGetUserItems: (void (^)(KKReturnStatus* returnStatus, NSDictionary* items)) returnBlock;

/**
 Method that adds items to the user's inventory
 If the instance of the user is not signed in, it will return an error.
 Synchronous
 @param items NSDictionary with the Key being the KKitem and the value being the quantity
 @param returnBlock Return block that will be called before the method returns
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 */
- (void) syncAddItems:(NSDictionary*) items return:(void (^)(KKReturnStatus* returnStatus)) returnBlock;

/**
 Method that removes items to the user's inventory
 If the instance of the user is not signed in, it will return an error.
 Synchronous
 @param items NSDictionary with the Key being the KKitem and the value being the quantity
 @param returnBlock Return block that will be called before the method returns
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 */
- (void) syncRemoveItems:(NSDictionary*) items return:(void (^)(KKReturnStatus* returnStatus)) returnBlock;

/**
 Method that that buys items and add them to the user's inventory
 If the instance of the user is not signed in, it will return an error.
 Synchronous
 @param items NSDictionary with the Key being the KKitem and the value being the quantity
 @param returnBlock Return block that will be called before the method returns
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 */
- (void) syncBuyItems:(NSDictionary*) items return:(void (^)(KKReturnStatus* returnStatus)) returnBlock;

/**
 Method that that redeems an Apple store purchase, requires an applestore receipt number
 If the instance of the user is not signed in, it will return an error.
 Synchronous
 @param items NSDictionary with the Key being the KKitem and the value being the quantity
 @param returnBlock Return block that will be called before the method returns
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 */
- (void) syncRedeemAppleStorePurchase: (NSString*) receipt return: (void (^)(KKReturnStatus* returnStatus, NSDictionary* items)) returnBlock;

/**
 Method that gets the leaderboard data for the current application
 If the instance of the user is not signed in, it will return an error.
 Synchronous
 @param returnBlock Return block that will be called before the method returns
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 @param leaderboards NSArray of KKLeadeboard objects representing the applicaiton leaderboards
 */
- (void) syncGetAppLeaderBoards: (void (^)(KKReturnStatus*, NSArray* leaderboards)) returnBlock;

/**
 Method that signs the user in with the Facebook token given. If an account was already connected to the Facebook account, it will be used.
 If not, a new Kumakore account will be created and linked to the facebook account.
 Synchronous
 @param token NSString containing the facebook token 
 @param returnBlock Block that will be called before the method returns
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 */
- (void) syncSignInUsingFacebook: (NSString*) token return: (void (^)(KKReturnStatus* returnStatus)) returnBlock;

/**
 Method that connects the Facebook account with the currently signed in user
 If the instance of the user is not signed in, it will return an error.
 If the user already has an associated facebook account, it will return an error.
 Synchronous
 @param token NSString containing the facebook token
 @param returnBlock Block that will be called before the method returns
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 */
- (void) syncConnectFacebookAccount: (NSString*) token return: (void (^)(KKReturnStatus*)) returnBlock;

/**
 Method that disassociates the current user with its associated Facebook account for the current app.
 If the instance of the user is not signed in, it will return an error.
 Synchronous
 @param returnBlock Block that will be called before the method returns
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 */
- (void) syncDeauthorizeFacebookAccount: (void (^)(KKReturnStatus*)) returnBlock;


/**
 Method that requests the list of Facebook friends of the user currently signed in.
 If the instance of the user is not signed in or does not have an associated Facebook account, it will return an error.
 Synchronous
 @param returnBlock Return block that will be called before the method returns
 @param limit Max number of friends to return
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 @param friendsList NSArray of KKFriend objects
 */
- (void) syncGetFacebookFriends: (NSString*) token return: (void (^)(KKReturnStatus* returnStatus, NSArray* friendsList)) returnBlock;

//async functions


/**
 Method that requests the list of matches the user is currently participating in
 If the instance of the user is not signed in, it will return an error.
 Asynchronous
 @param callBack Block that will be called when the method returns
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 @param matches NSArray of KKMatch objects representing the user's current matches
 */
- (void) asyncGetCurrentMatches:(void (^)(KKReturnStatus*, NSArray*)) callback;

/**
 Method that requests the list of matches the user has completed
 If the instance of the user is not signed in, it will return an error.
 Asynchronous
 @param callBack Block that will be called when the method returns
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 @param matches NSArray of KKMatch objects representing the user's completed matches
 */
- (void) asyncGetCompletedMatches:(void (^)(KKReturnStatus*, NSArray*)) callback;


/**
 Method that creates a new match between the user and a specific opponent
 If the instance of the user is not signed in, it will return an error.
 Asynchronous
 @param callBack Block that will be called when the method returns
 @param returnBlock Return block that will be called before the method returns
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 @param match KKMatch object representing the newly created match
 */
- (void) asyncCreateMatch:(KKUserData*) userData callback:(void (^)(KKReturnStatus*, KKMatch*)) callback;

/**
 Method that creates a new match between the user and a random opponent
 If the instance of the user is not signed in, it will return an error.
 Asynchronous
 @param callBack Block that will be called when the method returns
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 @param match KKMatch object representing the newly created match
 */
- (void) asyncCreateRandomMatch:(void (^)(KKReturnStatus*, KKMatch*)) callback;

/**
 Method that requests the list of achievements for the current application
 If the instance of the user is not signed in, it will return an error.
 Asynchronous
 @param callBack Block that will be called when the method returns
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 @param achievements NSArray of KKAchievement objects representing the applicaiton's achievements
 */
- (void) asyncGetAppAchievements:(void (^)(KKReturnStatus*, NSArray*)) callback;

/**
 Method that requests the list of achievements achieved by the user
 If the instance of the user is not signed in, it will return an error.
 Asynchronous
 @param callBack Block that will be called when the method returns
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 @param achievements NSArray of KKAchievement objects representing the user's achievements
 */
- (void) asyncGetAchievements:(void (^)(KKReturnStatus*, NSArray*)) callback;

/**
 Method that requests the list of items available for the current applicaiton
 If the instance of the user is not signed in, it will return an error.
 Asynchronous
 @param callBack Block that will be called when the method returns
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 @param items NSArray of KKItems representing the available itmes
 @param bundles NSArray of KKBundles representing the available bundles
 */
- (void) asyncGetAppItems:(void (^)(KKReturnStatus*, NSDictionary*, NSDictionary*)) callback;



/**
 Method that requests the list of items available for the current applicaiton
 If the instance of the user is not signed in, it will return an error.
 Asynchronous
 @param callBack Block that will be called when the method returns
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 @param items NSDictionary with the Key being the KKitem and the value being the quantity
 */
- (void) asyncGetUserItems:(void (^)(KKReturnStatus*, NSDictionary*)) callback;

/**
 Method that adds items to the user's inventory
 If the instance of the user is not signed in, it will return an error.
 Asynchronous
 @param callBack Block that will be called when the method returns
 @param items NSDictionary with the Key being the KKitem items to add and the value being the quantity
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 */
- (void) asyncAddItems:(NSDictionary*) items callback:(void (^)(KKReturnStatus*)) callback;


/**
 Method that removes items to the user's inventory
 If the instance of the user is not signed in, it will return an error.
 Asynchronous
 @param callBack Block that will be called when the method returns
 @param items NSDictionary with the Key being the KKitem and the value being the quantity
 @param returnBlock Return block that will be called before the method returns
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 */

- (void) asyncRemoveItems:(NSDictionary*) items callback: (void (^)(KKReturnStatus*)) callback;



/**
 Method that that buys items and add them to the user's inventory
 If the instance of the user is not signed in, it will return an error.
 Asynchronous
 @param callBack Block that will be called when the method returns
 @param items NSDictionary with the Key being the KKitem and the value being the quantity
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 */
- (void) asyncBuyItems:(NSDictionary*) items callback: (void (^)(KKReturnStatus*)) callback;


/**
 Method that that redeems an Apple store purchase, requires an applestore receipt number
 If the instance of the user is not signed in, it will return an error.
 Asynchronous
 @param callBack Block that will be called when the method returns
 @param items NSDictionary with the Key being the KKitem and the value being the quantity
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 */

- (void) asyncRedeemAppleStorePurchase: (NSString*) receipt callback: (void (^)(KKReturnStatus*, NSDictionary*)) callback ;

/**
 Method that gets the leaderboard data for the current application
 If the instance of the user is not signed in, it will return an error.
 Asynchronous
 @param callBack Block that will be called when the method returns
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 @param leaderboards NSArray of KKLeadeboard objects representing the applicaiton leaderboards
 */
- (void) asyncGetAppLeaderboards:(void (^)(KKReturnStatus*, NSArray*)) callback;

/**
 Method that signs the user in with the Facebook token given. If an account was already connected to the Facebook account, it will be used.
 If not, a new Kumakore account will be created and linked to the facebook account.
 Asynchronous
 @param callBack Block that will be called when the method returns
 @param token NSString containing the facebook token
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 */
- (void) asyncSignInUsingFacebook:(NSString*) token callback:(void (^)(KKReturnStatus*)) callback;


/**
 Method that connects the Facebook account with the currently signed in user
 If the instance of the user is not signed in, it will return an error.
 If the user already has an associated facebook account, it will return an error.
 Asynchronous
 @param callBack Block that will be called when the method returns
 @param token NSString containing the facebook token
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 */

- (void) asyncConnectFacebookAccount: (NSString*) token callback:(void (^)(KKReturnStatus*)) callback;

/**
 Method that disassociates the current user with its associated Facebook account for the current app.
 If the instance of the user is not signed in, it will return an error.
 Asynchronous
 @param callBack Block that will be called when the method returns
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 */
- (void) asyncDeauthorizeFacebookAccount: (void (^)(KKReturnStatus*)) callback;

/**
 Method that requests the list of Facebook friends of the user currently signed in.
 If the instance of the user is not signed in or does not have an associated Facebook account, it will return an error.
 Asynchronous
 @param callback Return block that will be called when the method returns
 @param limit Max number of friends to return
 @param returnStatus KKReturnStatus that contains information about how the method completed.
 @param friendsList NSArray of KKFriend objects
 */
- (void) asyncGetFacebookFriends: (NSString*) token callback:(void (^)(KKReturnStatus*, NSArray*)) callback;


@end
