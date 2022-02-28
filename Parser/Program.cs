using System;
using System.Threading;
using HtmlAgilityPack;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Parser
{
    class Program
    {
        static void Main()
        {
            string a;
            var mc = new Program();
            var t = new Task(mc.LentaParsing);
            var r = new Task(mc.ria);

            t.Start();
            r.Start();

            Console.WriteLine("Start programm");

            while (true) { 
                a = Console.ReadLine();
                if (a.ToLower() == "stop") {
                    //t.Kill();
                    break;
                }
            }
        }

        public void LentaParsing()
        {
            var html = @"https://lenta.ru/parts/news/";

            HtmlNode node;
            string temp_value = "";
            
            while (true)
            {
                HtmlWeb web = new HtmlWeb();

                var htmlDoc = web.Load(html);

                node = htmlDoc.DocumentNode.SelectSingleNode("//body/div/div/main/div/div/ul/li/a/h3");

                if (node.OuterHtml != temp_value)
                {
                    string[] Splitted = node.OuterHtml.Split(new[] {'<','>'});
                    temp_value = node.OuterHtml;
                    if (CheckTags(node.OuterHtml))
                    {
                        for (int i = 0; i < 3; i++) Console.WriteLine("---!!!ALERT!!!---");
                        Console.WriteLine(Splitted[2] + "\tLenta");
                        for (int i = 0; i < 3; i++) Console.WriteLine("---!!!ALERT!!!---");
                    }
                    else
                    {
                        Console.WriteLine(Splitted[2] + "\tLenta");
                    }
                    
                }
                Thread.Sleep(20000);
            }
        }
        public void ria()
        {
            var html = @"https://ria.ru/politics/";

            HtmlNode node;
            string temp_value = "";

            while (true)
            {
                HtmlWeb web = new HtmlWeb();

                var htmlDoc = web.Load(html);

                node = htmlDoc.DocumentNode.SelectSingleNode("//body/div/div/div/div/div/div/div/div/div/div/a[2]");

                /*Console.WriteLine(node.OuterHtml);*/
                if (node.OuterHtml != temp_value)
                {
                    string[] Splitted = node.OuterHtml.Split(new[] { '<', '>' });
                    temp_value = node.OuterHtml;
                    if (CheckTags(node.OuterHtml))
                    {
                        for (int i = 0; i < 3; i++) Console.WriteLine("---!!!ALERT!!!---");
                        Console.WriteLine(Splitted[2] + "\tRIA");
                        for (int i = 0; i < 3; i++) Console.WriteLine("---!!!ALERT!!!---");
                    }
                    else
                    {
                        Console.WriteLine(Splitted[2] + "\tRIA");
                    }

                }
                Thread.Sleep(20000);
            }
        }

        public bool CheckTags(string a) {
            string[] tag_words = { "ядер", "яо", "яп" };
            for (int i = 0; i < tag_words.Length; i++) {
                if (a.ToLower().Contains(tag_words[i])) return true; 
            }
            return false;
        }

    }
}
