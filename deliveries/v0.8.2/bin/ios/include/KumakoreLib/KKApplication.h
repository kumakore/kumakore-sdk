//
//  KumakoreApplication.h
//  KumakoreApplication
//
//  Created by Kumakoe on 8/16/13.
//  Copyright (c) 2013 Kumakore. All rights reserved.
//

#import <Foundation/Foundation.h>
@class KKReturnStatus;
@class KKUser;

/**
 Class that represents a Kumakore Application, stores the api key and any other variables needed
 */
@interface KKApplication : NSObject
{

}

//- (id) init:(NSString*) apiKey appVersion: (NSString*) appVersion;

/**
 * Initializes the KKApplication class, requires an apiKey specific to the Kumakore Application used.
 * @para apiKey Api key
 */
- (id) init:(NSString*) apiKey;

/**
 * Method which sets the Current App Version of the program.  When rest calls are made, 
 * if the App Version is incompatible, a return status of invalid app version will be returned.
 * @param appVersion Current app version
 */
- (void) setAppVersion:(NSString*) appVersion;

/**
 * Method to update the current dashboard version sent through headers to the most updated version.
 * If any method was called that returnd an invalid Dashboard Version status, calling this method will
 * update the version used in headers to the one received by that REST call.  Otherwise a rest call will be made 
 * to obtain the lastest Dashboard version
 * Synchronous
 * @param returnBlock Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 */
- (void) updateDashboardVersion: (void (^)(KKReturnStatus*)) returnBlock;

/**
 * Method that gets the Application Platforms for the current Kumakore Applicaiton
 * Synchronous
 * @param returnBlock Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 * @param min Minimum version allowed
 * @param max Maximum version allowed
 */
- (void) syncGetApplicationPlatforms: (void (^)(KKReturnStatus*, float min, float max)) returnBlock;

/**
 * Method that gets the Application Version for the current Kumakore Applicaiton
 * Synchronous
 * @param returnBlock Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 * @param version Current apllication version 
 */
- (void) syncGetAppVersion: (void (^)(KKReturnStatus*, float version)) returnBlock;

/**
 * Method that gets the Application Platforms for the current Kumakore Applicaiton
 * Asynchronous
 * @param callBack Block that will be called when the method finishes
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 * @param min Minimum version allowed
 * @param max Maximum version allowed
 */
- (void) asyncGetApplicationPlatforms:(void (^)(KKReturnStatus*, float min, float max)) callback;

/**
 * Method that gets the Application Version for the current Kumakore Applicaiton
 * Asynchronous
 * @param callBack Block that will be called when the method finishes
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 * @param version Current apllication version
 */
- (void) asyncGetAppVersion: (void (^)(KKReturnStatus*, float version)) callback;

/**
 * Method that gets the Application Rewards
 * Synchronous
 * @param returnBlock Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 * @param signupRewards NSDictionary of items as keys and quantities as their value as a reward for signing up
 * @param facebookSignupRewards NSDictionary of items as keys and quantities as their value as a reward for signing up with facebook
 * @param facebookMatchRewards NSDictionary of items as keys and quantities as their value as a reward for a match through facebook
 */
- (void) syncGetAppRewards: (void (^)(KKReturnStatus*, NSDictionary* signupRewards, NSDictionary* facebookSignupRewards, NSDictionary* facebookMatchRewards)) returnBlock;

/**
 * Method to create a new log message
 * Synchronous
 * @param returnBlock Block that will be called before the method returns
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 * @param type message type
 * @param name message name
 * @param level message level
 * @param message message text
 */
- (void) syncCreateNewLogMessage: (NSString*) type name: (NSString*) name level: (NSString*) level message: (NSString*) message  return: (void (^)(KKReturnStatus*)) returnBlock;

/**
 * Method that gets the Application Rewards
 * Asynchronous
 * @param callBack Block that will be called when the method finishes
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 * @param signupRewards NSDictionary of items as keys and quantities as their value as a reward for signing up
 * @param facebookSignupRewards NSDictionary of items as keys and quantities as their value as a reward for signing up with facebook
 * @param facebookMatchRewards NSDictionary of items as keys and quantities as their value as a reward for a match through facebook
 */
- (void) asyncGetAppRewards: (void (^)(KKReturnStatus*, NSDictionary*, NSDictionary*, NSDictionary*)) callback;

/**
 * Method to create a new log message
 * Asynchronous
 * @param callBack Block that will be called when the method finishes
 * @param returnStatus KKReturnStatus that contains information about how the method completed.
 * @param type message type
 * @param name message name
 * @param level message level
 * @param message message text
 */
- (void) asyncCreateNewLogMessage: (NSString*) type name: (NSString*) name level: (NSString*) level message: (NSString*) message callback:(void (^)(KKReturnStatus*)) callback;


@end
