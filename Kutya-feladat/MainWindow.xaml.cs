using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kutya_feladat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static int counter = 1;
        static int nameCounter = 0;
        static List<Dogs> Dogs = new List<Dogs>();
        public MainWindow()
        {
            InitializeComponent();
            getData("Kutyak.csv");
            getData("KutyaFajtak.csv");
            getData("KutyaNevek.csv");
            lblCounter.Content = $"3.Feladat: {nameCounter}";

            float sum = 0;
            for (int i = 0; i < Dogs.Count; i++)
            {
                sum = sum + Dogs[i].age;
            }
            sum = sum / Dogs.Count;
            lblAvgAge.Content = $"6.Feladat: {sum.ToString("#.##")}";

            int oldest = 0;
            string oldestDog = "";
            string oldestType = "";
            foreach (var item in Dogs)
            {
                if (item.age > oldest)
                {
                    oldest = item.age;
                    oldestDog = item.dogName;
                    oldestType = item.typeName;
                }
            }
            lblOldestDog.Content = $"7.Feladat: {oldestDog} {oldestType}";


            Dictionary<string, int> groupedByType = new Dictionary<string, int>();
            foreach (var item in Dogs)
            {
                if (item.lastMedicalCheck == "2018.01.10")
                {
                    if (!groupedByType.ContainsKey(item.typeName))
                    {
                        groupedByType[item.typeName] = 1;
                    }
                    else
                    {
                        groupedByType[item.typeName]++;
                    }
                }
            }
            lbl8thTask.Content = "8.Feladat: Január 10-én vizsgált kutyafajták:";
            foreach (var item in groupedByType)
            {
                lbPickedDate.Items.Add($"{item.Key}: {item.Value} kutya");
            }

            Dictionary<string, int> groupedByDate = new Dictionary<string, int>();
            foreach (var item in Dogs)
            {
                if (!groupedByDate.ContainsKey(item.lastMedicalCheck))
                {
                    groupedByDate[item.lastMedicalCheck] = 1;
                }
                else
                {
                    groupedByDate[item.lastMedicalCheck]++;
                }
            }

            string busiestDate = "";
            int mostAidedDogs = 0;
            foreach (var item in groupedByDate)
            {
                if (item.Value > mostAidedDogs)
                {
                    busiestDate = item.Key;
                    mostAidedDogs = item.Value;
                }
            }
            lblBusiest.Content = $"9. feladat: Legjobban leterhelt nap: {busiestDate}: {mostAidedDogs} kutya";
            writeStatics();
        }
        static void getData(string fileName)
        {
            StreamReader sr = new StreamReader(fileName);
            sr.ReadLine();
            switch (counter)
            {
                case 1:
                    counter++;
                    while (!(sr.EndOfStream))
                    {
                        string[] line = sr.ReadLine().Split(";");
                        Dogs dawg = new Dogs(
                            Convert.ToInt32(line[0]),
                            Convert.ToInt32(line[1]),
                            Convert.ToInt32(line[2]),
                            Convert.ToInt32(line[3]),
                            line[4],
                            null,
                            null,
                            null
                            );
                        Dogs.Add(dawg);
                    }
                    break;
                case 2:
                    counter++;
                    while (!(sr.EndOfStream))
                    {
                        string[] line = sr.ReadLine().Split(";");
                        foreach (var item in Dogs)
                        {
                            if (item.typeId == Convert.ToInt32(line[0]))
                            {
                                item.typeName = line[1];
                                item.ogName = line[2];
                            }
                        }
                    }
                    break;
                case 3:
                    counter++;
                    while (!(sr.EndOfStream))
                    {
                        string[] line = sr.ReadLine().Split(";");
                        nameCounter = Convert.ToInt32(line[0]);
                        foreach (var item in Dogs)
                        {
                            if (item.nameId == Convert.ToInt32(line[0]))
                            {
                                item.dogName = line[1];
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
            sr.Close();
        }

        static void writeStatics()
        {
            StreamWriter sw = new StreamWriter("nevstatisztika.txt");
            Dictionary<string, int> groupedByName = new Dictionary<string, int>();
            foreach (var item in Dogs)
            {
                if (!groupedByName.ContainsKey(item.dogName))
                {
                    groupedByName[item.dogName] = 1;
                }
                else
                {
                    groupedByName[item.dogName]++;
                }
            }

            foreach (var item in groupedByName.OrderByDescending(key => key.Value))
            {
                sw.WriteLine($"{item.Key};{item.Value}");
            }
            sw.Close();
        }
    }
}

