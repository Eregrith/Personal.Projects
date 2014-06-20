using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Tests
{
    public class BoursoKamaReader
    {
        private CookieCollection Cookies { get; set; }

        public enum Servers
        {
            Djaul = 3,
            Hyrkul = 17,
            Agride = 36
        };

        private Boolean DEBUG = false;

        public BoursoKamaReader()
        {
            Cookies = new CookieCollection();
        }

        public BigInteger GetFor(String accountName, String password, Servers server)
        {
            BigInteger kamas = 0;
            try
            {
                if (!Login(accountName, password))
                    throw new Exception("Could not login");

                if (!SelectServer(server))
                    throw new Exception("Could not select server");

                kamas = GetKamas();
                if (DEBUG) Console.WriteLine("You have {0} kamas", kamas);
            }
            catch (Exception e)
            {
                DebugExceptionToConsole(e);
            }
            return kamas;
        }

        private void Unblock()
        {
            AddShieldCookie();
            //if (DEBUG) Console.WriteLine("Trying to unblock");

            //String url = "https://account.ankama.com/votre-compte/securite-invalide?f=http://www.dofus.com/fr/achat-bourses-kamas-ogrines";

            //HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            //req.CookieContainer = new CookieContainer();
            //req.CookieContainer.Add(Cookies);

            ////req.CookieContainer.Add(new Cookie("PHPSESSID", phpsessid) { Domain = "www.newbiecontest.org" });

            ////req.CookieContainer.Add(new Cookie("SMFCookie89", smfCookie89) { Domain = "www.newbiecontest.org" });

            //req.Method = "POST";
            //req.Accept = "*/*";
            ////req.KeepAlive = true;
            //req.ContentType = "application/x-www-form-urlencoded";
            ////req.Referer = "http://www.dofus.com/fr/achat-bourses-kamas-ogrines/selection-serveur";
            ////req.UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/35.0.1916.153 Safari/537.36";
            ////req.Headers.Add("Accept-Encoding:gzip,deflate,sdch");
            //req.Headers.Add("Accept-Language:fr-FR,fr;q=0.8,en-US;q=0.6,en;q=0.4");
            //req.AllowAutoRedirect = true;

            //String post = "step=2";

            //byte[] bytes = System.Text.Encoding.ASCII.GetBytes(post);
            //req.ContentLength = bytes.Length;
            //System.IO.Stream os = req.GetRequestStream();
            //os.Write(bytes, 0, bytes.Length);
            //os.Close();

            //if (DEBUG) Console.WriteLine("Getting page {0}", url);

            //TimeSpan t1 = DateTime.Now.TimeOfDay;
            //TimeSpan t2;
            //String html = String.Empty;
            //BigInteger kamas = 0;
            //using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
            //{
            //    t2 = DateTime.Now.TimeOfDay;

            //    StringBuilder sb = new StringBuilder();
            //    Stream str = resp.GetResponseStream();
            //    String tmp;
            //    Byte[] buf = new Byte[8192];
            //    int count;
            //    do
            //    {
            //        count = str.Read(buf, 0, buf.Length);
            //        if (count > 0)
            //        {
            //            tmp = Encoding.ASCII.GetString(buf, 0, count);

            //            sb.Append(tmp);
            //        }
            //    } while (count > 0);

            //    html = sb.ToString();

            //    if (DEBUG)
            //    {
            //        Console.WriteLine("Page url :{0}", resp.ResponseUri);
            //        Console.WriteLine("Headers :");
            //        foreach (String header in resp.Headers.AllKeys)
            //        {
            //            Console.WriteLine("{0}:{1}", header, resp.Headers[header]);
            //        }
            //        Console.WriteLine("Contents:");
            //        Console.WriteLine(html);
            //    }

            //    //String[] lines = html.Split(new Char[] { '\n' });
            //    //int i;
            //    //Regex r = new Regex(@"<span class=""kamas"">");
            //    //for (if = 0; i < lines.Count(); i++)
            //    //{
            //    //    Match m = r.Match(lines[i]);
            //    //    if (m.Success)
            //    //        break;
            //    //}
            //    Regex r = new Regex("<span class=\"kamas\">[^0-9]*(?<kamas>[0-9 ]*)[ \t\r\n]*<img");
            //    Match m = r.Match(html);
            //    if (m.Success)
            //    {
            //        if (DEBUG) Console.WriteLine("Found match for kamas : [{0}]!", m.Groups["kamas"].Value.Replace(" ", string.Empty));
            //        kamas = BigInteger.Parse(m.Groups["kamas"].Value.Replace(" ", string.Empty));
            //    }
            //    else if (DEBUG) Console.WriteLine("No match for kamas");

            //    //XmlDocument xmlDocument = new XmlDocument();
            //    //xmlDocument.LoadXml(html);
            //    //string value = xmlDocument.SelectSingleNode("//*[contains(concat(' ', normalize-space(@class), ' '), ' kamas ')]").InnerText;

            //    //if (DEBUG) Console.WriteLine("Found value {0}", value);

            //    //kamas = BigInteger.Parse(value);

            //    if (resp.Headers.AllKeys.Contains("Set-Cookie"))
            //    {
            //        foreach (Cookie cookie in resp.Cookies)
            //            Cookies.Add(cookie);
            //    }
            //    if (resp.Headers.AllKeys.Contains("Location"))
            //    {
            //        GetPageContents(resp.Headers["Location"], false);
            //    }

            //    resp.Close();
            //}

        }

        private BigInteger GetKamas()
        {
            String url = "http://www.dofus.com/fr/achat-bourses-kamas-ogrines/0-francaise";

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            req.CookieContainer = new CookieContainer();
            req.CookieContainer.Add(Cookies);

            //req.CookieContainer.Add(new Cookie("PHPSESSID", phpsessid) { Domain = "www.newbiecontest.org" });

            //req.CookieContainer.Add(new Cookie("SMFCookie89", smfCookie89) { Domain = "www.newbiecontest.org" });

            req.Method = "GET";
            req.Accept = "*/*";
            //req.KeepAlive = true;
            req.ContentType = "application/x-www-form-urlencoded";
            //req.Referer = "http://www.dofus.com/fr/achat-bourses-kamas-ogrines/selection-serveur";
            //req.UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/35.0.1916.153 Safari/537.36";
            //req.Headers.Add("Accept-Encoding:gzip,deflate,sdch");
            req.Headers.Add("Accept-Language:fr-FR,fr;q=0.8,en-US;q=0.6,en;q=0.4");
            req.AllowAutoRedirect = true;

            if (DEBUG) Console.WriteLine("Getting page {0}", url);

            TimeSpan t1 = DateTime.Now.TimeOfDay;
            TimeSpan t2;
            String html = String.Empty;
            BigInteger kamas = 0;
            using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
            {
                t2 = DateTime.Now.TimeOfDay;

                StringBuilder sb = new StringBuilder();
                Stream str = resp.GetResponseStream();
                String tmp;
                Byte[] buf = new Byte[8192];
                int count;
                do
                {
                    count = str.Read(buf, 0, buf.Length);
                    if (count > 0)
                    {
                        tmp = Encoding.ASCII.GetString(buf, 0, count);

                        sb.Append(tmp);
                    }
                } while (count > 0);

                html = sb.ToString();

                if (DEBUG)
                {
                    Console.WriteLine("Page url :{0}", resp.ResponseUri);
                    Console.WriteLine("Headers :");
                    foreach (String header in resp.Headers.AllKeys)
                    {
                        Console.WriteLine("{0}:{1}", header, resp.Headers[header]);
                    }
                    Console.WriteLine("Contents:");
                    Console.WriteLine(html);
                }

                //String[] lines = html.Split(new Char[] { '\n' });
                //int i;
                //Regex r = new Regex(@"<span class=""kamas"">");
                //for (if = 0; i < lines.Count(); i++)
                //{
                //    Match m = r.Match(lines[i]);
                //    if (m.Success)
                //        break;
                //}
                Regex r = new Regex("<span class=\"kamas\">[^0-9]*(?<kamas>[0-9 ]*)[ \t\r\n]*<img");
                Match m = r.Match(html);
                if (m.Success)
                {
                    if (DEBUG) Console.WriteLine("Found match for kamas : [{0}]!", m.Groups["kamas"].Value.Replace(" ", string.Empty));
                    kamas = BigInteger.Parse(m.Groups["kamas"].Value.Replace(" ", string.Empty));
                }
                else if (DEBUG) Console.WriteLine("No match for kamas");

                //XmlDocument xmlDocument = new XmlDocument();
                //xmlDocument.LoadXml(html);
                //string value = xmlDocument.SelectSingleNode("//*[contains(concat(' ', normalize-space(@class), ' '), ' kamas ')]").InnerText;

                //if (DEBUG) Console.WriteLine("Found value {0}", value);

                //kamas = BigInteger.Parse(value);

                if (resp.Headers.AllKeys.Contains("Set-Cookie"))
                {
                    foreach (Cookie cookie in resp.Cookies)
                        Cookies.Add(cookie);
                }
                if (resp.Headers.AllKeys.Contains("Location"))
                {
                    GetPageContents(resp.Headers["Location"], false);
                }

                resp.Close();
            }

            return kamas;
        }

        private Boolean SelectServer(Servers server)
        {
            String url = "http://www.dofus.com/fr/achat-bourses-kamas-ogrines/selection-serveur";

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            req.CookieContainer = new CookieContainer();
            req.CookieContainer.Add(Cookies);

            //req.CookieContainer.Add(new Cookie("PHPSESSID", phpsessid) { Domain = "www.newbiecontest.org" });

            //req.CookieContainer.Add(new Cookie("SMFCookie89", smfCookie89) { Domain = "www.newbiecontest.org" });

            req.Method = "POST";
            req.Accept = "*/*";
            //req.KeepAlive = true;
            req.ContentType = "application/x-www-form-urlencoded";
            //req.Referer = "http://www.dofus.com/fr/achat-bourses-kamas-ogrines/selection-serveur";
            //req.UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/35.0.1916.153 Safari/537.36";
            //req.Headers.Add("Accept-Encoding:gzip,deflate,sdch");
            req.Headers.Add("Accept-Language:fr-FR,fr;q=0.8,en-US;q=0.6,en;q=0.4");
            req.AllowAutoRedirect = false;

            String post = "serverid=" + (Int32)server;

            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(post);
            req.ContentLength = bytes.Length;
            System.IO.Stream os = req.GetRequestStream();
            os.Write(bytes, 0, bytes.Length);
            os.Close();

            TimeSpan t1 = DateTime.Now.TimeOfDay;
            TimeSpan t2;
            String html = String.Empty;
            using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
            {
                t2 = DateTime.Now.TimeOfDay;

                StringBuilder sb = new StringBuilder();
                Stream str = resp.GetResponseStream();
                String tmp;
                Byte[] buf = new Byte[8192];
                int count;
                do
                {
                    count = str.Read(buf, 0, buf.Length);
                    if (count > 0)
                    {
                        tmp = Encoding.ASCII.GetString(buf, 0, count);

                        sb.Append(tmp);
                    }
                } while (count > 0);

                html = sb.ToString();

                if (DEBUG)
                {
                    Console.WriteLine("Page url :{0}", resp.ResponseUri);
                    Console.WriteLine("Headers :");
                    foreach (String header in resp.Headers.AllKeys)
                    {
                        Console.WriteLine("{0}:{1}", header, resp.Headers[header]);
                    }
                    Console.WriteLine("Contents:");
                    Console.WriteLine(html);
                }

                if (resp.Headers.AllKeys.Contains("Set-Cookie"))
                {
                    foreach (Cookie cookie in resp.Cookies)
                        Cookies.Add(cookie);
                }
                if (resp.Headers.AllKeys.Contains("Location"))
                {
                    if (GetPageContents(resp.Headers["Location"], false) == "http://www.dofus.com/fr/achat-bourses-kamas-ogrines/0-francaise")
                        return true;
                }

                resp.Close();
            }

            return false;
        }

        private Boolean Login(string accountName, string password)
        {
            String url = "https://account.ankama.com/sso";

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            req.CookieContainer = new CookieContainer();
            req.CookieContainer.Add(Cookies);

            req.Method = "POST";
            req.Accept = "*/*";
            //req.KeepAlive = true;
            req.ContentType = "application/x-www-form-urlencoded";
            //req.Referer = "http://www.dofus.com/fr/achat-bourses-kamas-ogrines/identification";
            //req.UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/35.0.1916.153 Safari/537.36";
            //req.Headers.Add("Accept-Encoding:gzip,deflate,sdch");
            req.Headers.Add("Accept-Language:fr-FR,fr;q=0.8,en-US;q=0.6,en;q=0.4");
            req.AllowAutoRedirect = false;
            
            String post = "action=login&from=http%3A%2F%2Fwww.dofus.com%2Ffr%2Fachat-bourses-kamas-ogrines%2Fidentification&login=" + accountName + "&password=" + password + "&remember=1";

            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(post);
            req.ContentLength = bytes.Length;
            System.IO.Stream os = req.GetRequestStream();
            os.Write(bytes, 0, bytes.Length);
            os.Close();

            if (DEBUG) Console.WriteLine("Getting page {0}", url);

            TimeSpan t1 = DateTime.Now.TimeOfDay;
            TimeSpan t2;
            String html = String.Empty;
            using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
            {
                t2 = DateTime.Now.TimeOfDay;

                StringBuilder sb = new StringBuilder();
                Stream str = resp.GetResponseStream();
                String tmp;
                Byte[] buf = new Byte[8192];
                int count;
                do
                {
                    count = str.Read(buf, 0, buf.Length);
                    if (count > 0)
                    {
                        tmp = Encoding.ASCII.GetString(buf, 0, count);

                        sb.Append(tmp);
                    }
                } while (count > 0);

                html = sb.ToString();

                if (DEBUG)
                {
                    Console.WriteLine("Page url :{0}", resp.ResponseUri);
                    Console.WriteLine("Headers :");
                    foreach (String header in resp.Headers.AllKeys)
                    {
                        Console.WriteLine("{0}:{1}", header, resp.Headers[header]);
                    }
                    Console.WriteLine("Contents:");
                    Console.WriteLine(html);
                }
                if (resp.Headers.AllKeys.Contains("Set-Cookie"))
                {
                    foreach (Cookie cookie in resp.Cookies)
                        Cookies.Add(cookie);
                }
                if (resp.Headers.AllKeys.Contains("Location"))
                {
                    if (GetPageContents(resp.Headers["Location"], false) == "http://www.dofus.com/fr/achat-bourses-kamas-ogrines/selection-serveur")
                        return true;
                }

                resp.Close();
            }

            return false;
        }

        #region Utils

        private string GetPageContents(string url, Boolean acceptRedirects = true)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            req.CookieContainer = new CookieContainer();
            req.CookieContainer.Add(Cookies);

            //req.CookieContainer.Add(new Cookie("PHPSESSID", phpsessid) { Domain = "www.newbiecontest.org" });

            req.AllowAutoRedirect = acceptRedirects;

            if (DEBUG) Console.WriteLine("Getting page {0}", url);

            TimeSpan t1 = DateTime.Now.TimeOfDay;
            TimeSpan t2;
            String html = String.Empty;
            using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
            {
                t2 = DateTime.Now.TimeOfDay;

                StringBuilder sb = new StringBuilder();
                Stream str = resp.GetResponseStream();
                String tmp;
                Byte[] buf = new Byte[8192];
                int count;
                do
                {
                    count = str.Read(buf, 0, buf.Length);
                    if (count > 0)
                    {
                        tmp = Encoding.ASCII.GetString(buf, 0, count);

                        sb.Append(tmp);
                    }
                } while (count > 0);

                html = sb.ToString();

                if (DEBUG)
                {
                    Console.WriteLine("Page url :{0}", resp.ResponseUri);
                    Console.WriteLine("Headers :");
                    foreach (String header in resp.Headers.AllKeys)
                    {
                        Console.WriteLine("{0}:{1}", header, resp.Headers[header]);
                    }
                    Console.WriteLine("Contents:");
                    Console.WriteLine(html);
                }
                if (resp.Headers.AllKeys.Contains("Set-Cookie"))
                {
                    foreach (Cookie cookie in resp.Cookies)
                        Cookies.Add(cookie);
                }
                if (resp.Headers.AllKeys.Contains("Location"))
                {
                    return GetPageContents(resp.Headers["Location"], false);
                }

                resp.Close();
            }

            if (DEBUG) Console.WriteLine("Returning URL {0}", url);
            
            return url;
        }

        private void DebugExceptionToConsole(Exception e)
        {
            Console.WriteLine("{0}", e.Message);
            if (e.InnerException != null)
            {
                Console.WriteLine("Inner exception:");
                DebugExceptionToConsole(e.InnerException);
            }
        }

        #endregion

    }
}
