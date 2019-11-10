namespace LokiLogger.WebExtension.AppConfig {
	public class IdentityConfig
	{

		public IdentityConfig()
		{
			User = new UserConfig();
			Password = new PasswordConfig();
			Lockout = new LockoutConfig();
			SignIn = new SignInConfig();
		}
		public UserConfig User { get; set; }
		public PasswordConfig Password { get; set; }
		public LockoutConfig Lockout { get; set; }
		public SignInConfig SignIn { get; set; }

	}

	public class UserConfig
	{
		public bool RequireUniqueEmail { get; set; }
	}

	public class PasswordConfig
	{

		public PasswordConfig()
		{
			RequireDigit = false;
			RequiredLength = 2;
			RequireLowercase = false;
			RequireUppercase = false;
			RequireNonAlphanumeric = false;
		}
		public int RequiredLength { get; set; }
		public bool RequireLowercase { get; set; }
		public bool RequireUppercase { get; set; }
		public bool RequireDigit { get; set; }
		public bool RequireNonAlphanumeric { get; set; }
	}

	public class LockoutConfig
	{
		public LockoutConfig()
		{
			AllowedForNewUsers = true;
			MaxFailedAccessAttempts = 1000;
			DefaultLockoutTimeSpanInMins = 1000;
		}
        
		public bool AllowedForNewUsers { get; set; }
		public int DefaultLockoutTimeSpanInMins { get; set; }
		public int MaxFailedAccessAttempts { get; set; }
	}

	public class SignInConfig {
		public SignInConfig()
		{
            
		}
		public bool RequireConfirmedEmail { get; set; }
		public bool RequireConfirmedPhoneNumber { get; set; }
	}

}