package com.kumakore.sample;

public class Helpers {

    private static final TestUsers ACTIVE_TEST_USER = TestUsers.ThomasEdison;
    
    public static String VIRTUAL_STORE_ITEM = "coins";
    
	public enum TestUsers {
		AndyWarhol (0), // CHBFIV - DEV - APP
		SebastianBach (1), // CHBFIV - DEV - ANDROID API KEY
		ClaudeMonet   (2), // CHBFIV - ALPHA - APP
		CharlesDarwin   (3), // CHBFIV - ALPHA - ANDROID API KEY
		ThomasEdison    (4),// OTTO - ALPHA - ANDROID API KEY
		BenjaminFranklin    (5);

	    private final int _id; 
	    TestUsers(int id) {
	    	_id = id;
	    }
	}

    public static String GetApiKey()
    {
        switch(ACTIVE_TEST_USER)
        {
            case AndyWarhol:
                return "729683e2d7dc955fb32a44b1958f8d1b";
            case SebastianBach:
                return "242d2176d6bd28ed21d0265a47061aa8";
            case ClaudeMonet:
                return "292c9e31e5187c58b58f5f8588edc92d";
            case CharlesDarwin:
                return "efc3993b2afd05a99f978342604046bc";
            case ThomasEdison:
                return "60cbe9a5d89b09b6096e178450021505";
            default:
                return "";
        }
    }

    public static int GetDashboardVersion()
    {
        switch (ACTIVE_TEST_USER)
        {
            case AndyWarhol:
            case SebastianBach:
                return 1381086834;
            case ClaudeMonet:
            case CharlesDarwin:
                return 1375571696;
            case ThomasEdison:
                return 1375571696;
            default:
                return -1;
        }
    }
    
    public static float GetAppVersion()
    {
        switch (ACTIVE_TEST_USER)
        {
            case AndyWarhol:
            case SebastianBach:
                return 0;
            case ClaudeMonet:
            case CharlesDarwin:
                return 0;
            case ThomasEdison:
                return 1.0F;
            default:
                return 0;
        }
    }

    public static String GetGsmSenderId()
    {
        switch (ACTIVE_TEST_USER)
        {
            case AndyWarhol:
            case SebastianBach:
            case ClaudeMonet:
            case CharlesDarwin:
                return "119922458786";
            case ThomasEdison:
            	return "119922458786";
            default:
                return "";
        }
    }

    public static String GetFacebookToken()
    {
        switch (ACTIVE_TEST_USER)
        {
            case ThomasEdison:
            	return "CAAGs9CrETEgBADjrytB7AMnQjAfbQQOWJWC1JxkaUFF26601inHEqnaH2yarWxIsqMnNW1GbOd0Wczkdgbxog2QxBYjQ9vmTuOUrVURmrxQ0AiZC7ThUyxqvc3rN40SJ9hs33rTwnVPVGxZCeI22A9JU9mvey3qd4srYeowGIT0wQLQyn6wjWd88ZAP8jWIe9sjxtz7b3t2ICQzkRWmInm50PksbCJnfRivxvwBFAZDZD";
            default:
                return "";
        }
    }
}