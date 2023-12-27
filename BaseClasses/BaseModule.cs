using AchievementTracker.Utils;

namespace AchievementTracker.BaseClasses
{
    /// <summary>
    /// The base class for a <see cref="DSharpPlus.CommandsNext.CommandsNextModule"/> module.
    /// </summary>
    public class BaseModule
    {
        public BaseModule()
        {
            Debug.Info($"\tLoading module \"{GetType().Name}\".");
        }
    }
}
