// A-Engine, Code version: 1

using UnityEngine;
using System.Collections;

namespace AEngine
{
	public class BaseEngineConstants
	{
		public const string AudioConfigurationPath = "External Assets/A-Engine/Configuration/";
        public const string AudioResConfigurationPath = "A-Engine/Configuration/";
        public const string AudioConfigurationShortFileName = "AudioConfiguration";

		public const string BaseSettingsPath = "A-Engine/Settings/";
		public const string AudioSettingsShortFileName = "AudioSettings";

		public const string MenuDataFileName = "TransitionData.cs";
		public const string MenuDataEnumForMenu = "EMenu";
		public const int MenuDataEnumsOffset = 1;

		public const string AudioNamesFileName = "AudioNames.cs";
		public const string AudioNamesEnumForBlocks = "EAudioBlock";
		public const string AudioNamesEnumForSounds = "ESound";
		public const string AudioNamesEnumForMusics = "EMusic";
		public const int AudioNamesEnumsOffset = 1;
	}
}