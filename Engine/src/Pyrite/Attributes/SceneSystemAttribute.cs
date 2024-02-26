namespace Pyrite.Attributes
{
    /// <summary>
    /// Indicate Pyrite that the system is used in every scene
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
    public class SceneSystemAttribute : Attribute { }
}
