## 基于DFA(Deterministic Finite Automaton 确定有穷自动机)算法，高性能敏感词过滤。基于MIT协议
## 支持敏感词检测和替换（默认替换成*）
## 代码尽量简化和备注了。(适用c#语言)

# 增加示例：

      var sensitiveWords = new List<string> { "bad", "evil", "八嘎" };
      SensitiveWordsDetection.InitSensitiveWords(sensitiveWords);
      var input = "This is a bad 八嘎 food";
      if (SensitiveWordsDetection.CheckSensitiveWords(input))
      {
      Debug.Log("Sensitive word detected!");
      }

      Debug.Log("Sensitive:" + SensitiveWordsDetection.ReplaceSensitiveWords(input));
