using syncinpos.Debugging;

namespace syncinpos
{
    public class syncinposConsts
    {
        public const string LocalizationSourceName = "syncinpos";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "1b5beabac30041c1a692b78919ae3d9f";
    }
}
