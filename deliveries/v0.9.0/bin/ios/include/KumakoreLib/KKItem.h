//
//  KKItem.h
//  KumakoreApplication
//
//  Created by Kumakoe on 10/9/13.
//  Copyright (c) 2013 Kumakore. All rights reserved.
//

#import <Foundation/Foundation.h>

/**
 * Class that represents an item
 * @param itemId
 * @param costInDollars
 * @param createdAt
 * @param updatedAt
 * @param itemName
 * @param itemType
 * @param earnable
 * @param productId
 * @param IAPIds
 */

@interface KKItem : NSObject

@property NSString *itemId;
@property int costInDollars;
@property NSString *createdAt;
@property NSString *updatedAt;
@property NSString *itemName;
@property NSString *itemType;
@property bool earnable;
@property NSString *productId;
@property NSArray *IAPIds;

@end

@implementation KKItem

@end
