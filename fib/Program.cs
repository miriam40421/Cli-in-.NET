//fib bundle --output D:/C#/bundlefile.txt
//using System.CommandLine

using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.CommandLine;
using System.ComponentModel.Design;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Text.RegularExpressions;
string y = "";
string[] allLanguage = { "*.java", "*.sql", "*.html", "*.cs", "*.css", "*.txt", "*.js", "*.jsx", "*.ts", "*.dll" };
string language = "";
var rspCommand = new Command("rsp", "all toghezer");
var bundleCommand = new Command("bundle", "bundle code  file to a single file");
bundleCommand.AddAlias("b");
var bundleOption = new Option<FileInfo>("--output", "File path and name");
bundleOption.AddAlias("--o");
var Bundlelanguage = new Option<string>("--language", "check the language") { IsRequired = true };
Bundlelanguage.AddAlias("--l");
var Bundlenote = new Option<bool>("--note", "write the folder place");
Bundlenote.AddAlias("--n");
var BundleSort = new Option<bool>("--sort", "sotr the files");
BundleSort.AddAlias("--s");
var BundleRemove = new Option<bool>("--remove", "remove the empty lines");
BundleRemove.AddAlias("--r");
var BundleAuthor = new Option<string>("--author", "write the name");
BundleAuthor.AddAlias("--a");

bundleCommand.AddOption(bundleOption);
bundleCommand.AddOption(Bundlelanguage);
bundleCommand.AddOption(Bundlenote);
bundleCommand.AddOption(BundleSort);
bundleCommand.AddOption(BundleRemove);
bundleCommand.AddOption(BundleAuthor);
bundleCommand.SetHandler((FileInfo output, string language, bool note, bool sort, bool remove, string author) =>
 {
     var s = "";



     try
     {
   
         var t = File.Create(output.FullName);
         t.Close();
         Console.WriteLine("file was created");

     }
     catch (DirectoryNotFoundException ex)
     {
         Console.WriteLine("Error: file path is invalid");
     }
     //בלחיצה allמביא את כל הקבצים 
     List<string> files = new List<string>();

     if (language == "all")
     {
         List<string> onelanguage = new List<string>();
         //עובר על מערך השפות
         foreach (var item in allLanguage)
         {//יוצר רשימה של שפות ששוות לשפה אחת 
             onelanguage = Directory.GetFiles(Directory.GetCurrentDirectory(), "*." + item, SearchOption.AllDirectories).ToList();
             //עובר על הרשימה הנוכחית ומוסיף לרשימת הקבצים
             foreach (var item1 in onelanguage)
             {
                 files.Add(item1);
             }

         }
         Console.WriteLine("the files are join!");

     }
     else
     {//אם כתבו יותר משפה אחת
         if (language.Contains(","))
         {
             Console.WriteLine(language);
             //יוצר מערך לשפות שהכניס
             string[] languages = language.Split(",");
             foreach (var item in languages)
             {
                 if (!allLanguage.Contains("*." + item))
                 {
                     Console.WriteLine("not valid " + item);
                 }
             }
             List<string> onelanguage = new List<string>();
             //עובר על מערך השפות שיצר

             foreach (var item in languages)
             {//יוצר רשימה של שפות ששוות לשפה אחת 
                 onelanguage = Directory.GetFiles(Directory.GetCurrentDirectory(), "*." + item, SearchOption.AllDirectories).ToList();
                 //עובר על הרשימה הנוכחית ומוסיף לרשימת הקבצים

                 foreach (var item1 in onelanguage)
                 {
                     files.Add(item1);
                 }

             }

         }

         //שפה אחת עובד בתוך כל התיקיות
         else if (allLanguage.Contains("*." + language))
         {  //הכנסת שפה אחת
             files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*." + language, SearchOption.AllDirectories).ToList();
             Console.WriteLine("the files are join!!");
         }
         else
         {
             Console.WriteLine("not exsist!!");
         }
     }
     //author-כתיבת השם למעלה

     s += author;
     s += "\n";
     //note-הדפסת שם התיקיה
     if (note)
     {
         s += Directory.GetCurrentDirectory();
         s += "\n";
     }
     //--sort
     if (sort)
     {
         var sortFiles = files.OrderBy(f => Path.GetExtension(f));
         foreach (var item in sortFiles)
         {
             Console.WriteLine(item);
         }
         //מעבר על הקבצים הממוינים
         foreach (string file in sortFiles)
         {//bin or debug --אם הקובץ לא מכיל 
             if (!file.Contains("Debug") || !file.Contains("bin"))
             {   //אם רוצה ניתוב

                 if (note)
                 { //הוספת הניתוב
                     //s += "\n";
                     s += file;
                     s += "\n";
                 }
                 Console.WriteLine(File.ReadAllText(file));
                 //הוספת תוכן הקובץ 
                 s += File.ReadAllText(file);

             }
         }

     }
     else
     {
         Console.WriteLine("files:");
         //הדפסת נתובי הקבצים
         foreach (string file in files)
             Console.WriteLine(file);
         //הדפסת תוכן הקצים
         foreach (string file in files)
         {//bin or debug --אם הקובץ לא מכיל 
             if (!file.Contains("Debug") || !file.Contains("bin"))
             {
                 //אם רוצה ניתוב
                 if (note)
                 {
                     //s += "\n";
                     s += file;
                     s += "\n";
                 }

                 Console.WriteLine(File.ReadAllText(file));
                 //הוספת תוכן הקובץ 
                 s += File.ReadAllText(file);
             }

         }
     }
     // הוספת כל הקבצים לקובץ שיצרנו
     File.WriteAllText(output.FullName, s);

     //remove-אם רוצה למחוק שורות ריקות
     if (remove)
     {
         var e = File.ReadAllLines(output.FullName).Where(f => !string.IsNullOrWhiteSpace(f));
         File.WriteAllLines(output.FullName, e);
     }

 }, bundleOption, Bundlelanguage, Bundlenote, BundleSort, BundleRemove, BundleAuthor);
//rsp-פקוזת


rspCommand.SetHandler(() =>
{

    string f = " b --o ";
    string word = "";
    Console.WriteLine("enter file name: ");
    word = Console.ReadLine();


    try
    {
        File.Create(word);
        Console.WriteLine("file was created");
        f += word;
        word = "";
        string y = "";
        Console.WriteLine("enter language/s,if all language write all:");
        word = Console.ReadLine();
        if (word.Contains(","))
        {
            string[] l = word.Split(",");
            foreach (var item in l)
            {
                if (!allLanguage.Contains("*." + item))
                {
                    y = "n";
                }
            }

            while ((!word.Equals("all")) && (!allLanguage.Contains("*." + word)) && (y.Equals("n")))
            {

                Console.WriteLine("enter language/s,if all language write all:");
                word = Console.ReadLine();
            }
        }

        f += (" --l ") + word;

        word = "";

        f += (" --n ");
        Console.WriteLine("write true if you want to get the file routing,else false:");
        word = Console.ReadLine();

        while ((word != "true") && (word != "false"))
        {
            Console.WriteLine("write true if you want to get the file routing,else false:");
            word = Console.ReadLine();

        }
        f += word;
        word = "";
        f += (" --s ");
        Console.WriteLine("write true if you want to sort the files ,else false:");
        word = Console.ReadLine();

        while ((word != "true") && (word != "false"))
        {
            Console.WriteLine("write true if you want to sort the files ,else false:");
            word = Console.ReadLine();

        }
        f += word;
        word = "";
        f += (" --r ");
        Console.WriteLine("write true if you want to remove empty lines ,else false:");
        word = Console.ReadLine();

        while ((word != "true") && (word != "false"))

        {
            Console.WriteLine("write true if you want to remove empty lines ,else false:");
            word = Console.ReadLine();

        }
        f += word;
        word = "";
        f += (" --a ");
        Console.WriteLine("enter name:");
        f += Console.ReadLine();
        File.WriteAllText("rsp.rsp", f);

    }
    catch (DirectoryNotFoundException ex)
    {
        Console.WriteLine("Error: file path is invalid");
    }
});



var rootCommand = new RootCommand("Root command for file bundler CLI");
rootCommand.AddCommand(bundleCommand);
rootCommand.AddCommand(rspCommand);
rootCommand.InvokeAsync(args);

