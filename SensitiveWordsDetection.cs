using System.Text;
using System.Collections.Generic;
/// <summary>
/// 敏感词汇检测
/// </summary>
public class SensitiveWordsDetection
{
    /// <summary>
    /// 词汇节点
    /// </summary>
    public class TrieNode
    {
        /// <summary>
        /// 敏感词是否结束
        /// </summary>
        /// <value></value>
        public bool IsEndOfWord { get; set; }
        /// <summary>
        /// 敏感词子节点
        /// </summary>
        /// <typeparam name="char"></typeparam>
        /// <typeparam name="TrieNode"></typeparam>
        /// <returns></returns>
        public Dictionary<char, TrieNode> Children { get; } = new Dictionary<char, TrieNode>();
    }
    /// <summary>
    /// 词汇树根节点
    /// </summary>
    /// <returns></returns>
    private static TrieNode _root = new TrieNode();
    /// <summary>
    /// 初始化敏感树
    /// </summary>
    public static void InitSensitiveWords(List<string> sensitiveWords)
    {
        foreach (var word in sensitiveWords)
        {
            var current = _root;
            foreach (var _char in word)
            {
                if (!current.Children.TryGetValue(_char, out var next))
                {
                    next = new TrieNode();
                    current.Children[_char] = next;
                }
                current = next;
            }
            current.IsEndOfWord = true; // 标记单个敏感词汇结束
        }
    }
    /// <summary>
    /// 检测是否有敏感词
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static bool CheckSensitiveWords(string input)
    {
        var current = _root;
        for (int i = 0; i < input.Length; i++)
        {
            if (current.Children.TryGetValue(input[i], out var next))
            {
                current = next;
            }
            else
            {
                current = _root;
            }
            if (current.IsEndOfWord)
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 替换敏感词
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string ReplaceSensitiveWords(string input, char replace = '*')
    {
        var output = new StringBuilder();
        var current = _root;
        var start = 0;
        for (int i = 0; i < input.Length; i++)
        {
            if (current.Children.TryGetValue(input[i], out var next))
            {
                current = next;
            }
            else
            {
                output.Append(input[start]);
                current = _root;
                start = i + 1;
            }
            if (current.IsEndOfWord)
            {
                output.Append(replace, i - start + 1);
                current = _root;
                start = i + 1;
            }
        }
        output.Append(input.Substring(start));
        return output.ToString();
    }
}
