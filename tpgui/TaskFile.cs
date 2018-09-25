using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
namespace xworks.taskprocess
{
    class TaskFile
    {
        public static List<Task> Tasks { get; set; }
        public static int chooseLine { get; set; }
        public List<Task> LoadTasks(string filePath)
        {
            Tasks = new List<Task>();
            bool judge = new bool();
            try
            {
                StreamReader sr = new StreamReader(filePath);
                judge = true;
            }
            catch (IOException)
            {
                judge = false;
            }
            //List<Task> Tasks = new List<Task>();
            if (judge == true)
            {
                XmlDocument doc = new XmlDocument();
                XmlReaderSettings settings = new XmlReaderSettings(); 
                settings.IgnoreComments = true;//忽略文档里的注释
                doc.Load(filePath);
                XmlElement rootElem = doc.DocumentElement;
                XmlNode tasks= doc.SelectSingleNode("tasks");
                XmlNodeList node = rootElem.GetElementsByTagName("task");
                foreach (XmlNode nodes in node)
                {
                    Task task = new Task();
                    XmlElement taskNode = (XmlElement)nodes;
                    task.Id= taskNode.GetAttribute("id").ToString();
                    XmlNodeList childNode = taskNode.ChildNodes;
                    task.Author = childNode.Item(0).InnerText;
                    task.SubmitTime = DateTime.ParseExact(childNode.Item(1).InnerText, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                    task.Priority= (TaskPriority)Enum.Parse(typeof(TaskPriority), childNode.Item(2).InnerText);
                    task.DueTime = DateTime.ParseExact(childNode.Item(3).InnerText, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                    task.Assignee = childNode.Item(4).InnerText;
                    task.Content = childNode.Item(5).InnerText;
                    task.HandlingNote = childNode.Item(6).InnerText;
                    task.Status = (TaskStatus)Enum.Parse(typeof(TaskStatus), childNode.Item(7).InnerText);
                    task.Checker = childNode.Item(8).InnerText;
                    task.CheckTime = DateTime.ParseExact(childNode.Item(9).InnerText, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                    task.FinishTime = DateTime.ParseExact(childNode.Item(10).InnerText, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                    Tasks.Add(task);
                }
            }
            return Tasks;
        }
        public void Addtask(TaskPriority priority, string duetime, string assignee, string content, string submittime)
        {
            Task task = new Task
            {
                Id = Guid.NewGuid().ToString(),
                Author = "",
                SubmitTime = DateTime.ParseExact(submittime, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture),
                Priority = priority,
                DueTime = DateTime.ParseExact(duetime, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture),
                Assignee = assignee,
                Content = content,
                HandlingNote = "",
                Status = (TaskStatus)Enum.Parse(typeof(TaskStatus), "0"),
                Checker = "",
                CheckTime = DateTime.ParseExact("19000101000000", "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture),
                FinishTime = DateTime.ParseExact("19000101000000", "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture)
            };
            Tasks.Add(task);

        }
        public void Change(string id, TaskPriority priority, string duetime, string assignee, string content)
        {
            foreach (Task x in Tasks)
            {
                if (x.Id == id)
                {
                    x.Priority = priority;
                    x.DueTime = DateTime.ParseExact(duetime, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                    x.Assignee = assignee;
                    x.Content = content;
                }
            }
        }
    }
}
