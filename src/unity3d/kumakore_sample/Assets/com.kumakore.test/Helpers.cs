using System;

namespace com.kumakore.test
{
	public class Helpers
    {
        private static readonly TestUsers ACTIVE_TEST_USER = TestUsers.CarlosFernandez;
        
	    public enum TestUsers
	    {
            AndyWarhol = 0, // CHBFIV - DEV - APP
            SebastianBach = 1, // CHBFIV - DEV - ANDROID API KEY
            ClaudeMonet = 2, // CHBFIV - ALPHA - APP
            CharlesDarwin = 3, // CHBFIV - ALPHA - ANDROID API KEY
            ThomasEdison = 4,
            BenjaminFranklin = 5,
			CarlosFernandez = 6,
	    }

        public static String GetApiKey()
        {
            switch(ACTIVE_TEST_USER)
            {
                case TestUsers.AndyWarhol:
                    return "729683e2d7dc955fb32a44b1958f8d1b";
                case TestUsers.SebastianBach:
                    return "242d2176d6bd28ed21d0265a47061aa8";
                case TestUsers.ClaudeMonet:
                    return "292c9e31e5187c58b58f5f8588edc92d";
                case TestUsers.CharlesDarwin:
                    return "e3198395da8e81cc27dffffd6ebdcb45";
				case TestUsers.CarlosFernandez:
					return "78ed3add3e4d66b818fbf1abb6689855";
                default:
                    return String.Empty;
            }
        }

        public static int GetDashboardVersion()
        {
            switch (ACTIVE_TEST_USER)
            {
                case TestUsers.AndyWarhol:
                case TestUsers.SebastianBach:
                    return 1381086834;
                case TestUsers.ClaudeMonet:
                case TestUsers.CharlesDarwin:
                    return 1375571696;
				case TestUsers.CarlosFernandez:
					return 1386951174;
                default:
                    return -1;
            }
        }

        public static String GetGsmSenderId()
        {
            switch (ACTIVE_TEST_USER)
            {
                case TestUsers.AndyWarhol:
                case TestUsers.SebastianBach:
                case TestUsers.ClaudeMonet:
                case TestUsers.CharlesDarwin:
				case TestUsers.CarlosFernandez:
                    return "119922458786";
                default:
                    return String.Empty;
            }
        }
	}
}