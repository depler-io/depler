using System.Reflection;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Depler.Validation;

public static class Must
{
     /// <summary>
    /// Specifies a precondition contract for the enclosing method or property, and throws
    /// an exception if the condition for the contract fails.
    /// </summary>
    /// <typeparam name="TException">The exception to throw if the condition is false.</typeparam>
    /// <param name="condition">The conditional expression to test.</param>
    [ContractAnnotation("halt <= condition: false")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void Be<TException>(bool condition)
        where TException : Exception, new()
    {
        if (!condition)
            throw new TException();
    }
    
    /// <summary>
    /// Specifies a precondition contract for the enclosing method or property, and throws
    /// an exception with the provided message if the condition for the contract fails.
    /// </summary>
    /// <typeparam name="TException">The exception to throw if the condition is false.</typeparam>
    /// <param name="condition">The conditional expression to test.</param>
    /// <param name="userMessage">The message to display if the condition is false.</param>
    [ContractAnnotation("halt <= condition: false")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void Be<TException>(bool condition, string userMessage)
        where TException : Exception, new()
    {
        if (condition)
            return;

        var ex = (TException)Activator.CreateInstance(typeof(TException))!;
        var internalFieldInfo = 
            typeof(TException).GetField(
                "_message", 
                BindingFlags.NonPublic | BindingFlags.Instance);
			
        if (internalFieldInfo != null)
            internalFieldInfo.SetValue(ex, userMessage);

        // ReSharper disable once PossibleNullReferenceException
        throw ex;
    }
    
    /// <summary>
    /// Check if value is null and throws an <see cref="ArgumentNullException"/> if it is true
    /// </summary>
    /// <exception cref="ArgumentNullException">When <paramref name="value"/> is null</exception>
    /// <typeparam name="T">Type of the argument checked for null</typeparam>
    /// <param name="value">Value checked for null</param>
    /// <param name="argumentName">Name of the argument to be put in the exception's text'</param>
    [ContractAnnotation("halt <= value: null")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void NotBeNull<T>(T? value, string? argumentName = null)
        where T : class
    {
        Be<ArgumentNullException>(
            value != null, 
            $"{argumentName ?? "Argument"} cannot be null");
    }

    /// <summary>
    /// Check if value is null or empty string and throws an <see cref="ArgumentOutOfRangeException"/> if it is true
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">When <paramref name="value"/> is null or empty</exception>
    /// <param name="value">Value checked for null</param>
    /// <param name="argumentName">Name of the argument to be put in the exception's text'</param>
    [ContractAnnotation("halt <= value: null")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void NotBeNullOrEmpty(string value, string? argumentName = null)
    {
        Be<ArgumentOutOfRangeException>(
            !string.IsNullOrEmpty(value), 
            $"{argumentName ?? "Argument"} cannot be null or empty string");
    }
    
    /// <summary>
    /// Check if value is an empty collections and throws an <see cref="ArgumentOutOfRangeException"/> if it is true
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">When <paramref name="value"/> is empty</exception>
    /// <param name="value">Value checked for null</param>
    /// <param name="argumentName">Name of the argument to be put in the exception's text'</param>
    [ContractAnnotation("halt <= value: null")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void NotBeEmpty<T>(IEnumerable<T> value, string? argumentName = null)
    {
        Be<ArgumentOutOfRangeException>(
            value.Any(), 
            $"{argumentName ?? "Argument"} cannot be null or empty string");
    }
    
    /// <summary>
    /// Check if value is null or empty collections and throws an <see cref="ArgumentOutOfRangeException"/> if it is true
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">When <paramref name="value"/> is null or empty</exception>
    /// <param name="value">Value checked for null</param>
    /// <param name="argumentName">Name of the argument to be put in the exception's text'</param>
    [ContractAnnotation("halt <= value: null")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void NotBeNullOrEmpty<T>(IEnumerable<T>? value, string? argumentName = null)
    {
        Be<ArgumentOutOfRangeException>(
            value != null && value.Any(), 
            $"{argumentName ?? "Argument"} cannot be null or empty string");
    }
}
