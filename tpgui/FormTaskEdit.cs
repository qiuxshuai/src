using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xworks.taskprocess
{
    public partial class FormTaskEdit : Form
    {
        public string changeoradd;
        public string[] chooseRow = new string[13];
        TaskPriority priority = (TaskPriority)Enum.Parse(typeof(TaskPriority), "Normal");
        ListViewItem oldItem = new ListViewItem();
		public FormTaskEdit()
		{
			InitializeComponent();
		}

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
             

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            TaskFile tf = new TaskFile();
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("作业者和详细不能为空");
            }
            else
            {
                if (dateTimePicker1.Value < DateTime.Now)
                {
                    MessageBox.Show("预定日应该为今天以后的日期");
                }
                else
                {
                    switch (changeoradd)
                    {
                        case "add":
                            tf.Addtask(priority, dateTimePicker1.Value.ToString("yyyyMMddHHmmss"), textBox2.Text, textBox1.Text, DateTime.Now.ToString("yyyyMMddHHmmss"));
                            break;
                        case "change":
                            tf.Change(chooseRow[11].ToString(), priority, dateTimePicker1.Value.ToString("yyyyMMddHHmmss"), textBox2.Text.ToString(), textBox1.Text.ToString());
                            break;
                    }
                    this.DialogResult = DialogResult.OK;
                    MessageBox.Show("编辑成功");
                    this.Close();
                }
            }
        }
    }
}
