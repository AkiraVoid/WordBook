using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;

namespace AkiraVoid.WordBook.Utilities;

/// <summary>
/// 包含一系列扩展方法。
/// </summary>
public static class Extensions
{
    /// <summary>
    /// 将字符串的首字母大写。
    /// </summary>
    /// <param name="str"></param>
    /// <returns>首字母大写后的字符串。</returns>
    public static string Capitalize(this string str)
    {
        return str.Length > 1 ? str[0].ToString().ToUpperInvariant() + str[1..] : str.ToUpperInvariant();
    }

    /// <summary>
    /// 提供类似 JavaScript 中 array.map 函数相似的功能。
    /// </summary>
    /// <typeparam name="TSource">原集合中元素的类型。</typeparam>
    /// <typeparam name="TTarget">新集合中元素的类型。</typeparam>
    /// <param name="source"></param>
    /// <param name="conversion">要执行的操作。</param>
    /// <returns>基于 <paramref name="conversion"/> 构建的新集合。</returns>
    /// <remarks>
    /// <b>JavaScript 中 array.map 函数</b>会在数组上执行指定的操作，并将该操作的返回值作为元素构建一个新数组。
    /// </remarks>
    public static IEnumerable<TTarget> Map<TSource, TTarget>(
        this IEnumerable<TSource> source,
        Func<TSource, TTarget> conversion)
    {
        var list = new List<TTarget>();
        foreach (var item in source)
        {
            list.Add(conversion(item));
        }

        return list.AsEnumerable();
    }

    /// <summary>
    /// 将集合随机打乱。该方法只能用于那些支持随机访问的集合。
    /// </summary>
    /// <typeparam name="T">集合中元素的类型。</typeparam>
    /// <param name="source"></param>
    /// <returns>打乱后的该集合本身。</returns>
    public static IList<T> Shuffle<T>(this IList<T> source)
    {
        // Knuth-Durstenfeld Shuffle 算法
        // 时间 O(n)，空间 O(1)
        for (var i = source.Count - 1; i > 0; i--)
        {
            var j = RandomNumberGenerator.GetInt32(i + 1); // toExclusive 参数不包含临界点，需要 +1
            (source[i], source[j]) = (source[j], source[i]);
        }

        return source;
    }

    /// <summary>
    /// 向集合中添加复数个元素。
    /// </summary>
    /// <typeparam name="T">集合中元素的类型。</typeparam>
    /// <param name="source"></param>
    /// <param name="targets">要添加的元素。</param>
    public static void AddRange<T>(this ObservableCollection<T> source, IEnumerable<T> targets)
    {
        foreach (var target in targets)
        {
            source.Add(target);
        }
    }
}