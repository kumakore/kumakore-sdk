﻿using System;
using com.kumakore;
using NUnit.Framework;
using System.Threading;
using System.Collections.Generic;

namespace com.kumakore.test
{
    [TestFixture]
	public class TestGift : TestBase
	{   

		private static readonly String RED_BALL_PRODUCT_ID = "com_sinuouscode_red_ball";
		private static readonly int GIFT_COUNT = 1;

		public static void All()
        {
			TestGift gift = new TestGift();
            gift.setup();
			
			// Gift
			gift.SyncGiftGetRequestReceived();
			gift.AsyncGiftGetRequestReceived();
        }
		
		[TestFixtureSetUp]
		public override void setup ()
		{
			base.setup ();
			app1 ().signin(TEST_1, PASSWORD).sync ();
			app2 ().signin(TEST_2, PASSWORD).sync ();
		}

		[Test]
		public void SyncGiftDeleteRedeemable()
		{
			app1().getUser().gifts().get().sync (delegate(ActionGiftGetRedeemable action)
			{
				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				GiftMap gifts =	app1().getUser().gifts();

				//NOTE: gifts.Values or gifts.Keys would both work here =)
				app1().getUser().gifts().delete().addGifts(gifts.Values).sync (delegate(ActionGiftDeleteRedeemable action2)
				{
					Assert.AreEqual(StatusCodes.SUCCESS, action2.getCode());
				});
			});
		}

		[Test]
		public void AsyncGiftDeleteRedeemable()
		{
			app1().getUser().gifts().get().sync (delegate(ActionGiftGetRedeemable action)
			{
				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				GiftMap gifts =	app1().getUser().gifts();

				//NOTE: gifts.Values or gifts.Keys would both work here =)
				app1().getUser().gifts().delete().addGifts(gifts.Values).sync (delegate(ActionGiftDeleteRedeemable action2)
				{
					Release();
					Assert.AreEqual(StatusCodes.SUCCESS, action2.getCode());
				});

				Wait();
			});
		}

		[Test]
		public void SyncGiftGetRedeem()
		{
			String giftId = String.Empty;

			app1().getUser().gifts().giveUserName(TEST_2, RED_BALL_PRODUCT_ID, 1).sync (delegate(ActionGiftGive action) {

				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				giftId =	action.getGiftId();
			});

			app2().getUser().gifts().redeem().addGiftId(giftId).sync (delegate(ActionGiftRedeem action)
			{
				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
			});
		}

		[Test]
		public void AsyncGiftGetRedeem()
		{
			String giftId = String.Empty;

			app1().getUser().gifts().giveUserName(TEST_2, RED_BALL_PRODUCT_ID, 1).sync (delegate(ActionGiftGive action) {
		
				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				giftId =	action.getGiftId();
			});

			app2().getUser().gifts().redeem().addGiftId(giftId).async (delegate(ActionGiftRedeem action)
			{
				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				Release();
			});

			Wait();
		}

		[Test]
		public void SyncGiftGetRedeemable()
		{
			app1().getUser().gifts().get().sync (delegate(ActionGiftGetRedeemable action)
			{
				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
			});
		}

		[Test]
		public void AsyncGiftGetRedeemable()
		{
			app1().getUser().gifts().get().async (delegate(ActionGiftGetRedeemable action)
			{
				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				Release();
			});

			Wait();
		}

		[Test]
		public void SyncGiftGive()
		{
			app1().getUser().gifts().giveUserName(TEST_2, RED_BALL_PRODUCT_ID, GIFT_COUNT).sync (delegate(ActionGiftGive action)
			{
				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
			});
		}

		[Test]
		public void AsyncGiftGive()
		{
			app1().getUser().gifts().giveUserName(TEST_2, RED_BALL_PRODUCT_ID, GIFT_COUNT).async (delegate(ActionGiftGive action)
			{
				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				Release();
			});

			Wait();
		}

		[Test]
		public void SyncGiftRequest()
		{
			app1().getUser().gifts().request().addUserName(TEST_2).
			addItem(RED_BALL_PRODUCT_ID, 1).sync (delegate(ActionGiftRequest action)
			{
				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
			});
		}

		[Test]
		public void AsyncGiftRequest()
		{
			app1().getUser().gifts().request().addUserName(TEST_2).
			addItem(RED_BALL_PRODUCT_ID, 1).async (delegate(ActionGiftRequest action)
			{
				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				Release();
			});

			Wait();
		}

		[Test]
		public void SyncGiftDeleteRequestReceived()
		{
			app1().getUser().gifts().requestsReceived().get().sync (delegate(ActionGiftGetRequestReceived action)
			{
				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				GiftRequestReceivedMap gifts = app1().getUser().gifts().requestsReceived();

				//NOTE: gifts.Values or gifts.Keys would both work here =)
				app1().getUser().gifts().requestsReceived().delete().addRequests(gifts.Values).sync (delegate(ActionGiftDeleteRequestReceived action2)
				{
						Assert.AreEqual(StatusCodes.SUCCESS, action2.getCode());
				});
			});
		}

		[Test]
		public void AsyncGiftDeleteRequestReceived()
		{		
			app1().getUser().gifts().requestsReceived().get().sync (delegate(ActionGiftGetRequestReceived action)
			{
				Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
				GiftRequestReceivedMap gifts = app1().getUser().gifts().requestsReceived();

				//NOTE: gifts.Values or gifts.Keys would both work here =)
				app1().getUser().gifts().requestsReceived().delete().addRequests(gifts.Values).async (delegate(ActionGiftDeleteRequestReceived action2)
				{
					Release();
					Assert.AreEqual(StatusCodes.SUCCESS, action2.getCode());
				});
							
				Wait();
			});
		}

		[Test]
		public void SyncGiftGetRequestReceived()
        {
			app1().getUser().gifts().requestsReceived().get().sync (delegate(ActionGiftGetRequestReceived action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
            });
        }

        [Test]
		public void AsyncGiftGetRequestReceived()
        {
			app1().getUser().gifts().requestsReceived().get().async (delegate(ActionGiftGetRequestReceived action)
            {
                Assert.AreEqual(StatusCodes.SUCCESS, action.getCode());
                Release();
            });

            Wait();
        }
	}
}