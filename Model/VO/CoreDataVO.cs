namespace UnityPureMVC.Core.Model.VO
{
    internal enum BlackoutStatus
    {
        NONE,
        IN,
        OUT
    }

    internal sealed class CoreDataVO
    {
        internal string version { get; set; }
        internal string currentEnvironment { get; set; }
        internal string defaultEnvironment { get; set; }
        internal bool isDebug { get; set; }
        internal BlackoutStatus blackoutStatus { get; set; }
    }
}