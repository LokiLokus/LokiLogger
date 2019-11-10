namespace LokiLogger.WebExtension.AppConfig {
	public class CookieConfig
	{
		public CookieConfig()
		{
			LoginPath = "/Login";
			LogoutPath = "/Logout";
			SlidingExpiration = true;
			AccessDeniedPath = "/AccessDenied";
			ExpireTimeSpanInMin = 1000;
		}
		public string LoginPath { get; set; }
		public string LogoutPath { get; set; }
		public string AccessDeniedPath { get; set; }
		public bool SlidingExpiration { get; set; }
		public int ExpireTimeSpanInMin { get; set; }
	}

}