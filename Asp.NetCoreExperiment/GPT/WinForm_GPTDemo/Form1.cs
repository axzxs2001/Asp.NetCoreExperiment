using System.ComponentModel;

namespace WinForm_GPTDemo
{



    public partial class Form1 : Form
    {
        // Create controls for each property in the CFRxChkUpldIn class
        TextBox hiRxnoTextBox = new TextBox();
        TextBox epcTokenTextBox = new TextBox();
        TextBox ecTokenTextBox = new TextBox();
        TextBox fixmedinsCodeTextBox = new TextBox();
        TextBox fixmedinsNameTextBox = new TextBox();
        TextBox pharCertTypeTextBox = new TextBox();
        TextBox pharCertnoTextBox = new TextBox();
        TextBox pharNameTextBox = new TextBox();
        TextBox pharCodeTextBox = new TextBox();
        TextBox rxChkOpnnTextBox = new TextBox();
        TextBox rxChkStasCodgTextBox = new TextBox();
        DateTimePicker rxChkTimeDatePicker = new DateTimePicker();
        TextBox rxSignVerifySnTextBox = new TextBox();
        Button submitButton = new Button();
        private void Form1_Load(object sender, EventArgs e)
        {
          
            //��ʼ��

        }
        public Form1()
        {
            InitializeComponent();
            // Set up the form
            this.Text = "My Form";
            this.Size = new Size(500, 650);

            // Define controls and labels
            InitializeControl(hiRxnoTextBox, "ҽ���������:", new Point(20, 20));
            InitializeControl(epcTokenTextBox, "���Ӵ�������:", new Point(20, 70));
            InitializeControl(ecTokenTextBox, "��ҩʦ����ƾ֤����:", new Point(20, 120));
            InitializeControl(fixmedinsCodeTextBox, "����ҽҩ�������:", new Point(20, 170));
            InitializeControl(fixmedinsNameTextBox, "����ҽҩ��������:", new Point(20, 220));
            InitializeControl(pharCertTypeTextBox, "��ҩʦ֤������:", new Point(20, 270));
            InitializeControl(pharCertnoTextBox, "��ҩʦ֤������:", new Point(20, 320));
            InitializeControl(pharNameTextBox, "��ҩʦ����:", new Point(20, 370));
            InitializeControl(pharCodeTextBox, "��ҽ��ҩʦ����:", new Point(20, 420));
            InitializeControl(rxChkOpnnTextBox, "����������:", new Point(20, 470));
            InitializeControl(rxChkStasCodgTextBox, "�������״̬����:", new Point(20, 520));

            // DateTimePicker
            Label rxChkTimeLabel = new Label();
            rxChkTimeLabel.Text = "�������ʱ��:";
            rxChkTimeLabel.Location = new Point(20, 570);
            rxChkTimeLabel.Width = 150;
            rxChkTimeDatePicker.Location = new Point(180, 570);
            this.Controls.Add(rxChkTimeLabel);
            this.Controls.Add(rxChkTimeDatePicker);

            InitializeControl(rxSignVerifySnTextBox, "����ǩ����ǩ��ˮ��:", new Point(20, 620));

            // Configure the submit button
            submitButton.Text = "Submit";
            submitButton.Location = new Point(180, 670);
            submitButton.Click += new EventHandler(SubmitButton_Click);
            this.Controls.Add(submitButton);
        }

        private void InitializeControl(Control control, string labelText, Point location)
        {
            Label label = new Label();
            label.Text = labelText;
            label.Location = new Point(location.X, location.Y);
            label.Width = 150;
            control.Location = new Point(location.X + 160, location.Y);
            this.Controls.Add(label);
            this.Controls.Add(control);
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            //CFRxChkUpldIn cFRxChkUpldIn = new CFRxChkUpldIn
            //{
            //    hiRxno = hiRxnoTextBox.Text,
            //    epcToken = epcTokenTextBox.Text,
            //    ecToken = ecTokenTextBox.Text,
            //    fixmedinsCode = fixmedinsCodeTextBox.Text,
            //    fixmedinsName = fixmedinsNameTextBox.Text,
            //    pharCertType = pharCertTypeTextBox.Text,
            //    pharCertno = pharCertnoTextBox.Text,
            //    pharName = pharNameTextBox.Text,
            //    pharCode = pharCodeTextBox.Text,
            //    rxChkOpnn = rxChkOpnnTextBox.Text,
            //    rxChkStasCodg = rxChkStasCodgTextBox.Text,
            //    rxChkTime = rxChkTimeDatePicker.Value,
            //    rxSignVerifySn = rxSignVerifySnTextBox.Text
            //};

            // Do something with the CFRxChkUpldIn object
        }
    }


}