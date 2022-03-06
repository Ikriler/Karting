using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using static Karting.Inventory;

namespace Karting
{
    public partial class ReportPrinting : Window
    {
        public ForInitList forTInfo;
        public ReportPrinting(ForInitList information = null)
        {
            InitializeComponent();
            DataController.StartTimerOnCurrentWindow(this.textBlock_DayXInfo, this.textBlock_DayXChanger);
            forTInfo = information;
            InitTInfo();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Inventory inventory = new Inventory();
            inventory.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            inventory.Show();
            this.Close();
        }

        private void printOthcet_Click(object sender, RoutedEventArgs e)
        {
            if (forTInfo == null) return;

            Microsoft.Office.Interop.Word._Application word_app = new Microsoft.Office.Interop.Word.Application();

            object missing = Type.Missing;
            Microsoft.Office.Interop.Word._Document word_doc = word_app.Documents.Add(ref missing, ref missing, ref missing, ref missing);

            Microsoft.Office.Interop.Word.Paragraph para = word_doc.Paragraphs.Add(ref missing);

            string old_font = para.Range.Font.Name;
            para.Range.Font.Name = "Times New Roman";

            para.Range.Text = "Выбрано тип А: " + forTInfo.TypeACount;
            para.Range.InsertParagraphAfter();
            para.Range.Text = "Выбрано тип B: " + forTInfo.TypeBCount;
            para.Range.InsertParagraphAfter();
            para.Range.Text = "Выбрано тип C: " + forTInfo.TypeCCount;
            para.Range.InsertParagraphAfter();
            para.Range.Text = "Остатки Номер: " + forTInfo.RestNumber;
            para.Range.InsertParagraphAfter();
            para.Range.Text = "Остатки Браслет: " + forTInfo.RestBraslet;
            para.Range.InsertParagraphAfter();
            para.Range.Text = "Остатки Шлем: " + forTInfo.RestHelm;
            para.Range.InsertParagraphAfter();
            para.Range.Text = "Остатки Экипировка: " + forTInfo.RestEquip;
            para.Range.InsertParagraphAfter();

            object filename = $"othcet.docx";
            word_doc.SaveAs(ref filename, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing,
                ref missing);

            object save_changes = false;
            word_doc.Close(ref save_changes, ref missing, ref missing);
            word_app.Quit(ref save_changes, ref missing, ref missing);

            MessageBox.Show("Документ создан!");
        }

        private void InitTInfo()
        {
            if (forTInfo == null) return;
            this.t_info.Text += "Выбрано тип А: " + forTInfo.TypeACount + "\n";
            this.t_info.Text += "Выбрано тип B: " + forTInfo.TypeBCount + "\n";
            this.t_info.Text += "Выбрано тип C: " + forTInfo.TypeCCount + "\n";
            this.t_info.Text += "Остатки Номер: " + forTInfo.RestNumber + "\n";
            this.t_info.Text += "Остатки Браслет: " + forTInfo.RestBraslet + "\n";
            this.t_info.Text += "Остатки Шлем: " + forTInfo.RestHelm + "\n";
            this.t_info.Text += "Остатки Экипировка: " + forTInfo.RestEquip + "\n";
        }
    }
}
