using UnityEngine;
using System.Collections;
using System;
using com.kumakore.plugin.util;

namespace com.kumakore.sample {

	public class KumakoreBehav : MonoBehaviour {

		public KumakoreApp app
		{
			get
			{
				return KumakoreAppSingleBehav.app;
			}
		}

		// returns true if error is handled
		protected virtual bool handleErrorCodes(KumakoreActionBase action) {
			if (action.getCode() == StatusCodes.APP_API_KEY_INVALID) {
				// developer used the wrong API key?
				//...
				
				return true;
			} else if (action.getCode() == StatusCodes.APP_DASHBOARD_VERSION_INVALID) {
				// dashboard version is not valid. update KumakoreApp dashboard version, then retry?
				// the server dashboard version is stored in the action
				// update the KumakoreApp dashboard version to match
				app.setDashboardVersion (action.getDashboardVersion ());
				
				//retry a few times?
				if (action.getExecutions() < 3) {
					//NOTE: no need to provide callback because the action already
					// associated the callback 
					// true to reset the action state
					action.async(true); 
				}
				
				return true;
			} else if (action.getCode() == StatusCodes.APP_SESSION_ID_INVALID) {
				// session id is not valid. get new session id, then retry?
				//..
				return true;
            } else if (action.getCode() == StatusCodes.APP_VERSION_EXPIRED) {
				// app version is not valid. force users to update or not?
				//...
				
				return true;
			} else if (action.getCode() == StatusCodes.NETWORK_ERROR) {
				//...
				return true;
			} else if (action.getCode() == StatusCodes.PROTOCOL_ERROR) {
				//...
				return true;
			} 
			
			return false;
		}
	}
}