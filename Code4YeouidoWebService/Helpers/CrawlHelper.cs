using Code4YeouidoWebService.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Code4YeouidoWebService.Helpers
{
    public class CrawlHelper
    {
        public class Member
        {
            public String Party { get; set; }
            public String Name { get; set; }
        }

        public static void CrawlBills(Code4YeouidoEntities db, int id)
        {
            HtmlWeb hw = new HtmlWeb();
            HtmlDocument doc = hw.Load("http://watch.peoplepower21.org/New/monitor_voteresult.php?page=" + id);
            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                String url = link.Attributes.First(e => e.Name.Equals("href")).Value;
                if (url.IndexOf("http://watch.peoplepower21.org/New/c_monitor_voteresult_detail.php?mbill") > -1)
                {

                    WebClient client = new WebClient();

                    client.Encoding = Encoding.UTF8;
                    String data = client.DownloadString(url);

                    HtmlDocument htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(data);

                    var node = htmlDoc.DocumentNode.SelectSingleNode("//body");

                    List<Member> 찬성 = new List<Member>();
                    List<Member> 청가 = new List<Member>();
                    List<Member> 결석 = new List<Member>();
                    List<Member> 반대 = new List<Member>();
                    List<Member> 기권 = new List<Member>();
                    List<Member> 불참 = new List<Member>();

                    List<string> partyNames = new List<string>() { "새누리당", "통합진보당", "새정치민주연합", "무소속", "정의당", "통합진보당", "녹색당" };
                    String title = null;
                    String when = null;
                    String subTitle = null;
                    foreach (var i in node.SelectNodes("//tr"))
                    {
                        if (i.InnerHtml.StartsWith("\n\t\t\t\t\t\t<td colspan=\"2\">\n\t\t\t\t\t\t\t<b>"))
                        {
                            String value = Regex.Replace(i.InnerHtml, "<.*?>", string.Empty);

                            title = i.SelectSingleNode("//b").InnerHtml;
                            //String when = Regex.Split(value, "\n\t\t\t\t\t\t\t")[1];
                            when = value.Replace("(대안)", "").Split(')')[1];
                            subTitle = when.Split('/')[1].Split(':')[0].Trim();
                            when = when.Split('/')[0].Trim();
                        }
                        else if (i.InnerHtml.StartsWith(" \n\t\t\t\t\t\t<td valign=\"TOP\" nowrap=\"\">"))
                        {
                            String value = Regex.Replace(i.InnerHtml, "<.*?>", string.Empty);
                            List<Member> listNow = null;
                            if (i.InnerHtml.IndexOf("찬성") > -1)
                            {
                                listNow = 찬성;
                                value = value.Replace("찬성", string.Empty);
                            }
                            else if (i.InnerHtml.IndexOf("청가") > -1)
                            {
                                listNow = 청가;
                                value = value.Replace("청가", string.Empty);
                            }
                            else if (i.InnerHtml.IndexOf("결석") > -1)
                            {
                                listNow = 결석;
                                value = value.Replace("결석", string.Empty);
                            }
                            else if (i.InnerHtml.IndexOf("반대") > -1)
                            {
                                listNow = 반대;
                                value = value.Replace("반대", string.Empty);
                            }
                            else if (i.InnerHtml.IndexOf("기권") > -1)
                            {
                                listNow = 기권;
                                value = value.Replace("기권", string.Empty);
                            }
                            else if (i.InnerHtml.IndexOf("불참") > -1)
                            {
                                listNow = 불참;
                                value = value.Replace("불참", string.Empty);
                            }

                            value = value.Replace("(", " ").Replace(")", " ").Replace("\n\t\t\n\t\t\t\t\t\t\t\t\t\t\t\t", " ").Replace("\n\t\t\n\t\t\t\t\t\t\t\t\t\t\t", " ").Replace("(\n\t\t\t\t\t\t\t\t\t\t\t\t", " ").Replace("\t\t\t\t\n\t\t\t\t\t\t\n\t\t\t\t\t\t\t\t\t\t\t", " ").Replace("\n\t\t\t\t\t\t", " ").Replace("\n\t\t\n\t\t\t\t", " ").Replace("\t\t\t\t\t\t\t", " ").Replace("\t\t\t\t\t\t", " ");//"\t\t\t\t\t\t\t새정치민주연합"

                            var tokens = value.Split(' ').ToList();
                            tokens.RemoveAll(e => e.Equals(":"));
                            tokens.RemoveAll(e => e.Equals("*"));
                            tokens.RemoveAll(e => e.Equals(""));
                            tokens.RemoveAll(e => e.EndsWith("명") && (e.StartsWith("1") || e.StartsWith("2") || e.StartsWith("3") || e.StartsWith("4") || e.StartsWith("5") || e.StartsWith("6") || e.StartsWith("7") || e.StartsWith("8") || e.StartsWith("9")));

                            String party = null;
                            foreach (var t in tokens)
                            {
                                if (partyNames.Contains(t))
                                    party = t;
                                else
                                    listNow.Add(new Member() { Party = party, Name = t });
                            }
                        }
                    }

                    if (title != null)
                    {
                        BillList b = db.BillList.Where(e => e.Number.Equals(subTitle) && e.Title.Equals(title)).FirstOrDefault();

                        if (b == null)
                        {
                            b = new BillList();
                            b.Title = title;
                            b.Number = subTitle;
                            DateTime dWhen;
                            DateTime.TryParse(when, out dWhen);
                            b.WhenOpen = dWhen;

                            db.BillList.Add(b);

                            try
                            {
                                foreach (var i in 찬성)
                                {
                                    BillChoice c = new BillChoice() { Choice = "찬성", Name = i.Name, Party = i.Party, BillId = b.BillId };
                                    db.BillChoice.Add(c);
                                }
                                foreach (var i in 청가)
                                {
                                    BillChoice c = new BillChoice() { Choice = "청가", Name = i.Name, Party = i.Party, BillId = b.BillId };
                                    db.BillChoice.Add(c);
                                }
                                foreach (var i in 결석)
                                {
                                    BillChoice c = new BillChoice() { Choice = "결석", Name = i.Name, Party = i.Party, BillId = b.BillId };
                                    db.BillChoice.Add(c);
                                }
                                foreach (var i in 반대)
                                {
                                    BillChoice c = new BillChoice() { Choice = "반대", Name = i.Name, Party = i.Party, BillId = b.BillId };
                                    db.BillChoice.Add(c);
                                }
                                foreach (var i in 기권)
                                {
                                    BillChoice c = new BillChoice() { Choice = "기권", Name = i.Name, Party = i.Party, BillId = b.BillId };
                                    db.BillChoice.Add(c);
                                }
                                foreach (var i in 불참)
                                {
                                    BillChoice c = new BillChoice() { Choice = "불참", Name = i.Name, Party = i.Party, BillId = b.BillId };
                                    db.BillChoice.Add(c);
                                }

                                db.SaveChanges();
                            }
                            catch (Exception e)
                            {

                            }
                        }
                    }
                }
            }
        }
    }
}