using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;

// ReadMe
// Помощник разработчикам карт. Программа на основе лога (в данной момент игры 1.0007 скачать патч) 
// выдергивает из лога путь отсутствующих текстур, и находит их в общем указанной Вами "папки-источник" (gamedata/textures). 
// 1) Путь к общей папке с текстурами - Устанавливается путь к папке источник со всеми текстурами(gamedata/textures)
// 2) Путь для выгрузки скопированных текстур - Задается путь для выгрузки текстур, которые были обнаружены в логе как(пример Can't find texture 'cars\btr)'. 
// Программа в автоматическом режиме скопирует все текстуры из папки источника в установленную Вами папку.
// 3) Путь к игровому лог файлу - Версия игры должна быть строго 1.0007 (скачать). На данной версии патча, 
// Вы должны запустить карту БЕЗ ТЕКСТУР, как карта загрузилась, сохраните лог(flush) и установите путь к этому файлу.
// Далее как все настроили, нажмите Старт. По окончанию работы будет выведена вся информация.
// Также в директории с программой в случае ошибок копирования или отсутствия текстур (В папке источника), будет выведен весь путь до файла.
// Добавлена возможность поиска недостающих bump файлов (! auto-generated bump map:)

namespace TexturesExtractor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadSettings();
            checkdir();
        }

        private void checkdir()
        {
            if (!Directory.Exists(out_dir.Text + "\\tga"))
                Directory.CreateDirectory(out_dir.Text + "\\tga");
            if (!Directory.Exists(out_dir.Text + "\\tga_not_dir"))
                Directory.CreateDirectory(out_dir.Text + "\\tga_not_dir");
            if (!Directory.Exists(out_dir.Text + "\\thm"))
                Directory.CreateDirectory(out_dir.Text + "\\thm");

        }
        private void btnStartThread_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Мы дадим Вам знать, когда копирование данных будет завершено.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            TH.RunWorkerAsync();
        }

        private void Save(string s)
        {
            StreamWriter writer = new StreamWriter("settings.txt", false, Encoding.GetEncoding("UTF-8"));
            writer.Write(s);
            writer.Close();
        }

        private void Log(string s)
        {
            StreamWriter writer = new StreamWriter("log.txt", true, Encoding.GetEncoding("UTF-8")); 
            writer.Write(s);
            writer.Close();
        }

        private void LoadSettings()
        {
            if (File.Exists("settings.txt"))
            {
                foreach (string s in File.ReadLines("settings.txt"))
                {
                    if (s.Contains("remote_dir="))
                        remote_dir.Text = s.Replace("remote_dir=", "");
                    else if (s.Contains("out_dir="))
                        out_dir.Text = s.Replace("out_dir=", "");
                    else if (s.Contains("log_dir="))
                        log_dir.Text = s.Replace("log_dir=", "");
                }
            }
        }

        private void save_dir()
        {
            Save("remote_dir=" + remote_dir.Text + Environment.NewLine +
                 "out_dir=" + out_dir.Text + Environment.NewLine +
                 "log_dir=" + log_dir.Text);
        }

        // Путь к ОБЩЕЙ Папки со всеми текстурами, которые только есть
        private void btn1In_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog OpenDir = new FolderBrowserDialog();
            if (OpenDir.ShowDialog() == DialogResult.OK)
            {
                remote_dir.Text = OpenDir.SelectedPath;
            }
        }

        // Путь к выгрузке найденных текстур
        private void btn2Out_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog OpenDir = new FolderBrowserDialog();
            if (OpenDir.ShowDialog() == DialogResult.OK)
            {
                out_dir.Text = OpenDir.SelectedPath;
            }
        }

        // Путь к лог файлу
        private void btn3Log_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.DefaultExt = "*.log";
            file.Filter = "Xray log files|*.log";
            if (file.ShowDialog() == DialogResult.OK && file.FileName.Length > 0)
            {
                log_dir.Text = file.FileName;
            }
        }

        int ok = 0, error = 0;
        private void TH_DoWork(object sender, DoWorkEventArgs e)
        {
            ok = 0;
            error = 0;
            if (File.Exists("log.txt"))
                File.Delete("log.txt");

            foreach (string str in File.ReadLines(log_dir.Text, Encoding.GetEncoding(1251)))
            {
                // Can't find texture 'ghost_particles\amik_hit_fx\water_splash\water_splash_amik'
                if (str.Contains("Can't find texture"))
                {
                    try
                    {
                        string filename = str.Replace("Can't find texture ", "").Replace("'", "").Replace("[", "").Replace("]", "");
                        // оставим только путь к папке. Последнее после слеша - имя файла
                        string dir = filename.TrimEnd('\\');
                        // проверим существует ли директория
                        if (!Directory.Exists(Path.GetDirectoryName(out_dir.Text + "\\" + dir)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(out_dir.Text + "\\" + dir));
                        }
                        File.Copy(remote_dir.Text + "\\" + filename + ".dds", out_dir.Text + "\\" + filename + ".dds", true);
                        ok++;
                    }
                    catch (Exception ex)
                    {
                        error++;
                        Log("[Can't find texture] " + ex.Message + Environment.NewLine);
                    }
                }
                // ! auto-generated bump map: cars\cars_uazreshetka_bump
                else if (str.Contains("! auto-generated bump map:")) 
                {
                    try
                    {
                        var filename = str.Replace("! auto-generated bump map: ", "").Replace("'", "").Replace("[", "").Replace("]", "");
                        // оставим только путь к папке
                        string dir = filename.TrimEnd('\\');
                        // проверим существует ли директория
                        if (!Directory.Exists(Path.GetDirectoryName(out_dir.Text + "\\" + dir + "\\")))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(out_dir.Text + "\\" + dir + "\\"));
                        }
                        File.Copy(remote_dir.Text + "\\" + filename + ".dds", out_dir.Text + "\\" + filename + ".dds", true);
                        ok++;
                    }
                    catch (Exception ex)
                    {
                        error++;
                        Log("[auto-generated bump map] " + ex.Message + Environment.NewLine);
                    }
                }
                // thm файлы
                else if (str.Contains("cannot find thm: "))
                {
                    try
                    {
                        // оставим только путь к папке
                        string filename = str.Remove(0, str.IndexOf("textures")).Replace(".thm", "");

                        string FILE_NAME = str.Remove(0, str.IndexOf("textures") + 8).Replace(".thm", "");
                        string CURRENT_DIR = filename.Remove(filename.IndexOf("\\"));
                        string FILE_NAME_OUT = FILE_NAME.Substring(0, FILE_NAME.LastIndexOf("\\"));

                        // проверим существует ли директория
                        if (!Directory.Exists(Path.GetDirectoryName(out_dir.Text + "\\thm\\" + FILE_NAME + "\\")))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(out_dir.Text + "\\thm\\" + CURRENT_DIR + FILE_NAME_OUT + "\\"));
                        }
                        // Скопируем файл в созданную директорию
                        File.Copy(remote_dir.Text + "\\" + filename + ".dds", out_dir.Text + "\\thm\\" + filename + ".dds", true);
                        ok++;
                    }
                    catch (Exception ex)
                    {
                        error++;
                        Log(ex.Message + Environment.NewLine);
                    }
                }       
                //  | | cannot find tga texture: c:\sdk\rawdata\textures\tile\tile_mortar_01.thm
                // путь для tga файлов
                else if (str.Contains("cannot find tga texture: "))
                {
                    try
                    {
                        string FILENAME_THM = str.Remove(0, str.IndexOf("textures")).Replace(".thm", "");
                        string FILENAME_OUT = FILENAME_THM.Split('\\')[2];
                        string FILENAME_DIR2 = str.Remove(0, str.IndexOf("textures") + 8).Replace(".thm", "");
                        string CURRENT_DIR = FILENAME_THM.Remove(FILENAME_THM.IndexOf("\\"));
                        string FILENAME_EXIT = FILENAME_DIR2.Substring(0, FILENAME_DIR2.LastIndexOf("\\"));
                        if (!Directory.Exists(Path.GetDirectoryName(out_dir.Text + "\\tga\\" + FILENAME_DIR2 + "\\")))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(out_dir.Text + "\\tga\\" + CURRENT_DIR + FILENAME_EXIT + "\\"));
                        }
                        File.Copy(remote_dir.Text + "\\" + FILENAME_THM + ".dds", out_dir.Text + "\\tga\\" + FILENAME_THM + ".dds", true);
                        File.Copy(remote_dir.Text + "\\" + FILENAME_THM + ".dds", out_dir.Text + "\\tga_not_dir\\" + FILENAME_OUT + ".dds", true);

                        ok++;
                    }
                    catch (Exception ex)
                    {
                        error++;
                        Log(ex.Message + Environment.NewLine); 
                    }
                }
            }
        }    

        private void TH_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (ok > 0 && error == 0)
                MessageBox.Show("Все файлы успешно скопированы", "Все готово!", MessageBoxButtons.OK,MessageBoxIcon.Information);
            else if (ok > 0 && error > 0)
                MessageBox.Show("Часть файлов: " + ok + " было успешно перенесено, но часть из них: " + error + " не было скопировано.\nПодробности в файле log.txt", "Завершение с ошибками", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (ok == 0 && error > 0)
                MessageBox.Show("Произошла ошибка, и файлы не были скопированы.\nПодробности в файле log.txt", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (ok == 0 && error == 0)
                MessageBox.Show("Возможно Вы не задали все параметры путей до файлов/папок.", "Неверные параметры", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа для автоматического поиска и копирования требуемых картой - текстур\nРазработано командой Team-stalker", "О Программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            save_dir();
        }
    }
}
