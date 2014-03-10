using UnityEngine;
using System.Collections;

namespace com.company.app
{
		public delegate void ModeChangedDelegate (IAppService app,AppModes previous,AppModes current);

		public interface IAppService
		{

				AppModes currentMode { get; set; }

				event ModeChangedDelegate ModeChanged;
		}

		public enum AppModes
		{
				brand = 1,
				loading = 2,
				home = 3,
				game = 4
		}
}