using Reflection.DIContainer.Attributes;

namespace Reflection.Entities
{
    [Export]
    public class Logger
    {
        [Import] private string Name { get; set; }
    }
}