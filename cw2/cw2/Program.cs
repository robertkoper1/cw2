using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

namespace cw2
{
    class Program
    {
        static void Main(string[] args)
        {
            string path =@"C:\Users\Robert\Desktop\dane.csv";
            string pathTo= @"C:\Users\Robert\Desktop\result.xml";
            string format="xml";

            if (args.Length == 2 || args.Length == 3)
            {
                if (args.Length == 2)
                {
                    var check = args[0].Split(".");
                    if (check[check.Length - 1] == "csv")
                    {
                        path = args[0];
                        if (args[1] == "xml" && args[1] == "json")
                        {
                            format = args[1];
                            pathTo = "result.xml";
                        }
                        else
                        {
                            pathTo = args[1];
                            format = "xml";
                        }
                    }
                    else
                    {
                        path = "data.csv";
                        pathTo = args[0];
                        format = args[1];
                    }

                }
                if (args.Length == 3)
                {
                    path = args[0];
                    pathTo = args[1];
                    format = args[2];
                }
            }
            else
            {
                //throw new ArgumentException("brak poprawnej ilosci argumentow");
            }

            var log = new FileStream("log.txt", FileMode.Create);
            var sw = new StreamWriter(log);
            var studenci = new List<Student>(); 
            

            try
            {
                var lines = File.ReadLines(path);
                
                foreach (var line in lines)
                {
                    Console.WriteLine(line);
                    var lineSplitted = line.Split(",");
                    if (lineSplitted.Length != 9)
                    {
                        sw.Write("Brak wystarczajacej liczby danych w linii: " + line);
                        continue;
                    }
                    var student = new Student(lineSplitted[0], lineSplitted[1], lineSplitted[4], lineSplitted[5], lineSplitted[6], lineSplitted[7], lineSplitted[8], new Studia(lineSplitted[2], lineSplitted[3]));
                    if (!studenci.Contains(student))
                    {
                        studenci.Add(student);

                    }
                    else
                    {
                        sw.Write("Powtorzono dane studenta" + line);
                    }


                }
            }
            catch(ArgumentException e1)
            {
                sw.Write("Podana ścieżka jest niepoprawna.");
            }
            catch(System.IO.FileNotFoundException e2)
            {
                sw.Write("Plik nazwa nie istnieje.");
            }

            if (format == "xml")
            {
                FileStream writer = new FileStream(pathTo, FileMode.Create); 
                XmlSerializer serializer = new XmlSerializer(typeof(List<Student>), new XmlRootAttribute("uczelnia"));  
                serializer.Serialize(writer, studenci);


            }else
            {
                var jsonString = JsonSerializer.Serialize(studenci); 
                File.WriteAllText(pathTo, jsonString);

            }

            sw.Close();
            log.Close();
            

        }
    }
}
