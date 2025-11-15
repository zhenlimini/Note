using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MarkdownLinkGenerator
{
    class Program
    {
        private static readonly List<string> _excludedFolders = new List<string> { ".git", "bin", "obj", ".vs" };
        private static readonly List<string> _excludedExtensions = new List<string> { ".png", ".jpg", ".jpeg", ".gif", ".bmp", ".ico" };

        static void Main(string[] args)
        {
            // 默认路径设置为上一层目录
            string repositoryPath = args.Length > 0 ? args[0] : Directory.GetParent(Directory.GetCurrentDirectory())?.FullName;
            
            // 输出文件也生成到上一层目录
            string outputFileName = args.Length > 1 ? args[1] : "README.md";
            string outputFile = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())?.FullName ?? Directory.GetCurrentDirectory(), outputFileName);
            
            // 如果上一层目录获取失败，使用当前目录
            if (string.IsNullOrEmpty(repositoryPath))
            {
                repositoryPath = Directory.GetCurrentDirectory();
                Console.WriteLine("警告: 无法获取上一层目录，使用当前目录");
            }
            
            try
            {
                string markdownContent = GenerateMarkdownLinks(repositoryPath);
                File.WriteAllText(outputFile, markdownContent.Trim()); // 去除首尾空行
                Console.WriteLine($"Markdown目录已生成: {outputFile}");
                Console.WriteLine($"扫描路径: {repositoryPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"错误: {ex.Message}");
            }
        }

        static string GenerateMarkdownLinks(string rootPath)
        {
            var lines = new List<string>();
            
            // 添加固定内容：标题和介绍
            lines.Add("# 学习笔记");
            lines.Add("");
            lines.Add("**道阻且长，行则将至。**");
            lines.Add("");
            lines.Add("## 目录结构");
            lines.Add("");
            
            // 获取根目录下的所有子目录（不包含根目录本身）
            var subDirectories = Directory.GetDirectories(rootPath)
                                        .Where(d => !_excludedFolders.Contains(Path.GetFileName(d)))
                                        .OrderBy(d => d)
                                        .ToArray();

            // 递归处理每个子目录
            foreach (string subDir in subDirectories)
            {
                string subDirContent = GenerateDirectoryContent(rootPath, subDir, 0);
                if (!string.IsNullOrEmpty(subDirContent))
                {
                    lines.Add(subDirContent);
                }
            }

            // 过滤掉空行，只返回有内容的行
            var nonEmptyLines = lines.Where(line => !string.IsNullOrWhiteSpace(line)).ToList();
            
            if (nonEmptyLines.Count == 0)
            {
                // 如果没有找到任何内容，仍然保留标题和介绍
                lines.RemoveRange(4, lines.Count - 4); // 移除"## 目录结构"之后的内容
                return string.Join(Environment.NewLine, lines);
            }
                
            return string.Join(Environment.NewLine, nonEmptyLines);
        }

        static string GenerateDirectoryContent(string rootPath, string currentPath, int level)
        {
            var lines = new List<string>();
            
            // 获取当前目录信息
            var directoryInfo = new DirectoryInfo(currentPath);
            
            // 排除不需要的文件夹
            if (_excludedFolders.Contains(directoryInfo.Name) || directoryInfo.Name.StartsWith("."))
                return string.Empty;

            // 获取所有Markdown文件，排除其他类型
            var mdFiles = Directory.GetFiles(currentPath, "*.md")
                                  .Where(f => !_excludedExtensions.Contains(Path.GetExtension(f).ToLower()))
                                  .OrderBy(f => f)
                                  .ToArray();

            // 获取子目录
            var subDirectories = Directory.GetDirectories(currentPath)
                                        .Where(d => !_excludedFolders.Contains(Path.GetFileName(d)))
                                        .OrderBy(d => d)
                                        .ToArray();

            // 生成当前目录的标题
            string directoryName = Path.GetFileName(currentPath);
            string indent = new string(' ', level * 2);
            lines.Add($"{indent}- **{directoryName}**");

            // 生成文件的Markdown链接
            foreach (string file in mdFiles)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                string relativePath = GetRelativePath(rootPath, file);
                string fileIndent = new string(' ', (level + 1) * 2);
                string encodedPath = relativePath.Replace(" ", "%20");
                lines.Add($"{fileIndent}- [{fileName}]({encodedPath})");
            }

            // 递归处理子目录
            foreach (string subDir in subDirectories)
            {
                string subDirContent = GenerateDirectoryContent(rootPath, subDir, level + 1);
                if (!string.IsNullOrEmpty(subDirContent))
                {
                    lines.Add(subDirContent);
                }
            }

            // 过滤掉空行，只返回有内容的行
            var nonEmptyLines = lines.Where(line => !string.IsNullOrWhiteSpace(line)).ToList();
            
            if (nonEmptyLines.Count == 0)
                return string.Empty;
                
            return string.Join(Environment.NewLine, nonEmptyLines);
        }

        static string GetRelativePath(string rootPath, string fullPath)
        {
            Uri rootUri = new Uri(rootPath + Path.DirectorySeparatorChar);
            Uri fullUri = new Uri(fullPath);
            return Uri.UnescapeDataString(rootUri.MakeRelativeUri(fullUri).ToString());
        }
    }
}