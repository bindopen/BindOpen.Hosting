using BindOpen.Data;

namespace BindOpen.Logging.Events;

/// <summary>
/// This interface defines a conditional event.
/// </summary>
public interface IBdoConditionalEvent : IBdoEvent, IBdoConditional
{
}