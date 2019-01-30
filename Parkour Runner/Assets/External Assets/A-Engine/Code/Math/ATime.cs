// A-Engine, Code version: 1

namespace AEngine
{
	public static class ATime 
	{
		/// <summary>
		/// Cashed form for Time.deltaTime.
		/// </summary>
		public static float deltaTime;

		/// <summary>
		/// Cashed form for RealTime.deltaTime.
		/// </summary>
		public static float realDeltaTime;

		/// <summary>
		/// Cashed form between Time.deltaTime and RealTime.deltaTime. If Time.timeScale = 0, then used RealTime.deltaTime. Time.deltaTime can be zero as a variation of pause.
		/// </summary>
		public static float actualDeltaTime;			
	}
}
