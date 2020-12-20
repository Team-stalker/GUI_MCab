using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Compress
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool Type = false;
        private void pathCmp_Click(object sender, EventArgs e)
        {
            OpenFileDialog ImportFile = new OpenFileDialog();
            ImportFile.DefaultExt = "*.*";
            ImportFile.Filter = "Выберите файл|*.*";
            if (ImportFile.ShowDialog() == DialogResult.OK && ImportFile.FileName.Length > 0)
            {
                LinkA.Text = ImportFile.FileName;
            }
        }
        private void PathOut_Click(object sender, EventArgs e)
        {
            OpenFileDialog ImportFile = new OpenFileDialog();
            ImportFile.DefaultExt = "*.*";
            ImportFile.Filter = "Выберите файл|*.cab";
            if (ImportFile.ShowDialog() == DialogResult.OK && ImportFile.FileName.Length > 0)
            {
                LinkB.Text = ImportFile.FileName;
            }
        }
        private void StartCmd_Click(object sender, EventArgs e)
        {
            if (Thread.IsBusy == false)
            {
                if (LinkA.TextLength == 0 && LinkB.TextLength == 0)
                {
                    MessageBox.Show("Выберите файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                DialogResult res = MessageBox.Show("Нажмите 'Да' - чтобы запаковать файл\nНажмите 'Нет' - чтобы распаковать файл", "Выберите вариант работы", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (res == DialogResult.Yes)
                {
                    INFO.Visible = true;
                    INFO.BackColor = Color.Gold;
                    INFO.Text = "Упаковка данных...";
                    groupBox1.Enabled = false;
                    groupBox2.Enabled = false;
                    Type = true;
                    Thread.RunWorkerAsync();
                    StartCmd.Enabled = false;
                }
                else
                {
                    INFO.Visible = true;
                    INFO.BackColor = Color.Lime;
                    INFO.Text = "Распаковка данных...";
                    groupBox1.Enabled = false;
                    groupBox2.Enabled = false;
                    Type = false;
                    Thread.RunWorkerAsync();
                    StartCmd.Enabled = false;
                }
            }
        }
        private void who_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Автор: Team-stalker\nГрафическая оболочка для архиваций файлов при помощи стандартного инструмента OC Windows Make .cab\nВ данный момент поддерживает упаковку и запаковку только 1 файла.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void help_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Выберите нужный файл и нажмите Старт.\nДождитесь завершения обработки - будет выведено сообщение\nЗапакованный файл будет находится в директории программы.\nПути не должны содержать латинские символы.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void Thread_DoWork(object sender, DoWorkEventArgs e)
        {
            if (Type)
                StartArgs("/C makecab " + LinkA.Text + " /L " + Environment.CurrentDirectory + "\\");     
            else
                StartArgs("/C expand " + LinkB.Text + " -I");
        }
        private void Thread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Type)
                Compress();
            else
                Decompress();
            INFO.Visible = false;
            StartCmd.Enabled = true;
            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
        }

        private void StartArgs(string Arg)
        {
            Process open = new Process();
            open.StartInfo.FileName = "cmd.exe";
            open.StartInfo.Arguments = Arg;
            open.StartInfo.Verb = "runas";
            open.StartInfo.CreateNoWindow = true;
            open.StartInfo.UseShellExecute = false;
            open.StartInfo.RedirectStandardOutput = true;
            open.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(866);
            open.Start();
            string result_info = open.StandardOutput.ReadToEnd();
            open.WaitForExit();
            open.Close();
            if (result_info.Contains("ret"))
            {
                MessageBox.Show("Ошибка. Путь к файлам не должен содержать русские буквы", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Compress()
        {
            string filefound = "";
            // Запомнить данные файла
            string savefilename = LinkA.Text.Split('.')[0];
            string savefileformat = LinkA.Text.Split('.')[1];
            int count = savefilename.Count(x => x == '\\');
            savefilename = savefilename.Split('\\')[count];
            string fileinfo = savefilename + "." + savefileformat[0] + savefileformat[1] + "_"; // имя файла после сжатия
            if (File.Exists(savefilename + ".cab"))
            {
                filefound = DateTime.Now.ToString("ssmmhh");
                MessageBox.Show("Данный файл в данной папке уже существует. Новый файл будет иметь данный формат: " + savefilename + ".cab_" + filefound, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (File.Exists(fileinfo))
            {
                if (filefound.Length != 0)
                    filefound = "_" + filefound;
                File.Move(fileinfo, savefilename + ".cab" + filefound);
                MessageBox.Show("Успешно завершено", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void Decompress()
        {
            MessageBox.Show("Успешно завершено", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}