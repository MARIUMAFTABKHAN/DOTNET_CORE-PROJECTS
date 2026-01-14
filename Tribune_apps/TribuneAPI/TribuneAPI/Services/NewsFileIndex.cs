using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Logging;

namespace TribuneAPI.Services
{
    public class NewsFileIndex : INewsFileIndex
    {
        private readonly ILogger<NewsFileIndex> _logger;
        private readonly string _rootPath = @"\\172.18.11.9\vfs\NEWS";
        private readonly Dictionary<string, string> _fileIndex = new(StringComparer.OrdinalIgnoreCase);

        public NewsFileIndex(ILogger<NewsFileIndex> logger)
        {
            _logger = logger;
            BuildIndex();
        }

        private void BuildIndex()
        {
            _logger.LogInformation("📂 Building file index from: {Path}", _rootPath);
            int count = 0;

            try
            {
                foreach (var file in Directory.EnumerateFiles(_rootPath, "*.*", SearchOption.AllDirectories))
                {
                    var fileName = Path.GetFileName(file);
                    count++;

                    if (!_fileIndex.ContainsKey(fileName))
                    {
                        _fileIndex[fileName] = file;
                        _logger.LogDebug("Indexed: {FileName} → {FullPath}", fileName, file);
                    }
                }

                _logger.LogInformation("✅ Indexed {Count} files from NEWS share", count);
                _logger.LogInformation("✅ FileIndex contains {IndexCount} unique entries", _fileIndex.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error indexing NEWS folder");
            }
        }


        public string? GetFilePath(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return string.Empty;

            fileName = fileName.Trim();
            _logger.LogInformation("🔍 Resolving file: {Input}", fileName);

            if (_fileIndex.TryGetValue(fileName, out var exactMatch))
            {
                _logger.LogInformation("✅ Exact match found: {Path}", exactMatch);
                return exactMatch;
            }

            string targetBase = Normalize(fileName);

            foreach (var kv in _fileIndex)
            {
                string currentBase = Normalize(kv.Key);
                if (currentBase.Contains(targetBase) || targetBase.Contains(currentBase))
                {
                    _logger.LogInformation("⚠️ Fuzzy match fallback: {Key} → {Path}", kv.Key, kv.Value);
                    return kv.Value;
                }
            }

            _logger.LogWarning("❌ No file found for: {FileName}", fileName);
            return string.Empty;
        }
        private string Normalize(string s)
        {
            return Path.GetFileNameWithoutExtension(s)
                       .ToLowerInvariant()
                       .Replace(" ", "")
                       .Replace("-", "")
                       .Replace("_", "");
        }

    }
}
