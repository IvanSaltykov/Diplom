using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorldResort.AdminDialog
{
    public class CreateTypeRoomDialog : Form
    {
        private TextBox textBoxNameTypeRoom;
        private TextBox textBoxPriceTypeRoom;
        private Label label1;
        private Label label2;
        private Button buttonSaveTypeRoom;

        public CreateTypeRoomDialog()
        {
            this.Text = "Тип номера";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Width = 300;
            this.Height = 150;

            textBoxNameTypeRoom = new TextBox();
            textBoxNameTypeRoom.Location = new System.Drawing.Point(100, 10);
            textBoxNameTypeRoom.Width = 150;
            this.Controls.Add(textBoxNameTypeRoom);

            label1 = new Label();
            label1.Text = "Название:";
            label1.Location = new System.Drawing.Point(10, 10);
            this.Controls.Add(label1);

            textBoxPriceTypeRoom = new TextBox();
            textBoxPriceTypeRoom.Location = new System.Drawing.Point(100, 40);
            textBoxPriceTypeRoom.Width = 150;
            this.Controls.Add(textBoxPriceTypeRoom);

            label2 = new Label();
            label2.Text = "Цена:";
            label2.Location = new System.Drawing.Point(10, 40);
            this.Controls.Add(label2);

            buttonSaveTypeRoom = new Button();
            buttonSaveTypeRoom.Text = "Сохранить";
            buttonSaveTypeRoom.DialogResult = DialogResult.OK;
            buttonSaveTypeRoom.Location = new System.Drawing.Point(100, 80);
            this.Controls.Add(buttonSaveTypeRoom);
        }

        public string TextBoxNameTypeRoom
        {
            get { return textBoxNameTypeRoom.Text; }
        }

        public string TextBoxPriceTypeRoom
        {
            get { return textBoxPriceTypeRoom.Text; }
        }
    }
}
