using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace xworks.taskprocess
{
    public partial class FormTaskList : Form
    {
        public FormTaskList()
        {
            InitializeComponent();
        }

        private void _toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            TaskFile tf = new TaskFile();
            string filePath = "";
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "所有文件（*xml*）|*.xml*";
            if (file.ShowDialog() == DialogResult.OK)
            {
                filePath = file.FileName;
            }
            if (filePath == "")
            {
                MessageBox.Show("请选择要打开的文件");
            }
            else
            {
                List<Task> tasks = tf.LoadTasks(filePath);
                AddToListview(tasks);

            }
        }
        private void AddToListview(List<Task> tasks)
        {
            listView1.Items.Clear();
            int i = 0;
            foreach (Task x in tasks)
            {
                i++;
                ListViewItem item = new ListViewItem();
                item.SubItems[0].Text = "#" + i.ToString();
                item.SubItems.Add(x.Author);
                item.SubItems.Add(x.SubmitTime.ToString("yy/MM/dd"));
                item.SubItems.Add(x.Content);
                item.SubItems.Add(x.HandlingNote);
                switch (Enum.GetName(typeof(TaskStatus), x.Status))
                {
                    case "NotStart":
                        item.SubItems.Add("未着手");
                        break;
                    case "Handling":
                        item.SubItems.Add("作业中");
                        break;
                    case "Finished":
                        item.SubItems.Add("已完成");
                        break;
                    case "Accept":
                        item.SubItems.Add("已经确认");
                        break;
                    case "Reject":
                        item.SubItems.Add("被驳回");
                        break;
                }
                if (x.FinishTime.ToString("yyyy/MM/dd") == "1900/01/01")
                {
                    item.SubItems.Add("");
                }
                else
                {
                    item.SubItems.Add(x.FinishTime.ToString("yy/MM/dd"));
                }
                item.SubItems.Add(x.Assignee);
                if (x.CheckTime.ToString("yyyy/MM/dd") == "1900/01/01")
                {
                    item.SubItems.Add("");
                }
                else
                {
                    item.SubItems.Add(x.CheckTime.ToString("yy/MM/dd"));
                }
                item.SubItems.Add(x.Checker);
                item.SubItems.Add(x.DueTime.ToString("yy/MM/dd"));
                switch (Enum.GetName(typeof(TaskPriority), x.Priority))
                {
                    case "High":
                        item.ForeColor = Color.FromArgb(255, 0, 0);
                        break;
                    case "Middle":
                        item.ForeColor = Color.FromArgb(178, 34, 34);
                        break;
                    case "Normal":
                        item.ForeColor = Color.FromArgb(0, 0, 0);
                        break;
                    case "Low":
                        item.ForeColor = Color.FromArgb(0, 100, 0);
                        break;
                }

                if (Enum.GetName(typeof(TaskStatus), x.Status) == "Finished")
                {
                    item.ForeColor = Color.FromArgb(128, 128, 128);
                }
                if (DateTime.Now.Day - x.CheckTime.Day <= 2 && Enum.GetName(typeof(TaskStatus), x.Status) != "Finished")
                {
                    item.BackColor = Color.FromArgb(240, 230, 140);
                }
                item.SubItems.Add(x.Id.ToString());
                item.SubItems.Add(Enum.GetName(typeof(TaskPriority), x.Priority));
                listView1.Items.Add(item);
            }

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            FormTaskEdit f = new FormTaskEdit();
            if (listView1.SelectedItems.Count == 1)
            {
                int a = listView1.SelectedItems[0].Index;
                TaskFile.chooseLine = a;
                for (int i = 0; i < 13; i++)
                {
                    f.chooseRow[i] = this.listView1.Items[a].SubItems[i].Text;
                }
                f.changeoradd = "change";
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("请选中一组数据");
            }
            if (f.DialogResult == DialogResult.OK)
            {
                listView1.Items[TaskFile.chooseLine].SubItems[7].Text = TaskFile.Tasks[TaskFile.chooseLine].Assignee;
                listView1.Items[TaskFile.chooseLine].SubItems[10].Text = TaskFile.Tasks[TaskFile.chooseLine].DueTime.ToString("yy/MM/dd");
                listView1.Items[TaskFile.chooseLine].SubItems[3].Text = TaskFile.Tasks[TaskFile.chooseLine].Content;
                switch (Enum.GetName(typeof(TaskPriority), TaskFile.Tasks[TaskFile.chooseLine].Priority))
                {
                    case "High":
                        listView1.Items[TaskFile.chooseLine].ForeColor = Color.FromArgb(255, 0, 0);
                        break;
                    case "Middle":
                        listView1.Items[TaskFile.chooseLine].ForeColor = Color.FromArgb(178, 34, 34);
                        break;
                    case "Normal":
                        listView1.Items[TaskFile.chooseLine].ForeColor = Color.FromArgb(0, 0, 0);
                        break;
                    case "Low":
                        listView1.Items[TaskFile.chooseLine].ForeColor = Color.FromArgb(0, 100, 0);
                        break;
                }
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            FormTaskProcess f = new FormTaskProcess();
            f.ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FormTaskConfirm f = new FormTaskConfirm();
            f.ShowDialog();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            FormLinkFile f = new FormLinkFile();
            f.ShowDialog();
        }

        private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TaskFile tf = new TaskFile();
            string filePath = "";
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "所有文件（*xml*）|*.xml*";
            if (file.ShowDialog() == DialogResult.OK)
            {
                filePath = file.FileName;
            }
            if (filePath == "")
            {
                MessageBox.Show("请选择要打开的文件");
            }
            else
            {
                List<Task> tasks = tf.LoadTasks(filePath);
                AddToListview(tasks);
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}
