using UnityEngine;
using System.Collections;

public interface IAppService {

	AppModes CurrentMode { get; set; }
}

public enum AppModes
{
	brand = 1,
	loading = 2,
	home = 3,
	game = 4
}