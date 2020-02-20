using System.Windows.Forms;

namespace WindowsFormsApp
{
    class MainForm : Form
    {
        Label userName = new Label();
        TextBox textBox = new TextBox();
        Button button = new Button();
        TextBox result = new TextBox();

        public MainForm()
        {
            InitComponent();
        }

        private void InitComponent()
        {
            userName.Top = 30;
            userName.Left = 20;
            userName.Width = 100;
            userName.Text = "Enter user name";
            Controls.Add(userName);

            textBox.Top = 30;
            textBox.Left = 130;
            textBox.Height = 70;
            textBox.Width = 150;
            Controls.Add(textBox);

            button.Top = 60;
            button.Left = 80;
            button.Text = "Ok";
            button.Click += new System.EventHandler(Button_Click);
            button.Height = 40;
            button.Width = 100;
            Controls.Add(button);

            result.Top = 160;
            result.Left = 20;
            result.ReadOnly = true;
            result.Multiline = true;
            result.Width = 300;
            result.Height = 200;
            Controls.Add(result);
        }

        private void Button_Click(object sender, System.EventArgs e)
        {
            result.Text = "Hello, " + textBox.Text + "!";
        }
    }
}
