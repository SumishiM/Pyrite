﻿namespace Pyrite.Attributes
{
    /// <summary>
    /// Indicate Pyrite that the system is a percistent system. Systems using this attribute will be in the <see cref="Game.PercistentWorld"/> only
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
    public class PercistentSystemAttribute : Attribute { }
}
