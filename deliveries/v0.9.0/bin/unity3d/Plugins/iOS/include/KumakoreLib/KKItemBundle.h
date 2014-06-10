//
//  KKItemBundle.h
//  KumakoreApplication
//
//  Created by Kumakoe on 10/9/13.
//  Copyright (c) 2013 Kumakore. All rights reserved.
//

#import <Foundation/Foundation.h>

/**
 * Class that represents a bundle
 * @param bundleId
 * @param costInDollars
 * @param createdAt
 * @param updatedAt
 * @param itemName
 * @param itemType
 * @param earnable
 * @param productId
 * @param rewardItems
 * @param IAPIds
 */
@interface KKItemBundle : NSObject

@property NSString *bundleId;
@property int costInDollars;
@property NSString *createdAt;
@property NSString *updatedAt;
@property NSString *itemName;
@property NSString *itemType;
@property bool earnable;
@property NSString *productId;
@property NSDictionary *rewardItems;
@property NSArray *IAPIds;

@end

@implementation KKItemBundle

@end
