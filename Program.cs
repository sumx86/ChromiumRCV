﻿using System;
using System.Collections.Generic;
using System.IO;

namespace ChromeRCV
{
    class Program
    {
        private static string LocalAppdata = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        private static List<string> Browsers = new List<string>
        {
            LocalAppdata + "\\Google\\Chrome",
            LocalAppdata + "\\Microsoft\\Edge",
            LocalAppdata + "\\Microsoft\\Edge Beta",
            LocalAppdata + "\\Maxthon\\Application"
        };

        public static Dictionary<string, List<ChromiumLogin>> GetLogins()
        {
            Dictionary<string, List<ChromiumLogin>> d = new Dictionary<string, List<ChromiumLogin>>();

            ChromiumLoginManager manager = new ChromiumLoginManager();
            foreach (var browserPath in Browsers) {
                if (Directory.Exists(browserPath)) {
                    if(manager.Reinitialize(browserPath) == null)
                        continue;
                    
                    List<ChromiumLogin> logins = manager.GetData();
                    d.Add(manager.Browser, logins);
                }
            }
            return d;
        }

        public static Dictionary<string, List<ChromiumHistory>> GetHistory()
        {   
            Dictionary<string, List<ChromiumHistory>> d = new Dictionary<string, List<ChromiumHistory>>();

            ChromiumHistoryManager manager = new ChromiumHistoryManager();
            foreach (var browserPath in Browsers) {
                if (Directory.Exists(browserPath)) {
                    if (manager.Reinitialize(browserPath) == null)
                        continue;

                    List<ChromiumHistory> history = manager.GetData();
                    d.Add(manager.Browser, history);
                }
            }
            return d;
        }

        public static Dictionary<string, List<ChromiumBookmark>> GetBookmarks()
        {
            Dictionary<string, List<ChromiumBookmark>> d = new Dictionary<string, List<ChromiumBookmark>>();

            ChromiumBookmarksManager manager = new ChromiumBookmarksManager();
            foreach (var browserPath in Browsers) {
                if (Directory.Exists(browserPath)) {
                    if (manager.Reinitialize(browserPath) == null)
                        continue;

                    List<ChromiumBookmark> bookmarks = manager.GetData();
                    d.Add(manager.Browser, bookmarks);
                }
            }
            return d;
        }

        public static Dictionary<string, List<ChromiumCookie>> GetCookies()
        {
            Dictionary<string, List<ChromiumCookie>> d = new Dictionary<string, List<ChromiumCookie>>();

            ChromiumCookiesManager manager = new ChromiumCookiesManager();
            foreach (var browserPath in Browsers) {
                if (Directory.Exists(browserPath)) {
                    if (manager.Reinitialize(browserPath) == null)
                        continue;

                    List<ChromiumCookie> cookies = manager.GetData();
                    d.Add(manager.Browser, cookies);
                }
            }
            return d;
        }

            public static void Usage()
        {
            Console.WriteLine(
                @"usage: ./ChromeRCV arg
Arguments: 
    cookies   - show all chromium cookies
    passwords - show all chromium passwords
    history   - show all chromium history
    bookmarks - show all chromium bookmarks"
            );
            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Usage();
                return;
            }

            if (args[0] == "passwords")
                logins = true;

            else if (args[0] == "cookies")
                cookies = true;

            else if (args[0] == "history")
                history = true;

            else if (args[0] == "bookmarks")
                bookmarks = true;

            if (logins) {
                Dictionary<string, List<ChromiumLogin>> loginData = GetLogins();
                if(loginData.Count > 0) {
                    foreach (KeyValuePair<string, List<ChromiumLogin>> entry in loginData) {
                        var browser = entry.Key;
                        Console.WriteLine($"\r\n==={browser.ToUpper()}===");

                        foreach (var login in entry.Value) {
                            if (!string.IsNullOrEmpty(login.Password)) {
                                login.Print();
                            }
                        }
                    }
                }
            }

            if(cookies) {
                Dictionary<string, List<ChromiumCookie>> cookiesData = GetCookies();
                if (cookiesData.Count > 0) {
                    foreach (KeyValuePair<string, List<ChromiumCookie>> entry in cookiesData) {
                        var browser = entry.Key;
                        Console.WriteLine($"\r\n==={browser.ToUpper()}===");

                        foreach (var cookie in entry.Value) {
                            cookie.Print();
                        }
                    }
                }
            }

            if(history) {
                Dictionary<string, List<ChromiumHistory>> historyData = GetHistory();
                if (historyData.Count > 0) {
                    foreach (KeyValuePair<string, List<ChromiumHistory>> entry in historyData) {
                        var browser = entry.Key;
                        Console.WriteLine($"\r\n==={browser.ToUpper()}===");

                        foreach (var historyEntry in entry.Value) {
                            historyEntry.Print();
                        }
                    }
                }
            }

            if(bookmarks) {
                Dictionary<string, List<ChromiumBookmark>> bookmarksData = GetBookmarks();
                if (bookmarksData.Count > 0) {
                    foreach (KeyValuePair<string, List<ChromiumBookmark>> entry in bookmarksData) {
                        var browser = entry.Key;
                        Console.WriteLine($"\r\n==={browser.ToUpper()}===");

                        foreach (var bookmarkEntry in entry.Value) {
                            bookmarkEntry.Print();
                        }
                    }
                }
            }
            Console.ReadKey();
        }

        #region cmdvars
            private static bool logins  = false;
            private static bool cookies = false;
            private static bool history = false;
            private static bool bookmarks = false;
        #endregion
    }
}
